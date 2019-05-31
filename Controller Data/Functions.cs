    private ReportData Funx(int x, int y,int z)
        {

            try
            {
                //Data Initialization
                ReportData result = new ReportData (); 
                Classes data = new Classes();

                result.Title = "Hello";
                result.Description = "World";
                result.Info = "";
                result.View_By = "";
                result.Type = "bar";  

                 /*................................Query.....................................*/
                string strConnString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                SqlCommand Query = new SqlCommand();
                Query.CommandType = CommandType.StoredProcedure;
                Query.CommandText = "Proc_";
  
                SqlParameter pX = Query.Parameters.AddWithValue("@X", X); 
                SqlParameter pY = Query.Parameters.AddWithValue("@Y", Y); 
                SqlParameter pZ = Query.Parameters.AddWithValue("@Z", Z);

                DataSet ds = new DataSet();
                SqlConnection conn = new SqlConnection(strConnString);
                conn.Open();
                Query.Connection = conn;
                SqlDataAdapter da = new SqlDataAdapter(Query);
                da.Fill(DataTable_Values);
                conn.Close();
                conn.Dispose();
                /*.....................................................................*/
                  
                foreach (DataRow row in DataTable_Values.Rows)
                {
                     
                } 
                result.Data = data;

                return result;
            }
            catch (Exception ex)
            {
                Express_Report_Output result = new Express_Report_Output();
                // Get stack trace for the exception with source file information
                var st = new StackTrace(ex, true);
                // Get the top stack frame
                var frame = st.GetFrame(0);
                // Get the line number from the stack frame
                var line = frame.GetFileLineNumber();
                result.Data = ex.Message + "," + line;
                return result;
            }
        }
