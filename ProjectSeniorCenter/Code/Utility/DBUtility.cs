using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

using ProjectSeniorCenter.Code.Entity;
using System.Reflection;
using System.Threading;
using ProjectSeniorCenter.Code.Utility;

namespace ProjectSeniorCenter.Code.Utility
{
    class DBUtility
    {

        private String _dbName;
        private String _tableName;

        public String TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="tableName"></param>
        private DBUtility(String dbName, String tableName)
        {
            this._dbName = dbName;
            this._tableName = tableName;
        }


        /// <summary>
        /// Factory Method
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DBUtility CreateDBUtility(String dbName, String tableName)
        {            
            return new DBUtility(dbName, tableName);
        }

        


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
        public Boolean AddData(NetworkData data)
        {
            //Declarations
            Boolean blnFlag = false;
            MongoDatabase objDB = GetServer().GetDatabase(_dbName);

            try
            {
                //Get the table
                MongoCollection objTable = objDB.GetCollection(_tableName);

                //Insert the data
                objTable.Insert(data);
                objTable.Save(data);
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
        public NetworkData GetData(String strDataBaseName, String strTableName, String strKey)
        {
            //Declarations
            NetworkData NetworkData = null;
            MongoCollection<NetworkData> colData = GetServer().GetDatabase(strDataBaseName).GetCollection<NetworkData>(strTableName);

            try
            {
                var query = new QueryDocument("Volunteer", strKey);
                foreach (NetworkData NetworkDataToFind in colData.Find(query))
                {
                    if (NetworkDataToFind.Id != null)
                    {
                        NetworkData = NetworkDataToFind;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log("GetData: " + ex.Message);

            }


            return NetworkData;

        }


        /// <summary>
        /// Retrieves the PII data of the volunteer
        /// </summary>
        /// <param name="strKey"></param>
        /// <returns></returns>
        public Person GetPersonData(String key)
        {
            //Declarations
            Person person = null;
            MongoCollection<Person> colData = GetServer().GetDatabase(_dbName).GetCollection<Person>(_tableName);

            try
            {
                var query = new QueryDocument("Volunteer", key);
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
                Logger.Log("GetData: " + ex.Message);

            }


            return person;
        }

        /// <summary>
        /// Retrieves the list of Allowed websites for volunteer
        /// </summary>
        /// <returns></returns>
        public List<Website> GetWebsites()
        {
            //Declarations
            List<Website> websites = null;
            MongoCollection<Website> colData = GetServer().GetDatabase(_dbName).GetCollection<Website>(_tableName);

            try
            {
                websites = colData.FindAll().ToList<Website>();
            }
            catch (Exception ex)
            {
                Logger.Log("GetData: " + ex.Message);

            }


            return websites;
        }



    }
}
