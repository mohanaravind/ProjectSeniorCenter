using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;

namespace ProjectSeniorCenter.Code.Entity
{
    /// <summary>
    /// This represents the list of PII details which would be populated  by the volunteer
    /// </summary>
    public class Person
    {
        public ObjectId Id { get; set; }
        public String Volunteer { get; set; }
        public String FullName { get; set; }
        public String DayOfBirth { get; set; }
        public String MonthOfBirth { get; set; }
        public String YearOfBirth { get; set; }
        public String PhoneNumber { get; set; }
        public String EmailAddress { get; set; }
        public String StreetAddress { get; set; }        
        public String City { get; set; }
        public String State { get; set; }
        public String Zip { get; set; }
        public String SpouseName { get; set; }
        public String[] Children { get; set; }
        public String[] GrandChildren { get; set; }
        public String[] PastEmployers { get; set; }
        public String[] CurrentEmployers { get; set; }
        public String LastUpdatedOn { get; set; }

        //Flag to state whether the person has an associated PII record in the DB
        public Boolean hasPII { get; set; }

        
        /// <summary>
        /// Copies the data from the given instance
        /// </summary>
        /// <param name="person"></param>
        public void Copy(Person person)
        {
            this.Volunteer = person.Volunteer;
            this.FullName = person.FullName;
            this.DayOfBirth = person.DayOfBirth;
            this.EmailAddress = person.EmailAddress;
            this.MonthOfBirth = person.MonthOfBirth;
            this.YearOfBirth = person.YearOfBirth;
            this.PhoneNumber = person.PhoneNumber;
            this.StreetAddress = person.StreetAddress;
            this.City = person.City;
            this.State = person.State;
            this.Zip = person.Zip;
            this.SpouseName = person.SpouseName;
            this.Children = person.Children;
            this.GrandChildren = person.GrandChildren;
            this.PastEmployers = person.PastEmployers;
            this.CurrentEmployers = person.CurrentEmployers;
            this.LastUpdatedOn = person.LastUpdatedOn;
        }

        Dictionary<String, String> _PII;

        /// <summary>
        /// Constructor
        /// </summary>
        public Person()
        {
            _PII = new Dictionary<String, String>();
        }

        /// <summary>
        /// Returns the PII information consolidated
        /// </summary>
        /// <returns></returns>
        public Dictionary<String, String> GetPII()
        {
            //If the PII is yet to be constructed
            if (_PII.Count == 0)
            {
                _PII.Add( "FullName"  , FullName.Replace(" ", "+"));
                _PII.Add( "DateOfBirth" , MonthOfBirth + "/" + DayOfBirth + "/" + YearOfBirth);                
                _PII.Add("PhoneNumber", PhoneNumber);
                _PII.Add("EmailAddress", EmailAddress);
                _PII.Add("StreetAddress", StreetAddress);
                _PII.Add("City", City);
                _PII.Add("State", State);
                _PII.Add("Zip", Zip);
                _PII.Add("SpouseName", SpouseName.Replace(" ", "+"));
                AddItems(_PII, Children, "Children");
                AddItems(_PII, GrandChildren, "GrandChildren");
                AddItems(_PII, PastEmployers, "PastEmployers");                
            }

            return _PII;
        }

        /// <summary>
        /// Adds the items to the dictionary
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="itemsToAdd"></param>
        /// <param name="key"></param>
        private void AddItems(Dictionary<String, String> parent, String[] itemsToAdd, String key)
        {
            int count = itemsToAdd.Length;

            for (int index = 0; index < count; index++)
            {
                //Add the item
                parent.Add(key + "_" + index.ToString(), itemsToAdd[index]);
            }            
        }

    }
}