using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectSeniorCenter.Code.Utility
{
    static class Utility
    {
        public static Dictionary<String,String> GetRequestParameters(String strRequestBody)
        {
            //Declaratoions
            Dictionary<String, String> dicRequestParameters = new Dictionary<string, string>(); 
            StringBuilder sbRequestParameters = new StringBuilder();

            try
            {
                //Get the parameters
                String[] arrParameters = strRequestBody.Split(new Char[] { '&' });

                foreach (String strParameter in arrParameters)
                {
                    String[] arrNameValuePairs = strParameter.Split(new Char[] { '=' });
                    //Add the request parameter
                    dicRequestParameters.Add(arrNameValuePairs[0], arrNameValuePairs[1]);
                }
            }
            catch (Exception ex)
            {
                DisplayException("GetRequestParamters", ex);
            }
            
            return dicRequestParameters;
        }


        /// <summary>
        /// Displays the error message while in debug mode
        /// </summary>
        /// <param name="strMethodName"></param>
        /// <param name="ex"></param>
        public static void DisplayException(String strMethodName, Exception ex)
        {

        }


        /// <summary>
        /// Sets the sniffer's configurations from application startup parameters
        /// </summary>
        public static void SetSnifferConfiguration(String[] arrgs)
        {
            Config enConfig;

            if (arrgs.Length > 0)
            {

            }
        }




    }
}
