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


//-------------------------CS Code---------------------------//
  //_____________________________________________________________________________________// 
                //Success Section//
                Global.Status = "Success,Success,Added Successfully";
                Global.DesignWarningColor(DivStatus, StatusType, lblStrongStatus, lblStatus, this);
                return;
                //EO Success Section//   
            }
            catch (Exception ex)
            {
                //_____________________________________________________________________________________// 
                //Failure Section//
                Global.Status = "Warning,Warning,Couldn't Complete the Requested Action";
                Global.DesignWarningColor(DivStatus, StatusType, lblStrongStatus, lblStatus, this);
                return;
                //EO Failure Section//   
            }