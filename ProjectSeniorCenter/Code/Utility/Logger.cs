using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ProjectSeniorCenter.Code.Utility
{
    public static class Logger
    {

        /// <summary>
        /// Event log to write the messages
        /// </summary>
        private static EventLog _eventLog;

        /// <summary>
        /// Constructor
        /// </summary>
        static Logger()
        {
            _eventLog = new EventLog();
            _eventLog.Source = Configurations.EventLog;
        }

        /// <summary>
        /// Writes up the log to system event management
        /// </summary>
        /// <param name="message"></param>
        public static void Log(String message, EventLogEntryType eventLogEntryType = EventLogEntryType.Information)
        {
            //Write the log entry
            _eventLog.WriteEntry(message, eventLogEntryType);
        }
    
    }
}
