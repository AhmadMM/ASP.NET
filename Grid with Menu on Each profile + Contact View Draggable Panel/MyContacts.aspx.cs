using Obout.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POSWeb
{
    public partial class MyContants_aspx : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    POSWeb.Global.Connect();
                    POSWeb.Global.LoadTable("SELECT TOP 10 PersonID, PrsFNme+' '+PrsLName as FullName,  PrsType as Type, PrsFixedPhone as PhoneNumber, PrsMobilePhone as MobileNumber, PrsEmail as Email, PrsWebsite as Website, PrsFullAdress as FullAddress, Active " +
                    " FROM  Person " +
                    " WHERE SaleMan='" + Convert.ToString(Session["ID"]) + "'", grid1);

                }
                catch (Exception ex)
                {
                }
            }
        }

        protected void gvContacts_RowCreated(object sender, Obout.Grid.GridRowEventArgs e)
        {
            if ((e.Row.RowType == GridRowType.Header))
            {
                //e.Row.Cells[12].ToolTip = "You Are Bounded by (Cold,Warm,Hot)";
                //e.Row.Cells[13].ToolTip = "You Are Bounded by (Low,Medium,High)";
                //e.Row.Cells[14].ToolTip = "You Are Bounded by (NI,BL,WN,P)";
                //e.Row.Cells[15].ToolTip = "You Are Bounded by (C,M,IP,CL,CB)";
            }
        }
         

        protected void btnCompromise_Click(object sender, EventArgs e)
        {
            try
            {
                POSWeb.Global.Connect(); 
                string ChosenID = Request.Form["GridID"];
                SqlCommand sqlCommand = new SqlCommand("_getEmployeeInfo", POSWeb.Global.con);
                sqlCommand.Parameters.AddWithValue("@ClientID", ChosenID);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //------------------------------------------------
                SqlDataReader dr = sqlCommand.ExecuteReader();
                while (dr.Read())
                {
                    txtFN.Text =dr["PrsFNme"].ToString();
                    txtLN.Text = dr["PrsLName"].ToString();
                    txtAddress.Text = dr["PrsFullAdress"].ToString();
                    txtEmail.Text = dr["PrsEmail"].ToString();
                    txtMN.Text = dr["PrsMobilePhone"].ToString();
                    txtPN.Text = dr["PrsFixedPhone"].ToString();
                    txtWebsite.Text = dr["PrsWebsite"].ToString();  
                }
                mydiv.Visible = true;

            }
            catch (Exception ex)
            { }
        }
    }
}