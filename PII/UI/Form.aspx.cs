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
                String windowsLogin = Thread.CurrentPrincipal.Identity.Name;

                //////Comment this out before release
                windowsLogin = @"Aravind-PC\Aravind";

                String[] name = windowsLogin.Split(new Char[] { '\\' });
                
                //Display the volunteer name on screen                
                lblName.Text = name[name.Length - 1];

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

                //Bind the days
                drpDay.DataSource = Utility.GetTableData(1, 31);
                drpDay.DataBind();

                //Bind the years
                drpYear.DataSource = Utility.GetTableData(1940, 1996);
                drpYear.DataBind();

                //Get the person data
                person = dbUtility.getData("Testing", "PII", lblName.Text);

                //Set the birth dates
                drpMonth.SelectedValue = person.MonthOfBirth;
                drpDay.SelectedValue = person.DayOfBirth;
                drpYear.SelectedValue = person.YearOfBirth;

                //Bind the personal information
                txtName.Text = person.FullName;
                txtCity.Text = person.City;
                txtEmail.Text = person.EmailAddress;
                txtSpouse.Text = person.SpouseName;
                txtState.Text = person.State;
                txtStreet.Text = person.StreetAddress;
                txtZip.Text = person.Zip;

                txtCurrentEmployer.Text = Utility.AppendWithComma(person.CurrentEmployers);
                txtPastEmployer.Text = Utility.AppendWithComma(person.PastEmployers);
                txtChildren.Text = Utility.AppendWithComma(person.Children);
                txtGrandChildren.Text = Utility.AppendWithComma(person.GrandChildren);

                //Populate the phone 
                phone = person.PhoneNumber.Split(new Char[] { '-' });
                txtPhone1.Text = phone[0];
                txtPhone2.Text = phone[1];
                txtPhone3.Text = phone[2];
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

                person = new Person
                {
                    Volunteer = lblName.Text,
                    Children = txtChildren.Text.Split(splitter),
                    GrandChildren = txtGrandChildren.Text.Split(splitter),
                    City = txtCity.Text,
                    CurrentEmployers = txtCurrentEmployer.Text.Split(splitter),
                    MonthOfBirth = drpMonth.SelectedValue,
                    DayOfBirth = drpDay.SelectedValue,
                    YearOfBirth = drpYear.SelectedValue,
                    EmailAddress = txtEmail.Text,
                    FullName = txtName.Text,
                    PastEmployers = txtPastEmployer.Text.Split(splitter),
                    PhoneNumber = txtPhone1.Text + "-" + txtPhone2.Text + "-" + txtPhone3.Text,
                    SpouseName = txtSpouse.Text,
                    State = txtState.Text,
                    StreetAddress = txtStreet.Text,
                    Zip = txtZip.Text
                };

                //Insert the data to database
                DBUtility dbUtility = new DBUtility();
                dbUtility.UpdateData("Testing", "PII", person);
            }
            catch (Exception ex)
            {                
                Logger.Log("PersistData: " + ex.Message);
            }
        }


        #endregion

    }
}