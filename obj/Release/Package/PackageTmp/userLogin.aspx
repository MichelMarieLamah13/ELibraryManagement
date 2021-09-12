<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="userLogin.aspx.cs" Inherits="ELibraryManagement.userlogin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container">
      <div class="row">
         <div class="col-md-6 mx-auto">
            <div class="card">
               <div class="card-body">
                  <div class="row">
                     <div class="col">
                        <center>
                           <img width="150px" src="imgs/generaluser.png"/>
                        </center>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col">
                        <center>
                           <h3>Member Login</h3>
                        </center>
                     </div>
                  </div>
                   <div class="row">
                            <div class="col-md-12">
                                <hr>
                            </div>
                            <div class="col-md-12">
                                 <% if (lstMsg.Count != 0)
                                {%>
                                    <div class="alert alert-<%=txtClasse %> alert-dismissible fade show m-auto" role="alert">
                                         <h4 class="alert-heading"><%=txtClasse.ToUpper() %></h4>
                                         <hr>
                                         <div class="mb-0">
                                             <ul class="list-group">
                                                 <% foreach(String e in lstMsg) {%>
                                                   <li class="list-group-item list-group-item-<%=txtClasse %>"><strong><%=e %></strong></li>
                                                 <% }%>
                                             </ul>
                                         </div>
                                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                                    <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <hr />
                            <% } %>
                            </div>
                        </div>
                  <div class="row">
                     <div class="col">
                        <label>Member ID</label>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" placeholder="Member ID"></asp:TextBox>
                        </div>
                        <label>Password</label>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
                        </div>
                        <div class="form-group">
                           <asp:Button class="btn btn-success btn-block btn-lg" ID="Button1" runat="server" Text="Login" OnClick="Button1_Click" />
                        </div>
                        <div class="form-group">
                           <a href="userSignUp.aspx"><input class="btn btn-info btn-block btn-lg" id="Button2" type="button" value="Sign Up" /></a>
                        </div>
                     </div>
                  </div>
               </div>
            </div>
            <a href="homePage.aspx"><< Back to Home</a><br><br>
         </div>
      </div>
   </div>
</asp:Content>
