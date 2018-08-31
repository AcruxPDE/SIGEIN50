<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="VentanaPlanVidaCarrera.aspx.cs" Inherits="SIGE.WebApp.FYD.VentanaPlanVidaCarrera" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">

    <style>
        .PrioridadAlta {
            background-color: red;
            text-align: center;
            color: white;
        }

        .PrioridadIntermedia {
            background-color: gold;
            text-align: center;
        }

        .PrioridadBaja {
            background-color: green;
            text-align: center;
        }

        .PrioridadOtra {
            background-color: lightgray;
            text-align: center;
        }


        
        .divRojo {
            height: 80%;
            width: 15px;
            border-radius: 5px;
            background: red;
        }

        .divAmarillo {
            height: 80%;
            width: 15px;
            border-radius: 5px;
            background: gold;
        }

        .divVerde {
            height: 80%;
            width: 15px;
            border-radius: 5px;
            background: green;
        }

         .divGris {
            height: 80%;
            width: 15px;
            border-radius: 5px;
            background: gray;
        }

        
        table.tablaColor {
            width: 100%;
        }

        td.porcentaje {
            width: 80%;
            text-align:left;
            padding: 1px;
        }

        td.color {
            width: 10%;
            padding: 1px;
        }


         /*.PivotVida td.rpgColumnHeaderZone {
            width: 100px !important;
        }*/

    </style>

    <script type="text/javascript">

        function OpenEmpleadoWindow() {

            var idEmpleado = <%# vIdEmpleado %>

            OpenSelectionWindow("../Administracion/Empleado.aspx?EmpleadoId=" + idEmpleado, "winEditarEmpleado", "Editar empleado");
        }

        function OpenPuestoWindow() {

            var idPuesto = <%# vIdPuesto %>

            OpenSelectionWindow("../Administracion/VentanaVistaDescriptivo.aspx?PuestoId=" + idPuesto, "winEditarPuesto", "Vista previa descripción del puesto");
        }

        function OpenDescriptivo(pIdPuesto) {
            var vURL = "../Administracion/VentanaVistaDescriptivo.aspx";
            var vTitulo = "Vista previa descripción del puesto";

            vURL = vURL + "?PuestoId=" + pIdPuesto;

            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 20,
                height: browserWnd.innerHeight - 20
            };

            openChildDialog(vURL, "winEditarPuesto", vTitulo, windowProperties);
        }

        function lstPuestosDoubleClickItem(sender, args) {
            var item = args.get_item();
            var idPuestoSeleccionado = item.get_value();

            OpenSelectionWindow("../Administracion/VentanaVistaDescriptivo.aspx?PuestoId=" + idPuestoSeleccionado, "winEditarPuesto", "Vista previa descripción del puesto");
        }

        function OpenSelectionWindow(pURL, pIdWindow, pTitle) {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 20,
                height: browserWnd.innerHeight - 20
            };

            openChildDialog(pURL, pIdWindow, pTitle, windowProperties)
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">


    <div style="height: 10px;"></div>
    <telerik:RadTabStrip runat="server" MultiPageID="mpPlanVidaCarrera" SelectedIndex="0" AutoPostBack="false">

        <Tabs>
            <telerik:RadTab Text="Datos generales"></telerik:RadTab>
            <telerik:RadTab Text="Datos de competencias"></telerik:RadTab>
            <telerik:RadTab Text="Datos de perfil del puesto"></telerik:RadTab>
        </Tabs>

    </telerik:RadTabStrip>
    <div style="height: 10px;"></div>
    <div style="height: calc(100% - 65px);">

        <telerik:RadMultiPage runat="server" ID="mpPlanVidaCarrera" SelectedIndex="0" Height="100%" BorderWidth="0">

            <telerik:RadPageView ID="pvGeneral" runat="server">

                <div style="clear: both; height: 10px;"></div>

                <div style="overflow: auto;">

                    <div class="ctrlBasico" style="width: 60%;">

                       <%-- <div class="ctrlBasico">
                            <label style="width: 140px;">Empleado</label>
                            <telerik:RadTextBox CssClass="ctrlBasico" runat="server" ID="txtClaveEmpleado" ReadOnly="true" Width="200px"></telerik:RadTextBox>
                            <telerik:RadTextBox CssClass="ctrlBasico" runat="server" ID="txtNombreEmpleado" ReadOnly="true" Width="500px"></telerik:RadTextBox>
                            <telerik:RadButton runat="server" ID="btnEditarEmpleado" Text="B" Width="35px" AutoPostBack="false" OnClientClicked="OpenEmpleadoWindow" />
                        </div>--%>

                    <div class="ctrlBasico">
                    <table class="ctrlTableForm">
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Empleado</label></td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="txtClaveEmpleado" runat="server" style="min-width: 100px;"></div>
                            </td>
                            <td class="ctrlTableDataBorderContext">
                                <div id="txtNombreEmpleado" runat="server" width="170" maxlength="1000" enabled="false"></div>
                            </td>
                          <td class="ctrlTableDataContext">
                                <telerik:RadButton runat="server" ID="btnEditarEmpleado" Text="V" Width="35px" AutoPostBack="false" OnClientClicked="OpenEmpleadoWindow" />
                            </td>
                        </tr>
                    </table>
                </div>
              <%-- <div class="ctrlBasico">
               
                </div>--%>
           <%-- <div style="clear: both; height: 10px;"></div>--%>
                        <%--<div class="ctrlBasico">
                            <label style="width: 140px;">Puesto Actual</label>
                            <telerik:RadTextBox CssClass="ctrlBasico" runat="server" ID="txtClavePuesto" ReadOnly="true" Width="200px"></telerik:RadTextBox>
                            <telerik:RadTextBox CssClass="ctrlBasico" runat="server" ID="txtNombrePuesto" ReadOnly="true" Width="500px"></telerik:RadTextBox>
                            <telerik:RadButton runat="server" ID="btnEditarPuesto" Text="B" Width="35px" AutoPostBack="false" OnClientClicked="OpenPuestoWindow" />
                        </div>--%>
                     <div class="ctrlBasico">
                    <table class="ctrlTableForm">
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Puesto Actual</label></td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="txtClavePuesto" runat="server" style="min-width: 100px;"></div>
                            </td>
                            <td class="ctrlTableDataBorderContext">
                                <div id="txtNombrePuesto" runat="server" width="170" maxlength="1000" enabled="false"></div>
                            </td>
                             <td class="ctrlTableDataContext">
                                <telerik:RadButton runat="server" ID="RadButton1" Text="V" Width="35px" AutoPostBack="false" OnClientClicked="OpenPuestoWindow" />
                            </td>
                        </tr>
                    </table>
                </div>
                <%--  <div class="ctrlBasico">
                <telerik:RadButton runat="server" ID="btnEditarPuesto" Text="B" Width="35px" AutoPostBack="false" OnClientClicked="OpenPuestoWindow" />
                 </div>
              <div style="clear: both; height: 10px;"></div>--%>

                       <%-- <div class="ctrlBasico">
                            <label style="width: 140px;">Período</label>
                            <telerik:RadTextBox CssClass="ctrlBasico" runat="server" ID="txtPeriodo" ReadOnly="true" Width="600px"></telerik:RadTextBox>
                        </div>--%>
                       <div class="ctrlBasico">
                    <table class="ctrlTableForm">
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Periodo</label></td>
                            <td class="ctrlTableDataBorderContext">
                                <div id="txtPeriodo" runat="server" width="170" maxlength="1000" enabled="false"></div>
                            </td>
                             <td class="ctrlTableDataContext">
                                <label>Tipo de Evaluación</label></td>
                            <td class="ctrlTableDataBorderContext">
                                <div id="txtTipoEvaluacion" runat="server" width="170" maxlength="1000" enabled="false"></div>
                            </td>
                        </tr>
                    </table>
                </div>

                       <%-- <div class="ctrlBasico">
                            <label style="width: 140px;">Tipo de Evaluación</label>
                            <telerik:RadTextBox CssClass="ctrlBasico" runat="server" ID="txtTipoEvaluacion" ReadOnly="true" Width="600px"></telerik:RadTextBox>
                        </div>--%>

               <%--   <div class="ctrlBasico">
                    <table class="ctrlTableForm">
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Tipo de Evaluación</label></td>
                            <td class="ctrlTableDataBorderContext">
                                <div id="txtTipoEvaluacion" runat="server" width="170" maxlength="1000" enabled="false"></div>
                            </td>
                        </tr>
                    </table>
                </div>--%>
                        <div style="clear: both; height: 10px;"></div>
                        <div class="ctrlBasico">
                            <fieldset>
                                <legend>
                                    <label>Puestos a comparar:</label></legend>

                                <div style="padding: 5px">
                                    <div>
                                        <telerik:RadListBox runat="server" ID="lstPuestos" AllowDelete="false" ButtonSettings-ShowDelete="false" Width="490" OnClientItemDoubleClicked="lstPuestosDoubleClickItem">
                                        </telerik:RadListBox>
                                    </div>
                                </div>
                            </fieldset>
                        </div>

                    </div>

                    <div class="ctrlBasico" style="width: 40%;">

                        <table style="width: 200px; padding-left: 5px;">
                            <tr>
                                <td>
                                    <label>Alta:</label>
                                </td>
                                <td>
                                    <div style="height: 20px; width: 100%; background-color: red;"></div>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <label>Intermedia:</label></td>
                                <td>
                                    <div style="height: 20px; width: 100%; background-color: gold;"></div>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <label>No necesaria:</label></td>
                                <td>
                                    <div style="height: 20px; width: 100%; background-color: green;"></div>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <label>No calificada:</label></td>
                                <td>
                                    <div style="height: 20px; width: 100%; background-color: lightgray;">
                                        <label style="text-align: center; width: 100%;">N/C</label>
                                    </div>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <label>No aplica:</label></td>
                                <td>
                                    <div style="height: 20px; width: 100%; background-color: lightgray;">
                                        <label style="text-align: center; width: 100%;">N/A</label>
                                    </div>
                                </td>
                            </tr>
                        </table>

                    </div>

                </div>

                <div style="clear: both; height: 10px;"></div>

                <telerik:RadGrid runat="server" HeaderStyle-Font-Bold="true" ID="grdCompetencias" ShowHeader="false" OnNeedDataSource="grdCompetencias_NeedDataSource" AutoGenerateColumns="false">
                    <MasterTableView>
                        <Columns>
                            <telerik:GridBoundColumn ItemStyle-Width="150" DataField="Key" UniqueName="Value"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn ItemStyle-Width="500" DataField="Value" UniqueName="Value"></telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>

            </telerik:RadPageView>

            <telerik:RadPageView ID="pvCompetencias" runat="server">

                <telerik:RadGrid runat="server" Height="100%" HeaderStyle-Font-Bold="true" ID="rgCompetencias" AutoGenerateColumns="true" OnNeedDataSource="rgCompetencias_NeedDataSource" ShowFooter="true" OnColumnCreated="rgCompetencias_ColumnCreated" OnItemDataBound="rgCompetencias_ItemDataBound">
                    <ClientSettings>
                        <Scrolling UseStaticHeaders="true" AllowScroll="true" SaveScrollPosition="true" />
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                </telerik:RadGrid>

               <%-- <telerik:RadPivotGrid runat="server" ID="pgCompetencias" OnNeedDataSource="pgCompetencias_NeedDataSource" RowTableLayout="Tabular" EmptyValue="N/A" OnCellDataBound="pgCompetencias_CellDataBound" 
                    ShowDataHeaderZone="false" ShowFilterHeaderZone="false" ShowRowHeaderZone="false" AllowFiltering="false" AllowSorting="false" ShowColumnHeaderZone="false">
                    <TotalsSettings ColumnsSubTotalsPosition="None" RowGrandTotalsPosition="Last" RowsSubTotalsPosition="None" ColumnGrandTotalsPosition="None" />
                    <Fields>
                        <telerik:PivotGridRowField Caption="" DataField="CL_COLOR">
                            <CellStyle Width="30px" />
                            <CellTemplate>
                                <div style="height: 100%; border-radius: 5px; background: <%# Container.DataItem.ToString() %>;">
                                    <br />
                                    <br />
                                </div>
                            </CellTemplate>
                        </telerik:PivotGridRowField>

                        <telerik:PivotGridRowField Caption="Factor" DataField="NB_COMPETENCIA">
                            <CellStyle Width="200px" />
                        </telerik:PivotGridRowField>

                        <telerik:PivotGridRowField Caption="Descripción" DataField="DS_COMPETENCIA"></telerik:PivotGridRowField>

                        <telerik:PivotGridColumnField Caption="Puestos" DataField="CL_PUESTO"></telerik:PivotGridColumnField>

                        <telerik:PivotGridAggregateField Aggregate="Average" DataField="PR_NO_COMPATIBILIDAD">
                            
                            <CellStyle Width="50px" />
                            <CellTemplate>
                                <div runat="server" id="divPromedio" style="width: 95%; margin-bottom: auto; margin-top: auto; float: left; text-align: right;">
                                    <%# String.Format("{0:N2}", Container.DataItem) %>
                                </div>
                                <div runat="server" id="divNa" style="width: 95%; margin-bottom: auto; margin-top: auto; float: left; text-align: right;">
                                    N/A
                                </div>
                                <div runat="server" id="divNc" style="width: 95%; margin-bottom: auto; margin-top: auto; float: left; text-align: right;">
                                    N/C
                                </div>
                                <div runat="server" id="divColorComparacion" style="float: right; height: 80%; width: 10px; border-radius: 5px;">
                                    <br />
                                    <br />
                                </div>
                            </CellTemplate>

                        </telerik:PivotGridAggregateField>
                    </Fields>
                </telerik:RadPivotGrid>--%>


            </telerik:RadPageView>

            <telerik:RadPageView ID="pvPuestos" runat="server">
                <div style="clear: both; height: 10px;"></div>
                <div class="ctrlBasico" style="width: 100%;">
                    <telerik:RadGrid runat="server" ID="grdpuestos" HeaderStyle-Font-Bold="true" OnNeedDataSource="grdpuestos_NeedDataSource" OnColumnCreated="grdpuestos_ColumnCreated" OnItemDataBound="grdpuestos_ItemDataBound">
                    </telerik:RadGrid>
                </div>

            </telerik:RadPageView>

        </telerik:RadMultiPage>

    </div>

    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true">
    </telerik:RadWindowManager>

</asp:Content>
