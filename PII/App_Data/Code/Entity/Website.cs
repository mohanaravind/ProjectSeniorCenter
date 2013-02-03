using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;

namespace PII.Code.Entity
{
    public class Website
    {
        public ObjectId Id { get; set; }
        public String Name { get; set; }
        public String URL { get; set; }
        public Boolean IsActive { get; set; }
        public String LastUpdatedOn { get; set; }


        /// <summary>
        /// Copies the data from the given instance
        /// </summary>
        /// <param name="website"></param>
        public void Copy(Website website)
        {
            this.Name = website.Name;
            this.URL = website.URL;
            this.IsActive = website.IsActive;
            this.LastUpdatedOn = website.LastUpdatedOn;
        }
    }
}