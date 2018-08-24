<%@ Page Title="" Language="C#" MasterPageFile="~/PDE/ContextPDE.master" AutoEventWireup="true" CodeBehind="VentanaNuevoComentario.aspx.cs" Inherits="SIGE.WebApp.PDE.VentanaNuevoComentario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function closeWindow() {
                GetRadWindow().close();
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div style="clear: both; height: 10px;"></div>
    <div>
        <telerik:RadTextBox ID="txtComentario"
            TextMode="MultiLine"
            runat="server"
            Width="100%"
            Height="170px">
        </telerik:RadTextBox>
    </div>
    <div style="clear: both; height: 10px;"></div>
    <div class="divControlDerecha">
        <telerik:RadButton ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" AutoPostBack="true">
        </telerik:RadButton>
        <telerik:RadButton ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelarAgregar_Click" AutoPostBack="True">
        </telerik:RadButton>
    </div>
    <telerik:RadWindowManager ID="rnMensaje" runat="server"></telerik:RadWindowManager>
</asp:Content>
