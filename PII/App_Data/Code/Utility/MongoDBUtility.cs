using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

using PII.Code.Entity;
using System.Reflection;
using System.Threading;

namespace PII.Code.Utility
{
    class DBUtility
    {

        /// <summary>
        /// Method to get the Mongo Server reference
        /// </summary>
        /// <returns>MongoServer</returns>
        private MongoServer GetServer()
        {
            //Declarations
            StringBuilder sbConnectionString = new StringBuilder();
            MongoServer objMongoServer = null;

            try
            {
                //objMongoServer = MongoServer.Create(sbConnectionString.ToString());
                objMongoServer = MongoServer.Create(Configurations.ConnectionString);
            }
            catch (Exception ex)
            {
                Logger.Log("GetServer: " + ex.Message);
            }


            return objMongoServer;
        }                  

        /// <summary>
        /// Adds the Data base
        /// </summary>
        /// <param name="strDBName"></param>
        /// <returns></returns>
        public Boolean AddData(String strDBName, String strTableName, Person objData)
        {
            //Declarations
            Boolean blnFlag = false;
            MongoDatabase objDB = GetServer().GetDatabase(strDBName);

            try
            {
                //Get the table
                MongoCollection objTable = objDB.GetCollection(strTableName);

                //Insert the data
                objTable.Insert(objData);
                objTable.Save(objData);
                blnFlag = true;
            }
            catch (Exception ex)
            {
                Logger.Log("AddData: " + ex.Message);
            }

            return blnFlag;
        }
        
        /// <summary>
        /// Returns the table data
        /// </summary>
        /// <param name="strTableName"></param>
        /// <returns></returns>
        public Person getData(String strDataBaseName, String strTableName, String strKey)
        {
            //Declarations
            Person person = null;
            MongoCollection<Person> colData = GetServer().GetDatabase(strDataBaseName).GetCollection<Person>(strTableName);

            try
            {
                var query = new QueryDocument("Volunteer", strKey);
                foreach (Person personToFind in colData.Find(query))
                {
                    if (personToFind.FullName != String.Empty)
                    {
                        person = personToFind;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log("getData: " + ex.Message);

            }


            return person;

        }

        /// <summary>
        /// Updates the existing data if present else inserts it as a new record
        /// </summary>
        /// <param name="strDataBaseName"></param>
        /// <param name="strTableName"></param>
        /// <param name="person"></param>
        /// <returns></returns>
        public Boolean UpdateData(String strDataBaseName, String strTableName, Person person)
        {
            //Declarations            
            MongoCollection<Person> colData = GetServer().GetDatabase(strDataBaseName).GetCollection<Person>(strTableName);
            Boolean isExistingData = false;
           
            try
            {
                QueryDocument queryDocument = new QueryDocument("Volunteer", person.Volunteer);
                foreach (Person personToUpdate in colData.Find(queryDocument))
                {
                    if (personToUpdate.FullName != String.Empty)
                    {
                        personToUpdate.Copy(person);
                        colData.Save(personToUpdate);
                        isExistingData = true;
                        break;
                    }
                }

                //If its not an existing data
                if (!isExistingData)
                    colData.Save(person);
                
            }
            catch (Exception ex)
            {
                Logger.Log("UpdateData: " + ex.Message);

            }

            return true;
            

        }

        /// <summary>
        /// Updates the existing data if present else inserts it as a new record
        /// </summary>
        /// <param name="strDataBaseName"></param>
        /// <param name="strTableName"></param>
        /// <param name="person"></param>
        /// <returns></returns>
        public Boolean UpdateData(String strDataBaseName, String strTableName, Website website)
        {
            //Declarations            
            MongoCollection<Website> colData = GetServer().GetDatabase(strDataBaseName).GetCollection<Website>(strTableName);
            Boolean isExistingData = false;

            try
            {
                QueryDocument queryDocument = new QueryDocument("Name", website.Name);
                foreach (Website websiteToUpdate in colData.Find(queryDocument))
                {
                    if (websiteToUpdate.Name != String.Empty)
                    {
                        websiteToUpdate.Copy(website);
                        colData.Save(websiteToUpdate);
                        isExistingData = true;
                        break;
                    }
                }

                //If its not an existing data
                if (!isExistingData)
                    colData.Save(website);

            }
            catch (Exception ex)
            {
                Logger.Log("UpdateData: " + ex.Message);

            }

            return true;


        }

        /// <summary>
        /// Returns the table data
        /// </summary>
        /// <param name="strTableName"></param>
        /// <returns></returns>
        public DataTable getData(String strDataBaseName, String strTableName, out List<Website> lstWebsites)
        {
            //Declarations
            DataTable dtData = new DataTable();
            MongoCollection<Website> colData = GetServer().GetDatabase(strDataBaseName).GetCollection<Website>(strTableName);
            MongoCursor<Website> curData = colData.FindAll();
            SiteChecker siteChecker;
            Int32 count;
            Int32 waitingTime;

            //Set the columns            
            dtData.Columns.Add("Name", typeof(String));
            dtData.Columns.Add("URL", typeof(String));
            dtData.Columns.Add("Status", typeof(String));
            dtData.Columns.Add("LastUpdatedOn", typeof(String));
            dtData.Columns.Add("Modify", typeof(String));
 
            //Initialize
            lstWebsites = new List<Website>();

            try
            {
                //Get the waiting time
                waitingTime = Configurations.SiteCheckerThreshold;

                //Add it to the list of websites
                lstWebsites = curData.ToList<Website>();

                //Create the data for the viewer
                foreach (Website objWebsite in curData)
                {                   
                    DataRow drRow = dtData.NewRow();

                    //Check for the site status
                    siteChecker = new SiteChecker(objWebsite.URL);

                    //Create the thread to check the site existence
                    Thread remoteCheck = new Thread(new ThreadStart(siteChecker.RemoteFileExists));
                    remoteCheck.Start();

                    //Initialize the count
                    count = 0;

                    //While the site has non existent
                    while (!siteChecker.Exists && count < waitingTime)
                    {
                        Thread.Sleep(1000);
                        count++;
                    }

                    remoteCheck.Abort();
                    remoteCheck.Join();

                    //Get the status of the website
                    objWebsite.IsActive = siteChecker.Exists;

                    drRow[0] = objWebsite.Name;
                    drRow[1] = objWebsite.URL;
                    drRow[2] = objWebsite.IsActive ? @"..\Images\active.png":@"..\Images\inactive.png";
                    drRow[3] = objWebsite.LastUpdatedOn;
                    drRow[4] = objWebsite.IsActive ? "Deactivate" : "Activate";

                    //Add the row
                    dtData.Rows.Add(drRow);
                }
            }
            catch (Exception ex)
            {
                Logger.Log("DBUtility.getData:" + ex.Message);
            }
            

            return dtData;

        }

        /// <summary>
        /// Deletes the document from MongoDB
        /// </summary>
        /// <param name="strDataBaseName"></param>
        /// <param name="strTableName"></param>
        /// <param name="website"></param>
        /// <returns></returns>
        public Boolean Delete(String strDataBaseName, String strTableName, Website website)
        {
            //Declaratoins
            Boolean result = false;

            try
            {
                MongoCollection<Website> colData = GetServer().GetDatabase(strDataBaseName).GetCollection<Website>(strTableName);

                //Remove the document
                colData.Remove(Query.EQ("_id", website.Id));

                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log("Delete:" + ex.Message);
                
            }

            return result;
        }
    }
}
