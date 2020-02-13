  [System.Web.Script.Services.ScriptMethod()]

        [System.Web.Services.WebMethod]

        public static List<string> GetCustomers(string prefixText)
        {

            DataTable dt = new DataTable();


            SqlCommand cmd = new SqlCommand("select [PersonID] ,[PrsFNme] + ' ' +[PrsLName] from [Person] where PrsType='Customer' AND  PrsFNme like @Name+'%' OR PrsLName like @Name+'%' ", Global.con);

            cmd.Parameters.AddWithValue("@Name", prefixText);

            SqlDataAdapter adp = new SqlDataAdapter(cmd);

            adp.Fill(dt);

            List<string> CityNames = new List<string>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                CityNames.Add(dt.Rows[i][1].ToString());

            }

            return CityNames;

        }