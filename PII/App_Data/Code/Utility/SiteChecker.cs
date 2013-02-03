using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;

namespace PII.Code.Utility
{
    public class SiteChecker
    {
        private String _strSiteToCheck = String.Empty;

        public Boolean Exists { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="strSiteToCheck"></param>
        public SiteChecker(String strSiteToCheck)
        {
            this._strSiteToCheck = strSiteToCheck;
        }

        ///
        /// Checks the file exists or not.
        ///
        /// The URL of the remote file.
        /// True : If the file exits, False if file not exists
        public void RemoteFileExists()
        {
            Exists = false;

            try
            {                
                //Creating the HttpWebRequest
                HttpWebRequest request = WebRequest.Create(_strSiteToCheck) as HttpWebRequest;
                //Setting the Request method HEAD, you can also use GET too.
                request.Method = "HEAD";
                //Getting the Web Response.
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //Returns TRUE if the Status code == 200
                Exists = (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                //Any exception will returns false.
                Exists = false;
            }


        }
    }
}