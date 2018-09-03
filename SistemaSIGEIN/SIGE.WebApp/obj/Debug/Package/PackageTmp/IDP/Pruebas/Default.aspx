<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/Pruebas/Prueba.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SIGE.WebApp.IDP.Pruebas.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headPruebas" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPruebas" runat="server">
    <div style="height: calc(100% - 100px); overflow:auto;">
        <div style="width:50%; margin:auto;" > 
        <div style="width:100%; margin-left:30%; margin-top:30%;" >
            <img src="../../Assets/images/LogotipoNombre.png" style="width: 40%;" />
        </div>
            <%-- <div style="width:100%;">
        <h1 style="text-align:center;">
            Gracias por completar las pruebas.
        </h1>
                 </div>--%>
            <div id="txtMensajeDespedida" runat="server" style="width: 98%; text-align: center; margin-left: 5px; margin-right: 5px; font-family:Arial; text-anchor:start; font-size:larger;"></div>
    </div>
    </div>
      <div style="clear:both; height:10px;"></div>
     <div style="height:90px;">
     </div>
    <telerik:RadWindowManager ID="rwmAlertas" runat="server"></telerik:RadWindowManager>
</asp:Content>
