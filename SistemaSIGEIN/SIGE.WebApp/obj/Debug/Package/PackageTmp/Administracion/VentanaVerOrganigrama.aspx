<%@ Page Title="" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="VentanaVerOrganigrama.aspx.cs" Inherits="SIGE.WebApp.Administracion.VentanaVerOrganigrama" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div style="height: 10px; clear: both;"></div>
        <label class="labelTitulo">Organigrama</label>
        <table class="ctrlTableForm">
            <tr>
                <td class="ctrlTableDataContext">
                    <label id="lbPuesto" name="lbPuesto" runat="server">Puesto:</label>
                </td>
                <td class="ctrlTableDataBorderContext">
                    <div id="txtClPuesto" runat="server"></div>
                </td>
            </tr>
        </table>
         <div style="height: calc(100% - 120px); overflow: auto;">
        <div style="position: relative;">
            <telerik:RadOrgChart ID="rocPuestos" runat="server" DisableDefaultImage="true" Height="100%" OnNodeDataBound="rocPuestos_NodeDataBound">
                <RenderedFields>
                    <ItemFields>
                        <telerik:OrgChartRenderedField DataField="nbItem" />
                    </ItemFields>
                </RenderedFields>
            </telerik:RadOrgChart>
            <telerik:RadContextMenu runat="server" ID="RadContextMenu1">
                <Items>
                    <telerik:RadMenuItem Text="Editar" Value="Puesto" />
                    <telerik:RadMenuItem Text="Editar" Value="Empleado" />
                </Items>
            </telerik:RadContextMenu>
        </div>
             </div>
   <telerik:RadWindowManager ID="rwMensaje" runat="server"></telerik:RadWindowManager>
</asp:Content>
