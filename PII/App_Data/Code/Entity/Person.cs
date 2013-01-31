using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;

namespace PII.Code.Entity
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
        }
    }
}