using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Threading;
using ProjectSeniorCenter.Code.Utility;

namespace ProjectSeniorCenter.UI
{
    public partial class Volunteer : Form
    {
        public Volunteer()
        {
            InitializeComponent();
        }

        #region Declarations

        private Thread _thNotifier;

        #endregion


        #region Properties

        /// <summary>
        /// Sets the notifier thread
        /// </summary>
        public Thread NotifierThread
        {
            set { _thNotifier = value; }
        }

        #endregion


        #region Event Handlers


        /// <summary>
        /// Gets triggered when the user opts to volunteer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnYes_Click(object sender, EventArgs e)
        {
            //Set the log message
            Logger.Log(Configurations.User + " | "  + " volunteers", System.Diagnostics.EventLogEntryType.Information);

            try
            {
                if (_thNotifier != null)
                {
                    _thNotifier.Abort();
                    _thNotifier.Join();
                }
            }
            catch (Exception ex)
            {
                //Set the error log message
                Logger.Log(Configurations.User + " | " + ex.InnerException.Message, System.Diagnostics.EventLogEntryType.Error);
            }
        }

        /// <summary>
        /// Gets triggered when the user opts not to volunteer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        /// <summary>
        /// Opens up the about page in the default browser
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkAbout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.google.com");
        }



        #endregion



    }
}
