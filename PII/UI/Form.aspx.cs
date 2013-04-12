using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PII.Code.Entity;
using PII.Code.Utility;
using System.Threading;

namespace PII.UI
{
    public partial class Form : System.Web.UI.Page
    {

        #region Handlers

        /// <summary>
        /// Gets triggered everytime when the page gets loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Get the logged in user's name            
                //String windowsLogin = Thread.CurrentPrincipal.Identity.Name;

                //////Comment this out before release
                //windowsLogin = @"Aravind-PC\Aravind";

                //String[] name = windowsLogin.Split(new Char[] { '\\' });
                
                //Display the volunteer name on screen                
                //lblName.Text = name[name.Length - 1];

                String user = String.Empty;

                if(Request.QueryString.Get("User") != null)
                    user = Request.QueryString.Get("User").Trim();

                lblName.Text = user;
                
                if (!Page.IsPostBack)
                   BindData();
            }
            catch (Exception ex)
            {
                Logger.Log("Page_Load: " + ex.Message);
            }                         
        }

        /// <summary>
        /// Gets triggered when the user submits the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            PersistData();
        }


        #endregion


        #region Methods

        /// <summary>
        /// Binds the data to UI
        /// </summary>
        private void BindData()
        {
            try
            {
                //Declarations
                DBUtility dbUtility = new DBUtility();
                Person person;
                String[] phone;
                String tableName, dataBaseName;

                //Get the table name and datatbase name from the configuration file
                tableName = Configurations.TableName;
                dataBaseName = Configurations.DatabaseName;

                drpMonth.CssClass = "input-success";

                //Bind the days
                drpDay.DataSource = Utility.GetTableData(1, 31);
                drpDay.DataBind();
                drpDay.CssClass = "input-success";

                //Bind the years
                drpYear.DataSource = Utility.GetTableData(1900, 1996);
                drpYear.DataBind();
                drpYear.CssClass = "input-success";

                //Get the person data
                person = dbUtility.getData(dataBaseName, tableName, lblName.Text);

                //Check if the person has a data or it has been configured not to display the PII 
                if (person == null || !Configurations.DisplayPII)
                    return;

                //Set the birth dates
                drpMonth.SelectedValue = person.MonthOfBirth;
                drpMonth.CssClass = "input-success";
                drpDay.SelectedValue = person.DayOfBirth;
                drpDay.CssClass = "input-success";
                drpYear.SelectedValue = person.YearOfBirth;
                drpYear.CssClass = "input-success";

                //Bind the personal information
                txtName.Text = person.FullName;
                txtName.CssClass = "input-success";
                txtCity.Text = person.City;
                txtCity.CssClass = "input-success";
                txtEmail.Text = person.EmailAddress;
                txtEmail.CssClass = "input-success";
                txtSpouse.Text = person.SpouseName;
                txtSpouse.CssClass = "input-success";
                txtState.Text = person.State;
                txtState.CssClass = "input-success";
                txtStreet.Text = person.StreetAddress;
                txtStreet.CssClass = "input-success";
                txtZip.Text = person.Zip;
                txtZip.CssClass = "input-success";

                txtCurrentEmployer.Text = Utility.AppendWithComma(person.CurrentEmployers);
                txtCurrentEmployer.CssClass = "input-success";
                txtPastEmployer.Text = Utility.AppendWithComma(person.PastEmployers);
                txtPastEmployer.CssClass = "input-success";
                txtChildren.Text = Utility.AppendWithComma(person.Children);
                txtChildren.CssClass = "input-success";
                txtGrandChildren.Text = Utility.AppendWithComma(person.GrandChildren);
                txtGrandChildren.CssClass = "input-success";

                //Populate the phone 
                phone = person.PhoneNumber.Split(new Char[] { '-' });
                txtPhone1.Text = phone[0];
                txtPhone1.CssClass = "input-success";
                txtPhone2.Text = phone[1];
                txtPhone2.CssClass = "input-success";
                txtPhone3.Text = phone[2];
                txtPhone3.CssClass = "input-success";

                lblUpdatedOn.Text = "Last updated on: " + person.LastUpdatedOn ;
            }
            catch (Exception ex)
            {
                Logger.Log("BindData: " + ex.Message);
            }    
                        
        }


        /// <summary>
        /// Persists the user PII data to the database
        /// </summary>
        private void PersistData()
        {
            try
            {
                Person person;
                Char[] splitter = new Char[] { ',' };
                String tableName, dataBaseName;

                //Get the table name and datatbase name from the configuration file
                tableName = Configurations.TableName;
                dataBaseName = Configurations.DatabaseName;

                //Check if the data is valid
                if (!ValidateData())
                    return;


                person = new Person
                {
                    Volunteer = lblName.Text.Trim(),
                    Children = txtChildren.Text.Trim().Split(splitter),
                    GrandChildren = txtGrandChildren.Text.Trim().Split(splitter),
                    City = txtCity.Text,
                    CurrentEmployers = txtCurrentEmployer.Text.Trim().Split(splitter),
                    MonthOfBirth = drpMonth.SelectedValue,
                    DayOfBirth = drpDay.SelectedValue,
                    YearOfBirth = drpYear.SelectedValue,
                    EmailAddress = txtEmail.Text.Trim(),
                    FullName = txtName.Text.Trim(),
                    PastEmployers = txtPastEmployer.Text.Trim().Split(splitter),
                    PhoneNumber = txtPhone1.Text.Trim() + "-" + txtPhone2.Text.Trim() + "-" + txtPhone3.Text.Trim(),
                    SpouseName = txtSpouse.Text.Trim(),
                    State = txtState.Text.Trim(),
                    StreetAddress = txtStreet.Text.Trim(),
                    Zip = txtZip.Text.Trim(),
                    LastUpdatedOn = DateTime.Now.ToShortDateString()
                }; 

                //Insert the data to database
                DBUtility dbUtility = new DBUtility();
                dbUtility.UpdateData(dataBaseName, tableName, person);

                DisplayMessage("Successfully updated your data");
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