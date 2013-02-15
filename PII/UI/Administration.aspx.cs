using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PII.Code.Utility;
using PII.Code.Entity;
using System.Threading;

namespace PII.UI
{
    public partial class Administration : System.Web.UI.Page
    {



        #region Handlers
        /// <summary>
        /// Gets triggered when the page gets loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //Get the logged in user's name            
            String windowsLogin = Thread.CurrentPrincipal.Identity.Name;

            //////Comment this out before release
            //windowsLogin = @"Aravind-PC\Aravind";
            
            String[] name = windowsLogin.Split(new Char[] { '\\' });

            Logger.Log(name[0]);
            Logger.Log(name[name.Length - 1]);

            if (!name[name.Length - 1].Trim().Equals(Configurations.Administrator))
            {                
                Response.Redirect("Unauthorized.html");
                return;
            }

            if(!Page.IsPostBack)
                BindData();
        }

        /// <summary>
        /// Gets triggered when the user clicks on add the website button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            PersistData();
            BindData();
        }

        
        /// <summary>
        /// Gets triggered when the user clicks on delete button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdWebsites_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                //Get the list of websites
                List<Website> websites = (List<Website>)Session["Websites"];
                Website websiteToDelete = null;
                DBUtility dbUtility = new DBUtility();
                String dbName = Configurations.DatabaseName;
                String tableName = Configurations.Websites;

                //Get the website which has to be deleted
                websiteToDelete = websites[e.RowIndex];

                if (!dbUtility.Delete(dbName, tableName, websiteToDelete))
                    throw new Exception("Error while deleting");

                //Bind the data again
                BindData();
            }
            catch (Exception ex)
            {
                DisplayMessage("Unable to delete the website");
                Logger.Log("grdWebsites_RowDeleting: " + ex.Message);                
            }

        }

        #endregion

        #region Methods
        /// <summary>
        /// Binds the data to the grid view
        /// </summary>
        private void BindData()
        {
            try
            {
                //Declarations
                DBUtility dbUtility = new DBUtility();
                String dbName = Configurations.DatabaseName;
                String tableName = Configurations.Websites;
                List<Website> websites;

                //Bind the grid view
                grdWebsites.DataSource = dbUtility.getData(dbName, tableName, out websites);
                grdWebsites.DataBind();

                //Add the list to session
                Session.Add("Websites", websites ?? new List<Website>());
            }
            catch (Exception ex)
            {
                Logger.Log("BindData: " + ex.Message); 
            }
        }


        /// <summary>
        /// Persists the website data to the database
        /// </summary>
        private void PersistData()
        {
            //Declaratoins
            DBUtility dbUtility = new DBUtility();
            String dbName = Configurations.DatabaseName;
            String tableName = Configurations.Websites;

            try
            {
                //Check if the data is valid
                if (!ValidateData())
                    return;

                Website website = new Website();

                website.Name = txtName.Text;
                website.URL = txtURL.Text;
                website.IsActive = true;
                website.LastUpdatedOn = DateTime.Now.ToShortDateString();

                //Update the data
                dbUtility.UpdateData(dbName, tableName, website);

                //Clear the textboxes
                txtName.Text = String.Empty;
                txtURL.Text = String.Empty;
            }
            catch (Exception ex)
            {
                DisplayMessage("Unable to update your data");
                Logger.Log("PersistData: " + ex.Message);
            }
        }

        /// <summary>
        /// Validates the user's input data
        /// </summary>
        /// <returns></returns>
        private bool ValidateData()
        {
            return true;
        }


        /// <summary>
        /// Displays the message through alert box
        /// </summary>
        /// <param name="message"></param>
        private void DisplayMessage(String message)
        {
            //Display the message            
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + message + "');", true);
        }


        #endregion





    }
}