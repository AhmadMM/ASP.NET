<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/blog.Master" CodeBehind="frmPosts.aspx.vb" ValidateRequest = "false" Inherits="Blogger.frmPosts" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/Pages.css" type="html/css" rel="stylesheet" />
    <script type="text/javascript">
        function previewFile() {
            var preview = document.querySelector('#<%=imgLoad.ClientID%>');
              var file = document.querySelector('#<%=imgUp.ClientID%>').files[0];
              var reader = new FileReader();

              reader.onloadend = function () {
                  preview.src = reader.result;
              }

              if (file) {
                  reader.readAsDataURL(file);
              } else {
                  preview.src = "";
              }
          }
    </script>
     
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div>
            <label id="lblTitle">Title:</label>
            <div>
                <asp:TextBox ID="txtTitle" runat="server" Enabled="false" TextMode="Search" CssClass="input_Text" placeholder="Title"></asp:TextBox>
            </div>
            
        </div>
        <div>
            
            <label id="lblText">Text:</label>
            <div>
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                <cc1:Editor runat="server" id="Editor1"></cc1:Editor>
            </div>
        </div>
        <div>
            <label id="lblBlogger">Blogger:</label>
            <div>
                <asp:DropDownList ID="ddlBlogger" runat="server" Enabled="false" CssClass="input_Text"></asp:DropDownList>
            </div>
        </div>
        <div>
            <div>
                <asp:CheckBox ID="cbisActive" runat="server" Enabled="false" Style="margin-left: 12%;" Text="&lt;span style=&quot;padding:5px;&quot;&gt;Activated &lt;/span&gt;" CssClass="cb"></asp:CheckBox>
                <asp:CheckBox ID="cbAllowComments" runat="server" Enabled="false" Style="margin-left: 12%;" Text="   &lt;span style=&quot;padding:5px;&quot;&gt;Allow Comments &lt;/span&gt;" CssClass="cb"></asp:CheckBox>
            </div>
        </div>
        <div>
            <label id="lblCreationdate">Post Date:</label>
            <div>
                <asp:TextBox ID="txtPD" runat="server" Enabled="false" TextMode="Date" CssClass="input_Text" placeholder="Post Date"></asp:TextBox>
            </div>
        </div>
         <div>
            <label id="lblHasImage">Has Image:</label>
            <div>
                <asp:CheckBox ID="cbHasImage" runat="server" Enabled="false" Style="margin-left: 12%;" Text="   &lt;span style=&quot;padding:5px;&quot;&gt;Has Image &lt;/span&gt;" CssClass="cb" onchange="javascript:setTimeout('__doPostBack(\'cbHasImage\',\'\')', 0)"></asp:CheckBox>
                
            </div>
        </div>
        <div id="imgDiv" runat="server" visible="false" style="visibility:hidden">
        <div>
            <label id="lblImageName">Image Name:</label>
            <div>
                <asp:TextBox ID="txtimgName" runat="server" Enabled="false" TextMode="Search" CssClass="input_Text" placeholder="Image Name"></asp:TextBox>
            </div>
        </div>
        <div>
            <label id="lblimage">Image:</label>
            <div>
                <div>
                    <asp:FileUpload ID="imgUp" onchange="previewFile()" runat="server" />
                    <asp:Image ID="imgLoad" runat="server" Height="225px" ImageUrl="~/img/loading.gif" Width="225px" />

                </div>
            </div>

        </div>
        </div>
        
    </div>
    <asp:Panel ID="Panel_Admin_Buttons" runat="server" CssClass="Panels_Responsive">
        <asp:ImageButton ID="btnADD" runat="server" CssClass="_button_first" ImageUrl="~/img/Buttons/Add.png" ToolTip="Add/Save" />
        <asp:ImageButton ID="btnEdit" runat="server" CssClass="_button" ImageUrl="~/img/Buttons/Edit.png" ToolTip="Edit/Save" />
        <asp:ImageButton ID="btnDelete" runat="server" CssClass="_button" ImageUrl="~/img/Buttons/Delete.png" ToolTip="Delete" />
        <asp:ImageButton ID="btnCancel" runat="server" CssClass="_button" ImageUrl="~/img/Buttons/Cancel.png" ToolTip="Cancel" />
    </asp:Panel>

    <br />

    <table style="margin-top: 8%; margin-bottom: 0%;">
        <tr>
            <td>
                <asp:TextBox ID="txtSearch" runat="server" CssClass="input_Text" TextMode="Search" placeholder='Search...' onchange="javascript:setTimeout('__doPostBack(\'txtSearch\',\'\')', 0)" AutoPostBack="true" Width="232px"></asp:TextBox>
            </td>
            <td>
                <div id="button-holder" style="margin-left: -25px; margin-top: -2px;">
                    <img src='img/Buttons/Search.png' />
                </div>
            </td>
        </tr>

    </table>

    <section style="margin-left: 0%;">
        <asp:GridView ID="gvPosts" runat="server" RowStyle-Wrap="false" CssClass="table-overflow"
            AutoGenerateColumns="true" AutoGenerateSelectButton="True">
        </asp:GridView>
    </section>
    <asp:Label ID="lbl" runat="server" />
</asp:Content>
