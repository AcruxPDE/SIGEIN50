<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="VentanaCursos.aspx.cs" Inherits="SIGE.WebApp.FYD.VentanaCursos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script id="MyScript" type="text/javascript">

        function ShowEditForm() {
            var selectedItem = $find("<%=grdCursoTema.ClientID %>").get_masterTableView().get_selectedItems()[0];
            if (selectedItem != undefined)
                OpenWindow(selectedItem.getDataKeyValue("ID_ITEM"));
            else
                radalert("Selecciona un tema.", 400, 150);
        }

        function OpenWindow(pIdTemaItem) {
            var vURL = "VentanaTemas.aspx";
            vURL = vURL + "?IdCursoItem=" + '<%# vIdListaCurso %>' + "&IdTemaItem=" + pIdTemaItem;
            vTitulo = "Editar Tema";

            var windowProperties = {};
            windowProperties.width = document.documentElement.clientWidth - 40;
            windowProperties.height = document.documentElement.clientHeight - 40;
            openChildDialog(vURL, "winAgregarTema", vTitulo, windowProperties);
        }

        function OpenSelectionWindow(sender, args) {
            var vBtnGuardarTema = $find("<%= rpvVentanaTemas.FindControl("radBtnGuardarTema").ClientID %>");
            var vBtnPuesto = $find("<%= rmpVentanaCurso.FindControl("radBtnBuscarPuesto").ClientID %>");
            var vBtnGuardarCompetencia = $find("<%= rpvVentanaCompetencias.FindControl("radBtnGuardarCompetencia").ClientID %>");
            var vBtnGuardarInstructor = $find("<%= rpvVentanaInstructores.FindControl("radBtnGuardarInstructor").ClientID %>");
            //radBtnEditarTema

            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 40,
                height: browserWnd.innerHeight - 40
            };



            if (sender == vBtnGuardarTema) {
                var windowProperties = {};
                windowProperties.width = document.documentElement.clientWidth - 40;
                windowProperties.height = document.documentElement.clientHeight - 40;
                openChildDialog("VentanaTemas.aspx?IdCursoItem=" + '<%# vIdListaCurso %>', "winAgregarTema", "Agregar Tema", windowProperties);
            }

            if (sender == vBtnPuesto)
                openChildDialog("../Comunes/SeleccionPuesto.aspx", "winSeleccion", "Selección de puesto", windowProperties);

            if (sender == vBtnGuardarCompetencia)
                openChildDialog("../Comunes/SeleccionCompetencia.aspx", "winSeleccionCompetencia", "Selección de competencia", windowProperties);

            if (sender == vBtnGuardarInstructor)
               // openChildDialog("/Comunes/SeleccionInstructor.aspx?IdCurso=" + '<%# vIdListaCurso %>' + "&IdCursoInstructor=" + '<%# vCursoId%>' , "winSeleccionInstructor", "Selección de instructor");
              //openChildDialog("/Comunes/SeleccionInstructor.aspx?IdCurso=" + '<%# vIdListaCurso %>' + "&IdCursoInstructor=" + '<%# vCursoId%>' , "winSeleccionInstructor", "Selección de instructor");
                openChildDialog("../Comunes/SeleccionInstructor.aspx?IdCurso=" + '<%# vIdListaCurso %>', "winSeleccionInstructor", "Selección de instructor" , windowProperties);
             
        }

        function useDataFromChild(pData) {
            if (pData != null) {
                var vSelectedData = pData[0];
                var list;
                switch (vSelectedData.clTipoCatalogo) {
                    case "PUESTO":
                        listPuesto = $find("<%= rpvVentanaCurso.FindControl("rlbPuesto").ClientID %>");
                        SetListBoxItem(listPuesto, "No seleccionado", 0);
                        SetListBoxItem(listPuesto, vSelectedData.nbPuesto, vSelectedData.idPuesto);
                        __doPostBack(vSelectedData.clTipoCatalogo, vSelectedData.idPuesto);
                        break;
                    case "COMPETENCIA":
                        var arrCompetencia = [];
                        for (var i = 0; i < pData.length; i++)
                            arrCompetencia.push({
                                //ID_INSTRUCTOR_COMPETENCIA : 0,
                                ID_COMPETENCIA: pData[i].idCompetencia,
                                CL_COMPETENCIA: pData[i].clCompetencia,
                                NB_COMPETENCIA: pData[i].nbCompetencia,
                                CL_TIPO_COMPETENCIA: pData[i].clTipoCompetencia
                            });
                        var datosCompetencia = JSON.stringify(arrCompetencia);
                        __doPostBack(vSelectedData.clTipoCatalogo, datosCompetencia);
                        break;
                    case "INSTRUCTOR":
                        var arrInstructor = [];
                        for (var i = 0; i < pData.length; i++)
                            arrInstructor.push({
                                //ID_INSTRUCTOR_COMPETENCIA : 0,
                                ID_INSTRUCTOR: pData[i].idInstructor,
                                CL_INSTRUCTOR: pData[i].clInstructor,
                                NB_INSTRUCTOR: pData[i].nbInstructor
                            });
                        var datosInstructor = JSON.stringify(arrInstructor);
                        __doPostBack(vSelectedData.clTipoCatalogo, datosInstructor);
                        break;


                    case "TEMA":
                        var arrTema = [];
                        for (var i = 0; i < pData.length; i++)
                            arrTema.push({
                                //ID_TEMA : 0,
                                CL_TEMA: pData[i].clTema,
                                NB_TEMA: pData[i].nbTema,
                                NO_DURACION: pData[i].duracion,
                                DS_DESCRIPCION: pData[i].descripcion,
                                LSTCOMPETENCIA: pData[i].vLstCompetencia,
                                LSTMATERIAL: pData[i].vLstMaterial
                            });
                        var datosTema = JSON.stringify(arrTema);
                        __doPostBack(vSelectedData.clTipoCatalogo, datosTema);
                        break;

                    case "AREATEMATICA":
                        var arrArea = [];
                        for (var i = 0; i < pData.length; i++)
                            arrArea.push({
                                ID_AREA_TEMATICA: pData[i].idAreaT,
                                CL_AREA_TEMATICA: pData[i].clAreaT,
                                NB_AREA_TEMATICA: pData[i].nbAreaT,
                            });
                        var datosArea = JSON.stringify(arrArea);
                        __doPostBack(vSelectedData.clTipoCatalogo, datosArea);
                        break;

                }
            }
        }

        function SetListBoxItem(list, text, value) {
            if (list != undefined) {
                list.trackChanges();

                var items = list.get_items();
                items.clear();

                var item = new Telerik.Web.UI.RadListBoxItem();
                item.set_text(text);
                item.set_value(value);
                item.set_selected(true);
                items.add(item);

                list.commitChanges();
            }
        }


        function CleanAreasSelection(sender, args) {
            var list = $find("<%=rlbPuesto.ClientID %>");
            SetListBoxItem(list, "Ninguno", "");
        }

        function closeWindow() {
            GetRadWindow().close();
        }

        function ConfirmarEliminar(sender, args) {
            var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
            { if (shouldSubmit) { this.click(); } });
            radconfirm('¿Deseas eliminar el área temática de este curso?, este proceso no podrá revertirse.', callBackFunction, 400, 170, null, "Eliminar área temática");
            args.set_cancel(true);

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramActualizaciones" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ramActualizaciones">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cmbAreaT" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblClAreaT" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblAreaT" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbAreaT">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cmbAreaT" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblClAreaT" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblAreaT" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminarAreaTCurso">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cmbAreaT" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblClAreaT" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblAreaT" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminarAreaTCurso">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnEliminarAreaTCurso" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="rmpVentanaCurso" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab Text="Curso"></telerik:RadTab>
            <telerik:RadTab Text="Competencias"></telerik:RadTab>
            <telerik:RadTab Text="Instructores"></telerik:RadTab>
            <telerik:RadTab Text="Temas"></telerik:RadTab>
            <telerik:RadTab Text="Documentación"></telerik:RadTab>
            <telerik:RadTab Text="Campos extra"></telerik:RadTab>
            <telerik:RadTab Text="STPS"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <div style="height: calc(100% - 100px); padding: 10px 10px 10px 10px;">
        <telerik:RadMultiPage ID="rmpVentanaCurso" runat="server" SelectedIndex="0">

            <telerik:RadPageView ID="rpvVentanaCurso" runat="server">
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblClave" name="lblClave" runat="server">Clave:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtClave" runat="server" Width="180px" MaxLength="50"></telerik:RadTextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                            ControlToValidate="txtClave" ForeColor="Red" CssClass="validacion" Font-Size="Smaller" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblNombre" name="lblNombre" runat="server">Nombre:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtNombre" runat="server" Width="450px" MaxLength="250"></telerik:RadTextBox>
                        <asp:RequiredFieldValidator ID="TextBoxRequiredFieldValidator" runat="server" Display="Dynamic"
                            ControlToValidate="txtNombre" ForeColor="Red" CssClass="validacion" Font-Size="Smaller" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblTipo" name="lblTipo" runat="server">Tipo:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadComboBox ID="cmbIdTipoCurso" runat="server" Width="180px">
                            <Items>
                                <telerik:RadComboBoxItem Value="INTERNO" Text="Interno" />
                                <telerik:RadComboBoxItem Value="EXTERNO" Text="Externo" />
                            </Items>
                        </telerik:RadComboBox>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="Label1" name="lblDuracion" runat="server">Duración:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadNumericTextBox ID="txtDuracion" MinValue="1" MaxValue="9999999999" runat="server" Width="100px" NumberFormat-DecimalDigits="0" IncrementSettings-InterceptMouseWheel="false" MaxLength="250"></telerik:RadNumericTextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                            ControlToValidate="txtDuracion" ForeColor="Red" CssClass="validacion" Font-Size="Smaller" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblPuesto" name="lblPuesto" runat="server">Puesto objetivo:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadListBox ID="rlbPuesto" ReadOnly="false" runat="server" Width="230px" MaxLength="100" ToolTip="Este campo deberá utilizarse únicamente cuando el curso está enfocado a desarrollar competencias en los participantes para que puedan desempeñar un puesto diferente al que hoy ocupan."></telerik:RadListBox>

                        <telerik:RadButton ID="radBtnBuscarPuesto" OnClientClicked="OpenSelectionWindow" AutoPostBack="false" runat="server" Text="B"></telerik:RadButton>

                        <telerik:RadButton ID="btnEliminarPuestoObjetivo" runat="server" Text="X" AutoPostBack="false" OnClientClicked="CleanAreasSelection"></telerik:RadButton>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div style="padding: 10px; text-align: justify; font-size: small;">
                    <p>
                        "Nota: Al elegir un puesto objetivo para Plantillas de Reemplazo únicamente se tomarán en cuenta las competencias específicas de éste y se eliminarán las existentes.
