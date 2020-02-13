<%@ Page Title="Staff Management | CRM" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Masterin/Page.Master" CodeBehind="Default.aspx.vb" Inherits="CRM._Default11" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../assets/js/Translator.js"></script>
    <link rel="stylesheet" href="http://www.w3schools.com/lib/w3.css" />
    <style type="text/css">
        .pointer {
            cursor: pointer;
            margin-left: 10%;
        }
    </style>
    <script>
        $('#remember').change(function () {
            if (this.checked) {
                $('#autoUpdate').fadeIn('slow');
            }
            else {
                $('#autoUpdate').fadeOut('slow');
            }
        });
    </script>
    <%--        <script type="text/javascript">
            function valueChanged() {
                if ($('cbHasImage').is(":checked"))
                    $(".imageDiv").show();
                else
                    $(".imageDiv").hide();
            }
        </script>--%>
    <script type="text/javascript">
        //Function To Display Popup
        function spp_show() {
            document.getElementById('panelspp').style.display = "block";
            document.getElementById('panelppp').style.display = "none";
        }
        function ppp_show() {
            document.getElementById('panelppp').style.display = "block";
            document.getElementById('panelspp').style.display = "none";
        }
    </script>

    <script src="http://code.jquery.com/jquery-latest.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="msg" runat="server">
    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
            <div id="DivStatus" runat="server" style="width: 100%;">
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
<asp:Content ID="Content4" ContentPlaceHolderID="body" runat="server">
    <asp:UpdatePanel ID="UpdatePanel37" runat="server">
        <ContentTemplate>
            </updatepanel>
    <a onclick="spp_show()" title="Standard Personal Profile" class="pointer">Standard Personal Profile</a>
            <a onclick="ppp_show()" title="Professional Personal Profile" class="pointer">Professional Personal Profile</a>
            <hr />
            <div id="panelspp" class="PopOut">
                <div>
                    <label id="lblPrsType" class="desc">Type:</label>
                    <div>
                        <asp:Panel ID="Panel2" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlType" runat="server" CssClass="input_Text" AutoPostBack="true" placeholder="Type">
                                        <asp:ListItem disabled Selected Value="NULL">----------------------</asp:ListItem>
                                        <asp:ListItem>Admin</asp:ListItem>
                                        <asp:ListItem>User</asp:ListItem>
                                    </asp:DropDownList>
                                    <ajaxToolkit:ListSearchExtender runat="server" BehaviorID="ddlType_ListSearchExtender"
                                        TargetControlID="ddlType" ID="ListSearchExtender1">
                                    </ajaxToolkit:ListSearchExtender>
                                    <span style="margin-left: 1%;" id="span2" runat="server">*</span>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlType" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
                <div>
                    <label id="lblPrsSubType" class="desc">Sub Type:</label>
                    <div>
                        <asp:Panel ID="Panel3" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlSType" runat="server" CssClass="input_Text"   placeholder="Type">
                                        <asp:ListItem disabled Selected Value="NULL">----------------------</asp:ListItem>
                                        <asp:ListItem>Designer</asp:ListItem>
                                        <asp:ListItem>Developer</asp:ListItem>
                                        <asp:ListItem>Manager</asp:ListItem>
                                        <asp:ListItem>Trainer</asp:ListItem>
                                        <asp:ListItem>Accountant</asp:ListItem>
                                        <asp:ListItem>Accountant Manager</asp:ListItem>
                                        <asp:ListItem>Warehouse Keeper</asp:ListItem>
                                        <asp:ListItem>Project Leader</asp:ListItem>
                                        <asp:ListItem>Secratery</asp:ListItem>
                                        <asp:ListItem>PreSales</asp:ListItem>
                                        <asp:ListItem>Sales</asp:ListItem>
                                        <asp:ListItem>Sales Supervisor</asp:ListItem>
                                    </asp:DropDownList>
                                    <ajaxToolkit:ListSearchExtender runat="server" BehaviorID="ddlSType_ListSearchExtender"
                                        TargetControlID="ddlSType" ID="ListSearchExtender2">
                                    </ajaxToolkit:ListSearchExtender>
                                    <span style="margin-left: 1%;" id="span3" runat="server">*</span>
                                </ContentTemplate>
                                <Triggers>
                                    <%--  <asp:AsyncPostBackTrigger ControlID="ddlSType" EventName="SelectedIndexChanged" />--%>
                                </Triggers>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
                  <div>
                    <label id="lblDep" class="desc">Department:</label>
                    <div>
                        <asp:Panel ID="Panel35" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlDep" runat="server" CssClass="input_Text"   placeholder="Department">
                                        <asp:ListItem disabled Selected Value="5">----------------------</asp:ListItem>
                                       
                                    </asp:DropDownList>
                                    <ajaxToolkit:ListSearchExtender runat="server" BehaviorID="ddlDep_ListSearchExtender"
                                        TargetControlID="ddlDep" ID="ListSearchExtender7">
                                    </ajaxToolkit:ListSearchExtender>
                                    <span style="margin-left: 1%;" id="span8" runat="server">*</span>
                                </ContentTemplate>
                                <Triggers> 
                                </Triggers>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
                <div>
                    <label id="lblFN" class="desc">First Name:</label>
                    <div>
                        <asp:Panel ID="Panel1" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtFN" runat="server" TextMode="Search" CssClass="input_Text" AutoPostBack="true" placeholder="First Name"></asp:TextBox>
                                    <span style="margin-left: 1%;" id="span1" runat="server">*</span>
                                </ContentTemplate>
                                <Triggers> 
                                    <asp:AsyncPostBackTrigger  ControlID="txtFN" EventName="TextChanged" />
                                </Triggers> 
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
                <div>
                    <label id="lblLN" class="desc">Last Name:</label>
                    <div>
                        <asp:Panel ID="Panel4" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtLN" runat="server" TextMode="Search" CssClass="input_Text" AutoPostBack="true" placeholder="Last Name" ></asp:TextBox>
                                    <span style="margin-left: 1%;" id="span4" runat="server">*</span>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger  ControlID="txtLN" EventName="TextChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
                <div>
                    <label id="lblPhoneNumber" class="desc">Phone Number:</label>
                    <div>
                        <asp:Panel ID="Panel5" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtPN1" runat="server" TextMode="Phone" CssClass="input_Text"   Style="width: 40%;" placeholder="Phone Number(#1)"></asp:TextBox>
                                    <asp:TextBox ID="txtPN2" runat="server" TextMode="Phone" CssClass="input_Text"   Style="width: 40%;" placeholder="Phone Number(#2)"></asp:TextBox>
                                    <span style="margin-left: 1%;" id="span5" runat="server">*</span>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
                <div>
                    <label id="lblMobileNumber" class="desc">Mobile Number:</label>
                    <div>
                        <asp:Panel ID="Panel6" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtMN" runat="server" TextMode="Phone" CssClass="input_Text"   placeholder="Mobile Number"></asp:TextBox>
                                    <span style="margin-left: 1%;" id="span6" runat="server">*</span>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
                <div>
                    <label id="lblEmail" class="desc">Email: </label>
                    <div>
                        <asp:Panel ID="Panel7" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="input_Text"   placeholder="Email Address"></asp:TextBox>
                                    <span style="margin-left: 1%;" id="span7" runat="server">*</span>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
                <div>
                    <label id="lblPrsCommissionPer" class="desc">Commission (%): </label>
                    <div>
                        <asp:Panel ID="Panel10" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtCommission" runat="server" TextMode="Number" CssClass="input_Text"   placeholder="Commission (%)"></asp:TextBox> 
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
                <div>
                    <label id="lblWebsite" class="desc">Website: </label>
                    <div>
                        <asp:Panel ID="Panel8" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtWebsite" runat="server" TextMode="Url" CssClass="input_Text"   placeholder="Website"></asp:TextBox> 
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
                <div>
                    <label id="lblCompany" class="desc">Company: </label>
                    <div>
                        <asp:Panel ID="Panel9" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtCompany" runat="server" TextMode="Search" CssClass="input_Text"   placeholder="Company Name"></asp:TextBox>
                                    <span style="margin-left: 1%;" id="span9" runat="server">*</span>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
                <div>
                    <label id="lblMOF" class="desc">Ministry of Finance: </label>
                    <div>
                        <asp:Panel ID="Panel11" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtMOF" runat="server" TextMode="Search" CssClass="input_Text"   placeholder="Ministry of Finance"></asp:TextBox> 
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
                <div>
                    <label id="lblDOB" class="desc">Date Of Birth: </label>
                    <div>
                        <asp:Panel ID="Panel13" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtDOB" runat="server" TextMode="Date" CssClass="input_Text"   placeholder="Date Of Birth"></asp:TextBox> 
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
                <div>
                    <label id="lblCity" class="desc">City: </label>
                    <div>
                        <asp:Panel ID="Panel14" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlCity" runat="server" CssClass="input_Text"   placeholder="City">
                                        <asp:ListItem disabled Selected Value="1">----------------------</asp:ListItem>
                                    </asp:DropDownList>
                                    <ajaxToolkit:ListSearchExtender runat="server" BehaviorID="ddlCity_ListSearchExtender"
                                        TargetControlID="ddlCity" ID="ListSearchExtender3">
                                    </ajaxToolkit:ListSearchExtender> 
                                </ContentTemplate>
                                <Triggers>
                                    <%--  <asp:AsyncPostBackTrigger ControlID="ddlCity" EventName="SelectedIndexChanged" />--%>
                                </Triggers>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
                <div>
                    <label id="lblZone" class="desc">Zone: </label>
                    <div>
                        <asp:Panel ID="Panel15" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlZone" runat="server" CssClass="input_Text"   placeholder="Zone">
                                        <asp:ListItem disabled Selected Value="1">----------------------</asp:ListItem> 
                                        </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
                <div>
                    <label id="lblAddress" class="desc">Address: </label>
                    <div>
                        <asp:Panel ID="Panel16" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtAddress" runat="server" TextMode="Multiline" CssClass="input_Text"   placeholder="Address"></asp:TextBox> 
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
                <div>
                    <label id="lblUBalance" class="desc">Balance ($):</label>
                    <div>
                        <asp:Panel ID="Panel17" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtUBalance" runat="server" TextMode="Number" CssClass="input_Text"   placeholder=" Balance ($)"></asp:TextBox> 
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
                <div>
                    <label id="lblLBalance" class="desc">Balance (LBP):</label>
                    <div>
                        <asp:Panel ID="Panel18" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtLBalance" runat="server" TextMode="Number" CssClass="input_Text"   placeholder=" Balance (LBP)"></asp:TextBox> 
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
                <div>
                    <label id="lblGender" class="desc">Gender:</label>
                    <div>
                        <asp:Panel ID="Panel19" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlGender" runat="server" CssClass="input_Text"   placeholder="Type">
                                        <asp:ListItem disabled Selected Value="NULL">----------------------</asp:ListItem>
                                        <asp:ListItem Value="Male" style="font-size: 200%;">♂</asp:ListItem>
                                        <asp:ListItem Value="Female" style="font-size: 200%;">♀</asp:ListItem>
                                    </asp:DropDownList>
                                    <ajaxToolkit:ListSearchExtender runat="server" BehaviorID="ddlGender_ListSearchExtender"
                                        TargetControlID="ddlGender" ID="ListSearchExtender4">
                                    </ajaxToolkit:ListSearchExtender>
                                    <span style="margin-left: 1%;" id="span18" runat="server">*</span>
                                </ContentTemplate>
                                <Triggers>
                                    <%--  <asp:AsyncPostBackTrigger ControlID="ddlGender" EventName="SelectedIndexChanged" />--%>
                                </Triggers>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
                <div>
                    <label id="lblFB" class="desc">Facebook: </label>
                    <div>
                        <asp:Panel ID="Panel20" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtFacebook" runat="server" TextMode="Search" CssClass="input_Text"   placeholder="@Facebook_Name"></asp:TextBox> 
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
                <div>
                    <label id="lblSalesman" class="desc">Salesman:</label>
                    <div>
                        <asp:Panel ID="Panel21" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlSalesman" runat="server" CssClass="input_Text"   placeholder="Type">
                                        <asp:ListItem disabled Selected Value="0">----------------------</asp:ListItem>
                                    </asp:DropDownList>
                                    <ajaxToolkit:ListSearchExtender runat="server" BehaviorID="ddlSalesman_ListSearchExtender"
                                        TargetControlID="ddlSalesman" ID="ListSearchExtender5">
                                    </ajaxToolkit:ListSearchExtender> 
                                </ContentTemplate>
                                <Triggers>
                                    <%--  <asp:AsyncPostBackTrigger ControlID="Salesman" EventName="SelectedIndexChanged" />--%>
                                </Triggers>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
                <div>
                    <label id="lblExt1" class="desc">Extension: </label>
                    <div>
                        <asp:Panel ID="Panel22" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtExtension" runat="server" TextMode="Number" CssClass="input_Text"   placeholder="Extension"></asp:TextBox> 
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
                <div>
                    <label id="lblBCat" class="desc">Category Bundle: </label>
                    <div>
                        <asp:Panel ID="Panel24" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlBCat" runat="server" CssClass="input_Text"   placeholder="Type">
                                        <asp:ListItem disabled Selected Value="2">----------------------</asp:ListItem>
                                    </asp:DropDownList>
                                    <ajaxToolkit:ListSearchExtender runat="server" BehaviorID="ddlBCat_ListSearchExtender"
                                        TargetControlID="ddlBCat" ID="ListSearchExtender6">
                                    </ajaxToolkit:ListSearchExtender> 
                                </ContentTemplate>
                                <Triggers>
                                    <%--  <asp:AsyncPostBackTrigger ControlID="Salesman" EventName="SelectedIndexChanged" />--%>
                                </Triggers>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
                <div>
                     <div>
                        <asp:Panel ID="Panel23" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                                <ContentTemplate>
                                    <asp:CheckBox ID="cbOA" runat="server"   class="checkbox"  Text="On Account " />
                                    <asp:CheckBox ID="cbActive" runat="server"   class="checkbox"  Text="Active " />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
                <div id="imgdiv" runat="server">
                        <asp:Panel ID="Panel12" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                <ContentTemplate>
                                    <div id="gallery">
                                        <label runat="server" id="lblimg" class="desc">Image:</label>
                                        <div>
                                            <asp:DropDownList ID="ddlImage" runat="server" CssClass="input_Text"
                                                AutoPostBack="True">
                                                <asp:ListItem Value="NaN">-------------------</asp:ListItem>
                                            </asp:DropDownList>

                                            <asp:Image ID="img" runat="server" Height="50px" Width="50px" ImageUrl="../assets/img/Loading.gif" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlimage" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
            </div>
            </div>
              </div>
            <%--            -----------------------------------------------------------------------------------------------------------------------------------------%>
            <div id="panelppp" class="PopOut">
                <div>
                    <label id="lblED" class="desc">Enrollment Date: </label>
                    <div>
                        <asp:Panel ID="Panel25" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtED" runat="server" TextMode="Date" CssClass="input_Text"   placeholder="Enrollment Date" Text=""></asp:TextBox> 
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
                <div>
                    <label id="lblENDD" class="desc">Ending Date: </label>
                    <div>
                        <asp:Panel ID="Panel26" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel28" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtENDD" runat="server" TextMode="Date" CssClass="input_Text"   placeholder="Ending Date" Text=""></asp:TextBox> 
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
                <div>
                    <label id="lblSSN" class="desc">Social Status (#): </label>
                    <div>
                        <asp:Panel ID="Panel27" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel29" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtSSN" runat="server" TextMode="Number" CssClass="input_Text"   placeholder="Social Status (#)"></asp:TextBox> 
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
                <div>
                    <label id="lblIDCardNo" class="desc">ID Card (#): </label>
                    <div>
                        <asp:Panel ID="Panel28" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel30" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtIDCardNo" runat="server" TextMode="Number" CssClass="input_Text"   placeholder="ID Card (#)"></asp:TextBox> 
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
                <div>
                    <label id="lblRegNo" class="desc">Registration (#): </label>
                    <div>
                        <asp:Panel ID="Panel29" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel31" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtRegistrationNo" runat="server" TextMode="Number" CssClass="input_Text"   placeholder=" Registration (#)"></asp:TextBox> 
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
                <div>
                    <label id="lblHasInsurance" class="desc">Insurance: </label>
                    <div>
                        <asp:Panel ID="Panel30" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel32" runat="server">
                                <ContentTemplate>
                                    <asp:CheckBox ID="cbInsurance" runat="server" Style="margin-left: 5%;" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
                <div>
                    <label id="lblInsuranceName" class="desc">Insurance Name: </label>
                    <div>
                        <asp:Panel ID="Panel31" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel33" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtInsuranceName" runat="server" TextMode="Search" CssClass="input_Text" placeholder="Insurance Name"></asp:TextBox> 
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
                <div>
                    <label id="lblInsuranceClass" class="desc">Insurance Class: </label>
                    <div>
                        <asp:Panel ID="Panel32" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel34" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtInsuranceClass" runat="server" TextMode="Search" CssClass="input_Text"   placeholder="Insurance Class"></asp:TextBox> 
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
                <div>
                    <label id="lblNSSF" class="desc">National Social Security Fund: </label>
                    <div>
                        <asp:Panel ID="Panel33" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel35" runat="server">
                                <ContentTemplate>
                                    <asp:CheckBox ID="cbNSSF" runat="server" Style="margin-left: 5%;" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
                <div>
                    <label id="lblNSSFNo" class="desc">National Social Security Fund (#): </label>
                    <div>
                        <asp:Panel ID="Panel34" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel36" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtNSSFNo" runat="server" TextMode="Number" CssClass="input_Text"   placeholder="National Social Security Fund (#)"></asp:TextBox> 
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel38" runat="server">
        <ContentTemplate>
            <div id="Buttons">
                <asp:Panel ID="Panel_Admin_Buttons" runat="server" CssClass="Panels_Responsive">
                    <p>
                        <button runat="server" id="btnAdd" class="btn btn-xs btn-success" onserverclick="Addition_Func"
                            style="font-size: 150%; margin-right: 10%;">
                            <i class="fa fa-plus-square-o" style="margin-right: 5px;"></i>Add</button>
                        <button runat="server" id="btnEdit" class="btn btn-xs btn-warning" onserverclick="Edition_Func"
                            style="font-size: 150%; margin-right: 10%;">
                            <i class="fa fa-pencil-square-o" style="margin-right: 5px;"></i>Edit</button>
                        <button runat="server" id="btn" class="btn btn-xs btn-danger" onserverclick="Deletion_Func"
                            style="font-size: 150%; margin-right: 10%;">
                            <i class="fa fa-minus" style="margin-right: 5px;"></i>Delete</button>
                        <button runat="server" id="btnCancel" class="btn btn-xs btn-info" onserverclick="Cancel_Func"
                            style="font-size: 150%; margin-right: 10%;">
                            <i class="fa fa-refresh" style="margin-right: 5px;"></i>Cancel</button>
                    </p>
                </asp:Panel>
            </div>
            <div>
                <asp:UpdatePanel ID="UpdatePanel39" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvData" runat="server" RowStyle-Wrap="false" CssClass="table-overflow"
                            AutoGenerateColumns="true" AutoGenerateSelectButton="True" ForeColor="Black" AllowPaging="True" AllowSorting="True" PageSize="5"  style="margin-left: 15%;">
                            <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
