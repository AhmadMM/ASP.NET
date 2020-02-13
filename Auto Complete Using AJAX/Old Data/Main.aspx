<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="frmOrder.aspx.cs" Inherits="POSWeb.frmOrder" %>

<%@ Register TagPrefix="oem" Namespace="OboutInc.EasyMenu_Pro" Assembly="obout_EasyMenu_Pro" %>

<%@ Register Assembly="obout_SuperForm" Namespace="Obout.SuperForm" TagPrefix="cc4" %>

<%@ Import Namespace="System.Data.OleDb" %>

<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>

<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc1" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
    


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Assets/css/buttons.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .ValidateThis {
            border-style: solid;
            border-color: coral;
        }

        .ValidateThisNone {
            border-style: none;
            border-color: transparent;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="msg" runat="server">
    <asp:UpdatePanel ID="UpdatePanel42xs" runat="server">
        <ContentTemplate>
            <div id="DivStatus" runat="server" style="width: 100%; position: initial; margin-top: 0; z-index: 999999 !important; top: 0;">

                <span aria-hidden="true" id="StatusType" runat="server"></span>

                <strong>
                    <asp:Label runat="server" ID="lblStrongStatus" Text=""></asp:Label></strong>
                <br />
                <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
            </div>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">
        setTimeout(function () {
            $(".alert").fadeTo(5000, 0).slideUp(3000, function () {
                $(this).remove();
            });
        }, 5000);
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 
    <div class="container">

        <div class="well form-horizontal" id="MyProfileForm">
            <div class="row">
                <div class="col-md-5">
                    <div class="form-group label-floating">
                        <asp:UpdatePanel runat="server" ID="upCurren">
                            <ContentTemplate>
                                <asp:RadioButtonList ID="rbPayment" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbPayment_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem> USD</asp:ListItem>
                                    <asp:ListItem Selected="True"> LBP</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                    ControlToValidate="rbPayment" ErrorMessage="RequiredFieldValidator">
                                </asp:RequiredFieldValidator>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="rbPayment" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-5">
                    <div class="form-group label-floating">
                        <label class="control-label">Date of Sale</label>
                        <asp:TextBox runat="server" ID="txtDateOfSale" class="form-control"  TextMode="Date"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-5">
                    <div class="form-group label-floating">
                        <label class="control-label">Related Document</label>
                        <asp:DropDownList runat="server" ID="ddlRelatedDoc" class="form-control"></asp:DropDownList>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-5">
                    <div class="form-group label-floating">
                        <label class="control-label">Customer Name</label>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate> 
                                <asp:TextBox runat="server" ID="txtCustomerName" class="form-control" AutoPostBack="true"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" TargetControlID="txtCustomerName"
                                    MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000"
                                    ServiceMethod="GetCustomers">
                                </asp:AutoCompleteExtender>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txtCustomerName" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-5">
                    <div class="form-group label-floating">
                        <label class="control-label">Payment Type</label>
                        <asp:DropDownList ID="ddlPaymentType" runat="server" class="form-control">
                            <asp:ListItem>Cash</asp:ListItem>
                            <asp:ListItem>on Account</asp:ListItem>
                            <asp:ListItem>Credit Note</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>   <div class="row">
                <div class="col-md-5">
                    <div class="form-group label-floating">
                        <label class="control-label">Comments</label>
                         <asp:TextBox runat="server" ID="txtComments" class="form-control" ></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-5">
                    <div class="form-group label-floating">
                        <asp:CheckBox runat="server" ID="cbPrintInvoice" Text="Print Invoice" />
                        <asp:CheckBox runat="server" ID="cbSendByMail" Text="Send By Mail" />
                        <asp:CheckBox runat="server" ID="cbPrintReceipts" Text="Print Receipts" />
                    </div>
                </div>
            </div>

            <div class="row">
                <div id="collapseTwo" class="panel-collapse collapse">
                    <div class="panel-body">
                        <asp:Panel ID="Panel5" runat="server">
                            <div class="shadowBox">
                                <div class="page-container">
                                    <div class="container">
                                        <div class="row">
                                            <div class="col-lg-12 ">
                                                <div class="table-responsive">
                                                    <%--~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~--%>
                                                    <div runat="server" id="divRate" >
                                                        Rate:
                                                    <asp:TextBox ID="txtLBPRate" runat="server" placeholder="LBP Rate" class="input_text" Text="1500" Width="50px" />
                                                        <br />
                                                    </div>
                                                    <%--~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~--%>

                                                    <%--~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~--%>
                                                    <input type='text' placeholder='Search... ' id='search-text-input' class="input_Search" />
                                                    <%--~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~--%>
                                                    <asp:GridView ID="gvOrders" runat="server" RowStyle-Wrap="false" ForeColor="Black" AllowSorting="True" PageSize="5" ShowHeader="true" HorizontalAlign="Left" Width="70%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" Style="color: black; border-collapse: collapse; overflow-x: scroll; display: grid;" ShowHeaderWhenEmpty="true" ShowFooter="true" OnRowDataBound="gvOrders_RowDataBound">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="_" ControlStyle-CssClass="displaynone" />
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Free" ShowHeader="True">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox runat="server" ID="cbFree" OnCheckedChanged="cbFree_CheckedChanged" AutoPostBack="true" />
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <button id="btnAddRow" runat="server" class="btn btn-Danger" onserverclick="btnAddRow_ServerClick">Add Row <span class="glyphicon                                        glyphicon-plus"></span></button>
                                                                    <br />
                                                                    <asp:Panel runat="server" ID="pnlMoney" Visible="false">
                                                                        Total(<asp:Label runat="server" ID="lblTotalCurrency"></asp:Label>):
                                                                            <asp:Label runat="server" ID="lblTotal"></asp:Label>
                                                                    </asp:Panel>
                                                                    <br />
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Item Code">
                                                                <ItemTemplate>
                                                                    <asp:TextBox runat="server" ID="txtItemCode" OnTextChanged="txtItemCode_TextChanged" AutoPostBack="true" />
                                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtItemCode"
                                                                        MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000"
                                                                        ServiceMethod="GetProductItemCode">
                                                                    </asp:AutoCompleteExtender>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Description">
                                                                <ItemTemplate>
                                                                    <asp:TextBox runat="server" ID="txtDescription" OnTextChanged="txtDescription_TextChanged" AutoPostBack="true" />
                                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txtDescription"
                                                                        MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000"
                                                                        ServiceMethod="GetProductDescription">
                                                                    </asp:AutoCompleteExtender>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Quantity">
                                                                <ItemTemplate>
                                                                    <asp:TextBox runat="server" ID="txtQuantity" TextMode="Number" min="1" OnTextChanged="txtQuantity_TextChanged" AutoPostBack="true" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Cost">
                                                                <ItemTemplate>
                                                                    <asp:TextBox runat="server" ID="txtCost" TextMode="Number" disabled />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Price">
                                                                <ItemTemplate>
                                                                    <asp:TextBox runat="server" ID="txtPrice" TextMode="Number" disabled />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Color" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:TextBox runat="server" ID="txtColor" TextMode="Color" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Discount %">
                                                                <ItemTemplate>
                                                                    <asp:TextBox runat="server" ID="txtDiscount" TextMode="Number" OnTextChanged="txtDiscount_TextChanged" AutoPostBack="true" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Discount Amount">
                                                                <ItemTemplate>
                                                                    <asp:TextBox runat="server" ID="txtDiscountAmount" TextMode="singleline" OnTextChanged="txtDiscountAmount_TextChanged" Text="0" AutoPostBack="true" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="VAT">
                                                                <ItemTemplate>
                                                                    <asp:TextBox runat="server" ID="txtVAT" TextMode="Number" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Total Cost">
                                                                <ItemTemplate>
                                                                    <asp:TextBox runat="server" ID="txtTCost" TextMode="Number" disabled/>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Total Price">
                                                                <ItemTemplate>
                                                                    <asp:TextBox runat="server" ID="txtTPrice" TextMode="singleline" disabled />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Expiry Date">
                                                                <ItemTemplate>
                                                                    <asp:TextBox runat="server" ID="txtExpiryDate" disabled />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Comments">
                                                                <ItemTemplate>
                                                                    <asp:TextBox runat="server" ID="txtComments" TextMode="MultiLine" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Label runat="server" ID="lblSubTotal"></asp:Label>
    <asp:UpdatePanel runat="server" ID="up1">
        <ContentTemplate>
            <div class="row">
                <div class="col-sm-4">
                    <button id="btnPurchase" runat="server" class="btn btn-warning" onserverclick="btnPurchase_ServerClick">Purchase <span class="glyphicon glyphicon-floppy-save"></span></button>
                </div>
                <div class="col-sm-4">
                    <button id="btnIsSaleLastPrice" runat="server" class="btn btn-success" onserverclick="btnIsSaleLastPrice_ServerClick">Last Price <span class="glyphicon glyphicon-usd"></span></button>
                </div>
                <div class="col-sm-4">
                    <button id="btnClear" runat="server" class="btn btn-info" onserverclick="btnClear_ServerClick">Clear<span class="glyphicon glyphicon-trash"></span></button>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnPurchase" />
            <asp:PostBackTrigger ControlID="btnIsSaleLastPrice" />
            <asp:PostBackTrigger ControlID="btnClear" />
        </Triggers>
    </asp:UpdatePanel>

    <fieldset class="fieldset">
        <legend>Summary:</legend>
        <div class="form-inline well">
            <div class="form-group">
                <fieldset class="fieldsetInternal" style="width:150px">
                    <div>
                        <label>Quantity:</label>
                    </div>
                    <div>
                        <label>Sub Total:</label></div>
                    <div>
                        <asp:TextBox ID="txtINVDiscountPER" runat="server" AutoPostBack="true" OnTextChanged="txtINVDiscountPER_TextChanged" style="width: 35px;"></asp:TextBox><label>Discount:  </label>
                    </div>
                    <div>
                        <label>Total Before VAT:</label>
                    </div>
                    <div>
                        <label>V.A.T:</label></div>
                </fieldset>
            </div>
            <div class="form-group">
                <fieldset class="fieldsetInternal">
                    <div>
                        <asp:TextBox runat="server" ID="txtINVTotalQuantity" TextMode="Number" min="0" Text="0" Style="margin-bottom: 7%; border: 0" placeholder="Total Quantity" disabled /> 
                    </div>
                    <div>
                        <asp:TextBox runat="server" ID="txtINVSubTotal_LBP" TextMode="Number" min="0" Text="0" Style="margin-bottom: 7%; border: 0" placeholder="Sub Total" disabled /><small>(L)</small>
                    </div>
                    <div>
                        <asp:TextBox runat="server" ID="txtINVDiscount_LBP" TextMode="Number" min="0" Text="0" Style="margin-bottom: 7%; border: 0" placeholder="Discount" disabled/><small>(L)</small>
                    </div>
                    <div>
                        <asp:TextBox runat="server" ID="txtINVTotalBeforeVAT_LBP" TextMode="Number" min="0" Text="0" Style="margin-bottom: 7%; border: 0" placeholder="Sub Total After Discount" disabled /><small>(L)</small>
                    </div>
                    <div>
                        <asp:TextBox runat="server" ID="txtINVAT_LBP" TextMode="Number" min="0" Text="0" Style="margin-bottom: 7%; border: 0" placeholder="V.A.T" disabled /><small>(L)</small>
                    </div>
                </fieldset>
            </div>
            <div class="form-group">
                <fieldset class="fieldsetInternal"> 
                     <div >
                       <br style="margin-top: 15px;" />
                    </div>
                    <div>
                        <asp:TextBox runat="server" ID="txtINVSubTotal_Dollar" TextMode="Number" min="0" Text="0" Style="margin-bottom: 7%; border: 0" placeholder="Sub Total" disabled /><small>($)</small>
                    </div>
                    <div>
                        <asp:TextBox runat="server" ID="txtINVDiscount_Dollar" TextMode="Number" min="0" Text="0" Style="margin-bottom: 7%; border: 0" placeholder="Discount"  disabled/><small>($)</small>
                    </div>
                    <div>
                        <asp:TextBox runat="server" ID="txtINVTotalBeforeVAT_Dollar" TextMode="Number" min="0" Text="0" Style="margin-bottom: 7%; border: 0" placeholder="Sub Total After Discount" disabled /><small>($)</small>
                    </div>
                    <div>
                        <asp:TextBox runat="server" ID="txtINVAT_Dollar" TextMode="Number" min="0" Text="0" Style="margin-bottom: 7%; border: 0" placeholder="V.A.T" disabled /><small>($)</small>
                    </div>
                </fieldset>
            </div>
        </div>
    </fieldset>
</asp:Content>
