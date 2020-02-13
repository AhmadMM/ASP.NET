<%@ Page Title="New Contacts" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="frmNewContacts.aspx.cs" Inherits="POSWeb.frmMyProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Assets/css/buttons.css" type="text/css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
            <div class="row">
                <div class="col-md-5">
                    <div class="form-group label-floating">
                        <label class="control-label">Company</label>
                        <asp:TextBox runat="server" ID="txtCompany" class="form-control"></asp:TextBox>
                    </div>
                </div>
            </div> 

        <div class="row">
            <div class="col-md-5">
                <div class="form-group label-floating">
                    <label class="control-label">Fist Name</label>
                    <asp:TextBox runat="server" ID="txtFN" class="form-control"></asp:TextBox>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-5">
                <div class="form-group label-floating">
                    <label class="control-label">Last Name</label>
                    <asp:TextBox runat="server" ID="txtLN" class="form-control"></asp:TextBox>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-5">
                <div class="form-group label-floating">
                    <label class="control-label">Email address</label>
                    <asp:TextBox runat="server" ID="txtEmail" TextMode="Email" class="form-control"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-5">
                <div class="form-group label-floating">
                    <label class="control-label">Adress</label>
                    <asp:TextBox runat="server" ID="txtAddress" class="form-control" TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>
        </div>


        <div class="row">
            <div class="col-md-5">
                <div class="form-group label-floating">
                    <label class="control-label">Date of Birth</label>
                    <asp:TextBox runat="server" ID="txtDOB" TextMode="Date" class="form-control"></asp:TextBox>
                </div>
            </div>
        </div>

              <div class="row">
            <div class="col-md-5">
                <div class="form-group label-floating">
                    <label class="control-label">Fixed Phone Number</label>
                    <asp:TextBox runat="server" ID="txtFxNum" TextMode="Phone" class="form-control"></asp:TextBox>
                </div>
            </div>
        </div>
              <div class="row">
            <div class="col-md-5">
                <div class="form-group label-floating">
                    <label class="control-label">Mobile Phone Number</label>
                    <asp:TextBox runat="server" ID="txtMbNum" TextMode="Phone" class="form-control"></asp:TextBox>
                </div>
            </div>
              </div>
    <div class="row">
        <div class="col-md-4"> 
            <a runat="server" id="btnSave" onserverclick="btnSave_ServerClick" class="Button">
                <span class="text">Add</span>
                <span class="line -right"></span>
                <span class="line -top"></span>
                <span class="line -left"></span>
                <span class="line -bottom"></span>
            </a>


        </div>
        <div class="col-md-4"> 
            <a runat="server" id="btnEdit" onserverclick="btnEdit_ServerClick" class="Button">
                <span class="text">Edit</span>
                <span class="line -right"></span>
                <span class="line -top"></span>
                <span class="line -left"></span>
                <span class="line -bottom"></span>
            </a>
        </div>
        <div class="col-md-4">
            <a runat="server" id="btnDelete" onserverclick="btnDelete_ServerClick" class="Button">
                <span class="text">Delete</span>
                <span class="line -right"></span>
                <span class="line -top"></span>
                <span class="line -left"></span>
                <span class="line -bottom"></span>
            </a>
        </div>
    </div>
</asp:Content>
