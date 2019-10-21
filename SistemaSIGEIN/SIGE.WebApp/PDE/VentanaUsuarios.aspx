<%@ Page Title="" Language="C#" MasterPageFile="~/PDE/ContextPDE.master" AutoEventWireup="true" CodeBehind="VentanaUsuarios.aspx.cs" Inherits="SIGE.WebApp.PDE.VentanaUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script id="MyScript" type="text/javascript">
        function closeWindow() {
            GetRadWindow().close();
        }


        function OpenEmployeeSelectionWindow() {
            openChildDialog("../Comunes/SeleccionEmpleado.aspx", "winSeleccionEmpleados", "Selección de empleados")
        }

        function useDataFromChild(pEmpleados) {

           <%-- if (pEmpleados != null) {
                var vEmpleadoSeleccionado = pEmpleados[0];
                console.info(vEmpleadoSeleccionado);
                var list = $find("<%=lstEmpleado.ClientID %>");
                list.trackChanges();

                var items = list.get_items();
                items.clear();

                var item = new Telerik.Web.UI.RadListBoxItem();
                item.set_text(vEmpleadoSeleccionado.nbEmpleado);
                item.set_value(vEmpleadoSeleccionado.idEmpleado);
                items.add(item);

                list.commitChanges();

                $find("<%= txtNbUsuario.ClientID %>").set_value(vEmpleadoSeleccionado.nbEmpleado);
                $find("<%= txtNbCorreoElectronico.ClientID %>").set_value(vEmpleadoSeleccionado.nbCorreoElectronico);--%>
            }
        }
    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server" SelectedIndex="0">

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" Visible="false" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxLoadingPanel ID="ralpNomina" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramNomina" runat="server" DefaultLoadingPanelID="ralpNomina">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnGuardarInformacionGeneral">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnGuardarInformacionGeneral" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <br />

    <telerik:RadTabStrip ID="rtsConfiguracion" runat="server" SelectedIndex="0" MultiPageID="rmpConfiguracion">
        <Tabs>
            <telerik:RadTab Text="Información general"></telerik:RadTab>
            <telerik:RadTab Text="Procesos"></telerik:RadTab>
            <telerik:RadTab Text="Contraseña"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>

    <div style="height: calc(100% - 130px); padding-top: 10px;">

        <telerik:RadMultiPage ID="rmpConfiguracion" runat="server" SelectedIndex="0" Height="100%">

            <telerik:RadPageView ID="rpvInformacionGeneral" runat="server">

                <div style="clear: both;"></div>

                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblClUsuario">Usuario:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtClUsuario" name="txtClUsuario" Width="300px" runat="server"></telerik:RadTextBox><br />
                        <asp:RequiredFieldValidator ID="reqtxtClUsuario" runat="server" Display="Dynamic" ControlToValidate="txtClUsuario" ErrorMessage="Campo obligatorio" CssClass="validacion"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div style="clear: both;"></div>

                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblEmpleado">Empleado:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadListBox ID="lstEmpleado" Width="300px" runat="server" OnClientItemDoubleClicking="OpenEmployeeSelectionWindow"></telerik:RadListBox>
                        <telerik:RadButton ID="btnBuscarEmpleado" runat="server" Text="B" OnClientClicked="OpenEmployeeSelectionWindow" AutoPostBack="false"></telerik:RadButton>
                    </div>
                </div>

                <div style="clear: both;"></div>

                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblNbUsuario">Nombre:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtNbUsuario" name="txtNbUsuario" runat="server" Width="300px"></telerik:RadTextBox><br />
                        <asp:RequiredFieldValidator ID="reqTxtNbUsuario" runat="server" Display="Dynamic" ControlToValidate="txtNbUsuario" ErrorMessage="Campo obligatorio" CssClass="validacion"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div style="clear: both;"></div>

                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblCorreoElectronico">Correo electrónico:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtNbCorreoElectronico" name="txtNbCorreoElectronico" runat="server" Width="300"></telerik:RadTextBox><br />
                        <asp:RequiredFieldValidator ID="reqTxtNbCorreoElectronico" runat="server" Display="Dynamic" ControlToValidate="txtNbCorreoElectronico" ErrorMessage="Campo obligatorio" CssClass="validacion"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div style="clear: both;"></div>

                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblRol">Rol:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadComboBox ID="cmbRol" runat="server" Width="300px"></telerik:RadComboBox>
                    </div>
                </div>

                <div style="clear: both;"></div>

                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblActivo">Activo:</label>
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

                <div style="clear: both; height: 20px;"></div>

                <div class="divControlDerecha">
                    <div class="ctrlBasico">
                        <telerik:RadButton ID="btnGuardarInformacionGeneral" runat="server" name="btnGuardarInformacionGeneral" AutoPostBack="true" Text="Guardar" OnClick="btnGuardarInformacionGeneral_Click" ></telerik:RadButton>
                    </div>
                </div>

            </telerik:RadPageView>

            <telerik:RadPageView ID="rpvProcesos" runat="server">

                <div style="margin-left: 20px;">

                    <label id="lbPeriodo" name="lbTabulador" runat="server">Acceso a:</label>

                    <div style="clear: both; height: 20px;"></div>

                    <div class="ctrlBasico" id="Div1" runat="server">
                        <div class="divControlDerecha">
                            <telerik:RadButton ID="rdComunicados" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false" >
                                <ToggleStates>
                                    <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                    <telerik:RadButtonToggleState Text="No"  PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                </ToggleStates>
                            </telerik:RadButton>
                            <label id="lblMisComunicados">Mis comunicados.</label>
                        </div>
                    </div>

                    <div style="clear: both; height: 20px;"></div>

                    <div class="ctrlBasico" id="Div2" runat="server">
                        <div class="divControDerecha">
                            <telerik:RadButton ID="rdTramites" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false" >
                                <ToggleStates>
                                    <telerik:RadButtonToggleState Text="Sí"  PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                    <telerik:RadButtonToggleState Text="No"  PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                </ToggleStates>
                            </telerik:RadButton>
                            <label id="lblMisTramites">Mis Trámites.</label>
                        </div>
                    </div>

                    <div style="clear: both; height: 20px;"></div>

                    <div class="ctrlBasico" id="Div3" runat="server">
                        <div class="divControDerecha">
                            <telerik:RadButton ID="rdCompromisos" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false" >
                                <ToggleStates>
                                    <telerik:RadButtonToggleState Text="Sí"  PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                    <telerik:RadButtonToggleState Text="No"  PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                </ToggleStates>
                            </telerik:RadButton>
                            <label id="lblMisCompromisos">Mis Compromisos.</label>
                        </div>
                    </div>

                    <div style="clear: both; height: 20px;"></div>

                    <div class="ctrlBasico" id="Div4" runat="server">
                        <div class="divControDerecha">
                            <telerik:RadButton ID="rdNomina" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false" >
                                <ToggleStates>
                                    <telerik:RadButtonToggleState Text="Sí"  PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                    <telerik:RadButtonToggleState Text="No"  PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                </ToggleStates>
                            </telerik:RadButton>
                            <label id="lblMisNomina">Mi Nómina.</label>
                        </div>
                    </div>
                </div>

                <div style="clear: both; height: 20px;"></div>

                <div class="divControlDerecha">
                    <div class="ctrlBasico">
                        <telerik:RadButton ID="btnGuardarProcesos" runat="server" name="btnGuardarProcesos" AutoPostBack="true" Text="Guardar" OnClick="btnGuardarProcesos_Click"></telerik:RadButton>
                    </div>
                </div>

            </telerik:RadPageView>

            <telerik:RadPageView ID="rpvContraseña" runat="server">
                <div style="clear: both;"></div>

                <div style="height: calc(100% - 100px);">

                    <telerik:RadGrid ID="grdContrasenas" runat="server" 
                        Height="100%" AutoGenerateColumns="false" ShowGroupPanel="True" AllowPaging="true"
                        AllowSorting="true" HeaderStyle-Font-Bold="true">
                        <ClientSettings>
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView ClientDataKeyNames="CL_USUARIO, FG_ACTIVO,FG_ENVIO" DataKeyNames="CL_USUARIO, FG_ACTIVO,FG_ENVIO" AutoGenerateColumns="false" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true">
                            <Columns>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Usuario" DataField="CL_USUARIO" UniqueName="CL_USUARIO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="160" FilterControlWidth="120" HeaderText="Nombre" DataField="NB_USUARIO" UniqueName="NB_USUARIO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="20" HeaderStyle-Width="70 " HeaderText="Contraseña" DataField="" UniqueName=""></telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>

            </telerik:RadPageView>

        </telerik:RadMultiPage>
    </div>

    <telerik:RadWindowManager ID="rwmAlertas" runat="server"></telerik:RadWindowManager>
    <telerik:RadWindowManager ID="rnExito" runat="server"></telerik:RadWindowManager>
</asp:Content>
