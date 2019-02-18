<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchPage.aspx.cs" Inherits="AITR.SearchPage" %>

<!DOCTYPE html>
<form id="form1" runat="server">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>Search Page</title>
    <link href="Content/myStyle.css" rel="stylesheet" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.6.3/css/all.css"/>
    <!-- Bootstrap core CSS -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.2.1/css/bootstrap.min.css" rel="stylesheet"/>
    <!-- Material Design Bootstrap -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/mdbootstrap/4.7.0/css/mdb.min.css" rel="stylesheet"/>
</head>
<body style="background-color:#99E394;">
   <nav class="navbar top white" style="height:118px;margin:0px;padding:0px;">
        <div class="row justify-content-between w-100" style="margin:0px">
            <div class="col navbar-brand" style="font-family: 'Cherry Cream Soda', cursive; font-size:55px; color:#12A6C7; margin-left:64px;">
                AITR
            </div>
            <div class="col justify-content-end" style="margin:0px" id="navbarTogglerDemo01">
                <div class="row justify-content-end mr-2">    
                    <asp:Label ID="currentStaff" runat="server" Text=""></asp:Label>
                    <asp:Button class="btn btn-white black-text" style="width:95px;height:43px;padding:0px;margin-top:30px;" runat="server" Text="Log Out" OnClick="logoutButton_Click" ID="logoutButton" />
                </div>  
            </div>
        </div>
    </nav>
        <div class="container w-50 h-auto  mt-5 mb-5 p-0" style="background-color:#ABABAB">
            <!-- Default form register -->
            <div class="text-center border border-light p-5">
                <p>
                    <h1>Search Page</h1>
                    Search criteria

                </p>
                 <p>
                <strong>Gender</strong>
                </p>
                <p>
                    <asp:RadioButtonList  runat="server" ID="genderRadioButtonList">
                        <asp:ListItem value="1">Male</asp:ListItem>
                        <asp:ListItem value="2">Female</asp:ListItem>
                    </asp:RadioButtonList>
                </p>
                <p>
                <strong>Bank Service</strong>
                </p>
                <p>
                    <asp:CheckBoxList  runat="server" ID="bankServiceCheckBoxList">
                        <asp:ListItem value="15">Internet Banking</asp:ListItem>
                        <asp:ListItem value="16">Home Loan</asp:ListItem>
                         <asp:ListItem value="17">Credit Card</asp:ListItem>
                          <asp:ListItem value="18">Share Investment</asp:ListItem>
                    </asp:CheckBoxList>
                </p>
                <p>
                <p>
                <strong>Section Of Newspaper User Read</strong>
                </p>
                <p>
                    <asp:RadioButtonList  runat="server" ID="sectionRadioButtonList">
                        <asp:ListItem value="21">Sport</asp:ListItem>
                        <asp:ListItem value="22">Travel</asp:ListItem>
                    </asp:RadioButtonList>
                </p>
                 <p>
                <strong>Travel</strong>
                </p>
                <p>
                    <asp:CheckBoxList  runat="server" ID="travelCheckBoxList">
                        <asp:ListItem value="27">Australia</asp:ListItem>
                        <asp:ListItem value="28">Pacific</asp:ListItem>
                        <asp:ListItem value="29">Europe</asp:ListItem>
                        <asp:ListItem value="30">Asia</asp:ListItem>
                    </asp:CheckBoxList>
                </p>
                <p>
                <p>
                    <asp:Button ID="SearchButton" runat="server" Text="Search" OnClick="SearchButton_Click" />
                </p>
                <asp:Label ID="warningTextBox" runat="server" style="color:red"></asp:Label>
                <p>
                    &nbsp;</p>
                <div class="col-6">
                    <table class="table table-bordered" style="background-color:white;width:100%;">
                        <thead>
                            <tr>
                                <th>Given Name</th>
                                <th>Last Name</th>
                                <th>Phone</th>
                                <th>Age</th>
                                <th>User IP Address</th>
                            </tr>
                        </thead>
                        <tbody id="tableBodySearch" runat="server">
        
                        </tbody>
                    </table>
                </div>
            </div>
            <!-- Default form register -->
        </div>
    <!-- Footer -->
    <footer class="page-footer white bottom" style="width:100%;height:76px;">

      <!-- Copyright -->
      <div class="footer-copyright white text-center pt-5" style="height:100%; color:black; font-size:16px; font-family: 'Cherry Cream Soda', cursive;">Copyright © 2018 AITR 2018
      </div>
      <!-- Copyright -->

    </footer>
    <!-- Footer -->
</body>
</html>
</form>
