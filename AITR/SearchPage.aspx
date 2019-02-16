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
                    <strong>Last Name</strong>
                    <asp:TextBox  runat="server" MaxLength="20"></asp:TextBox>
                </p>
                <p>
                    <strong>Given Name</strong>
                    <asp:TextBox  runat="server" MaxLength="20"></asp:TextBox>
                </p>
                <p>
                <strong>Banks</strong></p>
                <p>
                    <asp:CheckBoxList runat="server">
                    </asp:CheckBoxList>
                </p>
                <p>
                    <strong>Bank Services Used</strong></p>
                <p>
                    <asp:CheckBoxList runat="server">
                    </asp:CheckBoxList>
                </p>
                <p>
                    <asp:Button ID="SearchButton" runat="server" Text="Search" />
                </p>
                <p>
                    &nbsp;</p>
                <p>
                    <asp:GridView  runat="server">
                    </asp:GridView>
                </p>
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
