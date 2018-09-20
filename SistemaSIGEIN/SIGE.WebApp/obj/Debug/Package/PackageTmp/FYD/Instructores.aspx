<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/MenuFYD.master" AutoEventWireup="true" CodeBehind="Instructores.aspx.cs" Inherits="SIGE.WebApp.FYD.Instructores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script id="MyScript" type="text/javascript">
        function ShowInsertForm() {
            OpenWindow(null);
        }

        function ShowEditForm() {
            var selectedItem = $find("<%=grdInstructores.ClientID %>").get_masterTableView().get_selectedItems()[0];
            if (selectedItem != undefined)
                OpenWindow(selectedItem.getDataKeyValue("ID_INSTRUCTOR"));
            else
                radalert("Selecciona un instructor.", 400, 150);
        }

        function OpenWindow(pIdInstructor) {
            var vURL = "VentanaInstructores.aspx";
            var vTitulo = "Agregar Instructor";
            if (pIdInstructor != null) {
                vURL = vURL + "?InstructorId=" + pIdInstructor;
                vTitulo = "Editar Instructor";
            }
            var windowProperties = {};
            windowProperties.width = 900;
            windowProperties.height = document.documentElement.clientHeight - 40;
            openChildDialog(vURL, "winInstructor", vTitulo, windowProperties);
        }

        function onCloseWindow(oWnd, args) {
            $find("<%= grdInstructores.ClientID%>").get_masterTableView().rebind();
        }

        function confirmarEliminar(sender, args) {
            var masterTable = $find("<%= grdInstructores.ClientID %>").get_masterTableView();
            var selectedItem = masterTable.get_selectedItems()[0];
            if (selectedItem != undefined) {
                var vNombre = masterTable.getCellByColumnUniqueName(selectedItem, "NB_INSTRUCTOR").innerHTML;

                var vWindowsProperties = {
                    height: 250
                };

                confirmAction(sender, args, "¿Deseas eliminar al instructor " + vNombre + "?, este proceso no podrá revertirse");
            }
            else {
                radalert("Selecciona un instructor.", 400, 250);
                args.set_cancel(true);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server"></telerik:RadAjaxLoadingPanel>
    <label class="labelTitulo">Instructores</label>
    <div style="height: calc(100% - 100px);">
        <telerik:RadSplitter ID="splInstructores" runat="server" Width="100%" Height="100%" BorderSize="0">
            <telerik:RadPane ID="rpnGridInstructores" runat="server">
                <telerik:RadGrid ID="grdInstructores" runat="server" HeaderStyle-Font-Bold="true" ShowHeader="true" AllowPaging="true"
                    AllowSorting="true" Width="100%"
                    Height="100%"
                    AllowFilteringByColumn="true"
                    OnNeedDataSource="grdInstructores_NeedDataSource" OnItemCommand="grdInstructores_ItemCommand" OnItemDataBound="grdInstructores_ItemDataBound">
                    <GroupingSettings CaseSensitive="true" />
                    <ClientSettings AllowKeyboardNavigation="true">
                        <Selecting AllowRowSelect="true" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true"></Scrolling>
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="true" />
                    <MasterTableView ClientDataKeyNames="ID_INSTRUCTOR,CL_INTRUCTOR" DataKeyNames="ID_INSTRUCTOR" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" PageSize="10">
                        <NestedViewTemplate>
                            <div style="clear: both; height: 10px;"></div>
                            <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="rmpInstructor" SelectedIndex="0">
                                <Tabs>
                                    <telerik:RadTab Text="Cursos"></telerik:RadTab>
                                    <telerik:RadTab Text="Competencias"></telerik:RadTab>
                                    <telerik:RadTab Text="Contactos"></telerik:RadTab>
                                </Tabs>
                            </telerik:RadTabStrip>
                            <div style="clear: both; height: 10px;"></div>
                            <telerik:RadMultiPage ID="rmpInstructor" runat="server" SelectedIndex="0">
                                <telerik:RadPageView ID="rpvCursos" runat="server">
                                    <div style="clear: both; height: 10px;"></div>
                                    <telerik:RadGrid ID="grdCursos" runat="server" HeaderStyle-Font-Bold="true" ShowHeader="true"
                                        AllowSorting="true" Width="670">
                                        <ClientSettings AllowKeyboardNavigation="true">
                                            <Selecting AllowRowSelect="true" />
                                        </ClientSettings>
                                        <MasterTableView ClientDataKeyNames="ID_ITEM" DataKeyNames="ID_CURSO" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false">
                                            <Columns>
                                                <telerik:GridBoundColumn HeaderText="Clave" DataField="CL_CURSO" UniqueName="CL_CURSO" HeaderStyle-Width="175"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Nombre" DataField="NB_CURSO" UniqueName="NB_CURSO"></telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </telerik:RadPageView>
                                <telerik:RadPageView ID="rpvCompetencias" runat="server">
                                    <telerik:RadGrid ID="grdCompetencia" runat="server" HeaderStyle-Font-Bold="true" ShowHeader="true"
                                        AllowSorting="true" Width="670">
                                        <GroupingSettings CaseSensitive="true" />
                                        <ClientSettings AllowKeyboardNavigation="true">
                                            <Selecting AllowRowSelect="true" />
                                        </ClientSettings>
                                        <MasterTableView ClientDataKeyNames="ID_ITEM" DataKeyNames="ID_COMPETENCIA" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false">
                                            <Columns>
                                                <telerik:GridBoundColumn HeaderText="Clave" DataField="CL_COMPETENCIA" UniqueName="CL_COMPETENCIA" HeaderStyle-Width="175"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Nombre" DataField="NB_COMPETENCIA" UniqueName="NB_COMPETENCIA"></telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </telerik:RadPageView>
                                <telerik:RadPageView ID="rpvContactos" runat="server">
                                    <div class="ctrlBasico">
                                        <div class="ctrlBasico">
                                            <table class="ctrlTableForm">
                                                <tr>
                                                    <td class="ctrlTableDataContext">
                                                        <label>Correo electrónico:</label></td>
                                                    <td colspan="2" class="ctrlTableDataBorderContext">
                                                        <div id="txtTblEmail" runat="server" style="min-width: 100px;" enabled="false"></div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    <div style="clear: both;"></div>
                                    <telerik:RadGrid ID="grdTelefono" runat="server" HeaderStyle-Font-Bold="true" Width="250px">
                                        <ClientSettings AllowKeyboardNavigation="true">
                                            <Selecting AllowRowSelect="true" />
                                        </ClientSettings>
                                        <MasterTableView ClientDataKeyNames="ID_ITEM" DataKeyNames="ID_ITEM" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" HorizontalAlign="NotSet">
                                            <Columns>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" HeaderText="Teléfono" DataField="NB_TELEFONO" UniqueName="NB_TELEFONO"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" HeaderStyle-Width="0" HeaderText="Tipo" DataField="CL_TIPO" UniqueName="CL_TIPO" Visible="false"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" HeaderText="Tipo" DataField="NB_TIPO" UniqueName="NB_TIPO"></telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </telerik:RadPageView>
                            </telerik:RadMultiPage>
                            <div style="clear: both; height: 10px;"></div>
                        </NestedViewTemplate>
                        <Columns>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave" DataField="CL_INTRUCTOR" UniqueName="CL_INTRUCTOR" HeaderStyle-Width="180" FilterControlWidth="110"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Tipo" DataField="CL_TIPO_INSTRUCTOR" UniqueName="CL_TIPO_INSTRUCTOR" HeaderStyle-Width="100" FilterControlWidth="40"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre" DataField="NB_INSTRUCTOR" UniqueName="NB_INSTRUCTOR" HeaderStyle-Width="350" FilterControlWidth="275"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="120" FilterControlWidth="60" HeaderText="Último usuario que modifica" DataField="CL_USUARIO_APP_MODIFICA" UniqueName="CL_USUARIO_APP_MODIFICA"></telerik:GridBoundColumn>
                             <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Última fecha de modificación" DataField="FE_MODIFICA" UniqueName="FE_MODIFICA" DataFormatString="{0:d}"  HeaderStyle-Width="120" FilterControlWidth="60" DataType="System.DateTime"></telerik:GridDateTimeColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </telerik:RadPane>
            <telerik:RadPane ID="rpnOpcionesBusqueda" runat="server" Width="21">
                <telerik:RadSlidingZone ID="slzOpcionesBusqueda" runat="server" Width="18" ClickToOpen="true" SlideDirection="Left">
                    <telerik:RadSlidingPane ID="RSPAdvSearchInstructores" runat="server" Title="Buscar instructores por" Width="540" MinWidth="450" Height="100%">
                        <div style="padding: 20px;">
                            <div style="clear: both;"></div>
                            <div class="ctrlBasico">
                                <div class="divControlIzquierda">
                                    <label id="lblCompetencia" name="lblCompetencia" runat="server">Competencia:</label>
                                </div>
                                <div class="divControlDerecha">
                                    <telerik:RadComboBox ID="cmbCompetencia" runat="server" Width="300px" DataTextField="NB_COMPETENCIA" DataValueField="ID_COMPETENCIA"></telerik:RadComboBox>
                                </div>
                            </div>
                            <div style="clear: both;"></div>
                            <div class="ctrlBasico">
                                <div class="divControlIzquierda">
                                    <label id="lblCurso" name="lblCurso" runat="server">Curso:</label>
                                </div>
                                <div class="divControlDerecha">
                                    <telerik:RadComboBox ID="cmbCurso" Width="300px" runat="server" DataTextField="" DataValueField=""></telerik:RadComboBox>
                                </div>
                            </div>
                            <div style="clear: both;"></div>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnBuscar" OnClick="btnBuscar_Click" AutoPostBack="true" runat="server" Text="Buscar" Width="100"></telerik:RadButton>
                            </div>
                        </div>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </div>
    <div style="clear: both; height: 10px;"></div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnGuardar" OnClientClicked="ShowInsertForm" AutoPostBack="false" runat="server" Text="Agregar"></telerik:RadButton>
        </div>
       <div class="ctrlBasico">
        <telerik:RadButton ID="btnEditar" OnClientClicked="ShowEditForm" AutoPostBack="false" runat="server" Text="Editar" ></telerik:RadButton>
                   </div>
       <div class="ctrlBasico">
        <telerik:RadButton ID="btnEliminar" OnClientClicking="confirmarEliminar" AutoPostBack="true" OnClick="btnEliminar_Click" runat="server" Text="Eliminar"></telerik:RadButton>
    </div>

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="winSeleccionLocalizacion" runat="server" Title="Seleccionar localización" Height="620px" Width="550" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
            <telerik:RadWindow ID="winSeleccionCP" runat="server" Title="Seleccionar localización" Height="620px" Width="800" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
            <telerik:RadWindow ID="winSeleccionCompetencia" runat="server" Title="Seleccionar competencia" Height="620px" Width="880px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
            <telerik:RadWindow ID="winSeleccionCurso" runat="server" Title="Seleccionar curso" Height="620px" Width="680px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
            <telerik:RadWindow ID="winEmpleado" runat="server" Title="Seleccionar empleado" Width="750px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
            <telerik:RadWindow ID="winInstructor" runat="server" Title="Agregar/Editar Instructor" Behaviors="Close" Modal="true" VisibleStatusbar="false" OnClientClose="onCloseWindow"></telerik:RadWindow>
            <telerik:RadWindow ID="rwmAlertas" runat="server" Title="Agregar/Editar Rol" Height="500px" Width="900px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close" OnClientClose="onCloseWindow"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

</asp:Content>





