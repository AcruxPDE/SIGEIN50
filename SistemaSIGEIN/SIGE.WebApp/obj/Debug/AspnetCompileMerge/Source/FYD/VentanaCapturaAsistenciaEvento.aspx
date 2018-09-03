<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="VentanaCapturaAsistenciaEvento.aspx.cs" Inherits="SIGE.WebApp.FYD.VentanaCapturaAsistenciaEvento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script type="text/javascript">
        function closeWindow(oWnd, args) {
            GetRadWindow().close();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel runat="server" ID="ralpAsistencia"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager runat="server" ID="ramAsistencia" DefaultLoadingPanelID="ralpAsistencia">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnCalcular">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgAsistencia" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="txtAsistenciaGrupal" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <div style="clear: both; height: 5px;"></div>

    <%--<div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label>Evento:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadTextBox runat="server" ID="txtEvento" Width="200px" Enabled="false"></telerik:RadTextBox>
        </div>
    </div>--%>
    <div class="ctrlBasico">
                    <table class="ctrlTableForm">
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Evento:</label></td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="txtEvento" runat="server" style="min-width: 100px;"></div>
                            </td>
                        </tr>
                    </table>
                </div>

   <%-- <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label>Horas del curso:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadTextBox runat="server" ID="txtHorasCurso" Width="50px" Enabled="false">
                <DisabledStyle HorizontalAlign="Right" />
            </telerik:RadTextBox>
        </div>
    </div>--%>

     <div class="ctrlBasico">
                    <table class="ctrlTableForm">
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Horas del curso:</label></td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="txtHorasCurso" runat="server" style="min-width: 100px;"></div>
                            </td>
                        </tr>
                    </table>
                </div>
    <div style="clear: both; height: 10px;"></div>

    <div style="overflow: auto; height: calc(100% - 170px);">
        <telerik:RadGrid runat="server" ID="rgAsistencia" HeaderStyle-Font-Bold="true" OnNeedDataSource="rgAsistencia_NeedDataSource" AutoGenerateColumns="false" Width="100%" Height="100%">
            <ClientSettings>
                <Scrolling AllowScroll="true" UseStaticHeaders="true"></Scrolling>
            </ClientSettings>
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView EditMode="InPlace" DataKeyNames="ID_EVENTO_PARTICIPANTE">
                <Columns>
                    <telerik:GridBoundColumn UniqueName="CL_PARTICIPANTE" DataField="CL_PARTICIPANTE" HeaderText="Clave" ReadOnly="true">
                        <HeaderStyle Width="50px" />
                        <ItemStyle Width="50px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="NB_PARTICIPANTE" DataField="NB_PARTICIPANTE" HeaderText="Nombre" ReadOnly="true">
                        <HeaderStyle Width="400px" />
                        <ItemStyle Width="400px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn UniqueName="NO_TIEMPO" DataField="NO_TIEMPO" HeaderText="Horas cumplidas">
                        <ItemStyle Width="50px" />
                        <HeaderStyle Width="50px" />
                        <ItemTemplate>
                            <telerik:RadNumericTextBox runat="server" ID="txtTiempo" Width="100%" AutoPostBack="false" Text='<%# Bind("NO_TIEMPO") %>' NumberFormat-DecimalDigits="0" ShowSpinButtons="false">
                                <EnabledStyle HorizontalAlign="Right" />
                            </telerik:RadNumericTextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn UniqueName="PR_CUMPLIMIENTO" DataField="PR_CUMPLIMIENTO" HeaderText="% de asistencia" ReadOnly="true" >
                        <HeaderStyle Width="50px" />
                        <ItemStyle Width="50px" HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>

                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>

    <div style="clear: both; height: 10px;"></div>
    <div class="ctrlBasico" style="text-align: center">
        <label>Promedio de asistencia grupal:</label>
    </div>
    <div class="ctrlBasico">
        <telerik:RadNumericTextBox runat="server" ID="txtAsistenciaGrupal" NumberFormat-DecimalDigits="0" AutoPostBack="false" ReadOnly="true" ReadOnlyStyle-HorizontalAlign="Right" Width="80px"></telerik:RadNumericTextBox>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton runat="server" ID="btnCalcular" Text="Recalcular" AutoPostBack="true" OnClick="btnCalcular_Click"></telerik:RadButton>
    </div>
    <div style="clear: both; height: 10px;"></div>
    <div class="divControlesBoton">
        <telerik:RadButton runat="server" ID="btnAceptar" Text="Aceptar" OnClick="btnAceptar_Click"></telerik:RadButton>
        <telerik:RadButton runat="server" ID="btnCancelar" Text="Cancelar" AutoPostBack="false" OnClientClicked="closeWindow"></telerik:RadButton>
    </div>
    <telerik:RadWindowManager ID="rwmAsistencia" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
