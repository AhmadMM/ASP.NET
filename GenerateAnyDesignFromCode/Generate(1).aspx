  <asp:DataList ID="dsFillMessages" runat="server">
                                            <ItemTemplate>
                                                <div class="lv-item media">
                                                    <div class="lv-avatar pull-left">
                                                    </div>
                                                    <div class="media-body">
                                                        <div class="ms-item">
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Person")%>' Style="color: #f0ae13;"></asp:Label>
                                                            <p>
                                                                <asp:Label ID="lblMessage" runat="server" Text='<%# Bind("Message") %>'></asp:Label>
                                                                <small class="ms-date"><span class="fa fa-clock-o"></span>
                                                                    <asp:Label ID="lblDate" runat="server" Text='<%# Bind("DateTime")%>' CssClass="time"></asp:Label></small>
                                                            </p>
                                                        </div>

                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:DataList>