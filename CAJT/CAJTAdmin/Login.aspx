<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="CAJTAdmin.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login To Adminpage</title>
    <%--Stylesheet--%>
    <link href="css/styleAdmin.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />

    <%--Scripts--%>
    <script src="Scripts/jquery-1.9.0.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="container">

            <div id="loginbox" class="mainbox col-md-6 col-md-offset-3 col-sm-6 col-sm-offset-3">

                <div class="row">
                    <div class="col-sm-4">
                    </div>
                    <div class="col-sm-4">
                        <img src="images/icon.png" />
                    </div>
                    <div class="col-sm-4">
                    </div>
                </div>

                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="panel-title text-center">Kontorsprylar AB</div>
                    </div>

                    <div class="panel-body">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                            <asp:TextBox ID="userName" CssClass="form-control" placeholder="Username" runat="server"></asp:TextBox>
                        </div>

                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
                            <asp:TextBox ID="password" CssClass="form-control" placeholder="Password" runat="server"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <!-- Button -->
                            <div class="col-sm-12 controls button">
                                <a href="/index.aspx" class="btn btn-default pull-right">Log in</a>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
