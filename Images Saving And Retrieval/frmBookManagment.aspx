<%@ Page Title="Book Management" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="frmBookManagment.aspx.cs" Inherits="BCCApp.frmBookManagment" enableEventValidation="false" %>

<asp:Content ID="Content3" ContentPlaceHolderID="msg" runat="server">  

            <div id="DivStatus" runat="server" style="width: 100%; position: initial; margin-top: 0; z-index: 999999 !important; top: 0;">
               
                <span aria-hidden="true" id="StatusType" runat="server"></span>
                <strong>
                    <asp:Label runat="server" ID="lblStrongStatus" Text=""></asp:Label></strong>
                <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
            </div> 
 </asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> 
    <script type="text/javascript">
        function previewFile() {
            var preview = document.querySelector('#<%=imageLoad.ClientID%>');
                                                                              var file1 = document.querySelector('#<%=Uploadedimages.ClientID%>').files[0];
                                                                              var reader = new FileReader();

                                                                              reader.onloadend = function () {
                                                                                  preview.src = reader.result;
                                                                              }

                                                                              if (file1) {
                                                                                  reader.readAsDataURL(file1);
                                                                              } else {
                                                                                  preview.src = "";
                                                                              }
                                                                              if (file2) {
                                                                                  reader.readAsDataURL(file2);
                                                                              } else {
                                                                                  preview.src = "";
                                                                              }
                                                                          }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:UpdatePanel runat="server" ID="pnl">
        <ContentTemplate>
    <div id="maindiv" runat="server">
    <div class="well form-horizontal" id="BookInfo">
        <div class="row">
            <div class="col-md-5">
                <div class="form-group label-floating">
                    <label class="control-label">Title:</label>
                    <asp:TextBox runat="server" ID="txtTitle" class="form-control" TextMode="Search" placeholder="Title"></asp:TextBox>*
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-5">
                <div class="form-group label-floating">
                    <label class="control-label">Author:</label>
                    <asp:TextBox runat="server" ID="txtAuthor" class="form-control" TextMode="Search" placeholder="Author"></asp:TextBox>*
                </div>
            </div>
        </div>
         <div class="row">
            <div class="col-md-5">
                <div class="form-group label-floating">
                    <label class="control-label">Publisher:</label>
                    <asp:TextBox runat="server" ID="txtPublisher" class="form-control" TextMode="Search" placeholder="Publisher"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-5">
                <div class="form-group label-floating">
                    <label class="control-label">Price:</label>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:RadioButtonList runat="server" ID="rbPaymentType" RepeatDirection="Horizontal">
                                <asp:ListItem Value="LBP">LBP</asp:ListItem>
                                <asp:ListItem Value="USD">USD</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:TextBox runat="server" ID="txtPrice" class="form-control" TextMode="Search" placeholder="Price"></asp:TextBox>*
                        </ContentTemplate>
                        <Triggers>
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-5">
                <div class="form-group label-floating">
                    <label class="control-label">Version Number:</label>
                    <asp:TextBox runat="server" ID="txtVersionNumber" class="form-control" TextMode="Search" placeholder="Version Number"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-5">
                <div class="form-group label-floating">
                    <label class="control-label">Subject:</label>
                    <asp:TextBox runat="server" ID="txtSubject" class="form-control" TextMode="Search" placeholder="Subject"></asp:TextBox>*
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-5">
                <div class="form-group label-floating">
                    <label class="control-label">Class:</label>
                    <asp:DropDownList runat="server" ID="ddlClass" class="btn cur-p btn-outline-danger  form-control">
                    </asp:DropDownList>*
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-5">
                <div class="form-group label-floating">
                    <label class="control-label">Date:</label>
                    <asp:TextBox runat="server" ID="txtDate" class="form-control" TextMode="Date" placeholder="Date"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-5">
                <div class="form-group label-floating">
                    <label class="control-label">Published:</label>
                    <asp:CheckBox runat="server" ID="cbPublished" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-5">
                <div class="form-group label-floating">
                    <label class="control-label">Active</label>
                    <asp:CheckBox runat="server" ID="cbActive" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-5">
                <div class="form-group label-floating">
                    <label class="control-label">Notes</label>
                    <asp:TextBox runat="server" ID="txtNotes" class="form-control" TextMode="Search" placeholder="Search"></asp:TextBox>
                </div>
            </div>
        </div>
        <div>
            <div class="row">
                <div class="col-md-5">
                    <asp:updatepanel runat="server" ID="updk" UpdateMode="Conditional">
                        <ContentTemplate>

                            <label>Upload:</label>
                            <asp:FileUpload ID="Uploadedimages" runat="server" onchange="previewFile()" class="form-control" /><div>
                                <div>
                                    <label id="lblimage">Preview:</label><br />
                                    <asp:Image ID="imageLoad" runat="server" Height="100px" ImageUrl="~/assets/static/images/loading.gif"
                                        Width="100px" />
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <button id="btnRemoveImg" runat="server" class="btn cur-p btn-outline-Danger" onserverclick="btnRemoveImg_ServerClick" visible="false">X</button>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>

                        </ContentTemplate>
                        <Triggers>
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>

            <div>
                <div class="row" style="margin-top: 5%; width: 100%; overflow-x: auto;">
                    <asp:Panel ID="Panel1" runat="server">
                        <div class="table-responsive">
                            <%--~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~--%>
                            <input type='text' placeholder='Search... ' id='search-text-input' class="form-control" style="width: 20%;" />
                            <%--~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~--%>
                            <asp:GridView ID="gvBooks" runat="server" CssClass="table-overflow" AutoGenerateColumns="True" OnRowDataBound="gvBooks_RowDataBound" OnSelectedIndexChanged="gvBooks_SelectedIndexChanged">
                                <EmptyDataTemplate>
                                    No Record Found

                                </EmptyDataTemplate>
                            </asp:GridView>

                        </div>
                    </asp:Panel>

                </div>
            </div>
        </div>
    </div>
    <asp:UpdatePanel runat="server" ID="UpdatePanel7">
        <ContentTemplate>
            <div class="row">
                <div class="col-sm-3">
                    <button id="btnAdd" runat="server" class="btn cur-p btn-outline-primary" onserverclick="btnAdd_ServerClick">Add <span class="glyphicon glyphicon-floppy-save"></span></button>
                </div>
                <div class="col-sm-3">
                    <button id="btnEdit" runat="server" class="btn cur-p btn-outline-secondary" onserverclick="btnEdit_ServerClick">Edit<span class="glyphicon glyphicon-pen"></span></button>
                </div>
                <div class="col-sm-3">
                    <button id="btnDelete" runat="server" class="btn cur-p btn-outline-success" onserverclick="btnDelete_ServerClick">Delete<span class="glyphicon glyphicon-minus"></span></button>
                </div>
                      <div class="col-sm-3">
                        <button id="btnClear" runat="server" class="btn cur-p btn-outline-info" onserverclick="btnClear_ServerClick">Clear<span class="glyphicon glyphicon-trash"></span></button>
                    </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnAdd" />
            <asp:PostBackTrigger ControlID="btnEdit" />
            <asp:PostBackTrigger ControlID="btnDelete" />
            <asp:PostBackTrigger ControlID="btnClear" />
        </Triggers>
    </asp:UpdatePanel>
         </div>
            

        </ContentTemplate>
        <Triggers></Triggers>
    </asp:UpdatePanel>
</asp:Content>

