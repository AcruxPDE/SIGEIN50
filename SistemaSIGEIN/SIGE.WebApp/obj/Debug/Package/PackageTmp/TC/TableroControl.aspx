<%@ Page Title="" Language="C#" MasterPageFile="~/TC/MenuTC.Master" AutoEventWireup="true" CodeBehind="TableroControl.aspx.cs" Inherits="SIGE.WebApp.TC.TableroControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .RadListViewContainer {
            width: 200px;
            border: 1px solid lightgray;
            float: left;
            margin: 5px;
            border-radius: 5px;
        }

            .RadListViewContainer.Selected {
                background-color: #7F7F7F !important;
                color: #fff !important;
            }

            .RadListViewContainer > .SelectionOptions {
                overflow: auto;
                background-color: lightgray;
                padding: 10px;
            }

        .labelSubtitulo {
            margin-top: 20px;
            display: block;
            font-size: 1.6em;
        }
    </style>
    <script type="text/javascript">

        function GetWindowProperties() {
            return {
                width: document.documentElement.clientWidth - 20,
                height: document.documentElement.clientHeight - 20
            };
        }

        function vWindowsPropiertiesAgregar() {
            return {
                width: 750,
                height: document.documentElement.clientHeight - 20
            };
        }

        function onCloseWindow(sender, args) {
            var lista = $find('<%# rlvPeriodos.ClientID%>');
            lista.rebind();
        }

        function GetPeriodoNombre() {
            var listView = $find('<%= rlvPeriodos.ClientID%>');
            var selectedIndex = listView.get_selectedIndexes();
            if (selectedIndex.length > 0)
                return listView.get_clientDataKeyValue()[selectedIndex]["NB_PERIODO"];
            else
                return null;
        }

        function ConfirmarCerrar(sender, args) {
            var vNombrePeriodo = GetPeriodoNombre();
            var vIdPeriodo = GetPeriodoId();

            if (vIdPeriodo != null) {

                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                { if (shouldSubmit) { this.click(); } });

                radconfirm('¿Deseas cerrar el tablero ' + vNombrePeriodo + ' ?, una vez cerrado ya no será posible configurarlo.', callBackFunction, 400, 170, null, "Cerrar tablero");
                args.set_cancel(true);
            }
            else {
                radalert("Seleccione un tablero de control.", 400, 150, "");
                args.set_cancel(true);
            }
        }

        function GetPeriodoId() {
            var listView = $find('<%= rlvPeriodos.ClientID %>');
            var selectedIndex = listView.get_selectedIndexes();
            if (selectedIndex.length > 0)
                return listView.get_clientDataKeyValue()[selectedIndex]["ID_PERIODO"];
            else
                return null;
        }

        function AbrirVentanaNuevo() {
            var vWindowsPropierties = vWindowsPropiertiesAgregar();
            openChildDialog("VentanaNuevoTableroControl.aspx", "WinTableroControl", "Agregar tablero", vWindowsPropierties)
        }

        function OpenEditEventoWindow() {
            var vWindowsPropierties = vWindowsPropiertiesAgregar();
            var vIdPeriodo = GetPeriodoId();
            if (vIdPeriodo != null) {
                openChildDialog("VentanaNuevoTableroControl.aspx?pIdPeriodo=" + vIdPeriodo, "WinTableroControl", "Editar tablero", vWindowsPropierties);
            }
        }

        function OpenConsultaTablero() {
            var vWindowProperties = GetWindowProperties();
            var vIdPeriodo = GetPeriodoId();

            if (vIdPeriodo != null) {
                openChildDialog("VentanaConsultaTablero.aspx?pIdTablero=" + vIdPeriodo, "winConsultatablero", "Consultar tablero", vWindowProperties)
            }
            else {
                radalert("Selecciona un tablero de control.", 400, 150);
            }
        }

        function OpenConfigurarTableroWindow() {
            var vWindowProperties = GetWindowProperties();
            var vIdPeriodo = GetPeriodoId();

            if (vIdPeriodo != null) {
                openChildDialog("VentanaConfiguracionTablero.aspx?pIdPeriodo=" + vIdPeriodo, "WinTableroControl", "Configuración de tablero", vWindowProperties)
            }
            else {
                radalert("Selecciona un tablero de control.", 400, 150);
            }
        }

        function ConfirmarEliminar(sender, args) {
            var vNombrePeriodo = GetPeriodoNombre();
            var vIdPeriodo = GetPeriodoId();
            if (vIdPeriodo != null) {
                var vWindowPropierties = {
                    height: 200
                };
                confirmAction(sender, args, "Deseas eliminar el tablero " + vNombrePeriodo + "?, Este proceso no podra revertirse.");
            }
            else {
                radalert("Selecciona un tablero de control.", 400, 150);
                args.set_cancel(true);
            }
        }

        function ConfirmarCopiar(sender, args) {
            var vNombrePeriodo = GetPeriodoNombre();
            var vIdPeriodo = GetPeriodoId();
            if (vIdPeriodo != null) {
                var vWindowPropierties = {
                    height: 200
                };
                confirmAction(sender, args, "Deseas copiar el tablero " + vNombrePeriodo + "?, Este proceso no podra revertirse.");
            }
            else {
                radalert("Selecciona un tablero de control.", 400, 150);
                args.set_cancel(true);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <telerik:RadAjaxLoadingPanel ID="ralpPeriodos" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramOrganigrama" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rlvPeriodos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvPeriodos" LoadingPanelID="ralpPeriodos"></telerik:AjaxUpdatedControl>
                     <telerik:AjaxUpdatedControl ControlID="txtClPeriodo"  LoadingPanelID="ralpPeriodos" ></telerik:AjaxUpdatedControl>
                     <telerik:AjaxUpdatedControl ControlID="txtDsPeriodo"  LoadingPanelID="ralpPeriodos" ></telerik:AjaxUpdatedControl>
                     <telerik:AjaxUpdatedControl ControlID="txtClEstatus"  LoadingPanelID="ralpPeriodos" ></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtUsuarioMod" LoadingPanelID="ralpPeriodos" ></telerik:AjaxUpdatedControl>
                     <telerik:AjaxUpdatedControl ControlID="txtFechaMod"  LoadingPanelID="ralpPeriodos" ></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbAscendente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvPeriodos" LoadingPanelID="ralpPeriodos"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbDescendente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvPeriodos" LoadingPanelID="ralpPeriodos"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rfFiltros">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rfFiltros" LoadingPanelID="ralpPeriodos"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rfFiltros">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvPeriodos" LoadingPanelID="ralpPeriodos"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvPeriodos" LoadingPanelID="ralpPeriodos" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rdAgregar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvPeriodos" LoadingPanelID="ralpPeriodos"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnCopiar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlvPeriodos" LoadingPanelID="ralpPeriodos"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadSplitter ID="rsTableroControl" BorderSize="0" Width="100%" Height="100%" runat="server">
        <telerik:RadPane ID="rpTableroControl" runat="server">
            <label class="labelTitulo">Tablero de control</label>
            <div class="ctrlBasico">
                <telerik:RadListView ID="rlvPeriodos" runat="server" DataKeyNames="ID_PERIODO" ClientDataKeyNames="ID_PERIODO,NB_PERIODO" OnNeedDataSource="rlvPeriodos_NeedDataSource" OnItemCommand="rlvPeriodos_ItemCommand" AllowPaging="true" ItemPlaceholderID="ProductsHolder">
                    <LayoutTemplate>
                        <div style="overflow: auto; width: 700px;">
                            <div style="overflow: auto; overflow-y: auto; max-height: 450px;">
                                <asp:Panel ID="ProductsHolder" runat="server"></asp:Panel>
                            </div>
                            <div style="clear: both;"></div>
                            <telerik:RadDataPager ID="rdpPeriodos" runat="server" PagedControlID="rlvPeriodos" PageSize="6" Width="630">
                                <Fields>
                                    <telerik:RadDataPagerButtonField FieldType="FirstPrev"></telerik:RadDataPagerButtonField>
                                    <telerik:RadDataPagerButtonField FieldType="Numeric" PageButtonCount="5"></telerik:RadDataPagerButtonField>
                                    <telerik:RadDataPagerButtonField FieldType="NextLast"></telerik:RadDataPagerButtonField>
                                    <telerik:RadDataPagerGoToPageField CurrentPageText="Página:" TotalPageText="de" SubmitButtonText="Ir"></telerik:RadDataPagerGoToPageField>
                                </Fields>
                            </telerik:RadDataPager>
                        </div>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <div class="RadListViewContainer">
                            <div style="padding: 10px;">
                                <div style="overflow: auto; overflow-y: auto; height: 35px;">
                                    <label>Clave:</label>
                                    <%# Eval("CL_PERIODO") %>
                                </div>
                                <div>
                                    <label>Estatus:</label>
                                    <%# Eval("CL_ESTADO_PERIODO") %>
                                </div>
                                <div style="overflow: auto; overflow-y: auto; height: 50px;">
                                    <label>Descripción:</label>
                                    <%# Eval("DS_PERIODO") %>
                                </div>
                            </div>
                            <div class="SelectionOptions">
                                <telerik:RadButton ID="btnSeleccionar" runat="server" CommandName="Select" Text="Seleccionar" Width="100%"></telerik:RadButton>
                            </div>
                        </div>
                    </ItemTemplate>
                    <SelectedItemTemplate>
                        <div class="RadListViewContainer Selected">
                            <div style="padding: 10px;">
                                <div style="overflow: auto; overflow-y: auto; height: 35px;">
                                    <label style="color: white;">Clave:</label>
                                    <%# Eval("CL_PERIODO") %>
                                </div>
                                <div>
                                    <label style="color: white;">Estatus:</label>
                                    <%# Eval("CL_ESTADO_PERIODO") %>
                                </div>
                                <div style="overflow: auto; overflow-y: auto; height: 50px;">
                                    <label style="color: white;">Descripción:</label>
                                    <%# Eval("DS_PERIODO") %>
                                </div>
                            </div>
                            <div class="SelectionOptions">
                                <div style="width: 50%; float: left; padding-right: 5px;">
                                    <telerik:RadButton ID="btnEditar" runat="server" Text="Editar" Width="100%" OnClientClicking="OpenEditEventoWindow"></telerik:RadButton>
                                </div>
                                <div style="width: 50%; float: left; padding-right: 5px;">
                                    <telerik:RadButton ID="btnEliminar" runat="server" Text="Eliminar" Width="100%" OnClientClicking="ConfirmarEliminar" OnClick="btnEliminar_Click"></telerik:RadButton>
                                </div>
                            </div>
                        </div>
                    </SelectedItemTemplate>
                    <EmptyDataTemplate>
                        <div class="RadListViewContainer" style="overflow: auto; text-align: center; width: 660px; height: 100px;">
                            No hay tableros disponibles
                        </div>
                    </EmptyDataTemplate>
                </telerik:RadListView>
            </div>
            <div class="ctrlBasico" style="text-align: center;">
                <telerik:RadTabStrip runat="server" ID="rtsPeriodos" MultiPageID="rmpPeriodos" SelectedIndex="0">
                    <Tabs>
                        <telerik:RadTab runat="server" Text="Gestionar"></telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Detalles"></telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <div style="height: 10px;"></div>
                <telerik:RadMultiPage runat="server" ID="rmpPeriodos" SelectedIndex="0" Height="100%" Width="100%">
                    <telerik:RadPageView ID="rpvPeriodos" runat="server">
                        <div>
                            <label class="labelTitulo">Administrar</label>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnNuevo" runat="server" Text="Agregar" AutoPostBack="false" OnClientClicked="AbrirVentanaNuevo"></telerik:RadButton>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnConfigurar" runat="server" Text="Configurar" AutoPostBack="false" OnClientClicked="OpenConfigurarTableroWindow"></telerik:RadButton>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnCerrar" runat="server" Text="Cerrar" OnClientClicking="ConfirmarCerrar" OnClick="btnCerrar_Click"></telerik:RadButton>
                            </div>
                            <%-- <telerik:RadButton ID="btnEliminar" runat="server" Text="Eliminar" OnClientClicking="ConfirmarEliminar" OnClick="btnEliminar_Click"></telerik:RadButton>--%>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnCopiar" runat="server" Text="Copiar" OnClientClicking="ConfirmarCopiar" OnClick="btnCopiar_Click"></telerik:RadButton>
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                        <div>
                            <label class="labelTitulo">Consultas</label>
                            <telerik:RadButton ID="btnConsultar" runat="server" Text="Consultar" AutoPostBack="false" OnClientClicked="OpenConsultaTablero"></telerik:RadButton>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="rpvInformacion" runat="server">
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label style="width: 120px;" id="lblEvento" name="lblEvento" runat="server">Tablero:</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadTextBox ID="txtClPeriodo" Enabled="false" runat="server" Width="350px" MaxLength="1000"></telerik:RadTextBox>
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label style="width: 120px;" id="Label1" name="lblEvento" runat="server">Descripción:</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadTextBox ID="txtDsPeriodo" Enabled="false" runat="server" Width="350px" MaxLength="1000"></telerik:RadTextBox>
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label style="width: 120px;" id="Label2" name="lblEvento" runat="server">Estatus:</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadTextBox ID="txtClEstatus" Enabled="false" runat="server" Width="200px" MaxLength="1000"></telerik:RadTextBox>
                            </div>
                        </div>
                        <%--  <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <label style="width: 120px;" id="Label3" name="lblEvento" runat="server">Tipo de puestos:</label>
                            <div class="divControlDerecha">
                                <telerik:RadTextBox ID="txtTipo" Enabled="false" runat="server" Width="400px" MaxLength="1000"></telerik:RadTextBox>
                            </div>
                        </div>--%>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label style="width: 120px;" id="Label5" name="lblCurso" runat="server">Último usuario que modifica:</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadTextBox ID="txtUsuarioMod" Enabled="false" runat="server" Width="105px" MaxLength="1000"></telerik:RadTextBox>
                            </div>
                        </div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label style="width: 120px;" id="Label6" name="lblCurso" runat="server">Última fecha de modificación:</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadTextBox ID="txtFechaMod" Enabled="false" runat="server" Width="105px" MaxLength="1000"></telerik:RadTextBox>
                            </div>
                        </div>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </div>
        </telerik:RadPane>
        <telerik:RadPane ID="rpAyudaTableroControl" runat="server" Scrolling="None" Width="20px">
            <telerik:RadSlidingZone ID="rszAyudaTableroControl" runat="server" SlideDirection="Left" ExpandedPaneId="rsClimaLaboral" Width="20px" ClickToOpen="true">
                <telerik:RadSlidingPane ID="rspAyudaTableroControl" runat="server" Title="Ayuda" Width="240px" RenderMode="Mobile" Height="100%">
                    <div style="padding: 20px; text-align: justify;">
                        <p>
                            Utiliza esta página para crear, configurar, consultar o eliminar un tablero de control.
                        </p>
                    </div>
                </telerik:RadSlidingPane>
                <telerik:RadSlidingPane ID="rspOrdenarFiltrar" runat="server" Title="Ordenar y filtrar" Width="450px" RenderMode="Mobile" Height="100%">
                    <div style="clear: both; height: 10px;"></div>
                    <div>
                        <fieldset>
                            <legend>Ordenar por:</legend>
                            <telerik:RadComboBox ID="cmbOrdenamiento" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Clave del tablero" Value="CL_PERIODO" />
                                    <telerik:RadComboBoxItem Text="Nombre del tablero" Value="NB_PERIODO" />
                                    <telerik:RadComboBoxItem Text="Estatus" Value="CL_ESTADO_PERIODO" />
                                    <telerik:RadComboBoxItem Text="Fecha de creación" Value="FE_INICIO" />
                                </Items>
                            </telerik:RadComboBox>
                            <telerik:RadButton ID="rbAscendente" runat="server" ButtonType="ToggleButton" ToggleType="Radio" Text="Ascendente" GroupName="ordenamiento"></telerik:RadButton>
                            <telerik:RadButton ID="rbDescendente" runat="server" ButtonType="ToggleButton" ToggleType="Radio" Text="Descendente" GroupName="ordenamiento"></telerik:RadButton>
                        </fieldset>
                    </div>
                    <div style="clear: both; height: 10px;"></div>
                    <div>
                        <fieldset>
                            <legend>Filtrar por:</legend>
                            <telerik:RadFilter runat="server" ID="rfFiltros" FilterContainerID="rlvPeriodos" ExpressionPreviewPosition="Top" ApplyButtonText="Filtrar">
                            </telerik:RadFilter>
                        </fieldset>
                    </div>
                </telerik:RadSlidingPane>
            </telerik:RadSlidingZone>
        </telerik:RadPane>
    </telerik:RadSplitter>
    <div style="clear: both;"></div>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true" OnClientClose="onCloseWindow">
        <Windows>
            <telerik:RadWindow ID="winCumplimientoGlobal" runat="server" Animation="Fade" VisibleStatusbar="false" Behaviors="Close" Modal="true"></telerik:RadWindow>
            <telerik:RadWindow ID="WinClimaLaboral" runat="server" Width="1000px" Height="650px" VisibleStatusbar="false" ShowContentDuringLoad="false" Behaviors="Close" Modal="true" Animation="Fade" ReloadOnShow="false"></telerik:RadWindow>
            <telerik:RadWindow ID="WinTabuladores" runat="server" VisibleStatusbar="false" ShowContentDuringLoad="false" Behaviors="Close" Modal="true" ReloadOnShow="false" Animation="Fade" Width="1350px" Height="665px"></telerik:RadWindow>
            <telerik:RadWindow ID="winEmpleado" runat="server" Behaviors="Close" Modal="true" VisibleStatusbar="false"></telerik:RadWindow>
            <telerik:RadWindow ID="winDescriptivo" runat="server" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="rwReporte" runat="server" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="WinTableroControl" runat="server" Width="750px" Height="580px" VisibleStatusbar="false" ShowContentDuringLoad="false" Behaviors="Close" Modal="true" Animation="Fade" ReloadOnShow="false"></telerik:RadWindow>
            <telerik:RadWindow ID="winConsultatablero" runat="server" Width="760px" Height="590px" VisibleStatusbar="false" ShowContentDuringLoad="false" Behaviors="Close" Modal="true" Animation="Fade" ReloadOnShow="false"></telerik:RadWindow>
            <telerik:RadWindow ID="winSeleccion" runat="server" VisibleStatusbar="false" ShowContentDuringLoad="false" Behaviors="Close" Modal="true" Animation="Fade" ReloadOnShow="false" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
</asp:Content>
