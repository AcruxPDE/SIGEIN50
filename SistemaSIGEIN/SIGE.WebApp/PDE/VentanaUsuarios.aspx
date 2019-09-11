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

                if (pEmpleados != null) {
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
                $find("<%= txtNbCorreoElectronico.ClientID %>").set_value(vEmpleadoSeleccionado.nbCorreoElectronico);
                $find("<%= txtClUsuario.ClientID %>").set_value(vEmpleadoSeleccionado.clEmpleado);
                //$find(" txtContrasena.ClientID %>").set_value(vEmpleadoSeleccionado.Contyr);
            }
        }
    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server" SelectedIndex="0">

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" Visible="false" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxLoadingPanel ID="ralpNomina" runat="server"></telerik:RadAjaxLoadingPanel>
      <telerik:RadAjaxManager ID="ramNomina" runat="server" DefaultLoadingPanelID="ralpNomina">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lstEmpleado" />
                    <telerik:AjaxUpdatedControl ControlID="grdEmpleados" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>          
        </AjaxSettings>
    </telerik:RadAjaxManager>
     
   <%-- <div style="height: calc(100% - 30px);">--%>
        <telerik:RadTabStrip ID="rtsUsuarios" runat="server" MultiPageID="mpUsuarios" SelectedIndex="0">
            <Tabs>
                <telerik:RadTab TabIndex="0" Text="Único"></telerik:RadTab>
                <telerik:RadTab TabIndex="1" Text="Masivo"></telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <telerik:RadMultiPage ID="mpUsuarios" runat="server" SelectedIndex="0" Height="90%" >
            <telerik:RadPageView ID="rpvUsuario" runat="server" Height="90%">
                <div style="height: calc(100% - 20px);">
                    <br />
                       <div class="ctrlBasico" style="padding-left:20px;"> 
                    <%-- <div class="ctrlBasico">
                                    <telerik:RadButton RenderMode="Lightweight" ID="rbLigado" AccessKey="2" runat="server" ButtonType="ToggleButton" OnClientClicked=""
                                            ToggleType="Radio" Checked="true" AutoPostBack="true"  OnClick="Tipo_Click" GroupName="tipo" Text="Ligado a empleado" Visible="false" Font-Bold="true" Font-Size="Medium">
                                        </telerik:RadButton>
                                  
                                    </div>
                     <div class="ctrlBasico">
                                        <telerik:RadButton RenderMode="Lightweight" ID="rbNoLigado" AccessKey="2" runat="server" ButtonType="ToggleButton" OnClick="TipoNl_Click"
                                            ToggleType="Radio" GroupName="tipo" Text="No ligado a empleado" Visible="true" Font-Bold="true" Font-Size="Medium"   AutoPostBack="true">
                                        </telerik:RadButton></div>
                                    </div>--%>
                  <div class="ctrlBasico">
                    <div class="divControlIzquierda" style="width: 250px;">
                        <label>Buscar usuario:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadListBox runat="server" ID="lstEmpleado" Width="300px" AutoPostBack="false" >
                            <Items>
                                <telerik:RadListBoxItem Value="0" Text="No Seleccionado" />
                            </Items>
                        </telerik:RadListBox>
                        <telerik:RadButton runat="server" ID="btnBuscar" Text="B" AutoPostBack="false"
                            OnClientClicked="OpenEmployeeSelectionWindow">
                        </telerik:RadButton>
                    </div>
                </div>
                   <%--<telerik:RadTextBox ID="id_empleado" name="txtNbUsuario" runat="server" Visible="false" Width="300"></telerik:RadTextBox><br />--%>

                     <div style="clear: both; height: 10px;"></div>
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda">
                            <label name="lblClUsuario">Clave:</label>
                        </div>
                        <%--<div class="divControlDerecha">
                            <telerik:RadComboBox ID="cmbUsuarios" runat="server" Width="300px" EmptyMessage="Selecciona un usuario" AutoPostBack="true" OnSelectedIndexChanged="cmbUusarios_SelectedIndexChanged">
                            </telerik:RadComboBox>
                              <telerik:RadComboBox ID="cmbUsuariosNl" runat="server" Width="300px" EmptyMessage="Selecciona un usuario" AutoPostBack="true" OnSelectedIndexChanged="cmbUsuariosNl_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </div>--%>
                        <div class="divControlDerecha">
                            <telerik:RadTextBox ID="txtClUsuario" name="txtClUsuario" runat="server"></telerik:RadTextBox><br />
                            <%--<asp:RequiredFieldValidator ID="reqtxtClUsuario" runat="server" Display="Dynamic" ControlToValidate="txtClUsuario" ErrorMessage="Campo obligatorio" CssClass="validacion"></asp:RequiredFieldValidator>--%>
                        </div>
                    </div>
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda">
                            <label name="lblNbUsuario">Nombre:</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadTextBox ID="txtNbUsuario"  name="txtNbUsuario" runat="server" Width="300"></telerik:RadTextBox><br />
                        </div>
                    </div>
                    <div class="ctrlBasico">

                        <telerik:RadTextBox Visible="false" ID="txtContrasena"  name="txtContrasena" runat="server" Width="300"></telerik:RadTextBox><br />
                       
                    </div>
                    <div class="ctrlBasico">
                        <div style="clear: both; height: 10px;"></div>
                        <div class="ctrlBasico">
                            <label name="lblNbUsuario">Correo electrónico:</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadTextBox ID="txtNbCorreoElectronico" name="txtNbCorreoElectronico" runat="server" Width="300"></telerik:RadTextBox><br />
                            <%--<asp:RequiredFieldValidator ID="reqTxtNbCorreoElectronico" runat="server" Display="Dynamic" ControlToValidate="txtNbCorreoElectronico" ErrorMessage="Campo obligatorio" CssClass="validacion"></asp:RequiredFieldValidator>--%>
                        </div>
                    </div>
                    <div class="ctrlBasico">
                        <div style="clear: both; height: 10px;"></div>

                        <div class="divControlIzquierda">
                            <label name="lblRol">Rol:</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadComboBox ID="cmbRol" runat="server"></telerik:RadComboBox>
                        </div>
                    </div>
                    <div style="clear: both; height: 10px;"></div>
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
                </div>
                <div class="divControlDerecha">
                    <div class="ctrlBasico">
                        <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click"></telerik:RadButton>
                    </div>
                    <div class="ctrlBasico">
                        <telerik:RadButton ID="btnCancelar" runat="server" Text="Cancelar" AutoPostBack="false" OnClientClicked="closeWindow"></telerik:RadButton>
                    </div>
                </div>
                    </div>

            </telerik:RadPageView>
             <%--</div>--%>
             <telerik:RadPageView ID="rpvUsuarios" runat="server" Height="100%">
                <div style="height: calc(100% - 20px); padding-left:20px;"">
                <div style="clear: both; height: 10px;" ></div>

                   <telerik:RadGrid ID="grdEmpleados" ShowHeader="true" runat="server"
            AllowPaging="true" AllowSorting="true" GroupPanelPosition="Top" Width="99%" Height="100%"
            ClientSettings-EnablePostBackOnRowClick="false" AllowFilteringByColumn="true"
            OnNeedDataSource="grdEmpleados_NeedDataSource" AllowMultiRowSelection="true">
                       
                           <ClientSettings AllowKeyboardNavigation="true">
                                <Selecting AllowRowSelect="true" UseClientSelectColumnOnly="true" />
                                <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                            </ClientSettings>
                           <PagerStyle AlwaysVisible="true" />
                           <GroupingSettings CaseSensitive="false" />
                           <MasterTableView ClientDataKeyNames="ID_EMPLEADO, NB_EMPLEADO, NB_PATERNO, CORREO_ELECTRONICO, ID_USUARIO, CONTRASENA, ID_Grupo, ID_ROL" DataKeyNames="ID_EMPLEADO, NB_EMPLEADO, NB_PATERNO, CORREO_ELECTRONICO, ID_USUARIO, CONTRASENA, ID_Grupo, ID_ROL" EnableColumnsViewState="false" AllowPaging="false" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true"  EnableHeaderContextFilterMenu="true">
                               <Columns>
                                   <telerik:GridClientSelectColumn HeaderStyle-Width="35" ></telerik:GridClientSelectColumn>
                                   <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="190" FilterControlWidth="130" HeaderText="Nombre" DataField="NB_EMPLEADO" UniqueName="NB_EMPLEADO"></telerik:GridBoundColumn>
                                   <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="190" FilterControlWidth="130" HeaderText="Apellidos" DataField="NB_PATERNO" UniqueName="NB_PATERNO"></telerik:GridBoundColumn>
                                   <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="250" FilterControlWidth="170" HeaderText="Correo" DataField="CORREO_ELECTRONICO" UniqueName="CORREO_ELECTRONICO"></telerik:GridBoundColumn>
                                   <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="90" HeaderText="Usuario" DataField="ID_USUARIO" UniqueName="ID_USUARIO"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="180" FilterControlWidth="90" HeaderText="Contraseña" DataField="CONTRASENA" UniqueName="CONTRASENA"></telerik:GridBoundColumn>
                               </Columns>
                           </MasterTableView>
                       </telerik:RadGrid>
                    <div style="clear: both; height: 10px;"></div>
                        <div class="divControlDerecha">
                    <div class="ctrlBasico">
                        <telerik:RadButton ID="btnGuardarUsuarios" runat="server" Text="Guardar" OnClick="btnGuardarUsuarios_Click"></telerik:RadButton>
                    </div>
                    <div class="ctrlBasico">
                        <telerik:RadButton ID="btnCancelarUsuarios" runat="server" Text="Cancelar" AutoPostBack="false" OnClientClicked="closeWindow"></telerik:RadButton>
                    </div>
                </div>
                    </div>
           </telerik:RadPageView>
        </telerik:RadMultiPage>
   
    <telerik:RadWindowManager ID="rwmAlertas" runat="server"></telerik:RadWindowManager>
</asp:Content>
