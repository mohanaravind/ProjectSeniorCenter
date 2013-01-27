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

    }
}
