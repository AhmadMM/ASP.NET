//Class to convert List<T> to Datatable
 public static DataTable ToDataTable<T>(List<T> items)
    {
        DataTable dataTable = new DataTable(typeof(T).Name);

        //Get all the properties
        PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (PropertyInfo prop in Props)
        {
            //Setting column names as Property names
            dataTable.Columns.Add(prop.Name);
        }
        foreach (T item in items)
        {
            var values = new object[Props.Length];
            for (int i = 0; i < Props.Length; i++)
            {
                //inserting property values to datatable rows
                values[i] = Props[i].GetValue(item, null);
            }
            dataTable.Rows.Add(values);
        }
        //put a breakpoint here and check datatable
        return dataTable;
    }


//Function to convert List<string> to List<x>
List<x> ListFilteredSKUs = new List<CS_FilteredSKUs>(); 
ListFilteredXs = StringList.Select(strParams => new x() { xValue=strParams }).ToList();


//Convert List<x> to Datatable
 DataTable dtFilteredXs =  ToDataTable(ListFilteredXs);


//Send dtFilteredXs to UDT
SqlParameter SQLParam_FilteredXs = cmd.Parameters.AddWithValue("@FilteredXs", dtFilteredXs 
SQLParam_FilteredXs.SqlDbType = SqlDbType.Structured;
SQLParam_FilteredXs.TypeName = "dbo.Type_FilteredXs";
        
   