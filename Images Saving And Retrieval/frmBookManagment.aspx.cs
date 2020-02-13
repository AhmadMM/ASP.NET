using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BCCApp
{
    public partial class frmBookManagment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Session["PageName"] = "Book Managment";
                    Global.FillDropDownProc(ddlClass, "ClassRetrieval", "Name", "ID");
                    Global.LoadTableSP(gvBooks, "GetBooks");

                }
            }
            catch (Exception ex) { }

        }

        protected void btnAdd_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (txtAuthor.Text == string.Empty || txtPrice.Text == string.Empty || txtSubject.Text == string.Empty || txtTitle.Text == string.Empty || ddlClass.SelectedIndex == 0)
                {
                    StopOperation("There is an empty field");
                    return;

                }
                SqlCommand sqlCommand = new SqlCommand("BookInsertion", Global.con);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Title", txtTitle.Text);
                sqlCommand.Parameters.AddWithValue("@Author", txtAuthor.Text);
                sqlCommand.Parameters.AddWithValue("@Version", txtVersionNumber.Text);
                //-------------------------Price Variable-------------------------//
                double PriceDollar = 0;
                double PriceLBP = 0;

                if (rbPaymentType.Text == "LBP")
                {
                    PriceDollar = Convert.ToDouble(txtPrice.Text) / 1500;
                    PriceLBP = Convert.ToDouble(txtPrice.Text);
                }
                if (rbPaymentType.Text == "USD")
                {
                    PriceDollar = Convert.ToDouble(txtPrice.Text);
                    PriceLBP = Convert.ToDouble(txtPrice.Text) * 1500;
                }
                sqlCommand.Parameters.AddWithValue("@uPrice", PriceDollar);
                sqlCommand.Parameters.AddWithValue("@LPrice", PriceLBP);
                //-------------------------Price Variable-------------------------//
                sqlCommand.Parameters.AddWithValue("@GradeID", ddlClass.SelectedValue);
                sqlCommand.Parameters.AddWithValue("@Subject", txtSubject.Text);
                if (txtDate.Text == string.Empty)
                {
                    sqlCommand.Parameters.AddWithValue("@CDate", DateTime.Now);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@CDate", Convert.ToDateTime(txtDate.Text));
                }
                //-------------------------Checkboxes----------------------------------------//
                int Active = 0;
                int Published = 0;
                if (cbActive.Checked)
                {
                    Active = 1;
                }
                if (cbPublished.Checked)
                {
                    Published = 1;
                }
                sqlCommand.Parameters.AddWithValue("@isActive", Active);
                sqlCommand.Parameters.AddWithValue("@isPublished", Published);
                //-------------------------Checkboxes----------------------------------------//
                sqlCommand.Parameters.AddWithValue("@Publisher", txtPublisher.Text);
                sqlCommand.Parameters.AddWithValue("@Notes", txtNotes.Text);
                //-----------------------------------------------IMG DATA------------------------------------------//
                if (Uploadedimages.HasFile)
                {
                    System.IO.Stream fs = Uploadedimages.PostedFile.InputStream;
                    int iFileSize = (int)fs.Length;
                    if (iFileSize < 150000)  // 100KB
                    {

                        string contentType = Uploadedimages.PostedFile.ContentType;
                        System.IO.BinaryReader binReader = new System.IO.BinaryReader(fs);
                        byte[] buffer = new byte[16 * 1024];
                        using (MemoryStream ms = new MemoryStream())
                        {
                            int read;
                            while ((read = fs.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                ms.Write(buffer, 0, read);
                            }
                            sqlCommand.Parameters.AddWithValue("@imgName", txtTitle.Text + "," + txtVersionNumber.Text);
                            sqlCommand.Parameters.AddWithValue("@ContentType", contentType);
                            sqlCommand.Parameters.AddWithValue("@Content", ms.ToArray());

                        }
                    }
                    else
                    {
                        StopOperation("Image is more than 200kb");
                        return;
                    }
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@imgID", 0);
                    sqlCommand.Parameters.AddWithValue("@ContentType", "");
                    sqlCommand.Parameters.AddWithValue("@Content", 0);
                    sqlCommand.Parameters.AddWithValue("@imgName", "");

                }

                Global.Connect();
                sqlCommand.ExecuteNonQuery();
                ClearControls();
                SuccessOperation();

            }
            catch (Exception ex)
            {
                FailOperation();
            }
        }

        protected void gvBooks_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvBooks, "Select$" + e.Row.RowIndex);
                    e.Row.Attributes["style"] = "cursor:pointer";
                    e.Row.ToolTip = "Click On Row to View Item";
                }
            }
            catch (Exception ex)
            {
                FailOperation();
            }
        }

        private void SuccessOperation()
        {
            //_____________________________________________________________________________________// 
            //Success Section// 
            Global.Status = "Success,Success,The operation resulted by success";
            Global.DesignWarningColor(DivStatus, StatusType, lblStrongStatus, lblStatus, this);

            //EO Success Section// 
        }
        private void FailOperation()
        {
            //_____________________________________________________________________________________// 
            //Fail Section
            Global.Status = "Danger,Failure,The operation failed";
            Global.DesignWarningColor(DivStatus, StatusType, lblStrongStatus, lblStatus, this);

            //EO Success Section// 
        }
        private void StopOperation(string Reason)
        {
            //_____________________________________________________________________________________// 
            Global.Status = "Info,Info," + Reason;
            Global.DesignWarningColor(DivStatus, StatusType, lblStrongStatus, lblStatus, this);
            return;
            //EO Success Section// 
        }
        protected void gvBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                GridViewRow row = gvBooks.SelectedRow;
                //Title 1
                //Author 2
                //Publisher 3
                //Price 4
                //Version 5
                //Subject 6
                //Date 4
                //isPublished 5
                //Active 6 
                //Notes 6
                Session["RowID"] = row.Cells[0].Text;
                txtTitle.Text = row.Cells[1].Text;
                txtAuthor.Text = row.Cells[2].Text;
                txtVersionNumber.Text = row.Cells[3].Text;
                txtPrice.Text = row.Cells[4].Text;
                rbPaymentType.SelectedIndex = 1;
                ddlClass.Items.Insert(0, new ListItem(row.Cells[6].Text, "NaN"));
                ddlClass.Items.Insert(1, new ListItem("*************************************", "NaN"));
                ddlClass.SelectedIndex = 0;
                txtSubject.Text = row.Cells[7].Text;
                CheckBox cbox = (CheckBox)row.Cells[9].Controls[0];
                if ((cbox.Checked))
                {
                    cbPublished.Checked = true;
                }
                else
                {
                    cbPublished.Checked = false;
                }

                if ((row.Cells[8].Text == String.Empty))
                {

                }
                else
                {
                    txtDate.Text = DateTime.Parse(row.Cells[8].Text.ToString()).ToString("yyyy-MM-dd");
                }
                cbActive.Checked = true;
                btnRemoveImg.Visible = true;
                //-------------------------------------------------------------------------------------------------------------//
                imageLoad.ImageUrl = "~/ShowImage.ashx?FileName=" + row.Cells[10].Text;
                if (imageLoad.ImageUrl == "~/ShowImage.ashx?FileName=&nbsp;")
                {
                    imageLoad.ImageUrl = "~/assets/static/images/No_image_3x4.svg.png";
                }
                //-------------------------------------------------------------------------------------------------------------//
            }
            catch (Exception ex)
            {
                FailOperation();
            }

        }

        protected void btnDelete_ServerClick(object sender, EventArgs e)
        {
            try
            {

                if (Session["ROWID"] != null)
                {
                    SqlCommand sqlCommand = new SqlCommand("DELETEBooks", Global.con);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@ROWID", Convert.ToString(Session["RowID"]));
                    Global.Connect();
                    sqlCommand.ExecuteNonQuery();
                    ClearControls();
                    SuccessOperation();
                }
            }
            catch (Exception ex) { }
        }
        protected void btnClear_ServerClick(object sender, EventArgs e)
        {
            try
            {
                ClearControls();
            }
            catch (Exception ex) { }
        }

        protected void btnEdit_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (txtAuthor.Text == string.Empty || txtPrice.Text == string.Empty || txtSubject.Text == string.Empty || txtTitle.Text == string.Empty || ddlClass.SelectedIndex == 0)
                {
                    if (ddlClass.SelectedIndex == 0)
                    {

                        StopOperation("There is an empty field-Choose a class that isn't for display");
                    }
                    else
                    {
                        StopOperation("There is an empty field");
                    } return;

                }

                SqlCommand sqlCommand = new SqlCommand("EDITBooks", Global.con);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@RowIndex", Convert.ToString(Session["RowID"]));
                sqlCommand.Parameters.AddWithValue("@Title", txtTitle.Text);
                sqlCommand.Parameters.AddWithValue("@Author", txtAuthor.Text);
                sqlCommand.Parameters.AddWithValue("@Version", txtVersionNumber.Text);
                //-------------------------Price Variable-------------------------//
                double PriceDollar = 0;
                double PriceLBP = 0;

                if (rbPaymentType.Text == "LBP")
                {
                    PriceDollar = Convert.ToDouble(txtPrice.Text) / 1500;
                    PriceLBP = Convert.ToDouble(txtPrice.Text);
                }
                if (rbPaymentType.Text == "USD")
                {
                    PriceDollar = Convert.ToDouble(txtPrice.Text);
                    PriceLBP = Convert.ToDouble(txtPrice.Text) * 1500;
                }
                sqlCommand.Parameters.AddWithValue("@uPrice", PriceDollar);
                sqlCommand.Parameters.AddWithValue("@LPrice", PriceLBP);
                //-------------------------Price Variable-------------------------//
                sqlCommand.Parameters.AddWithValue("@GradeID", ddlClass.SelectedValue);
                sqlCommand.Parameters.AddWithValue("@Subject", txtSubject.Text);
                if (txtDate.Text == string.Empty)
                {
                    sqlCommand.Parameters.AddWithValue("@CDate", DateTime.Now);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@CDate", Convert.ToDateTime(txtDate.Text));
                }
                //-------------------------Checkboxes----------------------------------------//
                int Active = 0;
                int Published = 0;
                if (cbActive.Checked)
                {
                    Active = 1;
                }
                if (cbPublished.Checked)
                {
                    Published = 1;
                }
                sqlCommand.Parameters.AddWithValue("@isActive", Active);
                sqlCommand.Parameters.AddWithValue("@isPublished", Published);
                //-------------------------Checkboxes----------------------------------------//
                sqlCommand.Parameters.AddWithValue("@Publisher", txtPublisher.Text);
                sqlCommand.Parameters.AddWithValue("@Notes", txtNotes.Text);
                //-----------------------------------------------IMG DATA------------------------------------------//
                if (Convert.ToBoolean(Session["RemoveImage"]) == true)
                {
                    sqlCommand.Parameters.AddWithValue("@imgID", 0);

                }
                else
                {
                    if (Uploadedimages.HasFile)
                    {
                        System.IO.Stream fs = Uploadedimages.PostedFile.InputStream;
                        int iFileSize = (int)fs.Length;
                        if (iFileSize < 150000)  // 100KB
                        {

                            string contentType = Uploadedimages.PostedFile.ContentType;
                            System.IO.BinaryReader binReader = new System.IO.BinaryReader(fs);
                            byte[] buffer = new byte[16 * 1024];
                            using (MemoryStream ms = new MemoryStream())
                            {
                                int read;
                                while ((read = fs.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    ms.Write(buffer, 0, read);
                                }
                                sqlCommand.Parameters.AddWithValue("@imgName", txtTitle.Text + "," + txtVersionNumber.Text);
                                sqlCommand.Parameters.AddWithValue("@ContentType", contentType);
                                sqlCommand.Parameters.AddWithValue("@Content", ms.ToArray());

                            }
                        }
                    }
                    else
                    {
                        return;
                    }
                }

                Global.Connect();
                sqlCommand.ExecuteNonQuery();
                ClearControls();
                SuccessOperation();
            }
            catch (Exception ex)
            {
                FailOperation();


            }


        }
        protected void btnRemoveImg_ServerClick(object sender, EventArgs e)
        {
            try
            {
                Session["RemoveImage"] = true;
                imageLoad.ImageUrl = "~/assets/static/images/loading.gif";
            }
            catch
            {
                return;
            }
        }
        public void ClearControls()
        {
            foreach (var item in maindiv.Controls)
            {
                if (item is TextBox)
                {
                    ((TextBox)item).Text = "";
                }
                if (item is DropDownList)
                {
                    ((DropDownList)item).SelectedIndex = 0;
                } if (item is CheckBox)
                {
                    ((CheckBox)item).Checked = false;
                }
                txtPrice.Text = string.Empty;
            }

            Session["RemoveImage"] = false;
                imageLoad.ImageUrl = "~/assets/static/images/loading.gif";
            Global.FillDropDownProc(ddlClass, "ClassRetrieval", "Name", "ID");
            Global.LoadTableSP(gvBooks, "GetBooks");
            Session["ROWID"] = null;
        }


    }
}