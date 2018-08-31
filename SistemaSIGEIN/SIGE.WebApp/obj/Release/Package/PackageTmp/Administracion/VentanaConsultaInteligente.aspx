<%@ Page Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="VentanaConsultaInteligente.aspx.cs" Inherits="SIGE.WebApp.Administracion.VentanaConsultaInteligente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        <script type="text/javascript">
            function closeWindow() {
                if (GetRadWindow() != null)
                    GetRadWindow().close();
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div style="height: calc(100% - 30px); padding: 10px 0px 0px 0px;">
        <table>
            <tr>
                <td>
                    <telerik:RadLabel ID="RadLabel1" runat="server" Text="Nombre vista inteligente:" Width="165"></telerik:RadLabel>
                </td>
                <td>
                    <telerik:RadTextBox ID="txtNombre" runat="server" Width="178"></telerik:RadTextBox></td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <telerik:RadLabel ID="RadLabel2" runat="server" Text="Selecccionar archivo:"></telerik:RadLabel>
                </td>
                <td>
                    <telerik:RadAsyncUpload ID="rauSubirArchivo" MaxFileInputsCount="1" runat="server" MultipleFileSelection="Disabled" AllowedFileExtensions=".twbx"></telerik:RadAsyncUpload>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <telerik:RadLabel ID="rbArchivo" runat="server" Text="" ForeColor="Blue"></telerik:RadLabel>
                </td>
            </tr>
        </table>
        <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click"></telerik:RadButton>
    </div>
    <telerik:RadWindowManager ID="rwmAlertas" runat="server"></telerik:RadWindowManager>
</asp:Content>
