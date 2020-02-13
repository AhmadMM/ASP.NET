<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="frmBookDetails.aspx.cs" Inherits="BCCBookStore.frmBookDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <section id="content-holder" class="container-fluid container">
    <section class="row-fluid">
    	<div class="heading-bar">
        	<h2>Books Details</h2>
            <span class="h-line"></span>
        </div>
        <!-- Start Main Content -->
        <section class="span12 first">
            <section class="grid-holder features-books">

                <asp:Repeater runat="server" ID="dsFillBooks">
                    <ItemTemplate>
                      
                        <article class="item-holder">
                	<div class="span2">
                        <a>
                            <asp:Image runat="server" ID="imgBook" ImageUrl='<%# Bind("Content")%>' alt="" /></a>
                    </div>
                            <div class="span10">
                                <div class="title-bar"><a>
                                    <asp:Label runat="server" ID="lblBookName" Text='<%# Bind("BookName")%>'></asp:Label></a> <span>by
                                        <asp:Label runat="server" ID="lblAuthor" Text='<%# Bind("Author")%>'></asp:Label></span></div>
                                <span class="rating-bar">
                                    <img alt="Rating Star" src="images/rating-star.png"></span>
                                <p>   <asp:Label runat="server" ID="lblNotes" Text='<%# Bind("Notes")%>'></asp:Label></p>
                                <div class="cart-price">
                                    <a href="cart.html" class="cart-btn2">Add to Cart</a>
                                    <span class="price">
                                        <asp:Label runat="server" ID="lblPrice" Text='<%# Bind("BookPrice")%>'></asp:Label>$</span>
                                </div>
                            </div>
                        </article>
                    </ItemTemplate>
                </asp:Repeater>

            </section>

            <!-- End Grid View Section -->
            
        </section>
        <!-- End Main Content -->
      
    </section>
  </section>

</asp:Content>
