using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fiddler;
using System.Threading;
using ProjectSeniorCenter.Code.Utility;
using ProjectSeniorCenter.Code.Entity;


namespace ProjectSeniorCenter.Code
{
    /// <summary>
    /// Options to run the sniffer
    /// </summary>
    public enum Config
    {
        Run, Debug, ShowResponses, CaptureOnlyHTMLRequests, CaptureOnlyWithRequestParameters
    }

    /// <summary>
    /// This class contains all the required methods for doing data sniffing
    /// </summary>
    class Sniffer
    {
        /// <summary>
        /// The port at which the sniffer listens to as a proxy
        /// </summary>
        private Int32 _PORT;

        private Config _enConfiguration = Config.Run;

        public Boolean IsAlive { get; set; }


        private SnifferConfigHandler _snifferConfigHandler;

        private DBUtility _DBUtility;

        private Boolean _IsAllowedURL;

        /// <summary>
        /// Private constructor
        /// </summary>
        private Sniffer()
        {
            _PORT = Configurations.SnifferPort;

            //Get the sniffer config handler
            _snifferConfigHandler = SnifferConfigHandler.GetSnifferConfigHandler();

            //Create the DB Utility
            _DBUtility = DBUtility.CreateDBUtility(Configurations.DatabaseName, Configurations.User);

            //Initialize
            _IsAllowedURL = false;
        }

       
        /// <summary>
        /// Returns the Sniffer instance
        /// </summary>
        /// <returns></returns>
        public static Sniffer CreateSniffer()
        {

            return new Sniffer();
        }

        /// <summary>
        /// Sets the sniffer configuration
        /// </summary>
        public Config Configuration
        {
            get { return _enConfiguration; }
            set { _enConfiguration = value; }
        }

        /// <summary>
        /// This method starts the sniffing process
        /// </summary>
        public void Start()
        {
            try
            {
                //Add the listeners
                FiddlerApplication.BeforeRequest += new SessionStateHandler(FiddlerApplication_BeforeRequest);
                FiddlerApplication.BeforeResponse += new SessionStateHandler(FiddlerApplication_BeforeResponse);

                // For the purposes of this demo, we'll forbid connections to HTTPS 
                // sites that use invalid certificates
                //CONFIG.IgnoreServerCertErrors = false;

                //Creates the certificate if required
                //CreateCertificateIfRequired();

                //Start the fiddler for listening HTTP/HTTPS requests
                FiddlerApplication.Startup(_PORT, true, true);
            }
            catch (Exception ex)
            {              
                Utility.Logger.Log(ex.Message);
            }
        }

        
        /// <summary>
        /// Gets triggered before the request has been made
        /// </summary>
        /// <param name="objSession"></param>
        private void FiddlerApplication_BeforeRequest(Session objSession)
        {

            try
            {                    

                CreateCertificateIfRequired();

                //Declarations
                String strContentType = String.Empty;
                String strRequestedParameters = String.Empty;

                //Get the flag whether its an allowable URL
                Website website = _snifferConfigHandler.GetWebsite(objSession.fullUrl);

                //Check whether a matching website was found
                _IsAllowedURL = website != null;                    

                //Sniff out only if this URL was in the list of websites
                if (_IsAllowedURL)
                {
                    //Get the content type
                    strContentType = objSession.oRequest.headers["Accept"];

                    //If its not a capture onl only HTML requests or it has to be HTML only content

                    //Get the request headers
                    HTTPRequestHeaders objRequestHeaders = objSession.oRequest.headers;

                    //Construct the network data
                    NetworkData objNetworkData = new NetworkData
                    {
                        URLFullPath = objSession.fullUrl,
                        IsHTTPS = objSession.isHTTPS,
                        SentOn = objSession.Timers.ClientBeginRequest.ToString(),
                        Site = website
                    };


                    //Get the request body
                    String strRequestBody = objSession.GetRequestBodyAsString();
                    
                    //If its a POST request
                    if (objRequestHeaders.HTTPMethod == "POST")
                        //Get the request parameters
                        strRequestedParameters = strRequestBody;
                    else if (objRequestHeaders.HTTPMethod == "GET")
                    {
                        String[] arrQueryString = objNetworkData.URLFullPath.Split(new Char[] { '?' });

                        if (arrQueryString.Length > 1)
                            strRequestedParameters = arrQueryString[1];
                    }

                    //TO DO: Capture only if the content has any PII data
                    if (objNetworkData.ContainsPII(_snifferConfigHandler.Person, strRequestedParameters))
                        //Update the capture to Mongo DB
                        _DBUtility.AddData(objNetworkData);
                }
                else
                {
                    //Uncomment this if tampering of response is required
                    objSession.bBufferResponse = true;

                    //objSession.Abort();                   
                }
                
            }
            catch (ThreadAbortException ex)
            {
                ShutDown();                              
            }
            catch (Exception ex)
            {
                Utility.Logger.Log("FiddlerApplication_BeforeRequest: " + ex.Message);

                ShutDown();
            }

            
        }

        /// <summary>
        /// Gets triggered before the response gets rendered
        /// </summary>
        /// <param name="objSession"></param>
        private void FiddlerApplication_BeforeResponse(Session objSession)
        {
            //Check whether its an inaccessible URL
            if (!_IsAllowedURL)
            {
                String strRequestBody = objSession.GetResponseBodyAsString();
                objSession.utilSetResponseBody("<html><body><h1>You are not allowed to view this site.</h1></body></html>");
            }
        }

        /// <summary>
        /// Shuts down the fiddler application
        /// </summary>
        internal void ShutDown()
        {
            try
            {
                FiddlerApplication.Shutdown();
            }
            catch (Exception ex)
            {
                Utility.Logger.Log("ShutDown: " + ex.Message);
            }
            finally
            {
                IsAlive = false;
            }
        }


        /// <summary>
        /// Creates the certificates if required
        /// </summary>
        private void CreateCertificateIfRequired()
        {
            try
            {
                FiddlerApplication.CreateProxyEndpoint(_PORT, true, "localhost");

                CONFIG.IgnoreServerCertErrors = false;

                if (!Fiddler.CertMaker.rootCertExists())
                {
                    if (!Fiddler.CertMaker.createRootCert())
                    {
                        throw new Exception("Unable to create cert for FiddlerCore.");
                    }
                }

                if (!Fiddler.CertMaker.rootCertIsTrusted())
                {
                    if (!Fiddler.CertMaker.trustRootCert())
                    {
                        throw new Exception("Unable to install FiddlerCore's cert.");
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.Logger.Log("CreateCertificateIfRequired: " + ex.Message);
            }

        }


    }
}
