using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using ProjectSeniorCenter.UI;
using ProjectSeniorCenter.Code;

namespace ProjectSeniorCenter
{
    class Program
    {



        /// <summary>
        /// Main entry point for the application
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //Wire up the log off event
            SystemEvents.SessionEnding += new SessionEndingEventHandler(Session_Ending);

            Worker worker = new Worker();

            //Start the work
            worker.Work();
        }

  

        /// <summary>
        /// Gets triggered when the user session is ending
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Session_Ending(object sender, SessionEndingEventArgs e)
        {
            
        }

    }
}
