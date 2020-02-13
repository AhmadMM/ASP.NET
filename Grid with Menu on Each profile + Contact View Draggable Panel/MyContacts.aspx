<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="MyContacts.aspx.cs" Inherits="POSWeb.MyContants_aspx" %>

<%@ Register TagPrefix="oem" Namespace="OboutInc.EasyMenu_Pro" Assembly="obout_EasyMenu_Pro" %>

<%@ Register Assembly="obout_SuperForm" Namespace="Obout.SuperForm" TagPrefix="cc4" %>

<%@ Import Namespace="System.Data.OleDb" %>

<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>

<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc1" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript"> 

        function attachMenuToRecords(arrSelectedRecords) {

            var gridContainerID = "<%=grid1.ClientID%>_ob_grid1MainContainer";

            // attach menu to grid container
            ob_em_EasyMenu1.attachToControl(gridContainerID);
            for (var i = 0; i < arrSelectedRecords.length; i++) {
                var record = arrSelectedRecords[i];
                document.getElementById("ValueID").value = record.PersonID; 
            }
            // hide menu on clicking the grid container
            document.getElementById(gridContainerID).onclick = function () {
                ob_em_EasyMenu1.hideMenu();
            }

            gridIds = grid1.getRecordsIds().split(",");
            for (index = 0; index < gridIds.length; index++) {
                var rowId = gridIds[index];

                // attach menu to each grid row
                ob_em_EasyMenu1.attachToControl(rowId);

                var rowCells = document.getElementById(rowId).childNodes;
                   
                for (elIndex = 0; elIndex < rowCells.length; elIndex++) {
                    // stop the event propagation when click on grid cells to avoid rows unselection
                    rowCells[elIndex].onmousedown = function (e) {
                        var event = e || window.event;
                        // stop event propagation on right mouse click
                        if (event.button == 2) {
                            if (event.stopPropagation) { event.stopPropagation(); } else { event.cancelBubble = true; }
                        }
                    }
                }
            } 
        }

           function CallServerEvent(argument) {
               // store the clicked item id
               document.getElementById('<%=hdVal.ClientID %>').value = argument;
            // call the server side event (same as button click)
    <%= Page.ClientScript.GetPostBackEventReference(this.btnCompromise, "") %>
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="msg" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div id='container'  onclick='event.stopImmediatePropagation()' >   
    <obout:Grid ID="grid1" runat="server" CallbackMode="true"  
        AllowColumnResizing="true" ShowHeader="true" PageSize="10" ShowFooter="true" AllowMultiRecordSelection="true" AutoGenerateColumns="false" > 
        <ClientSideEvents  OnClientSelect="attachMenuToRecords" /> 
       <Columns>
            <obout:Column DataField="PersonID" HeaderText="Order ID" Width="100" runat="server"  Visible="false"/>
            <obout:Column DataField="FullName" HeaderText="Order ID" Width="100" runat="server" />
            <obout:Column DataField="Type" HeaderText="Name" Width="350" runat="server" />
            <obout:Column DataField="PhoneNumber" HeaderText="City" Width="115" runat="server" />
            <obout:Column DataField="MobileNumber" HeaderText="Country" Width="115" runat="server" />
            <obout:Column DataField="Email" HeaderText="Country" Width="115" runat="server" />
            <obout:Column DataField="Website" HeaderText="Country" Width="115" runat="server" />
            <obout:Column DataField="FullAddress" HeaderText="Country" Width="115" runat="server" />
            <obout:Column DataField="Active" HeaderText="Country" Width="115" runat="server" />
        </Columns>
    </obout:Grid> 
  
    <div id="mydiv" runat="server" visible="false" class="divVisible" onmousedown='mydragg.startMoving(this,"container",event);' onmouseup='mydragg.stopMoving("container");'  >
  <div id="mydivheader">Click here to move</div>
        <div class="well form-horizontal" id="MyProfileForm">
            <fieldset>
                <!-- Text input-->

                <div class="form-group">
                    <label class="col-md-4 control-label">First Name</label>
                    <div class="col-md-4 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                            <asp:TextBox ID="txtFN" runat="server" placeholder="First Name" class="form-control" TextMode="Search" style="width:100%"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <!-- Text input-->

                <div class="form-group">
                    <label class="col-md-4 control-label">Last Name</label>
                    <div class="col-md-4 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                            <asp:TextBox ID="txtLN" runat="server" placeholder="Last Name" class="form-control" TextMode="Search" style="width:100%"></asp:TextBox>
                        </div>  
                    </div>
                </div>

                <!-- Text input-->
                <div class="form-group">
                    <label class="col-md-4 control-label">E-Mail</label>
                    <div class="col-md-4 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-envelope"></i></span>
                            <asp:TextBox ID="txtEmail" runat="server" placeholder="E-Mail Address" class="form-control" TextMode="Search" style="width:100%"></asp:TextBox>
                        </div>
                    </div>
                </div>


                <!-- Text input-->

                <div class="form-group">
                    <label class="col-md-4 control-label">Mobile Phone #</label>
                    <div class="col-md-4 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-earphone"></i></span>
                            <asp:TextBox ID="txtMN" runat="server" placeholder="(845)555-1212" class="form-control" TextMode="Search" style="width:100%"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label">Fixed Phone #</label>
                    <div class="col-md-4 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-earphone"></i></span>
                            <asp:TextBox ID="txtPN" runat="server" placeholder="(845)555-1212" class="form-control" TextMode="Search" style="width:100%"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <!-- Text input-->

                <div class="form-group">
                    <label class="col-md-4 control-label">Address</label>
                    <div class="col-md-4 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-home"></i></span>
                            <asp:TextBox ID="txtAddress" runat="server" placeholder="Address" class="form-control" TextMode="Search" style="width:100%"></asp:TextBox>
                        </div> 
                    </div>
                </div>

                <!-- Text input-->
                <div class="form-group">
                    <label class="col-md-4 control-label">Website</label>
                    <div class="col-md-4 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-globe"></i></span>
                            <asp:TextBox ID="txtWebsite" runat="server" placeholder="Website or domain name" class="form-control" TextMode="Search" style="width:100%"></asp:TextBox>
                        </div>
                    </div>
                </div>
               
            </fieldset>
        </div>
    </div>   
     <script>
         var mydragg = function () {
             return {
                 move: function (divid, xpos, ypos) {
                     divid.style.left = xpos + 'px';
                     divid.style.top = ypos + 'px';
                 },
                 startMoving: function (divid, container, evt) {
                     evt = evt || window.event;
                     var posX = evt.clientX,
                         posY = evt.clientY,
                     divTop = divid.style.top,
                     divLeft = divid.style.left,
                     eWi = parseInt(divid.style.width),
                     eHe = parseInt(divid.style.height),
                     cWi = parseInt(document.getElementById(container).style.width),
                     cHe = parseInt(document.getElementById(container).style.height);
                     document.getElementById(container).style.cursor = 'move';
                     divTop = divTop.replace('px', '');
                     divLeft = divLeft.replace('px', '');
                     var diffX = 0,
                         diffY =0;
                     document.onmousemove = function (evt) {
                         evt = evt || window.event;
                         var posX = evt.clientX,
                             posY = evt.clientY,
                             aX = posX - diffX,
                             aY = posY - diffY;
                         if (aX < 0) aX = 0;
                         if (aY < 0) aY = 0;
                         if (aX + eWi > cWi) aX = cWi - eWi;
                         if (aY + eHe > cHe) aY = cHe - eHe;
                         mydragg.move(divid, aX, aY);
                     }
                 },
                 stopMoving: function (container) {
                     var a = document.createElement('script');
                     document.getElementById(container).style.cursor = 'default';
                     document.onmousemove = function () { }
                 },
             }
         }();

        </script>
<asp:Button ID="btnCompromise" runat="server" OnClick="btnCompromise_Click" Text="Button" style="display:none" />
   <input type="hidden" id="ValueID" name="GridID" value="" />
    <input type="hidden" id="hdVal" runat="server" />
    <oem:EasyMenu ID="EasyMenu1" runat="server" Width="200">
        <Components>
            <oem:MenuItem ID="menuItem2"  OnClientClick="CallServerEvent('menuItem2') "
                InnerHtml="View Profile">
            </oem:MenuItem>
            <oem:MenuItem ID="menuItem3" OnClientClick="orderInformation();"
                InnerHtml="Order Information">
            </oem:MenuItem>
            <oem:MenuItem ID="menuItem4" OnClientClick="orderDate();"
                InnerHtml="Order Date">
            </oem:MenuItem>
        </Components>
    </oem:EasyMenu> 
</div>
</asp:Content>
