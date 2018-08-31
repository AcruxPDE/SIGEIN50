<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="VentanaEvaluacionCompetenciasPS.aspx.cs" Inherits="SIGE.WebApp.FYD.VentanaEvaluacionCompetenciasPS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script type="text/javascript">

        function OpenInventario(pIdEmpleado) {
            var vURL = "../Administracion/Empleado.aspx";
            var vTitulo = "Ver Empleado";

            vURL = vURL + "?EmpleadoId=" + pIdEmpleado + "&pFgHabilitaBotones=False";

            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {};
            windowProperties.width = document.documentElement.clientWidth - 20;
            windowProperties.height = document.documentElement.clientHeight - 20;

            openChildDialog(vURL, "winSeleccion", vTitulo, windowProperties);
        }

        function OpenSelectionWindow(pURL, pIdWindow, pTitle) {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 40,
                height: browserWnd.innerHeight - 40
            };

            openChildDialog(pURL, pIdWindow, pTitle, windowProperties)
        }

        function OpenPuesto(idPuesto) {
            OpenSelectionWindow("../Administracion/VentanaVistaDescriptivo.aspx?PuestoId=" + idPuesto, "winSeleccion", "Vista previa descripción del puesto");
        }

        function OpenEventoPeriodo(pId, pIdEvaluado) {
            if (pId != null) {
                if (pIdEvaluado == 0) {
                    OpenSelectionWindow("VentanaEventoReporteParticipacion.aspx" + String.format("?IdEvento={0}", pId), "winSeleccion", "Reporte Evaluación de Resultados");
                }
                else {
                    if (pIdEvaluado != null) {
                        OpenSelectionWindow("ReporteIndividual.aspx" + String.format("?IdPeriodo={0}", pId) + String.format("&IdEvaluado={0}", pIdEvaluado), "winSeleccion", "Reporte Individual");
                    }
                }
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div style="height: 10px;"></div>
    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="mpPlanVidaCarrera" SelectedIndex="0" AutoPostBack="false">
        <Tabs>
            <telerik:RadTab Text="Datos generales"></telerik:RadTab>
            <telerik:RadTab Text="Evaluación de competencias"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <div style="height: calc(100% - 60px);">
        <telerik:RadMultiPage runat="server" ID="mpPlanVidaCarrera" SelectedIndex="0" Height="100%" BorderWidth="0">
            <telerik:RadPageView ID="pvGeneral" runat="server">
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico">
                    <fieldset>
                        <legend>
                            <label>Empleado a suplir:</label></legend>
                        <div style="padding: 5px">
                            <div class="ctrlBasico">
                                <telerik:RadGrid runat="server" ID="rgSuceder" AutoGenerateColumns="false" OnItemCreated="rgSuceder_ItemCreated">
                                       <MasterTableView DataKeyNames="M_PUESTO_ID_PUESTO,M_EMPLEADO_ID_EMPLEADO,M_PUESTO_CL_PUESTO, M_PUESTO_NB_PUESTO, M_EMPLEADO_CL_EMPLEADO, M_EMPLEADO_NB_EMPLEADO_COMPLETO " >
                                        <Columns>
                                            <telerik:GridHyperLinkColumn UniqueName="M_EMPLEADO_NB_EMPLEADO_COMPLETO" DataTextField="M_EMPLEADO_NB_EMPLEADO_COMPLETO" DataNavigateUrlFields="M_EMPLEADO_ID_EMPLEADO" HeaderText="Empleado" DataNavigateUrlFormatString="javascript:OpenInventario({0})"></telerik:GridHyperLinkColumn>
                                            <telerik:GridHyperLinkColumn UniqueName="M_PUESTO_NB_PUESTO" DataTextField="M_PUESTO_NB_PUESTO" DataNavigateUrlFields="M_PUESTO_ID_PUESTO" HeaderText="Puesto" DataNavigateUrlFormatString="javascript:OpenPuesto({0})"></telerik:GridHyperLinkColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                        </div>
                    </fieldset>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <div class="ctrlBasico">
                    <fieldset>
                        <legend>
                            <label>Candidato a sucesión:</label></legend>
                        <div style="padding: 5px">
                            <div class="ctrlBasico">
                                <telerik:RadGrid runat="server" ID="rgSucesor" AutoGenerateColumns="false" OnItemCreated="rgSucesor_ItemCreated">
                                     <MasterTableView DataKeyNames="M_PUESTO_ID_PUESTO,M_EMPLEADO_ID_EMPLEADO,M_PUESTO_CL_PUESTO, M_PUESTO_NB_PUESTO, M_EMPLEADO_CL_EMPLEADO, M_EMPLEADO_NB_EMPLEADO_COMPLETO " >
                                        <Columns>
                                            <telerik:GridHyperLinkColumn UniqueName="M_EMPLEADO_NB_EMPLEADO_COMPLETO" DataTextField="M_EMPLEADO_NB_EMPLEADO_COMPLETO" DataNavigateUrlFields="M_EMPLEADO_ID_EMPLEADO" HeaderText="Empleado" DataNavigateUrlFormatString="javascript:OpenInventario({0})"></telerik:GridHyperLinkColumn>
                                            <telerik:GridHyperLinkColumn UniqueName="M_PUESTO_NB_PUESTO" DataTextField="M_PUESTO_NB_PUESTO" DataNavigateUrlFields="M_PUESTO_ID_PUESTO" HeaderText="Puesto" DataNavigateUrlFormatString="javascript:OpenPuesto({0})"></telerik:GridHyperLinkColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                        </div>
                    </fieldset>
                </div>
                <div style="height: 15px; clear: both;"></div>
                <telerik:RadGrid runat="server" ID="grdCompetencias" ShowHeader="false" OnNeedDataSource="grdCompetencias_NeedDataSource" AutoGenerateColumns="false">
                    <MasterTableView>
                        <Columns>
                            <telerik:GridBoundColumn ItemStyle-Width="150" DataField="Key" UniqueName="Value"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn ItemStyle-Width="500" DataField="Value" UniqueName="Value"></telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </telerik:RadPageView>
            <telerik:RadPageView ID="pvCompetencias" runat="server">
                <telerik:RadGrid ID="grdGeneralIndividual" HeaderStyle-Font-Bold="true" runat="server" Height="100%" AutoGenerateColumns="false"
                    OnNeedDataSource="grdGeneralIndividual_NeedDataSource" OnItemCreated="grdGeneralIndividual_ItemCreated" ShowFooter="true" AllowMultiRowSelection="true" AllowPaging="false">
                    <ClientSettings EnablePostBackOnRowClick="false">
                        <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <MasterTableView ClientDataKeyNames="ID_COMPETENCIA" DataKeyNames="ID_COMPETENCIA, DS_EVENTO, CL_EVENTO" EnableColumnsViewState="false" AllowFilteringByColumn="false" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="" AllowFiltering="false">
                                <ItemStyle Height="60" Width="30" />
                                <HeaderStyle Height="60" Width="30" />
                                <ItemTemplate>
                                    <div class="ctrlBasico" style="height: 60px; width: 30px; float: left; background: <%#Eval("CL_COLOR") %>; border-radius: 5px;"></div>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="140" HeaderText="Factor" DataField="NB_COMPETENCIA" UniqueName="NB_COMPETENCIA"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="300" FilterControlWidth="240" HeaderText="Descripción" DataField="DS_COMPETENCIA" UniqueName="DS_COMPETENCIA" FooterText="Total:" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="templateCompatibilidad" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Center">
                                <ItemStyle Width="90" />
                                <HeaderStyle Width="90" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <div class="ctrlBasico" style="width: 100%;">
                                        <table style="width: 100%;" border="0">
                                            <tr>
                                                <td style="width: 40%;">
                                                    <div class="ctrlBasico" style="height: 30px; width: 15px; float: left; background: <%#Eval("CL_COLOR_COMPATIBILIDAD") %>; border-radius: 5px;"></div>
                                                </td>
                                                <td style="width: 60%;"><a style="float: right;" href="javascript:OpenEventoPeriodo(<%# Eval("ID_EVENTO") %>, <%# Eval("ID_EVALUADO") %>)"><%#Eval("PR_COMPATIBILIDAD") %> %</a></td>
                                            </tr>
                                        </table>
                                    </div>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-HorizontalAlign="Center" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="60" FilterControlWidth="30" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:0}" HeaderText="Perfil de puesto a suceder" DataField="NO_VALOR_NIVEL" UniqueName="NO_VALOR_NIVEL"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="80" FilterControlWidth="40" HeaderText="Fecha de evaluación" DataFormatString="{0:dd/MM/yyyy}" DataField="FECHA" UniqueName="FECHA"></telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </div>
</asp:Content>
