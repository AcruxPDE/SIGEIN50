<%@ Page Title="" Language="C#" MasterPageFile="~/EO/ContextEO.master" AutoEventWireup="true" CodeBehind="VentanaAdjuntarEvidenciaMetas.aspx.cs" Inherits="SIGE.WebApp.VentanaAdjuntarEvidenciaMetas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script>
        function OnCloseWindow() {
            GetRadWindow().close();
        }
        function closeWindow() {
            GetRadWindow().close();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div style="height: calc(100% - 80px);">
        <div style="clear: both; height: 10px;"></div>
        <table class="ctrlTableForm">
            <tr>
                <td class="ctrlTableDataContext">
                    <label>Meta:</label></td>
                <td colspan="2" class="ctrlTableDataBorderContext">
                    <div id="txtMeta" runat="server"></div>
                </td>

            </tr>

        </table>
        <div class="ctrlBasico">
            Tipo documento:<br />
            <telerik:RadComboBox ID="cmbTipoDocumento" runat="server">
                <Items>
                    <%--<telerik:RadComboBoxItem Text="Fotografía" Value="FOTOGRAFIA" />--%>
                    <telerik:RadComboBoxItem Text="Imagen" Value="IMAGEN" />
                    <telerik:RadComboBoxItem Text="Otro" Value="OTRO" />
                </Items>
            </telerik:RadComboBox>
        </div>
        <div class="ctrlBasico">
            Subir documento:<br />
            <telerik:RadAsyncUpload ID="rauDocumento" runat="server" MultipleFileSelection="Disabled"></telerik:RadAsyncUpload>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnAgregarDocumento" runat="server" Text="Agregar" OnClick="btnAgregarDocumento_Click"></telerik:RadButton>
        </div>
        <div style="clear: both;"></div>
        <div class="ctrlBasico">
            <telerik:RadGrid ID="grdDocumentos" HeaderStyle-Font-Bold="true" runat="server" Width="580" OnNeedDataSource="grdDocumentos_NeedDataSource">
                <ClientSettings>
                    <Scrolling UseStaticHeaders="true" />
                    <Selecting AllowRowSelect="true" />
                </ClientSettings>
                <GroupingSettings CaseSensitive="false" />
                <MasterTableView ClientDataKeyNames="ID_ARCHIVO,ID_ITEM" DataKeyNames="ID_ARCHIVO,ID_ITEM" AutoGenerateColumns="false" ShowHeadersWhenNoRecords="true">
                    <Columns>
                        <telerik:GridHyperLinkColumn HeaderText="Nombre del documento" DataTextField="NB_DOCUMENTO" DataNavigateUrlFields="ID_ARCHIVO,ID_DOCUMENTO,FE_CREATED_DATE,NB_DOCUMENTO,ID_ITEM" DataNavigateUrlFormatString="/Comunes/ObtenerDocumento.ashx?ArchivoId={0}&ArchivoNb={2:yyyyMMdd}{4}&ArchivoDescargaNb={3}" Target="_blank"></telerik:GridHyperLinkColumn>
                        <telerik:GridBoundColumn HeaderText="Tipo de documento" HeaderStyle-Width="200" DataField="CL_TIPO_DOCUMENTO" UniqueName="CL_TIPO_DOCUMENTO"></telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnDelDocumentos" runat="server" Text="Eliminar" OnClick="btnDelDocumentos_Click"></telerik:RadButton>
        </div>
    </div>
    <div class="divControlDerecha">

        <div style="clear: both; height: 20px;"></div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" AutoPostBack="true" OnClick="btnGuardar_Click"></telerik:RadButton>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnCancelar" runat="server" Text="Cancelar" AutoPostBack="false" OnClientClicked="OnCloseWindow"></telerik:RadButton>
        </div>
    </div>


    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
