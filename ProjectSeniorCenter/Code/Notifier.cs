using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using ProjectSeniorCenter.Code.Utility;

namespace ProjectSeniorCenter.Code
{
    public class Notifier
    {

        #region Declarations

        /// <summary>
        /// The notification form which has to be shown
        /// </summary>
        private Form _volunteer;

        #endregion

        #region Public Methods

        /// <summary>
        /// Consturctor
        /// </summary>
        /// <param name="volunteer"></param>
        public Notifier(Form volunteer)
        {
            this._volunteer = volunteer;
        }

        
        /// <summary>
        /// Starts the notifier process
        /// </summary>
        public void Start()
        {
            uint idleTime = 0;
            Int32 thresholdTime = Configurations.ThresholdTime;
            Int32 pollTime = Configurations.NotifierPollTime;

            try
            {
                while (true)
                {
                    //Get the system idle time
                    idleTime = Win32.GetIdleTime();

                    //Check whether the idle time has exceeded the threshold
                    if (idleTime > thresholdTime)
                    {
                        //Reset the idle time
                        idleTime = 0;

                        //Show the dialog
                        DialogResult result = _volunteer.ShowDialog();

                        if (result == DialogResult.Yes)                            
                            break;
                    }

                    //Wait till the polling time
                    Thread.Sleep(pollTime);
                }

                forceLogOff();
            }
            catch (ThreadAbortException)
            {
                forceLogOff();
            }

        }

        private void forceLogOff()
        {
            Win32.ForceLogOff();
            //System.Console.WriteLine("Logged Off");
        }


        #endregion

    }




}
