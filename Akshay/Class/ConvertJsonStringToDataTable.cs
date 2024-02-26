using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;

public class ConvertJsonStringToDataTable
{
   public DataTable JsonStringToDataTableOtherDetails(string jsonData)
   {    // Create a DataTable
       DataTable dataTable = new DataTable();

       // Parse JSON array
       jsonData = jsonData.Trim('[', ']');
       string[] jsonItems = jsonData.Split(new string[] { "}," }, StringSplitOptions.RemoveEmptyEntries);

       // Define columns based on the first JSON item
       string firstItem = jsonItems[0].Trim('{', '}');
       string[] firstItemKeyValuePairs = firstItem.Split(',');
       foreach (string keyValuePair in firstItemKeyValuePairs)
       {
           string[] keyValue = keyValuePair.Split(':');
           string columnName = keyValue[0].Trim('"').Trim(); // Remove leading and trailing whitespaces
           dataTable.Columns.Add(columnName);
       }

       // Populate DataTable with values
       foreach (string jsonItem in jsonItems)
       {
           string item = jsonItem.Trim('{', '}');
           string[] keyValuePairs = item.Split(',');

           DataRow dataRow = dataTable.NewRow();

           foreach (string keyValuePair in keyValuePairs)
           {
               string[] keyValue = keyValuePair.Split(':');
               string columnName = keyValue[0].Trim('"').Trim(); // Remove leading and trailing whitespaces

               // Check if the column exists before accessing its value
               if (dataTable.Columns.Contains(columnName))
               {
                   string value = keyValue.Length > 1 ? keyValue[1].Trim('"').Trim() : string.Empty; // Remove leading and trailing whitespaces
                   dataRow[columnName] = value;
               }
           }

           dataTable.Rows.Add(dataRow);
       }

       return dataTable;

   }
}