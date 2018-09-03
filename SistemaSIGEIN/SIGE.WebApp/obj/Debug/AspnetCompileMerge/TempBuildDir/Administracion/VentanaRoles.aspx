<%@ Page Title="" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="VentanaRoles.aspx.cs" Inherits="SIGE.WebApp.Administracion.VentanaRoles" EnableEventValidation="false" %>


<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script id="MyScript" type="text/javascript">
        function GetRadWindow() {
            var oWindow = null;
            if 
                (window.radWindow) oWindow = window.radWindow;
            else
                if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function closeWindow() {
            GetRadWindow().close();
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

        function OpenGruposWindows() {
            OpenSelectionWindow("../Comunes/SeleccionGrupo.aspx?mulSel=1", "winSeleccion", "Selección de grupos")
        }

        function useDataFromChild(pDato) {
            if (pDato != null) {
                console.info(pDato);
                var vDatosSeleccionados = pDato[0];
                switch (vDatosSeleccionados.clTipoCatalogo) {
                    case "GRUPO":
                        InsertarDato(EncapsularDatos("GRUPO", pDato));
                        break;
                }
            }
        }

        //FUNCTION INSERTAR DATO
        function InsertarDato(pDato) {
            var ajaxManager = $find('<%= RadAjaxManager1.ClientID %>');
            ajaxManager.ajaxRequest(pDato);
        }

        //FUNCION ENCAPSULAR DATO
        function EncapsularDatos(pClTipoDato, pLstDatos) {
            return JSON.stringify({ clTipo: pClTipoDato, oSeleccion: pLstDatos });
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgGrupos" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgGrupos" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="rgPlazasGrupo" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdMenuGeneral">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdMenuGeneral" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdMenuModulos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdMenuModulos" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rwmAlertas" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgGrupos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgPlazasGrupo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <br />
    <div style="height: calc(100% - 20px);">
        <telerik:RadSplitter ID="splRol" runat="server" Width="100%" Height="100%" BorderSize="0">
            <telerik:RadPane ID="panCampos" runat="server" Width="310px">

                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label name="lblClClave">Clave:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtClRol" name="txtClRol" runat="server" MaxLength="30"></telerik:RadTextBox><br />
                        <asp:RequiredFieldValidator ID="reqtxtClRol" runat="server" Display="Dynamic" ControlToValidate="txtClRol" ErrorMessage="Campo obligatorio" CssClass="validacion"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label name="lblNombre">Nombre:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtNbRol" name="txtNbRol" runat="server" MaxLength="100"></telerik:RadTextBox><br />
                        <asp:RequiredFieldValidator ID="reqtxtNbRol" runat="server" Display="Dynamic" ControlToValidate="txtNbRol" ErrorMessage="Campo obligatorio" CssClass="validacion"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div style="clear: both;"></div>
                 <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label name="lbPlantilla">Plantilla:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadComboBox ID="rcbPlantilla" runat="server" MaxLength="100" AutoPostBack="false" MarkFirstMatch="true" EmptyMessage="Selecciona"></telerik:RadComboBox><br />                      
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label name="lblNombre">Activo:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadButton ID="chkActivo" runat="server" ToggleType="CheckBox" name="chkActivo" Checked="true" AutoPostBack="false">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                            </ToggleStates>
                        </telerik:RadButton>
                    </div>
                </div>
                <div style="clear: both;"></div>  
                <div class="divControlDerecha">
                    <div class="ctrlBasico">
                        <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click"></telerik:RadButton>
                    </div>
                    <div class="ctrlBasico">
                        <telerik:RadButton ID="btnCancelar" runat="server" Text="Cancelar" AutoPostBack="false" OnClientClicked="closeWindow"></telerik:RadButton>
                    </div>
                </div>
            </telerik:RadPane>
            <telerik:RadSplitBar ID="barSeparador1" runat="server" Enabled="false" EnableResize="false" CollapseMode="None"></telerik:RadSplitBar>
            <telerik:RadPane ID="panPermisos" runat="server">
                <div style="margin-left: 20px; height: 100%;">
                    <telerik:RadTabStrip ID="tabPermisos" runat="server" SelectedIndex="0" MultiPageID="mpgPermisos">
                        <Tabs>
                            <telerik:RadTab Text="Menú general"></telerik:RadTab>
                            <telerik:RadTab Text="Menú módulos"></telerik:RadTab>
                            <telerik:RadTab Text="Menú adicionales"></telerik:RadTab>
                            <telerik:RadTab Text="Visión de grupos"></telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <div style="height: calc(100% - 45px);">
                        <telerik:RadMultiPage ID="mpgPermisos" runat="server" SelectedIndex="0" Height="100%">
                            <telerik:RadPageView ID="pvwMenuGeneral" runat="server" Height="100%">
                                <telerik:RadTreeList ID="grdMenuGeneral" runat="server" AutoGenerateColumns="false" AllowRecursiveDelete="true" ExpandCollapseMode="Client"
                                    DataKeyNames="ID_FUNCION" ParentDataKeyNames="ID_FUNCION_PADRE"
                                    OnItemDataBound="grdMenuGeneral_ItemDataBound"
                                    OnNeedDataSource="grdMenuGeneral_NeedDataSource"
                                    AllowMultiItemSelection="true" Height="100%">
                                    <ClientSettings>
                                        <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                        <Selecting AllowItemSelection="true" UseSelectColumnOnly="true" AllowToggleSelection="true" />
                                    </ClientSettings>
                                    <Columns>
                                        <telerik:TreeListSelectColumn UniqueName="SelectColumn" HeaderStyle-Width="40"></telerik:TreeListSelectColumn>
                                        <telerik:TreeListCheckBoxColumn DataField="FG_SELECCIONADO" Visible="false" UniqueName="FG_SELECCIONADO"></telerik:TreeListCheckBoxColumn>
                                        <telerik:TreeListBoundColumn DataField="NB_FUNCION" HeaderText="Nombre" UniqueName="NB_FUNCION"></telerik:TreeListBoundColumn>
                                        <telerik:TreeListBoundColumn DataField="CL_FUNCION" HeaderText="Clave" UniqueName="CL_FUNCION" HeaderStyle-Width="80"></telerik:TreeListBoundColumn>
                                    </Columns>
                                </telerik:RadTreeList>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="pvwMenuModulos" runat="server" Height="100%">
                                <telerik:RadTreeList ID="grdMenuModulos" runat="server" AutoGenerateColumns="false" AllowRecursiveDelete="true" ExpandCollapseMode="Client"
                                    DataKeyNames="ID_FUNCION" ParentDataKeyNames="ID_FUNCION_PADRE"
                                    OnItemDataBound="grdMenuModulos_ItemDataBound"
                                    OnNeedDataSource="grdMenuModulos_NeedDataSource"
                                    AllowMultiItemSelection="true" Height="100%">
                                    <ClientSettings>
                                        <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                        <Selecting AllowItemSelection="true" />
                                    </ClientSettings>
                                    <Columns>
                                        <telerik:TreeListSelectColumn UniqueName="SelectColumn" HeaderStyle-Width="40"></telerik:TreeListSelectColumn>
                                        <telerik:TreeListCheckBoxColumn DataField="FG_SELECCIONADO" Visible="false" UniqueName="FG_SELECCIONADO"></telerik:TreeListCheckBoxColumn>
                                        <telerik:TreeListBoundColumn DataField="NB_FUNCION" HeaderText="Nombre" UniqueName="NB_FUNCION"></telerik:TreeListBoundColumn>
                                        <telerik:TreeListBoundColumn DataField="CL_FUNCION" HeaderText="Clave" UniqueName="CL_FUNCION" HeaderStyle-Width="80"></telerik:TreeListBoundColumn>
                                    </Columns>
                                </telerik:RadTreeList>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="pvwMenuAdicionales" runat="server" Height="100%">
                                <telerik:RadTreeList ID="grdMenuAdicionales" runat="server" AutoGenerateColumns="false" AllowRecursiveDelete="true" ExpandCollapseMode="Client"
                                    DataKeyNames="ID_FUNCION" ParentDataKeyNames="ID_FUNCION_PADRE"
                                    OnItemDataBound="grdMenuAdicionales_ItemDataBound"
                                    OnNeedDataSource="grdMenuAdicionales_NeedDataSource"
                                    AllowMultiItemSelection="true" Height="100%">
                                    <ClientSettings>
                                        <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                        <Selecting AllowItemSelection="true" />
                                    </ClientSettings>
                                    <Columns>
                                        <telerik:TreeListSelectColumn UniqueName="SelectColumn" HeaderStyle-Width="40"></telerik:TreeListSelectColumn>
                                        <telerik:TreeListCheckBoxColumn DataField="FG_SELECCIONADO" Visible="false" UniqueName="FG_SELECCIONADO"></telerik:TreeListCheckBoxColumn>
                                        <telerik:TreeListBoundColumn DataField="NB_FUNCION" HeaderText="Nombre" UniqueName="NB_FUNCION"></telerik:TreeListBoundColumn>
                                        <telerik:TreeListBoundColumn DataField="CL_FUNCION" HeaderText="Clave" UniqueName="CL_FUNCION" HeaderStyle-Width="80"></telerik:TreeListBoundColumn>
                                    </Columns>
                                </telerik:RadTreeList>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="rpvGrupos" runat="server">
                                <div style="height: calc(100% - 20)">
                                    <div style="clear: both; height: 10px;"></div>
                                    <div class="divControlCenter">
                                        <div class="ctrlBasico" style="width: 420px; align-content: center;">
                                            <telerik:RadGrid
                                                ID="rgGrupos"
                                                runat="server"
                                                Width="420"
                                                Height="300"
                                                AllowPaging="true"
                                                AutoGenerateColumns="false"
                                                HeaderStyle-Font-Bold="true"
                                                EnableHeaderContextMenu="true"
                                                AllowMultiRowSelection="false"
                                                ClientSettings-EnablePostBackOnRowClick="true"
                                                OnItemCommand="rgGrupos_ItemCommand"
                                                OnNeedDataSource="rgGrupos_NeedDataSource">
                                                <GroupingSettings CaseSensitive="False" />
                                                <ClientSettings>
                                                    <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true" />
                                                    <Selecting AllowRowSelect="true" />
                                                </ClientSettings>
                                                <PagerStyle AlwaysVisible="true" />
                                                <MasterTableView DataKeyNames="ID_GRUPO" ClientDataKeyNames="ID_GRUPO" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                                                    <Columns>
                                                        <telerik:GridBoundColumn UniqueName="CL_GRUPO" DataField="CL_GRUPO" HeaderText="Clave" AutoPostBackOnFilter="true" HeaderStyle-Width="100" FilterControlWidth="30" CurrentFilterFunction="Contains"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="NB_GRUPO" DataField="NB_GRUPO" HeaderText="Nombre" AutoPostBackOnFilter="true" HeaderStyle-Width="290" FilterControlWidth="220" CurrentFilterFunction="Contains"></telerik:GridBoundColumn>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </div>
                                        <div class="ctrlBasico" style="float: left">
                                            <telerik:RadButton ID="btnAgregar" runat="server" Text="B" AutoPostBack="false" ToolTip="Seleccionar grupos" OnClientClicked="OpenGruposWindows"></telerik:RadButton>
                                            <div style="clear: both;"></div>
                                            <telerik:RadButton ID="btnEliminar" runat="server" Text="X" AutoPostBack="true" ToolTip="Eliminar grupo" OnClick="btnEliminar_Click"></telerik:RadButton>
                                        </div>
                                    </div>
                                    <div style="height: 15px; clear: both;"></div>
                                    <div style="clear: both; height: 10px;"></div>
                                    <div style="width: calc(100% - 20px);">
                                        <telerik:RadGrid
                                            ID="rgPlazasGrupo"
                                            runat="server"
                                            Width="100%"
                                            Height="400"
                                            AllowPaging="true"
                                            AutoGenerateColumns="false"
                                            HeaderStyle-Font-Bold="true"
                                            EnableHeaderContextMenu="true"
                                            AllowMultiRowSelection="false"
                                            OnItemDataBound="rgPlazasGrupo_ItemDataBound"
                                            OnNeedDataSource="rgPlazasGrupo_NeedDataSource">
                                            <GroupingSettings CaseSensitive="False" />
                                            <ClientSettings>
                                                <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true" />
                                                <Selecting AllowRowSelect="true" />
                                            </ClientSettings>
                                            <PagerStyle AlwaysVisible="true" />
                                            <MasterTableView DataKeyNames="ID_PLAZA" ClientDataKeyNames="ID_PLAZA" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                                                <Columns>
                                                    <telerik:GridBoundColumn UniqueName="CL_PLAZA" DataField="CL_PLAZA" HeaderText="Clave plaza" AutoPostBackOnFilter="true" HeaderStyle-Width="100" FilterControlWidth="30" CurrentFilterFunction="Contains"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn UniqueName="NB_PUESTO" DataField="NB_PUESTO" HeaderText="Nombre puesto" AutoPostBackOnFilter="true" HeaderStyle-Width="200" FilterControlWidth="140" CurrentFilterFunction="Contains"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn UniqueName="NB_EMPLEADO" DataField="NB_EMPLEADO" HeaderText="Nombre empleado" AutoPostBackOnFilter="true" HeaderStyle-Width="250" FilterControlWidth="180" CurrentFilterFunction="Contains"></telerik:GridBoundColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                </div>
                            </telerik:RadPageView>
                        </telerik:RadMultiPage>
                    </div>
                </div>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </div>
    <telerik:RadWindowManager ID="rwmAlertas" runat="server"></telerik:RadWindowManager>
</asp:Content>
