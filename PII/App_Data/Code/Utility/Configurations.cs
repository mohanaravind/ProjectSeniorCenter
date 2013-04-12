using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace PII.Code.Utility
{
    public static class Configurations
    {
        /// <summary>
        /// Returns the application configuration where events are logged
        /// </summary>
        /// <returns></returns>
        public static String EventLog
        {
            get { return ConfigurationManager.AppSettings["EventLog"]; }
        }


        /// <summary>
        /// Returns the table name in Mongo DB where the PII information are being stored
        /// </summary>
        /// <returns></returns>
        public static String TableName
        {
            get { return ConfigurationManager.AppSettings["TableName"]; }
        }


        /// <summary>
        /// Returns the websites table name in Mongo DB where the accessible websites information are being stored
        /// </summary>
        /// <returns></returns>
        public static String Websites
        {
            get { return ConfigurationManager.AppSettings["Websites"]; }
        }


        /// <summary>
        /// Returns the MongoDB Connection string
        /// </summary>
        public static String ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["MongoDB"].ConnectionString; }
        }

        /// <summary>
        /// Returns the database name
        /// </summary>
        public static String DatabaseName
        {
            get { return ConfigurationManager.ConnectionStrings["MongoDB"].ProviderName; }
        }

        /// <summary>
        /// Returns the site checker's waiting time
        /// </summary>
        /// <returns></returns>
        public static Int32 SiteCheckerThreshold
        {
            get
            {
                Int32 time = Int32.MinValue;
                String waitingTime = System.Configuration.ConfigurationManager.AppSettings.Get("SiteCheckerThreshold");

                //Parse the string
                Int32.TryParse(waitingTime, out time);

                return time;
            }
        }

        /// <summary>
        /// Returns the Administrator name
        /// </summary>
        public static String Administrator
        {
            get { return System.Configuration.ConfigurationManager.AppSettings.Get("Administrator"); }
        }

        /// <summary>
        /// Returns whether the PII data would be displayed to the user
        /// </summary>
        public static Boolean DisplayPII
        {
            get 
            { 
                Boolean result = false;
                Boolean.TryParse(System.Configuration.ConfigurationManager.AppSettings.Get("DisplayPII"), out result); 
                return result; 
            
            }
        }

    }
}
