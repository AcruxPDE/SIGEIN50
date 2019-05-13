<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/MenuFYD.master" AutoEventWireup="true" CodeBehind="Cursos.aspx.cs" Inherits="SIGE.WebApp.FYD.Cursos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script id="MyScript" type="text/javascript">
        function ShowInsertForm() {
            OpenWindow(null);
        }

        function ShowEditForm() {
            var selectedItem = $find("<%=grdCursos.ClientID %>").get_masterTableView().get_selectedItems()[0];
            if (selectedItem != undefined)
                OpenWindow(selectedItem.getDataKeyValue("ID_CURSO"));
            else
                radalert("Selecciona un curso.", 400, 150);
        }

        function OpenWindow(pIdCurso) {
            var vURL = "VentanaCursos.aspx";
            var vTitulo = "Agregar curso";
            if (pIdCurso != null) {
                vURL = vURL + "?CursoId=" + pIdCurso;
                vTitulo = "Editar Curso";
            }
            var windowProperties = {};
            windowProperties.width = document.documentElement.clientWidth - 400;
            windowProperties.height = document.documentElement.clientHeight - 40;
            openChildDialog(vURL, "winCurso", vTitulo, windowProperties);
        }

        function onCloseWindow(oWnd, args) {
            $find("<%= grdCursos.ClientID%>").get_masterTableView().rebind();
        }

        function confirmarEliminar(sender, args) {
            var masterTable = $find("<%= grdCursos.ClientID %>").get_masterTableView();
            var selectedItem = masterTable.get_selectedItems()[0];
            if (selectedItem != undefined) {
                var vNombre = masterTable.getCellByColumnUniqueName(selectedItem, "NB_CURSO").innerHTML;

                var vWindowsProperties = {
                    height: 250
                };

                confirmAction(sender, args, "¿Deseas eliminar el curso " + vNombre + "?, este proceso no podrá revertirse");
            }
            else {
                radalert("Selecciona un curso.", 400, 150);
                args.set_cancel(true);
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <label class="labelTitulo">Cursos</label>
    <div style="height: calc(100% - 100px);">
        <telerik:RadGrid ID="grdCursos" runat="server" HeaderStyle-Font-Bold="true" ShowHeader="true" AllowPaging="true" AllowSorting="true" Width="100%" Height="100%" AllowFilteringByColumn="true" OnNeedDataSource="grdCursos_NeedDataSource" OnItemCommand="grdCursos_ItemCommand" OnItemDataBound="grdCursos_ItemDataBound">
            <GroupingSettings CaseSensitive="true" />
            <ClientSettings AllowKeyboardNavigation="true">
                <Selecting AllowRowSelect="true" />
                <Scrolling AllowScroll="true" UseStaticHeaders="true"></Scrolling>
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" />
            <MasterTableView ClientDataKeyNames="ID_CURSO,CL_CURSO" DataKeyNames="ID_CURSO" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" PageSize="10">
                <NestedViewTemplate>
                    <div style="clear: both; height: 10px;"></div>
                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="rmpInstructor" SelectedIndex="0">
                        <Tabs>
                            <telerik:RadTab Text="Instructores"></telerik:RadTab>
                            <telerik:RadTab Text="Competencias"></telerik:RadTab>
                            <telerik:RadTab Text="Temas"></telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <div style="clear: both; height: 10px;"></div>
                    <telerik:RadMultiPage ID="rmpInstructor" runat="server" SelectedIndex="0">

                        <telerik:RadPageView ID="rpvInstructor" runat="server">
                            <div style="clear: both; height: 10px;"></div>
                            <telerik:RadGrid ID="grdCursosInstructor" HeaderStyle-Font-Bold="true" runat="server" ShowHeader="true"
                                AllowSorting="true" Width="670">
                                <ClientSettings AllowKeyboardNavigation="true">
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>
                                <MasterTableView ClientDataKeyNames="ID_INSTRUCTOR" DataKeyNames="ID_INSTRUCTOR" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false">
                                    <Columns>
                                        <telerik:GridBoundColumn HeaderText="Clave" DataField="CL_INSTRUCTOR" UniqueName="CL_INSTRUCTOR" HeaderStyle-Width="175"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Nombre" DataField="NB_INSTRUCTOR" UniqueName="NB_INSTRUCTOR"></telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </telerik:RadPageView>

                        <telerik:RadPageView ID="rpvCompetencias" runat="server">
                            <telerik:RadGrid ID="grdCursosCompetencia" HeaderStyle-Font-Bold="true" runat="server" ShowHeader="true"
                                AllowSorting="true" Width="670">
                                <GroupingSettings CaseSensitive="true" />
                                <ClientSettings AllowKeyboardNavigation="true">
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>
                                <MasterTableView ClientDataKeyNames="ID_ITEM" DataKeyNames="ID_COMPETENCIA" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false">
                                    <Columns>
                                        <telerik:GridBoundColumn HeaderText="Clave" DataField="CL_TIPO_COMPETENCIA" UniqueName="CL_TIPO_COMPETENCIA" HeaderStyle-Width="175"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Nombre" DataField="NB_COMPETENCIA" UniqueName="NB_COMPETENCIA"></telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </telerik:RadPageView>

                        <telerik:RadPageView ID="rpvTemas" runat="server">
                            <telerik:RadGrid ID="grdCursosTema" HeaderStyle-Font-Bold="true" runat="server" ShowHeader="true"
                                AllowSorting="true" Width="670">
                                <GroupingSettings CaseSensitive="true" />
                                <ClientSettings AllowKeyboardNavigation="true">
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>
                                <MasterTableView ClientDataKeyNames="ID_TEMA" DataKeyNames="ID_TEMA" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false">
                                    <Columns>
                                        <telerik:GridBoundColumn HeaderText="Clave" DataField="CL_TEMA" UniqueName="CL_TEMA" HeaderStyle-Width="175"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Nombre" DataField="NB_TEMA" UniqueName="NB_TEMA"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Duración" DataField="NO_DURACION" UniqueName="NO_DURACION"></telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </telerik:RadPageView>

                    </telerik:RadMultiPage>
                    <div style="clear: both; height: 10px;"></div>
                </NestedViewTemplate>
                <Columns>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave" DataField="CL_CURSO" UniqueName="CL_CURSO" HeaderStyle-Width="180" FilterControlWidth="110"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre" DataField="NB_CURSO" UniqueName="NB_CURSO" HeaderStyle-Width="300" FilterControlWidth="225"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Tipo" DataField="CL_TIPO_CURSO" UniqueName="CL_TIPO_CURSO" HeaderStyle-Width="100" FilterControlWidth="40"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Duración" DataField="NO_DURACION" UniqueName="NO_DURACION" HeaderStyle-Width="100" FilterControlWidth="40" DataFormatString="{0:N1}h"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="120" FilterControlWidth="60" HeaderText="Último usuario que modifica" DataField="CL_USUARIO_APP_MODIFICA" UniqueName="CL_USUARIO_APP_MODIFICA"></telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Última fecha de modificación" DataField="FE_MODIFICA" UniqueName="FE_MODIFICA" DataFormatString="{0:d}"  HeaderStyle-Width="120" FilterControlWidth="60" DataType="System.DateTime"></telerik:GridDateTimeColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
    <div style="clear: both; height: 10px;"></div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnGuardar" OnClientClicked="ShowInsertForm" AutoPostBack="false" runat="server" Text="Agregar"></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnEditar" OnClientClicked="ShowEditForm" AutoPostBack="false" runat="server" Text="Editar"></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnEliminar" OnClientClicking="confirmarEliminar" AutoPostBack="true" OnClick="btnEliminar_Click" runat="server" Text="Eliminar"></telerik:RadButton>
    </div>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="winCurso" runat="server" Title="Agregar/Editar Curso" Behaviors="Close" Modal="true" VisibleStatusbar="false" OnClientClose="onCloseWindow"></telerik:RadWindow>
            <telerik:RadWindow ID="winAgregarTema" runat="server" Title="Seleccionar curso" Height="520px" Width="680px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
            <telerik:RadWindow ID="winSeleccion" runat="server" Title="Seleccionar Puesto" Height="620px" Width="850" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
            <telerik:RadWindow ID="winSeleccionCompetencia" runat="server" Title="Seleccionar competencia" Height="620px" Width="880px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
            <telerik:RadWindow ID="winSeleccionInstructor" runat="server" Title="Seleccionar Instructor" Height="570px" Width="620px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
            <telerik:RadWindow ID="winMaterial" runat="server" Title="Agregar Material" Height="290px" Width="600px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
            <telerik:RadWindow ID="winSeleccionArea" runat="server" Title="Seleccionar área temática" Height="620px" Width="880px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
</asp:Content>
