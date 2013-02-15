using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MongoDB.Bson;

namespace ProjectSeniorCenter.Code.Entity
{
    #region Entity

    class NetworkData
    {
        public ObjectId Id { get; set; }
        public Website Site { get; set; }
        public String URLFullPath { get; set; }
        public Boolean IsHTTPS { get; set; }
        //public Dictionary<String, Int32> PII { get; set; }
        public List<String> PII { get; set; }
        public String SentOn { get; set; }

        /// <summary>
        /// Checks whether the network data contains any of the PII information
        /// and returns the flag
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public Boolean ContainsPII(Person person, String requestParameters)
        {
            Boolean containsPII = false;
            String sanitizedKey;

            try
            {
                //If there are no request parameters
                if (requestParameters == null)
                    return false;

                //Initialize
                //this.PII = new Dictionary<string, Int32>();
                this.PII = new List<string>();

                //Get the PII data
                Dictionary<String,String> dicPII = person.GetPII();

                foreach (KeyValuePair<String,String> pII in dicPII)
                {
                    //If the request parameter contains the PII information
                    if (requestParameters.Contains(pII.Value))
                    {                            
                        if(pII.Key.Contains("_"))
                            sanitizedKey = SanitizeKey(pII.Key);
                        else
                            sanitizedKey = pII.Key;

                        ////If this PII was already encountered
                        //if (this.PII.ContainsKey(sanitizedKey))
                        //    //Increment the count
                        //    this.PII[sanitizedKey] = this.PII[sanitizedKey] + 1;
                        //else
                        //    //Initialize the count
                        //    this.PII.Add(sanitizedKey, 1);

                        //If its not already present
                        if(!PII.Contains(sanitizedKey))
                            PII.Add(sanitizedKey);

                        containsPII = true;
                        break;
                    }
                }
                
            }
            catch (Exception ex)
            {
                containsPII = false;
            }
            
            
            return containsPII;
        }

        /// <summary>
        /// Sanitizes the key of _ 
        /// Eg: Children_1 becomes Children
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private String SanitizeKey(String key)
        {
            String sanitizedKey = key;
            String[] arrSanitizedKey;

            try
            {
                arrSanitizedKey = key.Split(new Char[] { '_' });
                sanitizedKey = arrSanitizedKey[0];
            }
            catch (Exception ex)
            {
                sanitizedKey = key;
            }
            
            return sanitizedKey;
        }
    }

    #endregion
}
