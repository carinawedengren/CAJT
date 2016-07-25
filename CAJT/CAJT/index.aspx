<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="CAJT.index" %>


<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">

    <div id="content">
        <h1>Välkommen till Kontorsprylar AB !</h1>

        <div class="headerLineContainer"><h2>Favoritkategorier</h2></div>

        <div class="row indexproduct">
            <div class="col-sm-6 col-md-4">
                <a href="#">
                    <img src="images/inktoner.jpg" alt="toner" height="350" width="350">
                </a>
                <div class="caption">
                    <h3>Bläck & Toner</h3>
                </div>
            </div>
            <div class="col-sm-6 col-md-4">
                <a href="#">
                    <img src="images/emballage.jpg" alt="toner" height="350" width="350">
                </a>
                <div class="caption">
                    <h3>Emballage</h3>
                </div>
            </div>
            <div class="col-sm-6 col-md-4">
                <a href="#">
                    <img src="images/pen.jpg" alt="toner" height="350" width="350">
                </a>
                <div class="caption">
                    <h3>Pennor</h3>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
