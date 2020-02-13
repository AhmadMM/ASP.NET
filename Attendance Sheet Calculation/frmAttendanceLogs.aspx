<%@ Page Title="Attendance |Caliber" Language="vb" AutoEventWireup="false" MasterPageFile="~/Caliber.Master"
    CodeBehind="frmAttendanceLogs.aspx.vb" Inherits="Caliber.frmAttendanceLogs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/tab-content/template1/tabcontent.css" rel="stylesheet" type="text/css" />
    <script src="css/tab-content/tabcontent.js" type="text/javascript"></script>
    <style type="text/css">
        .PagerCSS span {
            color: #009900;
            font-weight: bold;
            font-size: 16pt;
        }
    </style>
    <style runat="server">
        .file-upload {
            display: inline-block;
            overflow: hidden;
            text-align: center;
            vertical-align: middle;
            font-family: Arial;
            border: 1px solid #124d77;
            background: #007dc1;
            color: #fff;
            border-radius: 6px;
            -moz-border-radius: 6px;
            cursor: pointer;
            text-shadow: #000 1px 1px 2px;
            -webkit-border-radius: 6px;
        }

            .file-upload:hover {
                background: -webkit-gradient(linear, left top, left bottom, color-stop(0.05, #0061a7), color-stop(1, #007dc1));
                background: -moz-linear-gradient(top, #0061a7 5%, #007dc1 100%);
                background: -webkit-linear-gradient(top, #0061a7 5%, #007dc1 100%);
                background: -o-linear-gradient(top, #0061a7 5%, #007dc1 100%);
                background: -ms-linear-gradient(top, #0061a7 5%, #007dc1 100%);
                background: linear-gradient(to bottom, #0061a7 5%, #007dc1 100%);
                filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#0061a7', endColorstr='#007dc1',GradientType=0);
                background-color: #0061a7;
            }

        /* The button size */
        .file-upload {
            height: 30px;
        }

            .file-upload, .file-upload span {
                width: 90px;
            }

                .file-upload input {
                    top: 0;
                    left: 0;
                    margin: 0;
                    font-size: 11px;
                    font-weight: bold;
                    /* Loses tab index in webkit if width is set to 0 */
                    opacity: 0;
                    filter: alpha(opacity=0);
                }

                .file-upload strong {
                    font: normal 12px Tahoma,sans-serif;
                    text-align: center;
                    vertical-align: middle;
                }

                .file-upload span {
                    top: 0;
                    left: 0;
                    display: inline-block;
                    /* Adjust button text vertical alignment */
                    padding-top: 5px;
                }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="msg" runat="server">
    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
            <div id="DivStatus" runat="server" style="width: 75%;">
                <span aria-hidden="true" id="StatusType" runat="server"></span>
                <strong>
                    <asp:Label runat="server" ID="lblStrongStatus" Text=""></asp:Label></strong>
                <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
            </div>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
        <ContentTemplate>
            <asp:TextBox runat="server" ID="txtSearch" placeholder="Search..." CssClass="input_Text_100"
                AutoPostBack="true" TextMode="Search"></asp:TextBox>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="txtSearch" EventName="TextChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <div class="tabcontents">
            <label id="lblFromDate" class="desc">From Date:</label>
        <div>
            <asp:TextBox runat="server" ID="txtFromDate" placeholder="From Date..." CssClass="input_Text"
                AutoPostBack="true" TextMode="Date"></asp:TextBox>
                    <span style="margin-left: 1%;" id="span1" runat="server" 
                __designer:mapid="d0">*</span>
                </div>
        <label id="lblDate" class="desc">To Date:</label>
        <div>
            <asp:TextBox runat="server" ID="txtToDate" placeholder="To Date..." CssClass="input_Text"
                AutoPostBack="true" TextMode="Date"></asp:TextBox>
                    <span style="margin-left: 1%;" id="span3" runat="server" 
                __designer:mapid="d0">*</span>
                </div>
        <label id="lblProjectName" class="desc">Project:</label>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="ddlProject" CssClass="input_Text" runat="server" onchange="javascript:setTimeout('__doPostBack(\'ddlProject\',\'\')', 0)"
                        AutoPostBack="True">
                        <asp:ListItem Text="Select a Project" Value="NaN"></asp:ListItem>
                    </asp:DropDownList>
                    <span style="margin-left: 1%;" id="span2" runat="server">*</span>
                </ContentTemplate>
                <Triggers>
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <label id="lblDepartment" class="desc">Department:</label>
        <div>
            <asp:DropDownList ID="ddlDepartment" CssClass="input_Text" runat="server">
                <asp:ListItem Text="Select a Department" Value="NaN"></asp:ListItem>
            </asp:DropDownList>
        </div>

        <asp:FileUpload ID="FileUpload1" runat="server" />
        <asp:Button ID="btnUpload" runat="server" Text="Upload"
            OnClick="btnUpload_Click" />
        <asp:RadioButtonList ID="rbHDR" runat="server">
            <asp:ListItem Text="Yes" Value="Yes" Selected="True">
            </asp:ListItem>
            <asp:ListItem Text="No" Value="No"></asp:ListItem>
        </asp:RadioButtonList>

        <br />
        <asp:Panel ID="Panel_Admin_Buttons" runat="server" CssClass="Panels_Responsive">
            <button runat="server" id="btnAdd" class="btn btn-xs btn-success" style="font-size: 150%;
                margin-bottom: 5%; margin-top: 5%;"
                onserverclick="Addition_Func">
                <i class="fa fa-plus-square-o" style="padding-right: 10px;"></i>Add</button>
            <button runat="server" id="btnCancel" class="btn btn-xs btn-warning" style="font-size: 150%;
                margin-bottom: 5%; margin-top: 5%;"
                onserverclick="Cancel_Func">
                <i class="fa fa-unlink" style="padding-right: 10px;"></i>Cancel</button>
        </asp:Panel>

        <section style="margin-left: 0%;">

            <asp:GridView ID="GridView1" runat="server" CssClass="table-overflow"  AllowSorting="True">
                 <PagerStyle CssClass="PagerCSS" />
                <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
            </asp:GridView>
            <asp:Panel ID="Panel1" runat="server" CssClass="Panels_Responsive">
                <button runat="server" id="btnApplyChanges" class="btn btn-xs btn-danger" style="font-size: 150%;
                    margin-bottom: 5%; margin-top: 5%;"
                    onserverclick="ApplyChanges">
                    <i class="fa fa-slideshare" style="padding-right: 10px;"></i>Apply Changes</button>
            </asp:Panel>
        </section>

    </div>
</asp:Content>
