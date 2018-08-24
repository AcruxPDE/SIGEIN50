<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="ComentariosEntrevista.aspx.cs" Inherits="SIGE.WebApp.IDP.Solicitud.ComentariosEntrevista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">


    <div style="clear: both; height: 10px;"></div>

    <table class="ctrlTableForm">
        <tr>
            <td class="ctrlTableDataContext">
                <label>Clave de la requisición: </label>
            </td>
            <td class="ctrlTableDataBorderContext">
                <span runat="server" id="txtClaveRequisicion" style="width: 300px;"></span>
            </td>
        </tr>
        <tr>
            <td class="ctrlTableDataContext">
                <label>Puesto al que aplica el candidato: </label>
            </td>
            <td class="ctrlTableDataBorderContext">
                <span runat="server" id="txtPuestoAplicar" style="width: 300px;"></span>
            </td>
        </tr>
        <tr>
            <td class="ctrlTableDataContext">
                <label>Nombre del candidato: </label>
            </td>
            <td class="ctrlTableDataBorderContext">
                <span runat="server" id="txtCandidato" style="width: 300px;"></span>
            </td>
        </tr>
    </table>


    <div style="clear: both; height: 10px;"></div>


    <label>Comentarios de la entrevista: </label>
    <div class="ctrlBasico">
        <telerik:RadEditor Height="300px" Width="100%" ToolsWidth="500" EditModes="Design" ID="txtDsNotas" runat="server" ToolbarMode="ShowOnFocus" ToolsFile="~/Assets/AdvancedTools.xml"></telerik:RadEditor>
    </div>

    <div style="clear: both; height: 10px;"></div>

    <div class="divControlDerecha">
        <div class="ctrlBasico">
            <telerik:RadButton runat="server" ID="btnGuardar" UseSubmitBehavior="false" Text="Guardar" OnClick="btnGuardar_Click"></telerik:RadButton>
        </div>

        <div class="ctrlBasico">
            <telerik:RadButton runat="server" ID="btnCancelar" Text="Cancelar" OnClick="btnCancelar_Click"></telerik:RadButton>
        </div>
    </div>

    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>

</asp:Content>
