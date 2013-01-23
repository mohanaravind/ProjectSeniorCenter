using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        /// Returns the user name of current user
        /// </summary>
        /// <returns></returns>
        public static String User
        {
            get
            {
                return System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            }
        }
    }
}
