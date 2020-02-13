using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace BCCBookStore
{
    /// <summary>
    /// Summary description for server
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
   [System.Web.Script.Services.ScriptService]
    public class server : System.Web.Services.WebService
    {
        [System.Web.Script.Services.ScriptMethod]
         [WebMethod]
        public List<string> GetBookName(string prefixText)
        {
            try
            {
                Global.Connect();
                Global.Connection();
                SqlCommand sqlCommand = new SqlCommand("GetBooks", Global.con);
                sqlCommand.Parameters.AddWithValue("@Name", prefixText);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                //------------------------------------------------ 
                DataTable dt = new DataTable();

                SqlDataAdapter adp = new SqlDataAdapter(sqlCommand);

                adp.Fill(dt);

                List<string> Books = new List<string>();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Books.Add(dt.Rows[i][0].ToString());
                }
                return Books;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        [System.Web.Script.Services.ScriptMethod]
        [WebMethod]
        public List<string> GetBookPublisher(string prefixText)
        {
            try
            {
                Global.Connect();
                Global.Connection();
                SqlCommand sqlCommand = new SqlCommand("GetBooks", Global.con);
                sqlCommand.Parameters.AddWithValue("@PublisherName", prefixText);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                //------------------------------------------------ 
                DataTable dt = new DataTable();

                SqlDataAdapter adp = new SqlDataAdapter(sqlCommand);

                adp.Fill(dt);

                List<string> Books = new List<string>();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Books.Add(dt.Rows[i][0].ToString());
                }
                return Books;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//  [System.Web.Script.Services.ScriptMethod]
        [WebMethod]
        public List<string> GetBookAuthor(string prefixText)
        {
            try
            {
                Global.Connect();
                Global.Connection();
                SqlCommand sqlCommand = new SqlCommand("GetBooks", Global.con);
                sqlCommand.Parameters.AddWithValue("@Author", prefixText);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                //------------------------------------------------ 
                DataTable dt = new DataTable();

                SqlDataAdapter adp = new SqlDataAdapter(sqlCommand);

                adp.Fill(dt);

                List<string> Books = new List<string>();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Books.Add(dt.Rows[i][0].ToString());
                }
                return Books;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
     [System.Web.Script.Services.ScriptMethod]
        [WebMethod]
        public List<string> GetSchoolName(string prefixText)
        {
            try
            {
                Global.Connect();
                Global.Connection();
                SqlCommand sqlCommand = new SqlCommand("GetSchools", Global.con);
                sqlCommand.Parameters.AddWithValue("@Name", prefixText);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                //------------------------------------------------ 
                DataTable dt = new DataTable();

                SqlDataAdapter adp = new SqlDataAdapter(sqlCommand);

                adp.Fill(dt);

                List<string> School = new List<string>();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    School.Add(dt.Rows[i][0].ToString());
                }
                return School;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
    }
}
