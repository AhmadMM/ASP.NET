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
    public partial class frmOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    FirstGridViewRow();
                    CompromisePrivileges();
                    Global.FillDropDown(ddlRelatedDoc, "SELECT [DocID],[Description] from [Document]", "Description", "DocID");
                    Global.MoneyCurrency = "LBP";
                    Session["Currency"] = "LBP";
                }
            }
            catch (Exception ex)
            { 
                //_____________________________________________________________________________________// 
                //Failure Section//
                POSWeb.Global.Status = "Warning,Warning,Page Isn't Loaded In A Right Way";
                POSWeb.Global.DesignWarningColor(DivStatus, StatusType, lblStrongStatus, lblStatus, this); 
                return;
                //EO Failure Section//   
            }
        }
        protected void txtItemCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbPayment.SelectedIndex > -1)
                {

                    int RowIndex = ((GridViewRow)((TextBox)sender).NamingContainer).RowIndex;
                    TextBox txtItemCode = (TextBox)sender;
                    SqlCommand sqlCommand = new SqlCommand("_getItems", Global.con);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Input", txtItemCode.Text);
                    SqlDataReader Reader;
                    Global.Connect();
                    Reader = sqlCommand.ExecuteReader();
                    while (Reader.Read())
                    {
                        string ItemUcost = Reader["ItemUcost"].ToString();

                        TextBox txtDescription = (TextBox)gvOrders.Rows[RowIndex].Cells[3].FindControl("txtDescription");
                        TextBox txtCost = (TextBox)gvOrders.Rows[RowIndex].Cells[5].FindControl("txtCost");

                        TextBox txtPrice = (TextBox)gvOrders.Rows[RowIndex].Cells[6].FindControl("txtPrice");

                        TextBox txtVAT = (TextBox)gvOrders.Rows[RowIndex].Cells[8].FindControl("txtVAT");

                        TextBox txtQuantity = (TextBox)gvOrders.Rows[RowIndex].Cells[8].FindControl("txtQuantity");

                        Label lblTotalCurrency = (Label)gvOrders.FooterRow.FindControl("lblTotalCurrency"); 

                        Panel pnlMoney = (Panel)gvOrders.FooterRow.FindControl("pnlMoney");

                        txtDescription.Text = Reader["ItemEnDescription"].ToString();
                        txtItemCode.Text = Reader["ItemBarCode"].ToString();
                        txtVAT.Text = Convert.ToString(Reader["Vat"]);
                        txtCost.Text = Reader["ItemUcost"].ToString();
                        txtPrice.Text = Reader["ItemUprc"].ToString();
                        txtQuantity.Text = "1"; 
                        lblTotalCurrency.Text = Convert.ToString(Session["Currency"]);
                        pnlMoney.Visible = true;
                        if (Convert.ToString(Session["Currency"]) == "LBP")
                        {
                            txtPrice.Text = Convert.ToString(Convert.ToDouble(txtPrice.Text) * Convert.ToDouble(txtLBPRate.Text));
                            txtCost.Text = Convert.ToString(Convert.ToDouble(txtCost.Text) * Convert.ToDouble(txtLBPRate.Text));
                        }
                    }

                    CheckBox cbFree = (CheckBox)gvOrders.Rows[RowIndex].Cells[1].FindControl("cbFree");
                    GridViewRow ROW = ((GridViewRow)((TextBox)sender).NamingContainer);
                    //TextBox txtQuantity = (TextBox)sender;
                    if (cbFree.Checked)
                    {
                        TextBox txtTPrice = (TextBox)gvOrders.Rows[RowIndex].Cells[9].FindControl("txtTPrice");
                        txtTPrice.Text = "0";
                    }
                    else
                    {
                        TextBox txtTPrice = (TextBox)gvOrders.Rows[RowIndex].Cells[9].FindControl("txtTPrice");
                        txtTPrice.Text = Convert.ToString(Global.ReturnTotalPrice(gvOrders, ROW));
                    }

                    Label lblTotal = (Label)gvOrders.FooterRow.FindControl("lblTotal");
                    lblTotal.Text = Convert.ToString(Global.ReturnINVSubTotal(gvOrders));
                    txtINVTotalQuantity.Text = Convert.ToString(Global.ReturnINVTotalQuantity(gvOrders));
                    if (Convert.ToString(Session["Currency"]) == "LBP")
                    {
                        txtINVSubTotal_LBP.Text = Convert.ToString(Global.ReturnINVSubTotal(gvOrders));
                        //-----------------------------------------------------------------------------------------//
                        txtINVSubTotal_Dollar.Text = Convert.ToString(Global.ManuelRounding(Convert.ToDouble(txtINVSubTotal_LBP.Text) / Convert.ToDouble(txtLBPRate.Text), 2));
                    }
                    else if (Convert.ToString(Session["Currency"]) == "USD")
                    {
                        txtINVSubTotal_Dollar.Text = Convert.ToString(Global.ReturnINVSubTotal(gvOrders));
                        //-----------------------------------------------------------------------------------------//
                        txtINVSubTotal_LBP.Text = Convert.ToString(Convert.ToDouble(txtINVSubTotal_LBP.Text) * Convert.ToDouble(txtLBPRate.Text));
                    }
                }
                else
                {
                    //Stop Operation to Choose a currency 
                    //_____________________________________________________________________________________// 
                    //Failure Section//
                    POSWeb.Global.Status = "Warning,Warning,Kindly Select A Currency Before Any Operation";
                    POSWeb.Global.DesignWarningColor(DivStatus, StatusType, lblStrongStatus, lblStatus, this);
                    rbPayment.CssClass = "ValidateThis";
                    ((TextBox)sender).Text = string.Empty;
                    rbPayment.Focus();
                    return;
                    //EO Failure Section//   
                }
            }
            catch (Exception ex) {
                //_____________________________________________________________________________________// 
                //Failure Section//
                POSWeb.Global.Status = "Warning,Warning,Couldn't Bind Item";
                POSWeb.Global.DesignWarningColor(DivStatus, StatusType, lblStrongStatus, lblStatus, this);
                return;
                //EO Failure Section//   
            }
        }

        protected void txtDescription_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbPayment.SelectedIndex > -1)
                {
                    int RowIndex = ((GridViewRow)((TextBox)sender).NamingContainer).RowIndex;
                    TextBox txtDescription = (TextBox)sender;
                    SqlCommand sqlCommand = new SqlCommand("_getItems", Global.con);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Input", txtDescription.Text);
                    SqlDataReader Reader;
                    Global.Connect();
                    Reader = sqlCommand.ExecuteReader();
                    while (Reader.Read())
                    {

                        TextBox txtItemCode = (TextBox)gvOrders.Rows[RowIndex].Cells[2].FindControl("txtItemCode");

                        TextBox txtCost = (TextBox)gvOrders.Rows[RowIndex].Cells[5].FindControl("txtCost");

                        TextBox txtPrice = (TextBox)gvOrders.Rows[RowIndex].Cells[6].FindControl("txtPrice");

                        TextBox txtVAT = (TextBox)gvOrders.Rows[RowIndex].Cells[8].FindControl("txtVAT");

                        txtItemCode.Text = Reader["ItemBarCode"].ToString();
                        txtVAT.Text = Convert.ToString(Reader["Vat"]);
                        txtCost.Text = Reader["ItemUcost"].ToString();
                        if (Convert.ToString(Session["Currency"]) == "LBP")
                        {
                            txtPrice.Text = Convert.ToString(Convert.ToDouble(txtPrice.Text) * Convert.ToDouble(txtLBPRate.Text));
                            txtCost.Text = Convert.ToString(Convert.ToDouble(txtCost.Text) * Convert.ToDouble(txtLBPRate.Text));
                        }
                    }
                    GridViewRow ROW = ((GridViewRow)((TextBox)sender).NamingContainer);
                    //TextBox txtQuantity = (TextBox)sender;
                    CheckBox cbFree = (CheckBox)gvOrders.Rows[RowIndex].Cells[1].FindControl("cbFree");

                    if (cbFree.Checked)
                    {
                        TextBox txtTPrice = (TextBox)gvOrders.Rows[RowIndex].Cells[9].FindControl("txtTPrice");
                        txtTPrice.Text = "0";
                    }
                    else
                    {
                        TextBox txtTPrice = (TextBox)gvOrders.Rows[RowIndex].Cells[9].FindControl("txtTPrice");
                        txtTPrice.Text = Convert.ToString(Global.ReturnTotalPrice(gvOrders, ROW));
                    }
                    Label lblTotal = (Label)gvOrders.FooterRow.FindControl("lblTotal");
                    lblTotal.Text = Convert.ToString(Global.ReturnINVSubTotal(gvOrders));
                    txtINVTotalQuantity.Text = Convert.ToString(Global.ReturnINVTotalQuantity(gvOrders));
                    if (Convert.ToString(Session["Currency"]) == "LBP")
                    {
                        txtINVSubTotal_LBP.Text = Convert.ToString(Global.ReturnINVSubTotal(gvOrders));
                        //-----------------------------------------------------------------------------------------//
                        txtINVSubTotal_Dollar.Text = Convert.ToString(Global.ManuelRounding(Convert.ToDouble(txtINVSubTotal_LBP.Text) / Convert.ToDouble(txtLBPRate.Text), 2));
                    }
                    else if (Convert.ToString(Session["Currency"]) == "USD")
                    {
                        txtINVSubTotal_Dollar.Text = Convert.ToString(Global.ReturnINVSubTotal(gvOrders));
                        //-----------------------------------------------------------------------------------------//
                        txtINVSubTotal_LBP.Text = Convert.ToString(Convert.ToDouble(txtINVSubTotal_LBP.Text) * Convert.ToDouble(txtLBPRate.Text));
                    }
                }
                else
                {
                    //Stop Operation to Choose a currency 
                    //_____________________________________________________________________________________// 
                    //Failure Section//
                    POSWeb.Global.Status = "Warning,Warning,Kindly Select A Currency Before Any Operation";
                    POSWeb.Global.DesignWarningColor(DivStatus, StatusType, lblStrongStatus, lblStatus, this);
                    rbPayment.CssClass = "ValidateThis";
                    rbPayment.Focus();
                    ((TextBox)sender).Text = string.Empty;
                    return;
                    //EO Failure Section//  
                }
            }
            catch (Exception ex) {
                //_____________________________________________________________________________________// 
                //Failure Section//
                POSWeb.Global.Status = "Warning,Warning,Couldn't Bind Item";
                POSWeb.Global.DesignWarningColor(DivStatus, StatusType, lblStrongStatus, lblStatus, this);
                return;
                //EO Failure Section//   
            }

        }

        protected void btnUpdateMyProfile_ServerClick(object sender, EventArgs e)
        {

        }

        protected void btnIsSaleLastPrice_ServerClick(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex) { }
        }

        protected void gvOrders_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Obout.ComboBox.ComboBox ComboBDescriptionf = e.Row.Cells[3].FindControl("ComboBDescriptionf") as Obout.ComboBox.ComboBox;
                    Global.FillDropDown(ComboBDescriptionf, "SELECT TOP 40 ItemEnDescription, ItemId FROM Items", "ItemEnDescription", "ItemId");

                    DropDownList dd = e.Row.Cells[3].FindControl("ddlDesc") as DropDownList;
                    Global.FillDropDown(dd, "SELECT ItemEnDescription, ItemId FROM Items", "ItemEnDescription", "ItemId");

                }
            }
            catch (Exception ex)
            {

            }

        }

        protected void ComboBDescriptionf_DataBinding(object sender, EventArgs e)
        {
            try
            {
                int RowIndex = ((GridViewRow)((Obout.ComboBox.ComboBox)sender).NamingContainer).RowIndex;
                Obout.ComboBox.ComboBox ComboBDescriptionf = (Obout.ComboBox.ComboBox)sender;
                Global.FillDropDown(ComboBDescriptionf, "SELECT TOP 40 ItemEnDescription, ItemId FROM Items", "ItemEnDescription", "ItemId");

            }
            catch (Exception ex)
            { }
        }

        protected void btnClear_ServerClick(object sender, EventArgs e)
        {
            try
            {
                FirstGridViewRow();
            }
            catch (Exception ex) { 
              //_____________________________________________________________________________________// 
                //Failure Section//
                POSWeb.Global.Status = "Warning,Warning,Couldn't Clear Items";
                POSWeb.Global.DesignWarningColor(DivStatus, StatusType, lblStrongStatus, lblStatus, this);
                return;
                //EO Failure Section//   
            }
        } 
        protected void rbPayment_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbPayment.SelectedValue == " USD")
                {
                    Session["Currency"] = "USD";
                    Global.MoneyCurrency = "USD";
                    POSWeb.Global.Status = "";
                    POSWeb.Global.DesignWarningColor(DivStatus, StatusType, lblStrongStatus, lblStatus, this);
                    rbPayment.CssClass = "ValidateThis"; rbPayment.CssClass = "ValidateThisNone";
                   // divRate.Visible = false;
                    FirstGridViewRow();
                    foreach (DataControlField col in gvOrders.Columns)
                    {
                        if (col.HeaderText == "Cost" || col.HeaderText == "Cost(LBP)")
                        {
                            col.HeaderText = "Cost(" + Convert.ToString(Session["Currency"]) + ")";

                        }
                        if (col.HeaderText == "Price" || col.HeaderText == "Price(LBP)")
                        {
                            col.HeaderText = "Price(" + Convert.ToString(Session["Currency"]) + ")";
                        }
                    }
                }
                else if (rbPayment.SelectedValue == " LBP")
                {
                   // divRate.Visible = true;
                    Session["Currency"] = "LBP";
                    Global.MoneyCurrency = "LBP";
                    POSWeb.Global.Status = "";
                    POSWeb.Global.DesignWarningColor(DivStatus, StatusType, lblStrongStatus, lblStatus, this);
                    rbPayment.CssClass = "ValidateThisNone";
                    FirstGridViewRow();
                    foreach (DataControlField col in gvOrders.Columns)
                    {
                        if (col.HeaderText == "Cost" || col.HeaderText == "Cost(USD)")
                        {
                            col.HeaderText = "Cost(" + Convert.ToString(Session["Currency"]) + ")";
                        }
                        if (col.HeaderText == "Price" || col.HeaderText == "Price(USD)")
                        {
                            col.HeaderText = "Price(" + Convert.ToString(Session["Currency"]) + ")";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                //_____________________________________________________________________________________// 
                //Failure Section//
                rbPayment.SelectedIndex = -1;
                POSWeb.Global.Status = "Warning,Warning,Operation Failed";
                POSWeb.Global.DesignWarningColor(DivStatus, StatusType, lblStrongStatus, lblStatus, this);
                return;
                //EO Failure Section//   
            }
        }

        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int RowIndex = ((GridViewRow)((TextBox)sender).NamingContainer).RowIndex;
                GridViewRow ROW = ((GridViewRow)((TextBox)sender).NamingContainer);
                //TextBox txtQuantity = (TextBox)sender;
                CheckBox cbFree = (CheckBox)gvOrders.Rows[RowIndex].Cells[1].FindControl("cbFree");
                if (cbFree.Checked)
                {
                    TextBox txtTPrice = (TextBox)gvOrders.Rows[RowIndex].Cells[9].FindControl("txtTPrice");
                    txtTPrice.Text = "0";
                }
                else
                {
                    TextBox txtTPrice = (TextBox)gvOrders.Rows[RowIndex].Cells[9].FindControl("txtTPrice");
                    txtTPrice.Text = Convert.ToString(Global.ReturnTotalPrice(gvOrders, ROW));
                }

                Label lblTotal = (Label)gvOrders.FooterRow.FindControl("lblTotal");
                lblTotal.Text = Convert.ToString(Global.ReturnINVSubTotal(gvOrders));
                txtINVTotalQuantity.Text = Convert.ToString(Global.ReturnINVTotalQuantity(gvOrders));
                if (Convert.ToString(Session["Currency"]) == "LBP")
                {
                    txtINVSubTotal_LBP.Text = Convert.ToString(Global.ReturnINVSubTotal(gvOrders));
                    //-----------------------------------------------------------------------------------------//
                    txtINVSubTotal_Dollar.Text = Convert.ToString(Global.ManuelRounding(Convert.ToDouble(txtINVSubTotal_LBP.Text) / Convert.ToDouble(txtLBPRate.Text), 2));
                }
                else if (Convert.ToString(Session["Currency"]) == "USD")
                {
                    txtINVSubTotal_Dollar.Text = Convert.ToString(Global.ReturnINVSubTotal(gvOrders));
                    //-----------------------------------------------------------------------------------------//
                    txtINVSubTotal_LBP.Text = Convert.ToString(Convert.ToDouble(txtINVSubTotal_LBP.Text) * Convert.ToDouble(txtLBPRate.Text));
                }

            }
            catch (Exception ex) { 
              //_____________________________________________________________________________________// 
                //Failure Section//
                POSWeb.Global.Status = "Warning,Warning,Couldn't Increase The Quantity Of The Item";
                POSWeb.Global.DesignWarningColor(DivStatus, StatusType, lblStrongStatus, lblStatus, this);
                return;
                //EO Failure Section//   
            }
        }

        protected void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int RowIndex = ((GridViewRow)((TextBox)sender).NamingContainer).RowIndex;
                GridViewRow ROW = ((GridViewRow)((TextBox)sender).NamingContainer);

                TextBox txtTPrice = (TextBox)gvOrders.Rows[RowIndex].Cells[9].FindControl("txtTPrice");
                txtTPrice.Text = Convert.ToString(Global.ReturnTotalPrice(gvOrders, ROW));

                TextBox txtDiscountAmount = (TextBox)gvOrders.Rows[RowIndex].Cells[8].FindControl("txtDiscountAmount");
                txtDiscountAmount.Text = Convert.ToString(Global.ReturnDiscountAmount(Convert.ToDouble(((TextBox)gvOrders.Rows[RowIndex].Cells[5].FindControl("txtPrice")).Text), Convert.ToDouble(((TextBox)gvOrders.Rows[RowIndex].Cells[3].FindControl("txtQuantity")).Text), Convert.ToDouble(((TextBox)gvOrders.Rows[RowIndex].Cells[7].FindControl("txtDiscount")).Text)));
      
                Label lblTotal = (Label)gvOrders.FooterRow.FindControl("lblTotal");
                lblTotal.Text = Convert.ToString(Global.ReturnINVSubTotal(gvOrders));

                txtINVTotalQuantity.Text = Convert.ToString(Global.ReturnINVTotalQuantity(gvOrders)); 
                if (Convert.ToString(Session["Currency"]) == "LBP")
                {
                    txtINVSubTotal_LBP.Text = Convert.ToString(Global.ReturnINVSubTotal(gvOrders));
                    //-----------------------------------------------------------------------------------------//
                    txtINVSubTotal_Dollar.Text = Convert.ToString(Global.ManuelRounding(Convert.ToDouble(txtINVSubTotal_LBP.Text) / Convert.ToDouble(txtLBPRate.Text), 2));
                }
                else if (Convert.ToString(Session["Currency"]) == "USD")
                {
                    txtINVSubTotal_Dollar.Text = Convert.ToString(Global.ReturnINVSubTotal(gvOrders));
                    //-----------------------------------------------------------------------------------------//
                    txtINVSubTotal_LBP.Text = Convert.ToString(Convert.ToDouble(txtINVSubTotal_LBP.Text) * Convert.ToDouble(txtLBPRate.Text));
                }
            }
            catch (Exception ex) {
              //_____________________________________________________________________________________// 
                //Failure Section//
                POSWeb.Global.Status = "Warning,Warning,Couldn't Apply Discount To Item";
                POSWeb.Global.DesignWarningColor(DivStatus, StatusType, lblStrongStatus, lblStatus, this);
                return;
                //EO Failure Section//   
            }
        }

        protected void txtDiscountAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int RowIndex = ((GridViewRow)((TextBox)sender).NamingContainer).RowIndex;
                GridViewRow ROW = ((GridViewRow)((TextBox)sender).NamingContainer);

                TextBox txtTPrice = (TextBox)gvOrders.Rows[RowIndex].Cells[9].FindControl("txtTPrice");
                txtTPrice.Text = Convert.ToString(Global.ReturnTotalPrice(gvOrders, ROW));

                TextBox txtDiscountAmount = (TextBox)gvOrders.Rows[RowIndex].Cells[7].FindControl("txtDiscount");
                txtDiscountAmount.Text = Convert.ToString(Global.ReturnDiscountPercentage(Convert.ToDouble(((TextBox)gvOrders.Rows[RowIndex].Cells[8].FindControl("txtDiscountAmount")).Text), Convert.ToDouble(((TextBox)gvOrders.Rows[RowIndex].Cells[3].FindControl("txtQuantity")).Text), Convert.ToDouble(((TextBox)gvOrders.Rows[RowIndex].Cells[5].FindControl("txtPrice")).Text)));
                if (Convert.ToString(Session["Currency"]) == "LBP")
                {
                    txtTPrice.Text = Convert.ToString(Convert.ToDouble(txtTPrice.Text) * Convert.ToDouble(txtLBPRate.Text));
                }
                Label lblTotal = (Label)gvOrders.FooterRow.FindControl("lblTotal");
                lblTotal.Text = Convert.ToString(Global.ReturnINVSubTotal(gvOrders));
                txtINVTotalQuantity.Text = Convert.ToString(Global.ReturnINVTotalQuantity(gvOrders));
                txtINVTotalQuantity.Text = Convert.ToString(Global.ReturnINVTotalQuantity(gvOrders));
                if (Convert.ToString(Session["Currency"]) == "LBP")
                {
                    txtINVSubTotal_LBP.Text = Convert.ToString(Global.ReturnINVSubTotal(gvOrders));
                    //-----------------------------------------------------------------------------------------//
                    txtINVSubTotal_Dollar.Text = Convert.ToString(Global.ManuelRounding(Convert.ToDouble(txtINVSubTotal_LBP.Text) / Convert.ToDouble(txtLBPRate.Text), 2));
                }
                else if (Convert.ToString(Session["Currency"]) == "USD")
                {
                    txtINVSubTotal_Dollar.Text = Convert.ToString(Global.ReturnINVSubTotal(gvOrders));
                    //-----------------------------------------------------------------------------------------//
                    txtINVSubTotal_LBP.Text = Convert.ToString(Convert.ToDouble(txtINVSubTotal_LBP.Text) * Convert.ToDouble(txtLBPRate.Text));
                }
            }
            catch (Exception ex) { 
              //_____________________________________________________________________________________// 
                //Failure Section//
                POSWeb.Global.Status = "Warning,Warning,Couldn't Apply Discount Amount On Item";
                POSWeb.Global.DesignWarningColor(DivStatus, StatusType, lblStrongStatus, lblStatus, this);
                return;
                //EO Failure Section//   
            }
        }
        protected void cbFree_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int RowIndex = ((GridViewRow)((CheckBox)sender).NamingContainer).RowIndex;
                GridViewRow ROW = ((GridViewRow)((CheckBox)sender).NamingContainer);
                //TextBox txtQuantity = (TextBox)sender;
                CheckBox cbFree = (CheckBox)gvOrders.Rows[RowIndex].Cells[1].FindControl("cbFree");
                if (cbFree.Checked)
                {
                    TextBox txtTPrice = (TextBox)gvOrders.Rows[RowIndex].Cells[9].FindControl("txtTPrice");
                    txtTPrice.Text = "0";
                }
                else
                {
                    TextBox txtTPrice = (TextBox)gvOrders.Rows[RowIndex].Cells[9].FindControl("txtTPrice");
                    txtTPrice.Text = Convert.ToString(Global.ReturnTotalPrice(gvOrders, ROW));
                }

                Label lblTotal = (Label)gvOrders.FooterRow.FindControl("lblTotal");
                lblTotal.Text = Convert.ToString(Global.ReturnINVSubTotal(gvOrders));

                txtINVTotalQuantity.Text = Convert.ToString(Global.ReturnINVTotalQuantity(gvOrders));
                if (Convert.ToString(Session["Currency"]) == "LBP")
                {
                    txtINVSubTotal_LBP.Text = Convert.ToString(Global.ReturnINVSubTotal(gvOrders));
                    //-----------------------------------------------------------------------------------------//
                    txtINVSubTotal_Dollar.Text = Convert.ToString(Global.ManuelRounding(Convert.ToDouble(txtINVSubTotal_LBP.Text) / Convert.ToDouble(txtLBPRate.Text), 2));
                }
                else if (Convert.ToString(Session["Currency"]) == "USD")
                {
                    txtINVSubTotal_Dollar.Text =    Convert.ToString(Global.ReturnINVSubTotal(gvOrders));
                    //-----------------------------------------------------------------------------------------//
                    txtINVSubTotal_LBP.Text = Convert.ToString(Convert.ToDouble(txtINVSubTotal_LBP.Text) * Convert.ToDouble(txtLBPRate.Text));
                }

            }
            catch (Exception ex) {  
                //_____________________________________________________________________________________// 
                //Failure Section//
                POSWeb.Global.Status = "Warning,Warning,Couldn't Make Item Free";
                POSWeb.Global.DesignWarningColor(DivStatus, StatusType, lblStrongStatus, lblStatus, this);
                return;
                //EO Failure Section//   
            }
        }



        protected void txtINVDiscountPER_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtINVDiscount_LBP.Text = Convert.ToString(Global.ReturnINVDiscount(Convert.ToDouble(txtINVDiscountPER.Text), Convert.ToDouble(txtINVSubTotal_LBP.Text)));
                txtINVDiscount_Dollar.Text = Convert.ToString(Global.ManuelRounding(Convert.ToDouble(txtINVDiscount_LBP.Text) / Convert.ToDouble(txtLBPRate.Text),2));
                //,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,//
                txtINVTotalBeforeVAT_LBP.Text=Convert.ToString(Global.ReturnTotalB4VAT(Convert.ToDouble(txtINVDiscount_LBP.Text),Convert.ToDouble(txtINVSubTotal_LBP.Text)));
                txtINVTotalBeforeVAT_Dollar.Text=Convert.ToString(Global.ManuelRounding(Global.ReturnTotalB4VAT(Convert.ToDouble(txtINVDiscount_Dollar.Text),Convert.ToDouble(txtINVSubTotal_Dollar.Text)),2));
                //,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,//

                txtINVAT_Dollar.Text = Convert.ToString(Global.ManuelRounding(Global.ReturnINVVAT(gvOrders, Convert.ToDouble(txtINVDiscountPER.Text)) / Convert.ToDouble(txtLBPRate.Text),2));
                txtINVAT_LBP.Text = Convert.ToString(Global.ReturnINVVAT(gvOrders, Convert.ToDouble(txtINVDiscountPER.Text)));
            }
            catch (Exception ex)
            {
                //_____________________________________________________________________________________// 
                //Failure Section//
                POSWeb.Global.Status = "Warning,Warning,Couldn't Apply Discount To Receipt";
                POSWeb.Global.DesignWarningColor(DivStatus, StatusType, lblStrongStatus, lblStatus, this);
                return;
                //EO Failure Section//   
            }
        }



        protected void btnPurchase_ServerClick(object sender, EventArgs e)
        {
            try
            {
           //____________________________________General Sales___________________________________________________________________//

                if (txtDateOfSale.Text==string.Empty){

                    txtINVSubTotal_LBP.CssClass = "ValidateThisNone";
                    txtINVSubTotal_Dollar.CssClass = "ValidateThisNone";
                    txtINVTotalQuantity.CssClass = "ValidateThisNone";
                    txtCustomerName.CssClass = "ValidateThisNone";
                    txtDateOfSale.CssClass = "ValidateThisNone";
                    txtDateOfSale.CssClass = "ValidateThis";

                    return;
                }

                else if (txtCustomerName.Text == string.Empty)
                {
                    txtINVSubTotal_LBP.CssClass = "ValidateThisNone";
                    txtINVSubTotal_Dollar.CssClass = "ValidateThisNone";
                    txtINVTotalQuantity.CssClass = "ValidateThisNone";
                    txtDateOfSale.CssClass = "ValidateThisNone";
                    ddlPaymentType.CssClass = "ValidateThisNone";
                    txtCustomerName.CssClass = "ValidateThis";

                    return;
                }
                else if (ddlPaymentType.Text == string.Empty)
                {

                    txtINVSubTotal_LBP.CssClass = "ValidateThisNone";
                    txtINVSubTotal_Dollar.CssClass = "ValidateThisNone";
                    txtINVTotalQuantity.CssClass = "ValidateThisNone";
                    txtDateOfSale.CssClass = "ValidateThisNone";
                    txtCustomerName.CssClass = "ValidateThisNone";
                    ddlPaymentType.CssClass = "ValidateThis";

                    return;
                }
                else if (ddlPaymentType.Text == string.Empty)
                {

                    txtINVSubTotal_LBP.CssClass = "ValidateThisNone";
                    txtINVSubTotal_Dollar.CssClass = "ValidateThisNone";
                    txtINVTotalQuantity.CssClass = "ValidateThisNone";
                    txtDateOfSale.CssClass = "ValidateThisNone";
                    txtCustomerName.CssClass = "ValidateThisNone";
                    ddlPaymentType.CssClass = "ValidateThis";

                    return;
                }
                else if (txtINVTotalQuantity.Text == string.Empty)
                {
                    txtINVSubTotal_LBP.CssClass = "ValidateThisNone";
                    txtINVSubTotal_Dollar.CssClass = "ValidateThisNone";
                    txtDateOfSale.CssClass = "ValidateThisNone";
                    txtCustomerName.CssClass = "ValidateThisNone";
                    ddlPaymentType.CssClass = "ValidateThisNone"; 
                    txtINVTotalQuantity.CssClass = "ValidateThis";

                    return;
                }
                else if (txtINVSubTotal_LBP.Text == string.Empty || txtINVSubTotal_Dollar.Text == string.Empty)
                {
                    txtINVTotalQuantity.CssClass = "ValidateThisNone";
                    txtDateOfSale.CssClass = "ValidateThisNone";
                    txtCustomerName.CssClass = "ValidateThisNone";
                    ddlPaymentType.CssClass = "ValidateThisNone";
                    txtINVSubTotal_LBP.CssClass = "ValidateThis";
                    txtINVSubTotal_Dollar.CssClass = "ValidateThis";

                    return;
                }
                else{
                    txtINVTotalQuantity.CssClass = "ValidateThisNone";
                    txtDateOfSale.CssClass = "ValidateThisNone";
                    txtCustomerName.CssClass = "ValidateThisNone";
                    ddlPaymentType.CssClass = "ValidateThisNone";
                    txtINVSubTotal_LBP.CssClass = "ValidateThisNone";
                    txtINVSubTotal_Dollar.CssClass = "ValidateThisNone";
                    //___________________________________EVERYTHING_IS_VALID________________________________________// 
                    SqlCommand sqlCommand = new SqlCommand("_PurchaseOrderInsertion", Global.con);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("InvoiceNum", Convert.ToString(Global.ReturnSOINTNO("String"))); //
                    sqlCommand.Parameters.AddWithValue("InvoiceDate", Convert.ToDateTime(txtDateOfSale.Text));
                    sqlCommand.Parameters.AddWithValue("CustomerId",Global.getCustomerIDfromName(txtCustomerName.Text)); 
                    sqlCommand.Parameters.AddWithValue("TotalUPrc", txtINVSubTotal_Dollar.Text); 
                    sqlCommand.Parameters.AddWithValue("TotalLPrc", txtINVSubTotal_LBP.Text);
                    sqlCommand.Parameters.AddWithValue("Type", "Sales Order"); 
                    sqlCommand.Parameters.AddWithValue("PyamentType", ddlPaymentType.SelectedValue); 
                    sqlCommand.Parameters.AddWithValue("FullDate", Convert.ToString(DateTime.Now)); 
                    sqlCommand.Parameters.AddWithValue("Status", "Active"); 
                    sqlCommand.Parameters.AddWithValue("WareID", Global.getWareHouseTopID());
                    //
                    sqlCommand.Parameters.AddWithValue("INTNO",0);
                    //
                    sqlCommand.Parameters.AddWithValue("RelatedDocNo", ddlRelatedDoc.SelectedValue);
                    sqlCommand.Parameters.AddWithValue("Comission", 0);
                    sqlCommand.Parameters.AddWithValue("DileveryDate", Convert.ToDateTime(txtDateOfSale.Text));
                    sqlCommand.Parameters.AddWithValue("DueDate", Convert.ToDateTime(txtDateOfSale.Text));
                    sqlCommand.Parameters.AddWithValue("B", 0);
                    sqlCommand.Parameters.AddWithValue("UserName", Convert.ToString(Session["ID"])); 
                    sqlCommand.Parameters.AddWithValue("Cur", rbPayment.SelectedValue); 
                    sqlCommand.Parameters.AddWithValue("QINTNO", 0);
                    //
                    sqlCommand.Parameters.AddWithValue("SOINTNO", Global.ReturnSOINTNO("Number"));
                    //
                    sqlCommand.Parameters.AddWithValue("RINTNO",0); 
                    sqlCommand.Parameters.AddWithValue("Remarks", txtComments.Text); 
                    sqlCommand.Parameters.AddWithValue("VATU", txtINVAT_Dollar.Text); 
                    sqlCommand.Parameters.AddWithValue("VATL", txtINVAT_LBP.Text); 
                    sqlCommand.Parameters.AddWithValue("Rate", txtLBPRate.Text);
                    sqlCommand.Parameters.AddWithValue("Disc", txtINVDiscountPER.Text);
                    sqlCommand.Parameters.AddWithValue("DiscU", txtINVDiscount_Dollar.Text);
                    sqlCommand.Parameters.AddWithValue("DiscL", txtINVDiscount_LBP.Text);
                    sqlCommand.Parameters.AddWithValue("SubU1", txtINVSubTotal_Dollar.Text); 
                    sqlCommand.Parameters.AddWithValue("SubL1", txtINVSubTotal_LBP.Text);
                    sqlCommand.Parameters.AddWithValue("INTNOB", 0);
                                  Global.Connect();
                 sqlCommand.ExecuteNonQuery();
                 //_____________________________________________________________________________________// 
                 //Success Section//
                 POSWeb.Global.Status = "Success,Success,Ur Purchase Was Made Successfully";
                 POSWeb.Global.DesignWarningColor(DivStatus, StatusType, lblStrongStatus, lblStatus, this);
                 ClearTextBoxes(Page);
                 return;
                    //EO Success Section//  
                } 
          //____________________________________EOGeneral Sales___________________________________________________________________//

            }
            catch (Exception ex)
            {

                //_____________________________________________________________________________________// 
                //Failure Section//
                POSWeb.Global.Status = "Warning,Warning,Couldn't Purchase Item/s";
                POSWeb.Global.DesignWarningColor(DivStatus, StatusType, lblStrongStatus, lblStatus, this);
                ClearTextBoxes(Page);
                return;
                //EO Failure Section//   
            }
        }

        protected void ClearTextBoxes(Control p1)
        {
            foreach (Control ctrl in p1.Controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox t = ctrl as TextBox;

                    if (t != null)
                    {
                        t.Text = String.Empty;
                    }
                }
                else
                {
                    if (ctrl.Controls.Count > 0)
                    {
                        ClearTextBoxes(ctrl);
                    }
                }
            }
        }
        // Filling up the page//
        //---------------------------------------------------------------------------------------------------------------------------------------------------//
        //---------------------------------------------------------------------------------------------------------------------------------------------------//
        //---------------------------------------------------------------------------------------------------------------------------------------------------//
        //--------------------------Customers ---------------------------------------------------------------------
        [System.Web.Script.Services.ScriptMethod()]

        [System.Web.Services.WebMethod]

        public static List<string> GetCustomers(string prefixText)
        {
            try
            {
                Global.Connect();
                SqlCommand sqlCommand = new SqlCommand("_GetCustomers", POSWeb.Global.con);
                sqlCommand.Parameters.AddWithValue("@Name", prefixText);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                //------------------------------------------------ 
                DataTable dt = new DataTable();

                SqlDataAdapter adp = new SqlDataAdapter(sqlCommand);

                adp.Fill(dt);

                List<string> Customers = new List<string>();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Customers.Add(dt.Rows[i][1].ToString());
                }
                return Customers;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        //----------------------------------EOCustomers--------------------------------------------------------
        //--------------------------Description ---------------------------------------------------------------------
        [System.Web.Script.Services.ScriptMethod()]

        [System.Web.Services.WebMethod]

        public static List<string> GetProductDescription(string prefixText)
        {
            try
            {
                Global.Connect();
                SqlCommand sqlCommand = new SqlCommand("_GetDescriptionAuto", POSWeb.Global.con);
                sqlCommand.Parameters.AddWithValue("@Description", prefixText);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                //------------------------------------------------ 
                DataTable dt = new DataTable();

                SqlDataAdapter adp = new SqlDataAdapter(sqlCommand);

                adp.Fill(dt);

                List<string> Description = new List<string>();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Description.Add(dt.Rows[i][0].ToString());
                }
                return Description;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        //----------------------------------EODescription--------------------------------------------------------
        //--------------------------Description ---------------------------------------------------------------------
        [System.Web.Script.Services.ScriptMethod()]

        [System.Web.Services.WebMethod]

        public static List<string> GetProductItemCode(string prefixText)
        {
            try
            {
                Global.Connect();
                SqlCommand sqlCommand = new SqlCommand("_GetItemsAuto", POSWeb.Global.con);
                sqlCommand.Parameters.AddWithValue("@Code", prefixText);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                //--------------------------------------------------------// 
                DataTable dt = new DataTable();

                SqlDataAdapter adp = new SqlDataAdapter(sqlCommand);

                adp.Fill(dt);

                List<string> Code = new List<string>();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Code.Add(dt.Rows[i][0].ToString());
                }
                return Code;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        //----------------------------------EODescription--------------------------------------------------------
        private void FirstGridViewRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Col1", typeof(Boolean)));
            dt.Columns.Add(new DataColumn("Col2", typeof(string)));
            dt.Columns.Add(new DataColumn("Col3", typeof(string)));
            dt.Columns.Add(new DataColumn("Col4", typeof(string)));
            dt.Columns.Add(new DataColumn("Col5", typeof(string)));
            dt.Columns.Add(new DataColumn("Col6", typeof(string)));
            dt.Columns.Add(new DataColumn("Col7", typeof(string)));
            dt.Columns.Add(new DataColumn("Col8", typeof(string)));
            dt.Columns.Add(new DataColumn("Col9", typeof(string)));
            dt.Columns.Add(new DataColumn("Col10", typeof(string)));
            dt.Columns.Add(new DataColumn("Col11", typeof(string)));
            dt.Columns.Add(new DataColumn("Col12", typeof(string)));
            dt.Columns.Add(new DataColumn("Col13", typeof(string)));
            dt.Columns.Add(new DataColumn("Col14", typeof(string)));
            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["Col1"] = false;
            dr["Col2"] = string.Empty;
            dr["Col3"] = string.Empty;
            dr["Col4"] = string.Empty;
            dr["Col5"] = string.Empty;
            dr["Col6"] = string.Empty;
            dr["Col7"] = string.Empty;
            dr["Col8"] = string.Empty;
            dr["Col9"] = string.Empty;
            dr["Col10"] = string.Empty;
            dr["Col11"] = string.Empty;
            dr["Col12"] = string.Empty;
            dr["Col13"] = string.Empty;
            dr["Col14"] = string.Empty;
            dt.Rows.Add(dr);

            ViewState["CurrentTable"] = dt;

            gvOrders.DataSource = dt;
            gvOrders.DataBind();

            POSWeb.Global.HideID(gvOrders);
        }

        private void CompromisePrivileges()
        {
            try
            {
                if (Convert.ToBoolean(Session["HasTVA"]) == true)
                {
                    //DoNOOP
                }
                else
                {
                    foreach (DataControlField col in gvOrders.Columns)
                    {
                        if (col.HeaderText == "VAT")
                        {
                            col.Visible = false;
                        }
                    }

                }
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
                if (Convert.ToBoolean(Session["IsPrintA4"]) == true)
                {
                    //DoNOOP
                }
                else
                {
                    cbPrintInvoice.Visible = false;
                }
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
                if (Convert.ToBoolean(Session["IsPrintReceipt"]) == true)
                {
                    //DoNOOP
                }
                else
                {
                    cbPrintReceipts.Visible = false;
                }
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
                if (Convert.ToBoolean(Session["IsSaleLastPrice"]) == true)
                {
                    //DoNOOP
                }
                else
                {
                    btnIsSaleLastPrice.Visible = false;
                }
            }
            catch (Exception ex)
            {

            }


        }

        private void AddNewRow()
        {
            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        CheckBox cbBoxName =
                          (CheckBox)gvOrders.Rows[rowIndex].Cells[1].FindControl("cbFree");
                        TextBox txtItemCode =
                          (TextBox)gvOrders.Rows[rowIndex].Cells[2].FindControl("txtItemCode");
                        TextBox txtDescription =
                          (TextBox)gvOrders.Rows[rowIndex].Cells[3].FindControl("txtDescription");
                        TextBox txtQuantity =
                          (TextBox)gvOrders.Rows[rowIndex].Cells[4].FindControl("txtQuantity");
                        TextBox txtCost =
                          (TextBox)gvOrders.Rows[rowIndex].Cells[5].FindControl("txtCost");
                        TextBox txtPrice =
                          (TextBox)gvOrders.Rows[rowIndex].Cells[6].FindControl("txtPrice");
                        TextBox txtColor =
                       (TextBox)gvOrders.Rows[rowIndex].Cells[7].FindControl("txtColor");
                        TextBox txtDiscount =
                       (TextBox)gvOrders.Rows[rowIndex].Cells[8].FindControl("txtDiscount");
                        TextBox txtDiscountAmount =
                 (TextBox)gvOrders.Rows[rowIndex].Cells[9].FindControl("txtDiscountAmount"); TextBox
                     txtTPrice =
                  (TextBox)gvOrders.Rows[rowIndex].Cells[10].FindControl("txtTPrice"); TextBox
                      txtTCost =
                  (TextBox)gvOrders.Rows[rowIndex].Cells[11].FindControl("txtTCost"); TextBox
                      txtComments =
                  (TextBox)gvOrders.Rows[rowIndex].Cells[12].FindControl("txtComments"); TextBox
                      txtExpiryDate =
                  (TextBox)gvOrders.Rows[rowIndex].Cells[13].FindControl("txtExpiryDate"); TextBox
                      txtVAT =
                  (TextBox)gvOrders.Rows[rowIndex].Cells[14].FindControl("txtVAT");
                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;

                        dtCurrentTable.Rows[i - 1]["Col1"] = cbBoxName.Checked;
                        dtCurrentTable.Rows[i - 1]["Col2"] = txtItemCode.Text;
                        dtCurrentTable.Rows[i - 1]["Col3"] = txtDescription.Text;
                        dtCurrentTable.Rows[i - 1]["Col4"] = txtQuantity.Text;
                        dtCurrentTable.Rows[i - 1]["Col5"] = txtCost.Text;
                        dtCurrentTable.Rows[i - 1]["Col6"] = txtPrice.Text;
                        dtCurrentTable.Rows[i - 1]["Col7"] = txtColor.Text;
                        dtCurrentTable.Rows[i - 1]["Col8"] = txtDiscount.Text;
                        dtCurrentTable.Rows[i - 1]["Col9"] = txtDiscountAmount.Text;
                        dtCurrentTable.Rows[i - 1]["Col10"] = txtTPrice.Text;
                        dtCurrentTable.Rows[i - 1]["Col11"] = txtTCost.Text;
                        dtCurrentTable.Rows[i - 1]["Col12"] = txtComments.Text;
                        dtCurrentTable.Rows[i - 1]["Col13"] = txtExpiryDate.Text;
                        dtCurrentTable.Rows[i - 1]["Col14"] = txtVAT.Text;
                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;

                    gvOrders.DataSource = dtCurrentTable;
                    gvOrders.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousData();
        }

        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        CheckBox cbBoxName =
                          (CheckBox)gvOrders.Rows[rowIndex].Cells[1].FindControl("cbFree");
                        TextBox txtItemCode =
                          (TextBox)gvOrders.Rows[rowIndex].Cells[2].FindControl("txtItemCode");
                        TextBox txtDescription =
                          (TextBox)gvOrders.Rows[rowIndex].Cells[3].FindControl("txtDescription");
                        TextBox txtQuantity =
                          (TextBox)gvOrders.Rows[rowIndex].Cells[4].FindControl("txtQuantity");
                        TextBox txtCost =
                          (TextBox)gvOrders.Rows[rowIndex].Cells[5].FindControl("txtCost");
                        TextBox txtPrice =
                          (TextBox)gvOrders.Rows[rowIndex].Cells[6].FindControl("txtPrice");
                        TextBox txtColor =
                       (TextBox)gvOrders.Rows[rowIndex].Cells[7].FindControl("txtColor");
                        TextBox txtDiscount =
                       (TextBox)gvOrders.Rows[rowIndex].Cells[8].FindControl("txtDiscount");
                        TextBox txtDiscountAmount =
                 (TextBox)gvOrders.Rows[rowIndex].Cells[9].FindControl("txtDiscountAmount"); TextBox
                     txtTPrice =
                  (TextBox)gvOrders.Rows[rowIndex].Cells[10].FindControl("txtTPrice"); TextBox
                      txtTCost =
                  (TextBox)gvOrders.Rows[rowIndex].Cells[11].FindControl("txtTCost"); TextBox
                      txtComments =
                  (TextBox)gvOrders.Rows[rowIndex].Cells[12].FindControl("txtComments"); TextBox
                      txtExpiryDate =
                  (TextBox)gvOrders.Rows[rowIndex].Cells[13].FindControl("txtExpiryDate"); TextBox
                      txtVAT =
                  (TextBox)gvOrders.Rows[rowIndex].Cells[14].FindControl("txtVAT");

                        cbBoxName.Checked = Convert.ToBoolean(dt.Rows[i]["Col1"]);
                        txtItemCode.Text = dt.Rows[i]["Col2"].ToString();
                        txtDescription.Text = dt.Rows[i]["Col3"].ToString();
                        txtQuantity.Text = dt.Rows[i]["Col4"].ToString();
                        txtCost.Text = dt.Rows[i]["Col5"].ToString();
                        txtPrice.Text = dt.Rows[i]["Col6"].ToString();
                        txtColor.Text = dt.Rows[i]["Col7"].ToString();
                        txtDiscount.Text = dt.Rows[i]["Col8"].ToString();
                        txtDiscountAmount.Text = dt.Rows[i]["Col9"].ToString();
                        txtTPrice.Text = dt.Rows[i]["Col10"].ToString();
                        txtTCost.Text = dt.Rows[i]["Col11"].ToString();
                        txtComments.Text = dt.Rows[i]["Col12"].ToString();
                        txtExpiryDate.Text = dt.Rows[i]["Col13"].ToString();
                        txtVAT.Text = dt.Rows[i]["Col14"].ToString();
                        rowIndex++;
                    }
                }
            }
        }
        protected void btnAddRow_ServerClick(object sender, EventArgs e)
        {
            try
            {
                AddNewRow();
            }
            catch (Exception ex) { }
        }
         
    }
}