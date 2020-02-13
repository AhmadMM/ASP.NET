
    <%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
	  <asp:Panel runat="server" ID="PanelDescAutoComplete" Height="100px" ScrollBars="Vertical" Style="overflow-y: auto; overflow-x:hidden;text-overflow: ellipsis">
                                                      </asp:Panel>
  <div class="row">
                        <div class="col-md-5">
                            <div class="form-group label-floating">
                                <label class="control-label">Customer Name</label>
                                <asp:TextBox runat="server" ID="txtCustomerName" class="form-control"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtCustomerName"
                                    MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000"
                                    ServiceMethod="GetCustomers" CompletionListElementID="PanelDescAutoComplete">
                                </asp:AutoCompleteExtender>
                            </div>
                        </div>
                    </div>