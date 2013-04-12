using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ProjectSeniorCenter.Code.Utility
{
    public static class Configurations
    {
        /// <summary>
        /// Returns the application configuratoin where events are logged
        /// </summary>
        /// <returns></returns>
        public static String EventLog
        {
            get { return System.Configuration.ConfigurationManager.AppSettings.Get("EventLog");}
        }

        /// <summary>
        /// Returns the system idle threshold time
        /// </summary>
        /// <returns></returns>
        public static Int32 ThresholdTime
        {
            get
            {
                Int32 time = Int32.MinValue;
                String thresholdTime = System.Configuration.ConfigurationManager.AppSettings.Get("ThresholdTime");

                //Parse the string
                Int32.TryParse(thresholdTime, out time);

                return time;
            }
        }

        /// <summary>
        /// Returns the system notifier polling time
        /// </summary>
        /// <returns></returns>
        public static Int32 NotifierPollTime
        {
            get 
            {
                Int32 time = Int32.MinValue;
                String pollingTime = System.Configuration.ConfigurationManager.AppSettings.Get("NotifierPollTime");

                //Parse the string
                Int32.TryParse(pollingTime, out time);

                return time;
            }
        }


        /// <summary>
        /// Returns the sniffer polling time
        /// </summary>
        /// <returns></returns>
        public static Int32 SnifferPollTime
        {
            get
            {
                Int32 time = Int32.MinValue;
                String pollingTime = System.Configuration.ConfigurationManager.AppSettings.Get("SnifferPollTime");

                //Parse the string
                Int32.TryParse(pollingTime, out time);

                return time;
            }
        }

        /// <summary>
        /// Returns the port to which the sniffer listens to
        /// </summary>
        /// <returns></returns>
        public static Int32 SnifferPort
        {
            get
            {
                Int32 port = Int32.MinValue;
                String snifferPort = System.Configuration.ConfigurationManager.AppSettings.Get("SnifferPort");

                //Parse the string
                Int32.TryParse(snifferPort, out port);

                return port;
            }
        }

        /// <summary>
        /// Returns the user name of current user
        /// </summary>
        /// <returns></returns>
        public static String User
        {
            get
            {
                String[] name = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split(new Char[] { '\\' });

                return name[1];
            }
        }


        /// <summary>
        /// Returns the keyword of the volunteer 
        /// </summary>
        /// <returns></returns>
        public static String Volunteer
        {
            get { return System.Configuration.ConfigurationManager.AppSettings.Get("Volunteer"); }
        }

        /// <summary>
        /// Returns the keyword of the volunteer 
        /// </summary>
        /// <returns></returns>
        public static String AboutLink
        {
            get { return System.Configuration.ConfigurationManager.AppSettings.Get("AboutLink"); }
        }

        /// <summary>
        /// Returns the keyword of the volunteer 
        /// </summary>
        /// <returns></returns>
        public static String PIIFormLink
        {
            get { return System.Configuration.ConfigurationManager.AppSettings.Get("PIIForm"); }
        }

        /// <summary>
        /// Returns the keyword of the volunteer 
        /// </summary>
        /// <returns></returns>
        public static String AllowedHost
        {
            get { return System.Configuration.ConfigurationManager.AppSettings.Get("AllowedHost"); }
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
        /// Returns the PIITableName 
        /// </summary>
        /// <returns></returns>
        public static String PIITableName
        {
            get { return System.Configuration.ConfigurationManager.AppSettings.Get("PII"); }
        }


        /// <summary>
        /// Returns the WebsitesTableName 
        /// </summary>
        /// <returns></returns>
        public static String WebsitesTableName
        {
            get { return System.Configuration.ConfigurationManager.AppSettings.Get("Websites"); }
        }

    }
}
