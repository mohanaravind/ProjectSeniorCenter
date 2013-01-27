using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;

namespace PII.Code.Utility
{
    public static class Utility
    {

        /// <summary>
        /// Returns the data containing the number of data between a given interval
        /// </summary>
        /// <returns></returns>
        public static DataTable GetTableData(Int32 start, Int32 end)
        {
            //Declarations
            DataTable data = new DataTable();
            Int32 count = end + 1;

            data.Columns.Add("Data", typeof(String));

            //Construct the data
            for (int index = start; index < count; index++)
            {
                //Add a new data
                DataRow day = data.NewRow();

                day["Data"] = index.ToString();

                //Add the data
                data.Rows.Add(day);
            }

            return data;

        }


        /// <summary>
        /// Appends the array of data separated with comma
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static String AppendWithComma(String[] data)
        {
            StringBuilder result = new StringBuilder();
            Int32 count = data.Length - 1;

            for (int index = 0; index < count; index++)
            {
                result.Append(data[index]);
                result.Append(",");
            }

            result.Append(data[count]);


            return result.ToString();
        }
    }
}