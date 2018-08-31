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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
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
                    <telerik:AjaxUpdatedControl ControlID="rwmAlertas"/>
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
                        </telerik:RadMultiPage>
                    </div>
                </div>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </div>
    <telerik:RadWindowManager ID="rwmAlertas" runat="server"></telerik:RadWindowManager>
</asp:Content>
