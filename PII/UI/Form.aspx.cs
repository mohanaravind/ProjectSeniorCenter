using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PII.UI
{
    public partial class Form : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String windowsLogin = Page.User.Identity.Name;
            String[] name = windowsLogin.Split(new Char[] { '\\' });


            lblName.Text = name[1];
             
        }

        /// <summary>
        /// Gets triggered when the user submits the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //TODO
        }
    }
}