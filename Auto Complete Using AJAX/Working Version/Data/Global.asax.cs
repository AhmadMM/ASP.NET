using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

using Microsoft.VisualBasic;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Web.Routing;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI;
namespace BCCBookStore
{
    public class Global : System.Web.HttpApplication
    {

        public static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        public static string Status;
        public static string FlagStatus;
        public static string Type;
        public static string SubType;
        public static string RowID;
        public static string LastName;
        public static string UserName;
        public static int ArraySize;
        public static string Role;
        public static string PrsnID;
        public static Int32 Counter = 1;
        public static string TaskName;
        public static DataTable SharedDataTable = new DataTable();
        public static Int32 ExportID;
        public static int ImageFlag;
        public static string[] list = new string[10];
        public static string HtmlBuilder { get; set; }
        public static string Term;
        public static string TermID;
        public static string MoneyCurrency;
        public static string HtmlColorMsg = "";
        public static string Connection()
        {
            System.Configuration.Configuration rootWebConfig = default(System.Configuration.Configuration);
            rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/Web");
            System.Configuration.ConnectionStringSettings connString = default(System.Configuration.ConnectionStringSettings);
            if ((rootWebConfig.ConnectionStrings.ConnectionStrings.Count > 0))
            {
                connString = rootWebConfig.ConnectionStrings.ConnectionStrings["ConnectionString"];
                return connString.ConnectionString;
            }
            return null;
        }
        public static void Connect()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            if (con.State == ConnectionState.Closed)
            {
                //con.ConnectionString = Connection();
                con.Open();
            }
        }
        public static void LoadTableSP_BNS(GridView gvGrid, string SpName, string Class)
        {
            try
            {
                Global.Connect();
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection(Global.Connection());
                SqlCommand com = new SqlCommand(SpName, con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@GradeID", Class);
                SqlDataAdapter da = new SqlDataAdapter(com);
                try
                {
                    con.Open();
                    da.Fill(dt);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
                gvGrid.DataSource = dt;
                gvGrid.DataBind();
            }
            catch (Exception ex)
            {
            }
        }
        public static void LoadTableSP_EDITBNS(GridView gvGrid, string SpName, string Class, string School)
        {
            try
            {
                Global.Connect();
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection(Global.Connection());
                SqlCommand com = new SqlCommand(SpName, con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@GradesID", Class);
                com.Parameters.AddWithValue("@SchoolsID", School);
                SqlDataAdapter da = new SqlDataAdapter(com);
                try
                {
                    con.Open();
                    da.Fill(dt);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
                gvGrid.DataSource = dt;
                gvGrid.DataBind();
            }
            catch (Exception ex)
            {
            }
        }
        public static void LoadTableRepeater(Repeater gvGrid, string SpName, int NumbOfTop)
        {
            try
            {
                Global.Connect();
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection(Global.Connection());
                SqlCommand com = new SqlCommand(SpName, con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@TOPNUM", NumbOfTop);
                SqlDataAdapter da = new SqlDataAdapter(com);
                try
                {
                    con.Open();
                    da.Fill(dt);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
                dt.Columns.Add("Content");
                //DataRow d = null;
                foreach (DataRow d in dt.Rows)
                {
                    string imageName = d["FileName"].ToString();
                    d["Content"] = "~/ShowImage.ashx?FileName=" + imageName;
                    if (Convert.ToString(d["Content"]) == "~/ShowImage.ashx?FileName=")
                    {
                        d["Content"] = "~/images/No_image_3x4.svg.png";
                    }
                }
                gvGrid.DataSource = dt;
                gvGrid.DataBind();
            }
            catch (Exception ex)
            {
            }
        }
        public static void LoadTableRepeater(Repeater gvGrid, string SpName)
        {
            try
            {
                Global.Connect();
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection(Global.Connection());
                SqlCommand com = new SqlCommand(SpName, con);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(com);
                try
                {
                    con.Open();
                    da.Fill(dt);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
                dt.Columns.Add("Content");
                //DataRow d = null;
                foreach (DataRow d in dt.Rows)
                {
                    string imageName = d["FileName"].ToString();
                    d["Content"] = "~/ShowImage.ashx?FileName=" + imageName;
                    if (Convert.ToString(d["Content"]) == "~/ShowImage.ashx?FileName=")
                    {
                        d["Content"] = "~/images/No_image_3x4.svg.png";
                    }
                }
                gvGrid.DataSource = dt;
                gvGrid.DataBind();
            }
            catch (Exception ex)
            {
            }
        }
        public static void LoadTableRepeaterWithoutImage(Repeater gvGrid, string SpName, int NumbOfTop)
        {
            try
            {
                Global.Connect();
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection(Global.Connection());
                SqlCommand com = new SqlCommand(SpName, con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@TOPNUM", NumbOfTop);
                SqlDataAdapter da = new SqlDataAdapter(com);
                try
                {
                    con.Open();
                    da.Fill(dt);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }

                gvGrid.DataSource = dt;
                gvGrid.DataBind();
            }
            catch (Exception ex)
            {
            }
        }
        public static void LoadTableRepeater(Repeater gvGrid, string SpName, string PageName, string TypeName)
        {
            try
            {
                Global.Connect();
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection(Global.Connection());
                SqlCommand com = new SqlCommand(SpName, con);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(com);
                try
                {
                    con.Open();
                    da.Fill(dt);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
                dt.Columns.Add("Redirection");
                foreach (DataRow d in dt.Rows)
                {
                    d["Redirection"] = PageName + d[TypeName];
                }
                gvGrid.DataSource = dt;
                gvGrid.DataBind();
            }
            catch (Exception ex)
            {
            }
        }
        public static void LoadTableRepeater(Repeater gvGrid, Repeater rptPaging, string SpName, int NumbOfTop, string CategoryName, string PageName, string TypeName, int PageNumber)
        {
            try
            {
                Global.Connect();
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection(Global.Connection());
                SqlCommand com = new SqlCommand(SpName, con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@TOPNUM", NumbOfTop);
                com.Parameters.AddWithValue("@CategoryName", CategoryName);
                SqlDataAdapter da = new SqlDataAdapter(com);
                try
                {
                    con.Open();
                    da.Fill(dt);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
                dt.Columns.Add("Content");
                //DataRow d = null;
                dt.Columns.Add("Redirection");
                foreach (DataRow d in dt.Rows)
                {
                    string imageName = d["FileName"].ToString();
                    d["Content"] = "~/ShowImage.ashx?FileName=" + imageName;
                    if (Convert.ToString(d["Content"]) == "~/ShowImage.ashx?FileName=")
                    {
                        d["Content"] = "~/images/No_image_3x4.svg.png";
                    }
                    d["Redirection"] = PageName + d[TypeName];

                }
                //-----------------------------------Paging-----------------------------------------------------------------//
                PagedDataSource pgitems = new PagedDataSource();
                pgitems.DataSource = dt.DefaultView;
                pgitems.AllowPaging = true;
                //'''''''''''''//
                pgitems.PageSize = 9;
                pgitems.CurrentPageIndex = PageNumber;
                if (pgitems.PageCount >= 1)
                {
                    rptPaging.Visible = true;
                    ArrayList pages = new ArrayList();
                    for (int i = 0; i <= pgitems.PageCount - 1; i++)
                    {
                        pages.Add((i + 1));
                    }
                    rptPaging.DataSource = pages;
                    rptPaging.DataBind();
                }
                //-----------------------------------EO Paging--------------------------------------------------------------//

                gvGrid.DataSource = dt;
                gvGrid.DataBind();
            }
            catch (Exception ex)
            {
            }
        }
        public static DataTable getSPAC(string SpName, string prefixText)//AutoComplete Using Prefix
        {
            try
            {
                Global.Connect();
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection(Global.Connection());
                SqlCommand com = new SqlCommand(SpName, con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@prefixText", prefixText);
                SqlDataAdapter da = new SqlDataAdapter(com);
                try
                {
                    con.Open();
                    da.Fill(dt);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static void LoadTableSP(GridView gvGrid, string SpName)
        {
            try
            {
                Global.Connect();
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection(Global.Connection());
                SqlCommand com = new SqlCommand(SpName, con);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(com);
                try
                {
                    con.Open();
                    da.Fill(dt);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
                gvGrid.DataSource = dt;
                gvGrid.DataBind();
                HideID(gvGrid);
            }
            catch (Exception ex)
            {
            }
        }
        public static void LoadTable(string CommandText, GridView GridV)
        {
            try
            {
                Connect();
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand();
                sqlCommand.CommandText = CommandText;
                sqlCommand.Connection = con;
                DataTable dt = new DataTable();
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(sqlCommand);
                da.Fill(dt);
                GridV.DataSource = dt;
                GridV.DataBind();
                HideID(GridV);
            }
            catch (Exception ex)
            {
            }

        }
        public static DataTable RemoveDuplicateRows(DataTable dTable, string colName)
        {
            Hashtable hTable = new Hashtable();
            ArrayList duplicateList = new ArrayList();

            //Add list of all the unique item value to hashtable, which stores combination of key, value pair.
            //And add duplicate item value in arraylist.
            foreach (DataRow drow in dTable.Rows)
            {
                if (hTable.Contains(drow[colName]))
                {
                    duplicateList.Add(drow);
                }
                else
                {
                    hTable.Add(drow[colName], string.Empty);
                }
            }

            //Removing a list of duplicate items from datatable.
            foreach (DataRow dRow__2 in duplicateList)
            {
                dTable.Rows.Remove(dRow__2);
            }

            //Datatable which contains unique records will be return as output.
            return dTable;
        }
        public static void LoadTableWithoutDuplicate(string CommandText, GridView GridV, string ColumnName)
        {
            try
            {
                Connect();
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand();

                sqlCommand.CommandText = CommandText;
                sqlCommand.Connection = con;
                DataTable dt = new DataTable();
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(sqlCommand);
                da.Fill(dt);
                RemoveDuplicateRows(dt, ColumnName);
                GridV.DataSource = dt;
                GridV.DataBind();
                HideID(GridV);


            }
            catch (Exception ex)
            {
            }

        }
        public static void LoadTable_HideSecondROW(string CommandText, GridView GridV, bool hideLastCol = false, int count = 0)
        {
            try
            {
                Connect();
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand();

                sqlCommand.CommandText = CommandText;
                sqlCommand.Connection = con;
                DataTable dt = new DataTable();
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(sqlCommand);
                da.Fill(dt);
                count = dt.Rows.Count;
                GridV.DataSource = dt;
                GridV.DataBind();
                HideSecondID(GridV, hideLastCol);


            }
            catch (Exception ex)
            {
            }

        }
        public static void LoadTableWithoutDuplicates_HideSecondROW(string CommandText, GridView GridV, string ColumnName)
        {
            try
            {
                Connect();
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand();

                sqlCommand.CommandText = CommandText;
                sqlCommand.Connection = con;
                DataTable dt = new DataTable();
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(sqlCommand);
                da.Fill(dt);
                RemoveDuplicateRows(dt, ColumnName);
                GridV.DataSource = dt;
                GridV.DataBind();
                HideSecondID(GridV);


            }
            catch (Exception ex)
            {
            }

        }
        public static void LoadTablewithoutHide(string CommandText, GridView GridV)
        {
            try
            {
                Connect();
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand();

                sqlCommand.CommandText = CommandText;
                sqlCommand.Connection = con;
                DataTable dt = new DataTable();
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(sqlCommand);
                da.Fill(dt);
                GridV.DataSource = dt;
                GridV.DataBind();

            }
            catch (Exception ex)
            {
            }

        }
        public static void HideID(GridView GridV)
        {
            try
            {
                if (GridV.Columns.Count > 0)
                {
                    GridV.Columns[0].Visible = false;
                }
                else
                {
                    GridV.HeaderRow.Cells[0].Visible = false;
                    foreach (GridViewRow gvr_loopVariable in GridV.Rows)
                    {
                        gvr_loopVariable.Cells[0].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }
        public static void HideSecondID(GridView GridV, bool hideLastCol = false)
        {
            try
            {
                if (hideLastCol == true)
                {
                    GridV.HeaderRow.Cells[8].Visible = false;
                }
                GridV.HeaderRow.Cells[1].Visible = false;
                foreach (GridViewRow gvr in GridV.Rows)
                {
                    gvr.Cells[1].Visible = false;
                    if (hideLastCol == true)
                    {
                        gvr.Cells[8].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }
        public static void FillDropDown(DropDownList ddl, string CommandText, string CommandName, string CommandiD)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(CommandText, con);
                SqlDataReader reader = default(SqlDataReader);
                ddl.Items.Clear();
                try
                {
                    Connect();
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ListItem newItem = new ListItem();
                        newItem.Text = reader[CommandName].ToString();
                        newItem.Value = reader[CommandiD].ToString();
                        ddl.Items.Add(newItem);
                    }
                    reader.Close();
                    ddl.Items.Insert(0, new ListItem("----------Choose-----------", "NaN"));
                }
                catch (Exception err)
                {
                }
                finally
                {
                    con.Close();
                }

            }
            catch (Exception ex)
            {
            }
        }
        public static void FillDropDownProc(DropDownList ddl, string ProcedureName, string CommandName, string CommandiD)
        {
            try
            {
                Global.Connect();
                SqlCommand cmd = new SqlCommand(ProcedureName, Global.con);
                //cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                //cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                //cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = default(SqlDataReader);
                ddl.Items.Clear();
                try
                {
                    Connect();
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ListItem newItem = new ListItem();
                        newItem.Text = reader[CommandName].ToString();
                        newItem.Value = reader[CommandiD].ToString();
                        ddl.Items.Add(newItem);
                    }
                    reader.Close();
                }
                catch (Exception err)
                {
                }
                finally
                {
                    con.Close();
                }

            }
            catch (Exception ex)
            {
            }

            ddl.Items.Insert(0, new ListItem("----------------------------------------------------", "NaN"));
            ddl.SelectedIndex = 0;
        }
        public static void FillDropDownWithoutDash(DropDownList ddl, string CommandText, string CommandName, string CommandiD)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(CommandText, con);
                SqlDataReader reader = default(SqlDataReader);
                ddl.Items.Clear();
                try
                {
                    Connect();
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ListItem newItem = new ListItem();
                        newItem.Text = reader[CommandName].ToString();
                        newItem.Value = reader[CommandiD].ToString();
                        ddl.Items.Add(newItem);
                    }
                    reader.Close();
                }
                catch (Exception err)
                {
                }
                finally
                {
                    con.Close();
                }

            }
            catch (Exception ex)
            {
            }
        }
        public static DataSet GetData(string queryString)
        {

            // Retrieve the connection string stored in the Web.config file.
            // Dim connectionString As String = ConfigurationManager.ConnectionStrings("SDSS_DB").ConnectionString
            // con.openconnection()
            DataSet ds = new DataSet();


            try
            {
                // Connect to the database and run the query.
                // Dim connection As New SqlConnection(connectionString)
                dynamic adapter = null;
                //As New SqlDataAdapter(queryString, con)

                // Fill the DataSet.
                adapter.Fill(ds);


            }
            catch (Exception ex)
            {
            }

            return ds;
        }
        public static object GenerateUserName()
        {
            string allowedChars = "";
            allowedChars = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
            char[] sep = { ',' };
            string[] arr = allowedChars.Split(sep);
            string passwordString = "";
            string temp = "";
            Random rand = new Random();
            for (int i = 0; i <= Convert.ToInt32(4) - 1; i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                passwordString += temp;
            }
            return passwordString;

        }
        public static void DesignWarningColor(System.Web.UI.HtmlControls.HtmlGenericControl Div, System.Web.UI.HtmlControls.HtmlGenericControl Span, Label LabelStrong, Label LabelWeak, Page page)
        {
            try
            {
                if ((Status == null))
                {
                    // TODO: Exit Function: Warning!!! Need to return the value
                    return;
                }

                string[] strArr;
                strArr = Status.Split(',');
                LabelStrong.Text = strArr[1];
                LabelWeak.Text = strArr[2];
                for (int count = 0; (count <= (strArr.Length - 1)); count++)
                {
                    if ((strArr[0] == "Success"))
                    {
                        Div.Attributes["class"] = "alert alert-success";
                        ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "hideActionNotificationPopup", "hideActionNotificationPopup();", true);
                        // TODO: Exit Function: Warning!!! Need to return the value
                        return;
                    }
                    else if ((strArr[0] == "Info"))
                    {
                        Div.Attributes["class"] = "alert alert-info";
                        ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "hideActionNotificationPopup", "hideActionNotificationPopup();", true);
                        // TODO: Exit Function: Warning!!! Need to return the value
                        return;
                    }
                    else if ((strArr[0] == "Warning"))
                    {
                        Div.Attributes["class"] = "alert alert-warning";
                        ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "hideActionNotificationPopup", "hideActionNotificationPopup();", true);
                        // TODO: Exit Function: Warning!!! Need to return the value
                        return;
                    }
                    else if ((strArr[0] == "Danger"))
                    {
                        Div.Attributes["class"] = "alert alert-danger";
                        ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "hideActionNotificationPopup", "hideActionNotificationPopup();", true);
                        // TODO: Exit Function: Warning!!! Need to return the value
                        return;
                    }
                    else
                    {
                        // TODO: Exit Function: Warning!!! Need to return the value
                        return;
                    }

                }

            }
            catch (Exception ex)
            {
            }

        }
        public static void ClearControls(Page page)
        {
            foreach (var item in page.Controls)
            {
                if (item is TextBox)
                {
                    ((TextBox)item).Text = "";
                }
            }
            page.Session["ROWID"] = null;
        }
        public static void AdvancedSearch(Repeater gvGrid, string BookName, string BookPublisher, string BookAuthor, string GradeName, string CategroyName, string SchoolName)
        {
            try
            { 
                string sqlQuery = String.Empty;
                System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand();
                //--------------------------------------------------------------------------------BASE CASE---------------------------------------------------------------//

                if (((BookName == String.Empty)
                            && ((BookPublisher == String.Empty)
                            && ((BookAuthor == String.Empty)
                            && ((GradeName == String.Empty)
                            && ((CategroyName == String.Empty)
                            && ((SchoolName == String.Empty))))))))
                {
                    //DONOOP
                }
                //----------------------------------------------------------------------------END OF BASE CASE-------------------------------------------------------//
                else
                {
                    sqlQuery = ("SELECT        Books.Title AS BookName, Books.Author, Books.Version, Books.uPrice AS BookPrice, Files.FileName, Books.Subject,Books.Notes, Grade.Name," +
                        " Category.CategoryName, CONVERT(varchar, Books.CDate, 101) AS [Creation Date], Books.isPublished AS Published," +
                         "Files.FileName, Books.Publisher, Books.Notes, Books.isActive, Schools.Name" +
                            " FROM Books  LEFT JOIN" +
                         " Files ON Books.imgID = Files.FileID  LEFT JOIN"+
                         " Grade ON Books.GradeID = Grade.ID  LEFT JOIN" +
                         " Category ON Books.CategoryID = Category.ID  LEFT JOIN" +
                         " Schools ON Books.ID = Schools.ID WHERE");
                    //-----------------MAIN Query------------------//
                    //----------------------SubOrdinates------------------------------//
                    if (!string.IsNullOrEmpty(BookName))
                    {
                        if (sqlQuery.EndsWith("WHERE"))
                        {
                            sqlQuery = (sqlQuery + (" Books.Title LIKE N\'%" + BookName + "%\'"));
                        }
                        else
                        {
                            sqlQuery = (sqlQuery + ("OR  Books.Title LIKE N\'%" + BookName + "%\'"));
                        }

                    }
                     
                    if (!string.IsNullOrEmpty(BookPublisher))
                    {
                        if (sqlQuery.EndsWith("WHERE"))
                        {
                            sqlQuery = (sqlQuery + ("  Books.Publisher LIKE N\'%" + (BookPublisher + "%\'")));
                        }
                        else
                        {
                            sqlQuery = (sqlQuery + (" OR  Books.Publisher LIKE N\'%" + (BookPublisher + "%\'")));
                        }

                    }

                    if (!string.IsNullOrEmpty(BookAuthor))
                    {
                        if (sqlQuery.EndsWith("WHERE"))
                        {
                            sqlQuery = (sqlQuery + (" Books.Author LIKE N\'%" + (BookAuthor + "%\'")));
                        }
                        else
                        {
                            sqlQuery = (sqlQuery + (" OR Books.Author LIKE N\'%" + (BookAuthor + "%\'")));
                        }

                    }

                    if (GradeName != "----------------------------------------------------" && GradeName != string.Empty) 
                    {
                        if (sqlQuery.EndsWith("WHERE"))
                        {
                            sqlQuery = (sqlQuery + (" Grade.Name LIKE N\'%" + (GradeName + "%\'")));
                        }
                        else
                        {
                            sqlQuery = (sqlQuery + (" OR Grade.Name LIKE N\'%" + (GradeName + "%\'")));
                        }

                    }

                    if (CategroyName != "----------------------------------------------------" && CategroyName != string.Empty) 
                    {
                        if (sqlQuery.EndsWith("WHERE"))
                        {
                            sqlQuery = (sqlQuery + (" Category.CategoryName LIKE N\'%" + (CategroyName + "%\'")));
                        }
                        else
                        {
                            sqlQuery = (sqlQuery + (" OR Category.CategoryName LIKE N\'%" + (CategroyName + "%\'")));
                        }

                    }

                    if (!string.IsNullOrEmpty(SchoolName))
                    {
                        if (sqlQuery.EndsWith("WHERE"))
                        {
                            sqlQuery = (sqlQuery + (" Schools.Name LIKE N\'%" + (SchoolName + "%\'")));
                        }
                        else
                        {
                            sqlQuery = (sqlQuery + (" OR Schools.Name LIKE N\'%"+ (SchoolName + "%\'")));
                        }

                    }

                }
                if (sqlQuery.EndsWith("WHERE"))
                {
                    sqlQuery = (sqlQuery + (" Books.isActive=1"));
                }
                else { 
                sqlQuery += " AND Books.isActive=1";
                }
                sqlCommand.CommandText = sqlQuery;
                sqlCommand.Connection = con;
                Connect();
                Connection();
                DataTable dt = new DataTable(); 
                SqlDataAdapter da = new SqlDataAdapter(sqlCommand); 
                da.Fill(dt);
                dt.Columns.Add("Content");
             
                foreach (DataRow d in dt.Rows)
                {
                    string imageName = d["FileName"].ToString();
                    d["Content"] = "~/ShowImage.ashx?FileName=" + imageName;
                    if (Convert.ToString(d["Content"]) == "~/ShowImage.ashx?FileName=")
                    {
                        d["Content"] = "~/images/No_image_3x4.svg.png";
                    }
                }
                gvGrid.DataSource = dt;
                gvGrid.DataBind();

            }
            catch (Exception ex) { }
            // End If


        }
        protected void Application_Start(object sender, EventArgs e)
        {
            try
            {
                Session.Clear();
            }
            catch (Exception ex)
            {
                ex = null;
                return;
            }

        }
        protected void Session_Start(object sender, EventArgs e)
        {

        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }
        protected void Application_Error(object sender, EventArgs e)
        {

        }
        protected void Session_End(object sender, EventArgs e)
        {

        }
        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}