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

    }
}
