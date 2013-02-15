using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectSeniorCenter.Code.Entity;

namespace ProjectSeniorCenter.Code.Utility
{
    class SnifferConfigHandler
    {

        private List<Website> _websites;
        private Person _person;

        private List<String> _allowableHostNames;
        private StringBuilder _sbAllowableHostNames;

        private static SnifferConfigHandler _snifferConfigHandler;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="volunteer"></param>
        private SnifferConfigHandler()
        {
            //Get the required data
            GetData();           
        }


        #region Public Methods

        /// <summary>
        /// Returns the sniffer config handler
        /// </summary>
        /// <param name="volunteer"></param>
        /// <returns></returns>
        public static SnifferConfigHandler GetSnifferConfigHandler()
        {
            if (_snifferConfigHandler == null)
                    _snifferConfigHandler = new SnifferConfigHandler();

            return _snifferConfigHandler;
        }


        /// <summary>
        /// Checks whether the URL is allowed or not 
        /// as per the adminstrator
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public Website GetWebsite(String url)
        {
            Website website = null;

            try
            {
                //Sanitize the url
                url = url.Replace("https://", "");
                url = url.Replace("http://", "");

                String[] arrUrl = url.Split(new Char[] { '/' });

                url = arrUrl[0];

                //Get the matching website
                website = _websites.Find(websiteToFind => { return websiteToFind.URL.Contains(url); });
            }
            catch (Exception ex)
            {
                website = null;                
            }

            return website;
        }



        #endregion


        #region Private Methods


        /// <summary>
        /// Gets the required data from database
        /// </summary>
        private void GetData()
        {
            DBUtility dbUtility = DBUtility.CreateDBUtility(Configurations.DatabaseName, Configurations.PIITableName);

            //Initialize
            _sbAllowableHostNames = new StringBuilder();

            //Get the person data (PII Information)
            _person = dbUtility.GetPersonData(Configurations.User);

            //Get the list of accessible websites
            dbUtility.TableName = Configurations.WebsitesTableName;
            _websites = dbUtility.GetWebsites();

            //Build the allowable Hostnames
            _allowableHostNames = new List<string>();

            foreach  (Website website in _websites)
            {
                _allowableHostNames.Add(website.URL);
                _sbAllowableHostNames.Append(website.URL);
            }


        }



        #endregion

        #region Properties

        public Person Person
        {
            get { return _person; }
        }

        #endregion
    }

}
