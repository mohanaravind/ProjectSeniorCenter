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
using ProjectSeniorCenter.Code;

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
        private Notifier _notifier;

        #endregion


        #region Properties

        /// <summary>
        /// Sets the notifier thread
        /// </summary>
        public Thread NotifierThread
        {
            set { _thNotifier = value; }
        }

        /// <summary>
        /// Notifier instance 
        /// </summary>
        public Notifier NotifierInstance
        {
            set { _notifier = value; }
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
            this.Close();           
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

        /// <summary>
        /// Gets triggered when the form gets loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Volunteer_Load(object sender, EventArgs e)
        {
            //Setting the About URL link
            LinkLabel.Link link = new LinkLabel.Link();
            link.LinkData = Configurations.AboutLink;
            lnkAbout.Links.Add(link);            
        }

        #endregion

    }
}
