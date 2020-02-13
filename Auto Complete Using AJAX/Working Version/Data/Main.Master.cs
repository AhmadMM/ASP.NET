using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BCCBookStore
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblCopyRightYear.Text = Convert.ToString(DateTime.Today.Year);
            // GETTopCategory
            try
            {
                if (!IsPostBack) { 
                Global.LoadTableRepeater(dsFillCategory, "GETTopCategory", "~/Books-From-Categories_id=main_cd=13012018?CategoryName=", "CategoryName");
                Global.FillDropDownProc(ddlCategory, "GETCategory", "CategoryName", "ID");
                Global.FillDropDownProc(ddlClass, "ClassRetrieval", "Name", "ID");
}

            }
            catch (Exception ex) { }
        }

        protected void btnAdvancedSearch_ServerClick(object sender, EventArgs e)
        {
            try
            {
                Session["BookName"] = txtBookName.Text;
                Session["BookPublisher"] = txtPublisherName.Text;
                Session["BookAuthor"] = txtAuthor.Text;
                Session["SchoolName"] =txtSchoolName.Text;
                Session["GradeName"] = ddlClass.SelectedItem.Text;
                Session["CategroyName"] = ddlCategory.SelectedItem.Text;
                Response.Redirect("Book-Details_id=main_cd=16012018");

            }
            catch (Exception ex) { }
        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            try
            {
                Session["BookName"] = txtBooksNames.Text;
                Response.Redirect("Book-Details_id=main_cd=16012018");
            }
            catch (Exception ex) { }
        }

    }
}