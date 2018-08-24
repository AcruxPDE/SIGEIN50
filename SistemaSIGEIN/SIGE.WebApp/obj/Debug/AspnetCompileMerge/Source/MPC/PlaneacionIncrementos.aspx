<%@ Page Title="" Language="C#" MasterPageFile="~/MPC/ContextMC.master" AutoEventWireup="true" CodeBehind="PlaneacionIncrementos.aspx.cs" Inherits="SIGE.WebApp.MPC.PlaneacionIncrementos1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <style>
        .RadGrid1Class {
            background-color: #FFFFFF;
        }

        .RadGrid2Class {
            background-color: #E6E6FA;
        }
    </style>
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script type="text/javascript">

            function OpenEmployeeSelectionWindow() {
                openChildDialog("SeleccionTabuladorEmpleado.aspx?&IdTabulador=" + <%=vIdTabulador%> +"&vClTipoSeleccion=TABULADOR", "winSeleccion", "Selección de empleados")
            }

            function AbrirVentanaIncremento() {
                var vIdTabulador = "<%=vIdTabulador%>";
                openChildDialog("VentanaIncrementoTabulador.aspx?IdTabulador=" + vIdTabulador, "winIncrementoGeneral", "Denifir incremento general")
            }

            function onCloseWindow(oWnd, args) {
                GetRadWindow().close();
            }

            function CloseWindow(oWnd, args) {
                $find("<%=grdPlaneacionIncrementos.ClientID%>").get_masterTableView().rebind();
            }

            function ConfirmarGuardar(sender, args) {
                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                { if (shouldSubmit) { this.click(); } });

                radconfirm('Se afectará el inventario de Empleados y el Historial, aplicando a la columna de Sueldo nominal nuevo (no la de Sueldo nominal actual). Si cuentas con una interfaz de nómina no es recomendable seleccionar esta opción. ¿Estás seguro que deseas continuar?', callBackFunction, 400, 250, null, "Aviso");
                args.set_cancel(true);
            }

            function useDataFromChild(pDato) {
                if (pDato != null) {
                    console.info(pDato);
                    var vDatosSeleccionados = pDato[0];
                    var arr = [];
                    switch (vDatosSeleccionados.clTipoCatalogo) {
                        case "TABULADOR_EMPLEADO":
                            for (var i = 0; i < pDato.length; ++i) {
                                arr.push(pDato[i].idEmpleado);
                            }
                            var datos = JSON.stringify(arr);
                            __doPostBack(vDatosSeleccionados.clTipoCatalogo, datos);
                            break;
                        case "UPDATE":
                            __doPostBack(vDatosSeleccionados.clTipoCatalogo);
                            break;
                    }
                }
            }
        </script>

    </telerik:RadCodeBlock>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdPlaneacionIncrementos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPlaneacionIncrementos" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtNominaActual" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtNominaPlaneada" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtDiferencia" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtDiferenciaPr" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <%-- <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdPlaneacionIncrementos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPlaneacionIncrementos" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>--%>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnRecalcular">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPlaneacionIncrementos" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cmbCuartilIncremento">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cmbCuartilIncremento" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="grdPlaneacionIncrementos" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPlaneacionIncrementos" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="txnSueldoNuevo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPlaneacionIncrementos" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadTabStrip ID="rtsTabuladorMaestro" runat="server" SelectedIndex="0" MultiPageID="rmpPlaneacionIncrementos">
        <Tabs>
            <telerik:RadTab Text="Contexto" SelectedIndex="0"></telerik:RadTab>
            <telerik:RadTab Text="Planeación de incrementos" SelectedIndex="1"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="rmpPlaneacionIncrementos" runat="server" SelectedIndex="0" Height="90%">
        <telerik:RadPageView ID="rpvContexto" runat="server" Height="100%">
            <div style="clear: both; height: 10px;"></div>
            <div class="ctrlBasico">
                <table class="ctrlTableForm">
                    <tr>
                        <td class="ctrlTableDataContext">
                            <label>Versión:</label></td>
                        <td colspan="2" class="ctrlTableDataBorderContext">
                            <div id="txtClTabulador" runat="server" style="min-width: 100px;"></div>
                        </td>
                    </tr>
                    <tr>
                        <td class="ctrlTableDataContext">
                            <label>Descripción:</label></td>
                        <td colspan="2" class="ctrlTableDataBorderContext">
                            <div id="txtNbTabulador" runat="server" style="min-width: 100px;"></div>
                        </td>
                    </tr>
                    <tr>
                        <td class="ctrlTableDataContext">
                            <label>Notas:</label></td>
                        <td colspan="2" class="ctrlTableDataBorderContext">
                            <div id="txtDescripción" runat="server" style="min-width: 100px;"></div>
                        </td>
                    </tr>
                    <tr>
                        <td class="ctrlTableDataContext">
                            <label>Fecha:</label></td>
                        <td colspan="2" class="ctrlTableDataBorderContext">
                            <div id="txtFecha" runat="server" style="min-width: 100px;"></div>
                        </td>
                    </tr>
                    <tr>
                        <td class="ctrlTableDataContext">
                            <label>Vigencia:</label></td>
                        <td colspan="2" class="ctrlTableDataBorderContext">
                            <div id="txtVigencia" runat="server" style="min-width: 100px;"></div>
                        </td>
                    </tr>
                    <tr>
                        <td class="ctrlTableDataContext">
                            <label>Tipo de puestos:</label></td>
                        <td colspan="2" class="ctrlTableDataBorderContext">
                            <div id="txtPuestos" runat="server" style="min-width: 100px;"></div>
                        </td>
                    </tr>
                </table>
            </div>
        </telerik:RadPageView>
        <telerik:RadPageView ID="rpvPLaneacion" runat="server" Height="100%">
            <div style="height: calc(100% - 120px);">
                <telerik:RadSplitter ID="rsDefinicionCriterios" Width="100%" Height="100%" BorderSize="0" runat="server">
                    <telerik:RadPane ID="rpDefinicionCriterios" runat="server">
                        <%-- <div class="ctrlBasico">
                    <label id="lbTabulador"
                        name="lbTabulador"
                        runat="server">
                        Tabulador:</label>
                    <telerik:RadTextBox ID="txtClTabulador"
                        runat="server"
                        Width="150px"
                        MaxLength="800"
                        Enabled="false">
                    </telerik:RadTextBox>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtDsTabulador"
                        runat="server"
                        Width="260px"
                        MaxLength="800"
                        Enabled="false">
                    </telerik:RadTextBox>
                </div>
                <div class="ctrlBasico">
                    <label id="lbFecha"
                        name="lbFecha"
                        runat="server">
                        Fecha:</label>
                    <telerik:RadDatePicker ID="rdpCreacion" Enabled="false" runat="server" Width="140px">
                    </telerik:RadDatePicker>
                </div>
                <div class="ctrlBasico">
                    <label id="lbVigencia"
                        name="lbVigencia"
                        runat="server">
                        Vigencia:</label>
                    <telerik:RadDatePicker ID="rdpVigencia" Enabled="false" runat="server" Width="140px">
                    </telerik:RadDatePicker>
                </div>
                <div style="clear: both"></div>--%>
                        <div class="ctrlBasico">
                            <label id="lblSemaforoVerde"
                                name="lblSemaforoVerde"
                                runat="server">
                                Variación para semáforo verde:</label>
                            <telerik:RadTextBox ID="txtSemaforoVerde"
                                runat="server"
                                Width="50px"
                                MaxLength="800"
                                Enabled="false">
                            </telerik:RadTextBox>
                        </div>
                        <div class="ctrlBasico">
                            <label id="lblSemaforoAmarillo"
                                name="lblSemaforoAmarillo"
                                runat="server">
                                Variación para semáforo amarillo:</label>
                            <telerik:RadTextBox ID="txtSemaforoAmarillo"
                                runat="server"
                                Width="50px"
                                MaxLength="800"
                                Enabled="false">
                            </telerik:RadTextBox>
                        </div>
                        <div class="ctrlBasico">
                            <label id="lblCuartilIncremento"
                                name="lblCuartilIncremento"
                                runat="server">
                                Cuartil del tabulador maestro para incrementos de sueldo:
                            </label>
                        </div>
                        <div class="ctrlBasico">
                            <telerik:RadComboBox Filter="Contains" runat="server" ID="cmbCuartilIncremento" Width="190" MarkFirstMatch="true" AutoPostBack="true"
                                DropDownWidth="190" OnSelectedIndexChanged="cmbCuartilIncremento_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </div>
                        <div style="clear: both;"></div>
                        <div style="height: calc(100% - 45px); overflow: auto;">
                            <telerik:RadGrid ID="grdPlaneacionIncrementos"
                                runat="server"
                                Height="100%"
                                AllowSorting="true"
                                AutoGenerateColumns="false"
                                HeaderStyle-Font-Bold="true"
                                OnNeedDataSource="grdPlaneacionIncrementos_NeedDataSource"
                                OnItemDataBound="grdPlaneacionIncrementos_ItemDataBound">
                                <GroupingSettings CaseSensitive="false" />
                                <ClientSettings>
                                    <Scrolling AllowScroll="true" UseStaticHeaders="true"></Scrolling>
                                </ClientSettings>
                                <PagerStyle AlwaysVisible="true" />
                                <MasterTableView DataKeyNames="ID_TABULADOR_EMPLEADO, NO_NIVEL" AutoGenerateColumns="false" EditMode="InPlace" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true">
                                    <ColumnGroups>
                                        <telerik:GridColumnGroup HeaderText="Tabulador medio" Name="TABMEDIO" HeaderStyle-HorizontalAlign="Center">
                                        </telerik:GridColumnGroup>
                                    </ColumnGroups>
                                    <Columns>
                                        <%--<telerik:GridEditCommandColumn ButtonType="ImageButton" HeaderStyle-Width="40"></telerik:GridEditCommandColumn>--%>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" ReadOnly="true" AllowFiltering="false" HeaderStyle-Width="60" HeaderText="No." DataField="NUM_ITEM" UniqueName="NUM_ITEM" ItemStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" ReadOnly="true" HeaderStyle-Width="180" FilterControlWidth="90" HeaderText="Área/Departamento" DataField="NB_DEPARTAMENTO" UniqueName="NB_DEPARTAMENTO"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" ReadOnly="true" HeaderStyle-Width="210" FilterControlWidth="130" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO"></telerik:GridBoundColumn>
                                        <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" ReadOnly="true" HeaderStyle-Width="210" FilterControlWidth="130" HeaderText="Nombre del ocupante" DataField="NB_EMPLEADO" UniqueName="NB_EMPLEADO"></telerik:GridBoundColumn>--%>
                                        <telerik:GridTemplateColumn AutoPostBackOnFilter="true" ReadOnly="true" HeaderStyle-Width="180" FilterControlWidth="90" HeaderText="Nombre del ocupante" DataField="NB_EMPLEADO" UniqueName="NB_EMPLEADO">
                                            <ItemTemplate>
                                                <span title="<%# Eval("CL_EMPLEADO") %>"><%# Eval("NB_EMPLEADO") %></span>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn AutoPostBackOnFilter="true" ReadOnly="true" HeaderStyle-Width="110" FilterControlWidth="30" HeaderText="Mínimo" DataField="MN_MINIMO_CUARTIL" UniqueName="MN_MINIMO_CUARTIL" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <label id="gbcMinimo" runat="server">
                                                    <%#string.Format("{0:C}",Math.Abs((decimal)Eval("MN_MINIMO_CUARTIL")))%>
                                                </label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn AutoPostBackOnFilter="true" ReadOnly="true" HeaderStyle-Width="110" FilterControlWidth="30" HeaderText="Máximo" DataField="MN_MAXIMO_CUARTIL" UniqueName="MN_MAXIMO_CUARTIL" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <label id="gbcMaximo" runat="server">
                                                    <%#string.Format("{0:C}",Math.Abs((decimal)Eval("MN_MAXIMO_CUARTIL")))%>
                                                </label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" ReadOnly="true" HeaderStyle-Width="110" FilterControlWidth="30" DataFormatString="{0:C}" HeaderText="Sueldo nominal actual" DataField="MN_SUELDO_ORIGINAL" UniqueName="MN_SUELDO_ORIGINAL" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn AutoPostBackOnFilter="false" AllowFiltering="false" ReadOnly="true" HeaderStyle-Width="130" FilterControlWidth="50" HeaderText="Diferencia" DataField="DIFERENCIA" UniqueName="DIFERENCIA" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%#string.Format("{0:N2}",Math.Abs((decimal)Eval("DIFERENCIA")))%>%
                                       <span style="border: 1px solid gray; background: <%# Eval("COLOR_DIFERENCIA") %>; border-radius: 5px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
                                        <img src='/Assets/images/Icons/25/Arrow<%# Eval("ICONO") %>.png' />
                                                </span>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn AutoPostBackOnFilter="true" HeaderStyle-Width="150" FilterControlWidth="70" HeaderText="Sueldo nominal nuevo" DataField="MN_SUELDO_NUEVO" UniqueName="MN_SUELDO_NUEVO" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox runat="server" ID="txnSueldoNuevo" Name="txnSueldoNuevo" OnTextChanged="txnSueldoNuevo_TextChanged" AutoPostBack="true" Width="140px" Text='<%#Eval("MN_SUELDO_NUEVO")%>' MinValue="0" ShowSpinButtons="true" NumberFormat-DecimalDigits="2"></telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn AutoPostBackOnFilter="false" ReadOnly="true" AllowFiltering="false" HeaderStyle-Width="130" FilterControlWidth="50" HeaderText="Diferencia" DataField="DIFERENCIA_NUEVO" UniqueName="DIFERENCIA_NUEVO" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%#string.Format("{0:N2}",Math.Abs((decimal)Eval("DIFERENCIA_NUEVO")))%>%
                                        <span style="border: 1px solid gray; background: <%# Eval("COLOR_DIFERENCIA_NUEVO") %>; border-radius: 5px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
                                        <img src='/Assets/images/Icons/25/Arrow<%# Eval("ICONO_NUEVO")%>.png' />
                                                </span>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" Visible="true" ItemStyle-HorizontalAlign="Right" Display="true" HeaderStyle-Width="120" FilterControlWidth="50" HeaderText="Fecha de cambio" DataField="FE_CAMBIO_SUELDO" UniqueName="FE_CAMBIO_SUELDO" DataFormatString="{0:d}"></telerik:GridBoundColumn>
                                        <%-- <telerik:GridTemplateColumn AutoPostBackOnFilter="true"  HeaderStyle-Width="150" FilterControlWidth="70" HeaderText="Fecha de cambio" DataField="FE_CAMBIO_SUELDO" UniqueName="FE_CAMBIO_SUELDO" ItemStyle-HorizontalAlign="Right" >
                                     <ItemTemplate>
                                         <telerik:RadDatePicker ID="rdpFeCambioSueldo" runat="server" SelectedDate='<%#Eval("FE_CAMBIO_SUELDO")%>' Width="140px"></telerik:RadDatePicker>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>--%>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>
                    </telerik:RadPane>
                    <telerik:RadPane ID="rpAyudaDefinicionCriterios" runat="server" Scrolling="None" Width="22px">
                        <telerik:RadSlidingZone ID="rszAyudaDefinicionCriterios" runat="server" SlideDirection="Left" ExpandedPaneId="rsDefinicionCriterios" Width="22px">
                            <telerik:RadSlidingPane ID="rspDefinicionCriterios" runat="server" Title="Simbología" Width="450px" RenderMode="Mobile" Height="100%">
                                <div style="padding: 10px; text-align: justify;">
                                    <telerik:RadGrid ID="grdCodigoColores"
                                        runat="server"
                                        Height="215"
                                        Width="400"
                                        AllowSorting="true"
                                        AllowFilteringByColumn="true"
                                        HeaderStyle-Font-Bold="true"
                                        ShowHeader="true"
                                        OnNeedDataSource="grvClasificacionCompetencia_NeedDataSource">
                                        <ClientSettings>
                                            <Scrolling AllowScroll="true" UseStaticHeaders="false"></Scrolling>
                                        </ClientSettings>
                                        <GroupingSettings CaseSensitive="false" />
                                        <PagerStyle AlwaysVisible="true" />
                                        <MasterTableView AutoGenerateColumns="false" AllowPaging="false" AllowFilteringByColumn="false" ShowHeadersWhenNoRecords="true">
                                            <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="True" ShowExportToCsvButton="false" ShowRefreshButton="false"
                                                AddNewRecordText="Insertar" />
                                            <Columns>
                                                <telerik:GridTemplateColumn HeaderText="Color" HeaderStyle-Width="60" AllowFiltering="false">
                                                    <ItemTemplate>
                                                        <div style="margin: auto; width: 25px; border: 1px solid gray; background: <%# Eval("COLOR")%>; border-radius: 5px;">&nbsp;&nbsp;</div>
                                                        &nbsp;
                                                </div>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" HeaderStyle-Width="260" HeaderText="Descripción" DataField="DESCRIPCION"></telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>
                            </telerik:RadSlidingPane>
                            <telerik:RadSlidingPane ID="rspAyudaPlaneacionIncrementos" runat="server" Title="Ayuda" Width="240px" RenderMode="Mobile" Height="100%">
                                <div style="padding: 10px; text-align: justify;">
                                    <p>
                                        Guardar los cambios tal como se muestran en la tabla y se afectará el
                                        inventario de Empleados y el Historial, aplicando a la columna de Sueldos
                                        (no la de nuevos sueldos).
                                    </p>
                                    <p>Nota: Si cuentas con una interfaz de nómina no es recomendable seleccionar esta opción.</p>
                                </div>
                            </telerik:RadSlidingPane>
                        </telerik:RadSlidingZone>
                    </telerik:RadPane>
                </telerik:RadSplitter>
                <div style="clear: both; height: 10px;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblNominaActual"
                            name="lblNominaActual"
                            runat="server">
                            Costo de nómina actual:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtNominaActual"
                            runat="server"
                            Width="120px"
                            MaxLength="800"
                            Enabled="false"
                            ReadOnly="true"
                            DisabledStyle-HorizontalAlign="Right">
                        </telerik:RadTextBox>
                    </div>
                </div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblNominaPlaneada"
                            name="lblNominaPlaneada"
                            runat="server">
                            Costo de nómina planeada:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtNominaPlaneada"
                            runat="server"
                            Width="120px"
                            MaxLength="800"
                            Enabled="false"
                            ReadOnly="true"
                            DisabledStyle-HorizontalAlign="Right">
                        </telerik:RadTextBox>
                    </div>
                </div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblDiferencia"
                            name="lblDiferencia"
                            runat="server">
                            Diferencia:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtDiferencia"
                            runat="server"
                            Width="120px"
                            MaxLength="800"
                            Enabled="false"
                            ReadOnly="true"
                            DisabledStyle-HorizontalAlign="Right">
                        </telerik:RadTextBox>
                    </div>
                </div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblDiferenciaPr"
                            name="lblDiferenciaPr"
                            runat="server">
                            Diferencia %:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtDiferenciaPr"
                            runat="server"
                            Width="120px"
                            MaxLength="800"
                            Enabled="false"
                            ReadOnly="true"
                            DisabledStyle-HorizontalAlign="Right">
                        </telerik:RadTextBox>
                    </div>
                </div>
                <div style="clear: both; height: 5px;"></div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnSeleccionar" AutoPostBack="false" runat="server" OnClientClicked="OpenEmployeeSelectionWindow" Text="Seleccionar empleados" Width="170"></telerik:RadButton>
                </div>
                <%--<div class="ctrlBasico">
        <telerik:RadButton ID="btnRecalcular" AutoPostBack="true" runat="server" OnClick="btnRecalcular_Click" Text="Recalcular" Width="150"></telerik:RadButton>
    </div>--%>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnAplicarIncremento" AutoPostBack="false" runat="server" Text="Incremento general" OnClientClicked="AbrirVentanaIncremento" Width="150"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnInventario" AutoPostBack="true" runat="server" Text="Afectar inventario" OnClientClicking="ConfirmarGuardar" ToolTip="Guardar los cambios tal como se muestran en la tabla y se afectará el inventario de Empleados y el Historial, aplicando a la columna de Sueldo nominal nuevo (no la de Sueldo nominal actual).  Nota: si cuentas con una interfaz de nómina no es recomendable seleccionar esta opción." OnClick="btnInventario_Click" Width="150"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnGuardar" AutoPostBack="true" runat="server" Text="Guardar" OnClick="btnGuardar_Click" Width="150"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnGuardarCerrar" AutoPostBack="true" runat="server" Text="Guardar y cerrar" OnClick="btnGuardarCerrar_Click" Width="150"></telerik:RadButton>
                </div>
            </div>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
    <telerik:RadToolTipManager ID="rttmSecuancias"
        runat="server"
        Position="MiddleRight"
        CssClass="RadToolTip_MC"
        OnAjaxUpdate="rttmSecuancias_AjaxUpdate"
        ShowDelay="1000"
        ShowEvent="OnMouseOver"
        HideEvent="LeaveTargetAndToolTip"
        RelativeTo="Element"
        Animation="Fade">
    </telerik:RadToolTipManager>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server"></telerik:RadWindowManager>

</asp:Content>
