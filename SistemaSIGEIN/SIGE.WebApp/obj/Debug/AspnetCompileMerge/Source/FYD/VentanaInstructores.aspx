<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="VentanaInstructores.aspx.cs" Inherits="SIGE.WebApp.FYD.VentanaInstructores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script id="MyScript" type="text/javascript">

        function ShowSelecEmpleado() {
            var vURL = "../Comunes/SeleccionEmpleado.aspx";
            var vTitulo = "Selección de empleado";
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 40,
                height: browserWnd.innerHeight - 40
            };
            openChildDialog(vURL, "winEmpleado", vTitulo, windowProperties);
        }

        function OpenSelectionWindow(sender, args) {
            //            var vLstPais = $find("= rpvVentanaInstructor.FindControl("rlbPais").ClientID %>");
            //           var vBtnPais = $find("= rpvVentanaInstructor.FindControl("radBtnBuscarpais").ClientID %>");
            var vLstEstados = $find("<%= rpvVentanaInstructor.FindControl("rlbEstado").ClientID %>");
            var vBtnEstados = $find("<%= rpvVentanaInstructor.FindControl("radBtnBuscarestado").ClientID %>");
            var vLstMunicipios = $find("<%= rpvVentanaInstructor.FindControl("rlbMunicipio").ClientID %>");
            var vBtnMunicipios = $find("<%= rpvVentanaInstructor.FindControl("radBtnBuscarmunicipio").ClientID %>");
            var vLstColonia = $find("<%= rpvVentanaInstructor.FindControl("rlbColonia").ClientID %>");
            var vBtnColonia = $find("<%= rpvVentanaInstructor.FindControl("radBtnBuscarcolonia").ClientID %>");

            var vBtnBuscaCP = $find("<%= rpvVentanaInstructor.FindControl("radBtnBuscaCP").ClientID %>");
            var vTxtCP = $find("<%= rpvVentanaInstructor.FindControl("txtCP").ClientID %>");

            var vBtnGuardarCompetencia = $find("<%= rpvVentanaCompetencias.FindControl("radBtnGuardarCompetencia").ClientID %>");
            var vBtnGuardarCurso = $find("<%= rpvVentanaCompetencias.FindControl("radBtnGuardarCurso").ClientID %>");

            //           if (sender == vBtnPais)
            //               openChildDialog("/Comunes/SeleccionLocalizacion/SeleccionPais.aspx", "winSeleccionLocalizacion", "Selección de País");


            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 40,
                height: browserWnd.innerHeight - 40
            };

            if (sender == vBtnEstados)
                openChildDialog("../Comunes/SeleccionLocalizacion/SeleccionEstado.aspx", "winSeleccionLocalizacion", "Selección de estado", windowProperties);

            if (sender == vBtnMunicipios) {
                var items = vLstEstados.get_items();
                var clEstado = items.getItem(0).get_value();
                openChildDialog("../Comunes/SeleccionLocalizacion/SeleccionMunicipio.aspx?ClEstado=" + clEstado, "winSeleccionLocalizacion", "Selección de municipio", windowProperties);
            }

            if (sender == vBtnColonia) {
                var items = vLstEstados.get_items();
                var clEstado = items.getItem(0).get_value();
                var items = vLstMunicipios.get_items();
                var clMunicipio = items.getItem(0).get_value();
                openChildDialog("../Comunes/SeleccionLocalizacion/SeleccionColonia.aspx?ClEstado=" + clEstado + "&ClMunicipio=" + clMunicipio, "winSeleccionLocalizacion", "Selección de colonia", windowProperties);
            }

            if (sender == vBtnGuardarCompetencia)
                openChildDialog("../Comunes/SeleccionCompetencia.aspx", "winSeleccionCompetencia", "Selección de competencia", windowProperties);

            if (sender == vBtnGuardarCurso) {
                var vIdInstructor = '<%# vIdInstructor %>';
                openChildDialog("../Comunes/SeleccionCurso.aspx?IdInstructor=" + vIdInstructor, "winSeleccionCurso", "Selección de curso", windowProperties);
            }

            if (sender == vBtnBuscaCP) {
                openChildDialog("../Comunes/SeleccionLocalizacion/SeleccionCP.aspx", "winSeleccionCP", "Selección de Codigo Postal", windowProperties);
            }
        }

        function useDataFromChild(pData) {
            if (pData != null) {
                var vSelectedData = pData[0];
                var list;
                switch (vSelectedData.clTipoCatalogo) {
                    //                 case "PAIS":
                    //                     list = $find("= rpvVentanaInstructor.FindControl("rlbPais").ClientID %>");
                    //                     SetListBoxItem(list, vSelectedData.nbDato, vSelectedData.clDato);
                    //                     break;
                    case "ESTADO":
                        listEstado = $find("<%= rpvVentanaInstructor.FindControl("rlbEstado").ClientID %>");
                        listMunicipio = $find("<%= rpvVentanaInstructor.FindControl("rlbMunicipio").ClientID %>");
                        listColonia = $find("<%= rpvVentanaInstructor.FindControl("rlbColonia").ClientID %>");
                        SetListBoxItem(listEstado, vSelectedData.nbDato, vSelectedData.clDato);
                        SetListBoxItem(listMunicipio, "No seleccionado", "");
                        SetListBoxItem(listColonia, "No seleccionado", "");
                        break;
                    case "MUNICIPIO":
                        listMunicipio = $find("<%= rpvVentanaInstructor.FindControl("rlbMunicipio").ClientID %>");
                        listColonia = $find("<%= rpvVentanaInstructor.FindControl("rlbColonia").ClientID %>");
                        SetListBoxItem(listMunicipio, vSelectedData.nbDato, vSelectedData.clDato);
                        SetListBoxItem(listColonia, "No seleccionado", "");
                        break;
                    case "COLONIA":
                        list = $find("<%= rpvVentanaInstructor.FindControl("rlbColonia").ClientID %>");
                        SetListBoxItem(list, vSelectedData.nbDato, vSelectedData.clDato);
                        break;
                    case "EMPLEADO":
                        __doPostBack('EMPLEADO', vSelectedData.idEmpleado);
                        break;
                    case "COMPETENCIA":
                        var arrCompetencia = [];
                        for (var i = 0; i < pData.length; i++)
                            arrCompetencia.push({
                                //ID_INSTRUCTOR_COMPETENCIA : 0,
                                ID_COMPETENCIA: pData[i].idCompetencia,
                                CL_COMPETENCIA: pData[i].clCompetencia,
                                NB_COMPETENCIA: pData[i].nbCompetencia
                            });
                        var datosCompetencia = JSON.stringify(arrCompetencia);
                        __doPostBack(vSelectedData.clTipoCatalogo, datosCompetencia);
                        break;
                    case "CURSO":
                        var arrCurso = [];
                        for (var j = 0; j < pData.length; j++)
                            arrCurso.push({
                                //ID_INSTRUCTOR_CURSO : 0,
                                ID_CURSO: pData[j].idCurso,
                                CL_CURSO: pData[j].clCurso,
                                NB_CURSO: pData[j].nbCurso
                            });
                        var datosCurso = JSON.stringify(arrCurso);
                        __doPostBack(vSelectedData.clTipoCatalogo, datosCurso);
                        break;
                        case "CODIGOPOSTAL":

                            var NB_ESTADO = pData[0].nbEstado;
                            var CL_ESTADO = pData[0].clEstado;
                            var NB_MUNICIPIO = pData[0].nbMunicipio;
                            var CL_MUNICIPIO = pData[0].clMunicipio;
                            var NB_COLONIA = pData[0].nbColonia;
                            var CL_COLONIA = pData[0].clColonia;
                            var NB_CP = pData[0].nbCP;
                        
                            listEstado = $find("<%= rpvVentanaInstructor.FindControl("rlbEstado").ClientID %>");
                            listMunicipio = $find("<%= rpvVentanaInstructor.FindControl("rlbMunicipio").ClientID %>");
                            listColonia = $find("<%= rpvVentanaInstructor.FindControl("rlbColonia").ClientID %>");
                        
                            SetListBoxItem(listEstado, NB_ESTADO, CL_ESTADO);
                            SetListBoxItem(listMunicipio, NB_MUNICIPIO, CL_MUNICIPIO);
                            SetListBoxItem(listColonia, NB_COLONIA, CL_COLONIA)
                            var vTxtCP = $find("<%= rpvVentanaInstructor.FindControl("txtCP").ClientID %>");
                            vTxtCP.set_value(NB_CP);
                            break
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

        function closeWindow() {
            GetRadWindow().close();
        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <%--    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <ClientEvents />
    <AjaxSettings>
    <telerik:AjaxSetting AjaxControlID="radBtnAgregarTelefono">
    <UpdatedControls>
    <telerik:AjaxUpdatedControl ControlID="grdTelefono" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel2"></telerik:AjaxUpdatedControl>
    <telerik:AjaxUpdatedControl ControlID="mtxtTelParticular" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel2"></telerik:AjaxUpdatedControl>
    </UpdatedControls>
    </telerik:AjaxSetting>
    <telerik:AjaxSetting AjaxControlID="radBtnEliminarTelefono">
    <UpdatedControls>
    <telerik:AjaxUpdatedControl ControlID="grdTelefono" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel2"></telerik:AjaxUpdatedControl>
    </UpdatedControls>
    </telerik:AjaxSetting>
    </AjaxSettings>
    </telerik:RadAjaxManager>--%>
    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="rmpVentanaInstructor" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab Text="Instructor"></telerik:RadTab>
            <telerik:RadTab Text="Competencias"></telerik:RadTab>
            <telerik:RadTab Text="Cursos"></telerik:RadTab>
            <telerik:RadTab Text="Documentación"></telerik:RadTab>
            <telerik:RadTab Text="Campos extra"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <div style="height: calc(100% - 100px); padding: 10px 10px 10px 10px; overflow: auto;">

        <telerik:RadMultiPage ID="rmpVentanaInstructor" runat="server" SelectedIndex="0" Height="100%">

            <telerik:RadPageView ID="rpvVentanaInstructor" runat="server">
                <div class="ctrlBasico">
                    <telerik:RadButton ID="rbInstInterno" runat="server" OnClick="rbInstInterno_Click" ToggleType="Radio" ButtonType="ToggleButton" Checked="true" GroupName="Radios" AutoPostBack="true">
                        <ToggleStates>
                            <telerik:RadButtonToggleState Text="Instructor interno"></telerik:RadButtonToggleState>
                            <telerik:RadButtonToggleState Text="Instructor interno"></telerik:RadButtonToggleState>
                        </ToggleStates>
                    </telerik:RadButton>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="rbInstExterno" runat="server" OnClick="rbInstExterno_Click" ToggleType="Radio" ButtonType="ToggleButton"
                        GroupName="Radios" AutoPostBack="true">
                        <ToggleStates>
                            <telerik:RadButtonToggleState Text="Instructor externo"></telerik:RadButtonToggleState>
                            <telerik:RadButtonToggleState Text="Instructor externo"></telerik:RadButtonToggleState>
                        </ToggleStates>
                    </telerik:RadButton>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblClave" name="lblClave" runat="server">Clave:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtClave" runat="server" Width="180px" MaxLength="20"></telerik:RadTextBox>
                        <telerik:RadButton ID="radBtnBuscarclave" OnClientClicked="ShowSelecEmpleado" AutoPostBack="false" runat="server" Text="B"></telerik:RadButton>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblNombre" name="lblNombre" runat="server">Nombre:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtNombre" runat="server" Width="450px" MaxLength="250"></telerik:RadTextBox>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblNombreValIns" name="lblNombreValIns" runat="server">Nombre de quien valida al instructor:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtNombreValIns" runat="server" Width="450px" MaxLength="250"></telerik:RadTextBox>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblRFC" name="lblRFC" runat="server">RFC:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtRFC" runat="server" Width="230px" MaxLength="20"></telerik:RadTextBox>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblCURP" name="lblCURP" runat="server">CURP:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtCURP" runat="server" Width="230px" MaxLength="20"></telerik:RadTextBox>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblRegistro" name="lblRegistro" runat="server">Registro STPS:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtRegistro" runat="server" Width="230px" MaxLength="30"></telerik:RadTextBox>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblCP" name="lblCP" runat="server">C.P.:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtCP" runat="server" Width="150px" MaxLength="10"></telerik:RadTextBox>
                        <telerik:RadButton ID="radBtnBuscaCP" OnClientClicked="OpenSelectionWindow" AutoPostBack="false" runat="server" Text="B"></telerik:RadButton>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblEstado" name="lblEstado" runat="server">Estado:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadListBox ID="rlbEstado" ReadOnly="true" runat="server" Width="230px" MaxLength="100"></telerik:RadListBox>
                        <telerik:RadButton ID="radBtnBuscarestado" OnClientClicked="OpenSelectionWindow" AutoPostBack="false" runat="server" Text="B"></telerik:RadButton>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblMunicipio" name="lblMunicipio" runat="server">Municipio:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadListBox ID="rlbMunicipio" ReadOnly="true" runat="server" Width="230px" MaxLength="100"></telerik:RadListBox>
                        <telerik:RadButton ID="radBtnBuscarmunicipio" OnClientClicked="OpenSelectionWindow" AutoPostBack="false" runat="server" Text="B"></telerik:RadButton>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblColonia" name="lblColonia" runat="server">Colonia:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadListBox ID="rlbcolonia" ReadOnly="true" runat="server" Width="230px" MaxLength="100"></telerik:RadListBox>
                        <telerik:RadButton ID="radBtnBuscarcolonia" OnClientClicked="OpenSelectionWindow" AutoPostBack="false" runat="server" Text="B"></telerik:RadButton>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblCalle" name="lblCalle" runat="server">Calle:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtCalle" runat="server" Width="450px" MaxLength="100"></telerik:RadTextBox>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblNoexterior" name="lblNoexterior" runat="server">No. exterior:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtNoexterior" runat="server" Width="150px" MaxLength="20"></telerik:RadTextBox>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblNointerior" name="lblNointerior" runat="server">No. interior:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtNointerior" runat="server" Width="150px" MaxLength="20"></telerik:RadTextBox>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblEscolaridad" name="lblEscolaridad" runat="server">Escolaridad:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtEscolaridad" runat="server" Width="450px" MaxLength="512"></telerik:RadTextBox>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblFechaNac" name="lblFechaNac" runat="server">Fecha de nacimiento:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadDatePicker ID="txtFeNacimiento" Width="150px" runat="server" MinDate="01/01/1900">
                            <Calendar ID="Calendar1" runat="server"></Calendar>
                        </telerik:RadDatePicker>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="ctrlBasico">
                        <label id="lblTelefono" style="width: 170px;" name="lblTelefono" runat="server">Teléfono particular:</label>
                    </div>
                    <div class="ctrlBasico">
                        <label id="lblTipoTel" name="lblTipoTel" runat="server">Tipo:</label>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="ctrlBasico">
                        <telerik:RadMaskedTextBox RenderMode="Lightweight" ID="mtxtTelParticular" runat="server" Mask="(###) ###-####"></telerik:RadMaskedTextBox>
                    </div>
                    <div class="ctrlBasico">
                        <telerik:RadComboBox ID="cmbIdTipoTelefono" runat="server" Width="100px"></telerik:RadComboBox>
                        <telerik:RadButton ID="radBtnAgregarTelefono" AutoPostBack="true" OnClick="radBtnAgregarTelefono_Click" runat="server" Text="Agregar" Width="100"></telerik:RadButton>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="ctrlBasico">
                        <telerik:RadGrid ID="grdTelefono" runat="server" HeaderStyle-Font-Bold="true" Width="250px" OnNeedDataSource="grdTelefono_NeedDataSource">
                            <ClientSettings AllowKeyboardNavigation="true">
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                              <GroupingSettings CaseSensitive="false" />
                            <MasterTableView ClientDataKeyNames="ID_ITEM" DataKeyNames="ID_ITEM" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" HorizontalAlign="NotSet">
                                <Columns>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" HeaderText="Teléfono" DataField="NB_TELEFONO" UniqueName="NB_TELEFONO"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" HeaderStyle-Width="0" HeaderText="Tipo" DataField="CL_TIPO" UniqueName="CL_TIPO" Visible="false"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" HeaderText="Tipo" DataField="NB_TIPO" UniqueName="NB_TIPO"></telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                    <div class="ctrlBasico">
                        <telerik:RadButton ID="radBtnEliminarTelefono" AutoPostBack="true" OnClick="radBtnEliminarTelefono_Click" runat="server" Text="Eliminar" Width="100"></telerik:RadButton>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblEmail" name="lblEmail" runat="server">Correo electrónico:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtEmail" runat="server" Width="450px" MaxLength="512"></telerik:RadTextBox>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblCostoHora" name="lblCostoHora" runat="server">Costo por hora:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadNumericTextBox ID="txtCostoHora" DataType="Decimal" runat="server" Width="150px" MaxLength="13"></telerik:RadNumericTextBox>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblCostoPart" name="lblCostoPart" runat="server">Costo por participante:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadNumericTextBox ID="txtCostoPart" DataType="Decimal" runat="server" Width="150px" MaxLength="13"></telerik:RadNumericTextBox>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblEvidencia" name="lblEvidencia" runat="server">Evidencia de competencias:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtEvidencia" runat="server" Width="450px" Height="130px" TextMode="MultiLine" MaxLength="1000"></telerik:RadTextBox>
                    </div>
                </div>
            </telerik:RadPageView>

            <telerik:RadPageView ID="rpvVentanaCompetencias" runat="server">
                <telerik:RadGrid ID="grdInstructorCompetencia" runat="server" HeaderStyle-Font-Bold="true" ShowHeader="true" AllowPaging="false"
                    AllowSorting="true" GroupPanelPosition="Top" Width="720px" GridLines="None"
                    Height="380px"
                    AllowFilteringByColumn="true"
                    ClientSettings-EnablePostBackOnRowClick="false"
                    OnNeedDataSource="grdInstructorCompetencia_NeedDataSource">
                    <GroupingSettings CaseSensitive="false" />
                    <ClientSettings AllowKeyboardNavigation="true">
                        <Selecting AllowRowSelect="true" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="false" />
                    <MasterTableView ClientDataKeyNames="ID_ITEM" DataKeyNames="ID_INSTRUCTOR_COMPETENCIA" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" PageSize="10"
                        HorizontalAlign="NotSet" EditMode="EditForms">
                        <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="false" ShowExportToCsvButton="false"
                            RefreshText="Actualizar" AddNewRecordText="Insertar" />
                        <Columns>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave" DataField="ID_COMPETENCIA" UniqueName="ID_COMPETENCIA" HeaderStyle-Width="0" FilterControlWidth="80"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave" DataField="CL_COMPETENCIA" UniqueName="CL_COMPETENCIA" HeaderStyle-Width="175" FilterControlWidth="100"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre" DataField="NB_COMPETENCIA" UniqueName="NB_COMPETENCIA" FilterControlWidth="225"></telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                <div style="clear: both; height: 10px;"></div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="radBtnGuardarCompetencia" OnClientClicked="OpenSelectionWindow" AutoPostBack="true" runat="server" Text="Agregar" Width="100"></telerik:RadButton>
                    <telerik:RadButton ID="radBtnEliminarCompetencia" OnClick="radBtnEliminarCompetencia_Click" AutoPostBack="true" runat="server" Text="Eliminar" Width="100"></telerik:RadButton>
                </div>
            </telerik:RadPageView>

            <telerik:RadPageView ID="rpvVentanaCursos" runat="server">
                <telerik:RadGrid ID="grdInstructorCurso" runat="server" HeaderStyle-Font-Bold="true" ShowHeader="true" AllowPaging="false"
                    AllowSorting="true" GroupPanelPosition="Top" Width="720px" GridLines="None"
                    Height="380px"
                    AllowFilteringByColumn="true"
                    ClientSettings-EnablePostBackOnRowClick="false"
                    OnNeedDataSource="grdInstructorCurso_NeedDataSource">
                    <GroupingSettings CaseSensitive="false" />
                    <ClientSettings AllowKeyboardNavigation="true">
                        <Selecting AllowRowSelect="true" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="false" />
                    <MasterTableView ClientDataKeyNames="ID_ITEM" DataKeyNames="ID_INSTRUCTOR_CURSO" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" PageSize="10"
                        HorizontalAlign="NotSet" EditMode="EditForms">
                        <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="false" ShowExportToCsvButton="false"
                            RefreshText="Actualizar" AddNewRecordText="Insertar" />
                        <Columns>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave" DataField="ID_CURSO" UniqueName="ID_CURSO" HeaderStyle-Width="0" FilterControlWidth="80"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave" DataField="CL_CURSO" UniqueName="CL_CURSO" HeaderStyle-Width="175" FilterControlWidth="100"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre" DataField="NB_CURSO" UniqueName="NB_CURSO" FilterControlWidth="225"></telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                <div style="clear: both; height: 10px;"></div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="radBtnGuardarCurso" AutoPostBack="true" OnClientClicked="OpenSelectionWindow" runat="server" Text="Agregar" Width="100"></telerik:RadButton>
                    <telerik:RadButton ID="radBtnEliminarCurso" AutoPostBack="true" OnClick="radBtnEliminarCurso_Click" runat="server" Text="Eliminar" Width="100"></telerik:RadButton>
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
                        <MasterTableView ClientDataKeyNames="ID_ARCHIVO,ID_ITEM" DataKeyNames="ID_ARCHIVO,ID_ITEM" AutoGenerateColumns="false" ShowHeadersWhenNoRecords="true" NoMasterRecordsText="No existen documentos asociados al instructor">
                            <Columns>
                                <telerik:GridHyperLinkColumn HeaderText="Nombre del documento" DataTextField="NB_DOCUMENTO" DataNavigateUrlFields="ID_ARCHIVO,ID_DOCUMENTO,FE_CREATED_DATE,NB_DOCUMENTO,ID_ITEM" DataNavigateUrlFormatString="/Comunes/ObtenerDocumento.ashx?ArchivoId={0}&ArchivoNb={2:yyyyMMdd}{4}&ArchivoDescargaNb={3}" Target="_blank"></telerik:GridHyperLinkColumn>
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
        </telerik:RadMultiPage>

    </div>
    <div style="clear: both; height: 10px;"></div>
    <div class="divControlDerecha">
        <div class="ctrlBasico">
            <telerik:RadButton ID="radBtnGuardar" AutoPostBack="true" runat="server" Text="Guardar" Width="100" OnClick="radBtnGuardar_Click"></telerik:RadButton>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="radBtnCancelar" AutoPostBack="false" runat="server" Text="Cancelar" Width="100" OnClientClicked="closeWindow"></telerik:RadButton>
        </div>
    </div>
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>

