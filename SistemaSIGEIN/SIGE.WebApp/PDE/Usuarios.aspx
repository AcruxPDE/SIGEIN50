<%@ Page Title="" Language="C#" MasterPageFile="~/PDE/ContextPDE.master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="SIGE.WebApp.PDE.Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script id="5" type="text/javascript">


        function OpenEmpleadosSelectionWindow() {
            OpenSelectionWindow("../Comunes/SeleccionEmpleado.aspx", "winSeleccion", "Selección de evaluados");
        }

        function OpenPuestoSelectionWindow() {
            OpenSelectionWindow("../Comunes/SeleccionPuesto.aspx?mulSel=1", "winSeleccion", "Selección de evaluados por puestos");
        }

        function OpenAreaSelectionWindow() {
            OpenSelectionWindow("../Comunes/SeleccionArea.aspx?", "winSeleccion", "Selección de evaluados por áreas/departamento");
        }


        function OpenSelectionWindow(pURL, pIdWindow, pTitle) {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 30,
                height: browserWnd.innerHeight - 30
            };
            openChildDialog(pURL, pIdWindow, pTitle, windowProperties)
        }

        function EncapsularSeleccion(pClTipoSeleccion, pLstSeleccion) {
            return JSON.stringify({ clTipo: pClTipoSeleccion, oSeleccion: pLstSeleccion });
        }

        function InsertEvaluado(pDato) {
            var ajaxManager = $find('<%= ramUsuarios.ClientID%>');
            ajaxManager.ajaxRequest(pDato);
        }

        function useDataFromChild(pDato) {

            if (pDato != null) {
                switch (pDato[0].clTipoCatalogo || vDatosSeleccionados.clTipoCatalogo) {
                    case "EMPLEADO":
                        InsertEvaluado(EncapsularSeleccion("EVALUADO", pDato));
                        break;
                    case "PUESTO":
                        InsertEvaluado(EncapsularSeleccion("PUESTO", pDato));
                        break;
                    case "DEPARTAMENTO":
                        InsertEvaluado(EncapsularSeleccion("AREA", pDato));
                        break;
                    case "EVALUADOR":
                        InsertEvaluado(EncapsularSeleccion("EVALUADOR", pDato));
                        break;
                }
            }
        }

    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">


    <telerik:RadAjaxLoadingPanel ID="ralpUsuarios" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramUsuarios" runat="server" DefaultLoadingPanelID="ralpUsuarios" OnAjaxRequest="ramUsuarios_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ramUsuarios">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdUsuarios" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="grdContrasenas" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnGuardarInformacionGeneral">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnGuardarInformacionGeneral" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSeleccionPorPersona">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdUsuarios" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdUsuarios">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdUsuarios" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSeleccionPorPuesto">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdUsuarios" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSeleccionPorArea">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdUsuarios" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadTabStrip ID="rtsConfiguracion" runat="server" SelectedIndex="0" MultiPageID="rmpConfiguracion">
        <Tabs>
            <telerik:RadTab Text="Selección de Usuarios"></telerik:RadTab>
            <telerik:RadTab Text="Procesos"></telerik:RadTab>
            <telerik:RadTab Text="Contraseña"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>

    <div style="height: calc(100% - 130px); padding-top: 10px;">

        <telerik:RadMultiPage ID="rmpConfiguracion" runat="server" SelectedIndex="0" Height="100%">

            <telerik:RadPageView ID="rpvSeleccionUsuarios" runat="server">

                <div style="height: calc(100% - 100px);">

                    <telerik:RadGrid ID="grdUsuarios" 
                        OnNeedDataSource="grdUsuarios_NeedDataSource"
                        runat="server"
                        AllowMultiRowSelection="true"
                        ShowFooter="true"
                        Height="100%" 
                        AutoGenerateColumns="false" 
                        EnableHeaderContextMenu="true" 
                        ShowGroupPanel="True" 
                        AllowPaging="true"
                        AllowSorting="true" 
                        HeaderStyle-Font-Bold="true">
                        <ClientSettings>
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView DataKeyNames="ID_EMPLEADO, CL_EMPLEADO"
                            AllowPaging="false" AllowSorting="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true"
                            EnableHeaderContextFilterMenu="true" ClientDataKeyNames="ID_EMPLEADO" EnableHierarchyExpandAll="true" HierarchyDefaultExpanded="false">
                            <Columns>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="No. Empleado" DataField="CL_EMPLEADO" UniqueName="CL_EMPLEADO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="160" FilterControlWidth="120" HeaderText="Nombre Completo" DataField="NB_EMPLEADO" UniqueName="NB_EMPLEADO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="170" FilterControlWidth="100" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="170" FilterControlWidth="100" HeaderText="Área/Departamento" DataField="NB_DEPARTAMENTO" UniqueName="NB_DEPARTAMENTO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="100" HeaderText="Correo electronico" DataField="CL_CORREO_ELECTRONICO" UniqueName="CL_CORREO_ELECTRONICO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="100" HeaderText="Estatus" DataField="NB_ESTATUS" UniqueName="NB_ESTATUS"></telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <div style="clear: both; height: 10px;"></div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnSeleccionPorPersona" runat="server" Text="Seleccionar por persona" AutoPostBack="true" OnClientClicked="OpenEmpleadosSelectionWindow"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnSeleccionPorPuesto" runat="server" Text="Seleccionar por puesto" AutoPostBack="false" OnClientClicked="OpenPuestoSelectionWindow"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnSeleccionPorArea" runat="server" Text="Seleccionar por área/departamento" AutoPostBack="false" OnClientClicked="OpenAreaSelectionWindow"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="RadButton1" runat="server" Text="Eliminar" AutoPostBack="false"></telerik:RadButton>
                </div>
                <div class="divControlesBoton">
                    <div class="ctrlBasico">
                        <telerik:RadButton runat="server" ID="btnGuardarEvaluado" Text="Guardar" AutoPostBack="false"></telerik:RadButton>
                    </div>
                </div>

            </telerik:RadPageView>

            <telerik:RadPageView ID="rpvProcesos" runat="server">

                <div style="margin-left: 20px;">

                    <label id="lbPeriodo" name="lbTabulador" runat="server">Acceso a:</label>

                    <div style="clear: both; height: 20px;"></div>

                    <div class="ctrlBasico" id="Div1" runat="server">
                        <div class="divControlDerecha">
                            <telerik:RadButton ID="rdComunicados" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false">
                                <ToggleStates>
                                    <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                    <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                </ToggleStates>
                            </telerik:RadButton>
                            <label id="lblMisComunicados">Mis comunicados.</label>
                        </div>
                    </div>

                    <div style="clear: both; height: 20px;"></div>

                    <div class="ctrlBasico" id="Div2" runat="server">
                        <div class="divControDerecha">
                            <telerik:RadButton ID="rdTramites" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false">
                                <ToggleStates>
                                    <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                    <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                </ToggleStates>
                            </telerik:RadButton>
                            <label id="lblMisTramites">Mis Trámites.</label>
                        </div>
                    </div>

                    <div style="clear: both; height: 20px;"></div>

                    <div class="ctrlBasico" id="Div3" runat="server">
                        <div class="divControDerecha">
                            <telerik:RadButton ID="rdCompromisos" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false">
                                <ToggleStates>
                                    <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                    <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                </ToggleStates>
                            </telerik:RadButton>
                            <label id="lblMisCompromisos">Mis Compromisos.</label>
                        </div>
                    </div>

                    <div style="clear: both; height: 20px;"></div>

                    <div class="ctrlBasico" id="Div4" runat="server">
                        <div class="divControDerecha">
                            <telerik:RadButton ID="rdNomina" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false">
                                <ToggleStates>
                                    <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                    <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                </ToggleStates>
                            </telerik:RadButton>
                            <label id="lblMisNomina">Mi Nómina.</label>
                        </div>
                    </div>
                </div>

                <div style="clear: both; height: 20px;"></div>

            </telerik:RadPageView>

            <telerik:RadPageView ID="rpvContraseña" runat="server">
                <div style="clear: both;"></div>

                <div style="height: calc(100% - 100px);">

                    <telerik:RadGrid ID="grdContrasenas" 
                        OnNeedDataSource="grdContrasenas_NeedDataSource" 
                        runat="server"
                        Height="100%" AutoGenerateColumns="false" ShowGroupPanel="True" AllowPaging="true"
                        AllowSorting="true" HeaderStyle-Font-Bold="true">
                        <ClientSettings>
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView ClientDataKeyNames="ID_EMPLEADO,CL_EMPLEADO" DataKeyNames="ID_EMPLEADO,CL_EMPLEADO" AutoGenerateColumns="false" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true">
                            <Columns>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Usuario" DataField="CL_EMPLEADO" UniqueName="CL_EMPLEADO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="160" FilterControlWidth="120" HeaderText="Nombre" DataField="NB_EMPLEADO" UniqueName="NB_EMPLEADO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="160" FilterControlWidth="120" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="20" HeaderStyle-Width="70 " HeaderText="Contraseña" DataField="NB_CONTRASEÑA" UniqueName="NB_CONTRASEÑA"></telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>

            </telerik:RadPageView>


        </telerik:RadMultiPage>
    </div>

    <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true" OnClientClose="returnDataToParentPopup">
    </telerik:RadWindowManager>
    
</asp:Content>
