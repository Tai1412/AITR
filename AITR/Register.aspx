<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="AITR.Register" %>

<!DOCTYPE html>
<form id="form2" runat="server">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register</title>
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
                </div>  
            </div>
        </div>
    </nav>
        <div class="container w-50 h-auto  mt-5 mb-5 p-0" style="background-color:#ABABAB">
            <!-- Default form register -->
            <div class="text-center border border-light p-5">

                <p class="text-left" style="font-size:20px;"><i class="fas fa-user-plus green-text"></i>   Create Free Account </p>

                 <!-- DOB -->
                <asp:TextBox runat="server" type="text" id="ageTextBox" class="form-control mb-4" placeholder="Your Age"></asp:TextBox>

                <!-- Phone number -->
                <asp:TextBox runat="server" TextMode="Number" id="phoneNumberTextBox" class="form-control mb-4" placeholder="Your Phone Number"></asp:TextBox>
                <div class="row mb-4">
                    <div class="col">
                        <!-- First name -->
                        <asp:TextBox runat="server" type="text" id="giveNameTextBox" class="form-control mb-4" placeholder="Your Given Name"></asp:TextBox>
                    </div>
                    <div class="col">
                        <!-- Last name -->
                        <asp:TextBox runat="server" type="text" id="lastNameTextBox" class="form-control mb-4" placeholder="Your Last Name"></asp:TextBox>
                    </div>
                </div>

                <!-- Sign up button -->
                <asp:Button class="btn btn-success my-4 btn-block"  style="font-size:18px;" ID="RegisterButton" runat="server" Text="Register" OnClick="RegisterButton_Click" />
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