Adicionalmente no podrá agregar otras competencias específicas que no sean del puesto objetivo."
                    </p>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblNotas" name="lblNotas" runat="server">Notas:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadEditor Height="100" Width="500" ToolsWidth="500" EditModes="Design" ID="txtDsNotas" runat="server" ToolbarMode="ShowOnFocus" ToolsFile="~/Assets/AdvancedTools.xml"></telerik:RadEditor>
                    </div>
                </div>
            </telerik:RadPageView>

            <telerik:RadPageView ID="rpvVentanaCompetencias" runat="server">
                <telerik:RadGrid ID="grdCursoCompetencia" HeaderStyle-Font-Bold="true" runat="server" ShowHeader="true" AllowPaging="false"
                    AllowSorting="true" GroupPanelPosition="Top" Width="700px" GridLines="None"
                    Height="390px"
                    AllowFilteringByColumn="true"
                    ClientSettings-EnablePostBackOnRowClick="false" OnNeedDataSource="grdCursoCompetencia_NeedDataSource">
                    <GroupingSettings CaseSensitive="true" />
                    <ClientSettings AllowKeyboardNavigation="true">
                        <Selecting AllowRowSelect="true" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="false" />
                    <MasterTableView ClientDataKeyNames="ID_ITEM" DataKeyNames="ID_COMPETENCIA" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" PageSize="20"
                        HorizontalAlign="NotSet" EditMode="EditForms">
                        <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="false" ShowExportToCsvButton="false"
                            RefreshText="Actualizar" AddNewRecordText="Insertar" />
                        <Columns>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="id" DataField="ID_COMPETENCIA" UniqueName="ID_COMPETENCIA" HeaderStyle-Width="0" FilterControlWidth="80"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Categoría" DataField="CL_TIPO_COMPETENCIA" UniqueName="CL_TIPO_COMPETENCIA" HeaderStyle-Width="125" FilterControlWidth="60"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre" DataField="NB_COMPETENCIA" UniqueName="NB_COMPETENCIA" FilterControlWidth="325"></telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                <div style="clear: both; height: 10px;"></div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="radBtnGuardarCompetencia" OnClientClicked="OpenSelectionWindow" AutoPostBack="false" runat="server" Text="Agregar" Width="100"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="radBtnEliminaCompetencia" OnClick="radBtnEliminarCompetencia_Click" AutoPostBack="true" runat="server" Text="Eliminar" Width="100"></telerik:RadButton>
                </div>
            </telerik:RadPageView>

            <telerik:RadPageView ID="rpvVentanaInstructores" runat="server">
                <telerik:RadGrid ID="grdCursoInstructor" HeaderStyle-Font-Bold="true" runat="server" ShowHeader="true" AllowPaging="false"
                    AllowSorting="true" GroupPanelPosition="Top" Width="700px" GridLines="None"
                    Height="390px"
                    AllowFilteringByColumn="true"
                    ClientSettings-EnablePostBackOnRowClick="false" OnNeedDataSource="grdCursoInstructor_NeedDataSource">
                    <GroupingSettings CaseSensitive="false" />
                    <ClientSettings AllowKeyboardNavigation="true">
                        <Selecting AllowRowSelect="true" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="false" />
                    <MasterTableView ClientDataKeyNames="ID_ITEM" DataKeyNames="ID_INSTRUCTOR" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" PageSize="20"
                        HorizontalAlign="NotSet" EditMode="EditForms">
                        <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="false" ShowExportToCsvButton="false"
                            RefreshText="Actualizar" AddNewRecordText="Insertar" />
                        <Columns>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="id" DataField="ID_INSTRUCTOR" UniqueName="ID_INSTRUCTOR" HeaderStyle-Width="0" FilterControlWidth="80"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave" DataField="CL_INSTRUCTOR" UniqueName="CL_INSTRUCTOR" HeaderStyle-Width="125" FilterControlWidth="60"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre" DataField="NB_INSTRUCTOR" UniqueName="NB_INSTRUCTOR" FilterControlWidth="325"></telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                <div style="clear: both; height: 10px;"></div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="radBtnGuardarInstructor" OnClientClicked="OpenSelectionWindow" AutoPostBack="false" runat="server" Text="Agregar" Width="100"></telerik:RadButton>
                </div>
                <div>
                    <telerik:RadButton ID="radBtnEliminarInstructor" OnClick="radBtnEliminarInstructor_Click" AutoPostBack="true" runat="server" Text="Eliminar" Width="100"></telerik:RadButton>
                </div>
            </telerik:RadPageView>

            <telerik:RadPageView ID="rpvVentanaTemas" runat="server">
                <telerik:RadGrid ID="grdCursoTema" runat="server" HeaderStyle-Font-Bold="true" ShowHeader="true" AllowPaging="false"
                    AllowSorting="true" GroupPanelPosition="Top" Width="700px" GridLines="None"
                    Height="390px"
                    AllowFilteringByColumn="true"
                    ClientSettings-EnablePostBackOnRowClick="false" OnNeedDataSource="grdCursoTema_NeedDataSource">
                    <GroupingSettings CaseSensitive="false" />
                    <ClientSettings AllowKeyboardNavigation="true">
                        <Selecting AllowRowSelect="true" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="false" />
                    <MasterTableView ClientDataKeyNames="ID_ITEM, ID_TEMA" DataKeyNames="ID_TEMA" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" PageSize="20"
                        HorizontalAlign="NotSet" EditMode="EditForms">
                        <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="false" ShowExportToCsvButton="false"
                            RefreshText="Actualizar" AddNewRecordText="Insertar" />
                        <Columns>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="id" DataField="ID_TEMA" UniqueName="ID_TEMA" HeaderStyle-Width="0" FilterControlWidth="0"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave" DataField="CL_TEMA" UniqueName="CL_TEMA" HeaderStyle-Width="125" FilterControlWidth="60"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre" DataField="NB_TEMA" UniqueName="NB_TEMA" HeaderStyle-Width="400" FilterControlWidth="325"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Duración" DataField="NO_DURACION" UniqueName="NO_DURACION" HeaderStyle-Width="125" FilterControlWidth="60" DataFormatString="{0:N1}h"></telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                <div style="clear: both; height: 10px;"></div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="radBtnGuardarTema" OnClientClicked="OpenSelectionWindow" AutoPostBack="false" runat="server" Text="Agregar" Width="100"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="radBtnEditarTema" OnClientClicked="ShowEditForm" AutoPostBack="false" runat="server" Text="Editar" Width="100"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="radBtnEliminaTema" OnClick="BtnEliminarTema_Click" AutoPostBack="true" runat="server" Text="Eliminar" Width="100"></telerik:RadButton>
                </div>
            </telerik:RadPageView>

            <telerik:RadPageView ID="rpvDocumentacion" runat="server">
                <div class="ctrlBasico">
                    Tipo documento:<br />
                    <telerik:RadComboBox ID="cmbTipoDocumento" runat="server">
                        <Items>
                            <telerik:RadComboBoxItem Text="Imagen" Value="IMAGEN" />
                            <telerik:RadComboBoxItem Text="Otro" Value="OTRO" />
                        </Items>
                    </telerik:RadComboBox>
                </div>
                <div class="ctrlBasico">
                    Subir documento:<br />
                    <telerik:RadAsyncUpload ID="rauDocumento" runat="server" MultipleFileSelection="Disabled"></telerik:RadAsyncUpload>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnAgregarDocumento" runat="server" Text="Agregar" OnClick="btnAgregarDocumento_Click"></telerik:RadButton>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <telerik:RadGrid ID="grdDocumentos" runat="server" HeaderStyle-Font-Bold="true" OnNeedDataSource="grdDocumentos_NeedDataSource" Width="580">
                        <ClientSettings>
                            <Scrolling UseStaticHeaders="true" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <MasterTableView ClientDataKeyNames="ID_ARCHIVO,ID_ITEM" DataKeyNames="ID_ARCHIVO,ID_ITEM" AutoGenerateColumns="false" ShowHeadersWhenNoRecords="true" NoMasterRecordsText="No existen documentos asociados al curso">
                            <Columns>
                                <telerik:GridHyperLinkColumn HeaderText="Nombre del documento" DataTextField="NB_DOCUMENTO" DataNavigateUrlFields="ID_ARCHIVO,ID_DOCUMENTO,FE_CREATED_DATE,NB_DOCUMENTO,ID_ITEM" DataNavigateUrlFormatString="~/Comunes/ObtenerDocumento.ashx?ArchivoId={0}&ArchivoNb={2:yyyyMMdd}{4}&ArchivoDescargaNb={3}" Target="_blank"></telerik:GridHyperLinkColumn>
                                <telerik:GridBoundColumn HeaderText="Tipo de documento" HeaderStyle-Width="200" DataField="CL_TIPO_DOCUMENTO" UniqueName="CL_TIPO_DOCUMENTO"></telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnDelDocumentos" runat="server" Text="Eliminar" OnClick="btnDelDocumentos_Click"></telerik:RadButton>
                </div>

            </telerik:RadPageView>

            <telerik:RadPageView ID="pvwCamposExtras" runat="server">
            </telerik:RadPageView>
            <telerik:RadPageView ID="rpvAreasTematicas" runat="server">
                <div style="clear: both; height: 10px;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblAreaTematica" name="lblAreaTematica" runat="server">Área temática:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadComboBox ID="cmbAreaT" Skin="Bootstrap" name="cmbAreaT" CssClass="textbox"
                            runat="server"
                            Width="380px"
                            MarkFirstMatch="true"
                            EmptyMessage="Selecciona"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="cmbAreaT_SelectedIndexChanged"
                            EnableLoadOnDemand="true"
                            ValidationGroup="vArea">
                        </telerik:RadComboBox>
                    </div>
                </div>

                <div style="clear: both; height: 20px;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierdaAT">
                        <label id="lblCveAreaT" name="lblClAreaT" runat="server">Clave del área temática:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadLabel runat="server" ID="lblClAreaT"></telerik:RadLabel>
                    </div>
                </div>
                <div style="clear: both; height: 10px;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierdaAT">
                        <label id="lblAreaTematicaS" name="lblAreaTematicaS" runat="server">Área temática seleccionada:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadLabel runat="server" ID="lblAreaT"></telerik:RadLabel>
                        <label runat="server" id="lblAreaTemat"></label>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div style="float: right; padding-right: 10px; padding-bottom: 10px;">
                    <telerik:RadButton runat="server" ID="btnEliminarAreaTCurso" OnClick="btnEliminarAreaTCurso_Click" OnClientClicking="ConfirmarEliminar" Text="Eliminar área temática" Width="170" ToolTip="Eliminar el área temática del curso"></telerik:RadButton>
                </div>
                <div style="clear: both; height: 80px;"></div>
                <div style="padding: 10px; text-align: justify; font-size: small;">
                    <p>
                        "Nota: Es importante que seleccione un área temática del listado. Esto es para poder relacionar el curso con el área 
                        temática y facilitar la solicitud de constancias de competencias o habilidades a la Secretaría del Trabajo y Previsión Social."
                    </p>
                </div>
                <div style="clear: both;"></div>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </div>
    <div style="clear: both; height: 5px;"></div>
    <div class="divControlDerecha">
        <div class="ctrlBasico">
            <telerik:RadButton ID="radBtnGuardar" AutoPostBack="true" runat="server" Text="Guardar" Width="100" OnClick="radBtnGuardar_Click"></telerik:RadButton>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="radBtnCancelar" AutoPostBack="false" runat="server" Text="Cancelar" Width="100" OnClientClicked="closeWindow"></telerik:RadButton>
        </div>
    </div>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
