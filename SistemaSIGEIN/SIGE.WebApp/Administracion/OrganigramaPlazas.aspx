<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeBehind="OrganigramaPlazas.aspx.cs" Inherits="SIGE.WebApp.Administracion.OrganigramaPlazas" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../Assets/css/cssOrgChart.css" />
    <script type="text/javascript" src="../Assets/js/appOrgChart.js"></script>
    <script type="text/javascript">
        function OpenPlazasSelectionWindow() {
            OpenSelectionWindow("../Comunes/SeleccionPlaza.aspx?mulSel=0", "winSeleccion", "Selección de plaza")
        }

        function OpenDepartamentosSelectionWindow() {
            OpenSelectionWindow("../Comunes/SeleccionArea.aspx?mulSel=0", "winSeleccion", "Selección de área/departamento")
        }

        function OpenCamposSelectionWindow() {
            OpenSelectionWindow("../Comunes/SeleccionAdscripciones.aspx?MultiSeleccion=0", "winSeleccion", "Selección de campo adicional")
        }

        function OpenPuestoSelectionWindow() {
            OpenSelectionWindow("../Comunes/SeleccionPlazaPuesto.aspx?mulSel=0", "winSeleccion", "Selección de puesto")
        }

        function OpenOrganigrama() {
            var vUrl = "";
            var lbPlazas = $find("<%= lstPlaza.ClientID %>");
            var lbPuesto = $find("<%= lstPuesto.ClientID %>");
            var sel1 = null;
            var sel = null;
            
            if (lbPlazas != null) {
                sel = lbPlazas.get_selectedItem();
            }else if (lbPuesto != null) {
                 sel1 = lbPuesto.get_selectedItem();
            }
           

            if (sel != null) {
                vUrl = "VentanaOrganigramaPlazas.aspx";
                var vValue = sel.get_value();
                if (vValue != null && vValue != "") {
                    vUrl = vUrl + "?idPlaza=" + vValue;
                }
                else {
                    radalert("Selecciona la plaza a partir de la cual se generará el organigrama.", 400, 150, "Plaza");
                    return;
                }

                var combo = $find('<%=cmbEmpresas.ClientID %>');
                var vIdEmpresa = combo.get_selectedItem().get_value();
                if (vIdEmpresa != null && vIdEmpresa != "")
                    vUrl = vUrl + "&IdEmpresa=" + vIdEmpresa;

                var lbArea = $find("<%= rlbArea.ClientID %>");
                var selArea = lbArea.get_selectedItem();
                var lbCampos = $find("<%= RadListBox2.ClientID %>");
                var selCampos= lbCampos.get_selectedItem();
                if ((selArea != null && selArea.get_value() != "") && (selCampos != null && selCampos.get_value() != "")) {
                        radalert("Selecciona solo un filtro (área/departamento o campo adicional).", 400, 150, "Plaza");
                        return;
                    }               
                else {
                    if (selArea != null && selArea.get_value() != "") {
                        var vValueArea = selArea.get_value();
                        if (vValueArea != "")
                        vUrl = vUrl + "&IdDepartamento=" + vValueArea;
                    }
                    else if (selCampos != null && selCampos.get_value() != "") {
                        var vValueCampo = selCampos.get_value();
                        if (vValueCampo != "")
                        vUrl = vUrl + "&IdCampo=" + vValueCampo;
                    }
                }


                var vNiveles = $find("<%=txtNiveles.ClientID %>");
                var vValorNiveles = vNiveles.get_value();
                if(vValorNiveles != null && vValorNiveles != "")
                    vUrl = vUrl + "&NoNiveles=" + vValorNiveles;

            } else if (sel1 != null) {
                vUrl = "VentanaOrganigramaPuesto.aspx";
                var vValue = sel1.get_value();
                if (vValue != null && vValue != "") {
                    vUrl = vUrl + "?idPuesto=" + vValue;
                }
                else {
                    radalert("Selecciona el puesto a partir del cual se generará el organigrama.", 400, 150, "Puesto");
                    return;
                }

                var combo = $find('<%=cmbEmpresas.ClientID %>');
                var vIdEmpresa = combo.get_selectedItem().get_value();
                if (vIdEmpresa != null && vIdEmpresa != "")
                    vUrl = vUrl + "&IdEmpresa=" + vIdEmpresa;

                var lbArea = $find("<%= rlbArea.ClientID %>");
                var selArea = lbArea.get_selectedItem();
                var lbCampos = $find("<%= RadListBox2.ClientID %>");
                var selCampos = lbCampos.get_selectedItem();
                if ((selArea != null && selArea.get_value() != "") && (selCampos != null && selCampos.get_value() != "")) {
                    radalert("Selecciona solo un filtro (área/departamento o campo adicional).", 400, 150, "Plaza");
                    return;
                }
                else {
                    if (selArea != null && selArea.get_value() != "") {
                        var vValueArea = selArea.get_value();
                        if (vValueArea != "")
                            vUrl = vUrl + "&IdDepartamento=" + vValueArea;
                    }
                    else if (selCampos != null && selCampos.get_value() != "") {
                        var vValueCampo = selCampos.get_value();
                        if (vValueCampo != "")
                            vUrl = vUrl + "&IdCampo=" + vValueCampo;
                    }
                }


                var vNiveles = $find("<%=txtNiveles.ClientID %>");
                var vValorNiveles = vNiveles.get_value();
                if (vValorNiveles != null && vValorNiveles != "")
                    vUrl = vUrl + "&NoNiveles=" + vValorNiveles;


            }
            else {
                radalert("Selecciona el puesto o la plaza a partir del cual se generará el organigrama.", 400, 150, "Plaza/Puesto");
                return;
            }


            OpenSelectionWindow(vUrl, "winSeleccion", "Organigrama")
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

        function useDataFromChild(pDato) {
            var vLstDato = {
                idItem: "",
                nbItem: ""
            };

            if (pDato != null) {
                switch (pDato[0].clTipoCatalogo) {
                    case "PLAZA":
                        //console.info(pDato);
                        var vDatosSeleccionados = pDato[0];
                        vLstDato.idItem = vDatosSeleccionados.idPlaza;
                        vLstDato.nbItem = vDatosSeleccionados.nbPlaza;
                        ChangePlazaItem(vLstDato.idItem, vLstDato.nbItem);
                        break;
                    case "DEPARTAMENTO":
                        var vDatosSeleccionados = pDato[0];
                        vLstDato.idItem = vDatosSeleccionados.idArea;
                        vLstDato.nbItem = vDatosSeleccionados.nbArea;
                        ChangeDepartamentosItem(vLstDato.idItem, vLstDato.nbItem);
                        break;
                    case "ADSCRIPCION":
                        var vDatosSeleccionados = pDato[0];
                        vLstDato.idItem = vDatosSeleccionados.idCampo;
                        vLstDato.nbItem = vDatosSeleccionados.nbValor;
                        ChangeCamposItem(vLstDato.idItem, vLstDato.nbItem);
                        break;
                    case "PUESTO":
                        var vDatosSeleccionados = pDato[0];
                        vLstDato.idItem = vDatosSeleccionados.idPlaza;
                        vLstDato.nbItem = vDatosSeleccionados.nbPlaza;
                        ChangePuestoItem(vLstDato.idItem, vLstDato.nbItem);
                        break;
                }
            }
        }

        function SelectPlazaOrigen(sender, args) {
            var item = args.get_item();
            if (item)
                ChangePlazaItem(item.get_value(), item.get_text());
        }

        function CleanPlazasSelection(sender, args) {
            ChangePlazaItem("", "Seleccione");
        }

        function CleanDepartamentosSelection(sender, args) {
            ChangeDepartamentosItem("", "Seleccione");
        }

        function CleanCamposSelection(sender, args) {
            ChangeCamposItem("", "Seleccione");
        }

        function CleanPuestoSelection(sender, args) {
            ChangePuestoItem("", "Seleccione");
        }

        function ChangePlazaItem(pIdItem, pNbItem) {
            var vListBox = $find("<%=lstPlaza.ClientID %>");
            if (vListBox != null) {
                vListBox.trackChanges();

                var items = vListBox.get_items();
                items.clear();

                var item = new Telerik.Web.UI.RadListBoxItem();
                item.set_text(pNbItem);
                item.set_value(pIdItem);
                items.add(item);
                item.set_selected(true);
                vListBox.commitChanges();

                //var ajaxManager = $find("<= ramOrganigrama.ClientID %>");
                //ajaxManager.ajaxRequest("seleccionPlaza"); //Making ajax request with the argument   
            }
        }

        function ChangeCamposItem(pIdItem, pNbItem) {
            var vListBox = $find("<%=RadListBox2.ClientID %>");
            if (vListBox != null) {
                vListBox.trackChanges();

                var items = vListBox.get_items();
                items.clear();

                var item = new Telerik.Web.UI.RadListBoxItem();
                item.set_text(pNbItem);
                item.set_value(pIdItem);
                items.add(item);
                item.set_selected(true);
                vListBox.commitChanges();
            }

        }

        function ChangeDepartamentosItem(pIdItem, pNbItem) {
            var vListBox = $find("<%=rlbArea.ClientID %>");
            if (vListBox != null) {
                vListBox.trackChanges();

                var items = vListBox.get_items();
                items.clear();

                var item = new Telerik.Web.UI.RadListBoxItem();
                item.set_text(pNbItem);
                item.set_value(pIdItem);
                items.add(item);
                item.set_selected(true);
                vListBox.commitChanges();
            }
        }

        function ChangePuestoItem(pIdItem, pNbItem) {
            var vListBox = $find("<%=lstPuesto.ClientID %>");
            if (vListBox != null) {
                vListBox.trackChanges();

                var items = vListBox.get_items();
                items.clear();

                var item = new Telerik.Web.UI.RadListBoxItem();
                item.set_text(pNbItem);
                item.set_value(pIdItem);
                items.add(item);
                item.set_selected(true);
                vListBox.commitChanges();
            }
         }
    </script>

    <!-- Load Pako ZLIB library to enable PDF compression -->
    <script src="../Assets/js/pako.min.js"></script>

    <style type="text/css">
        .k-pdf-export.RadOrgChart .rocGroup:before,
        .k-pdf-export.RadOrgChart.rocSimple .rocNode:after,
        .k-pdf-export.RadOrgChart .rocGroup:after,
        .k-pdf-export.RadOrgChart.rocSimple .rocItem:after,
        .k-pdf-export.RadOrgChart.rocSimple .rocItemTemplate:after {
            width: 1px !important;
        }

        .kendo-pdf-hide-pseudo-elements:after,
        .kendo-pdf-hide-pseudo-elements:before {
            display: none !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxLoadingPanel ID="ralpOrganigrama" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramOrganigrama" runat="server">
        <AjaxSettings>
            <%--            <telerik:AjaxSetting AjaxControlID="ramOrganigrama">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rocPlazas" LoadingPanelID="ralpOrganigrama"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lstAscendencia" LoadingPanelID="ralpOrganigrama"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" LoadingPanelID="ralpOrganigrama" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
            <%--  <telerik:AjaxSetting AjaxControlID="chkMostrarEmpleados">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rocPlazas" LoadingPanelID="ralpOrganigrama"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lstAscendencia" LoadingPanelID="ralpOrganigrama"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
            <telerik:AjaxSetting AjaxControlID="cmbEmpresas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rocPlazas" LoadingPanelID="ralpOrganigrama"></telerik:AjaxUpdatedControl>
                    <%--   <telerik:AjaxUpdatedControl ControlID="lstAscendencia" LoadingPanelID="ralpOrganigrama"></telerik:AjaxUpdatedControl>--%>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbPlazaPuesto">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lstPlaza" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lstPuesto" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rocPlazas" UpdatePanelHeight="100%"  UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnBuscarPuesto" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnLimpiarPuesto" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnBuscarSeleccionPuesto" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnLimpiarSeleccionPuesto" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                     <telerik:AjaxUpdatedControl ControlID="cmbEmpresas" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" ></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblEmpresa" UpdatePanelHeight="100%"  UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                
                
                
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div style="height: calc(100% - 10px);">
        <telerik:RadSplitter ID="RadSplitter2" Width="100%" Height="100%" BorderSize="0" runat="server">
            <telerik:RadPane ID="RadPane2" runat="server">
                <div style="width: 700px;">
                    <div class="ctrlBasico">
                        <label name="lblIdPuesto">Generar organigrama a partir de la plaza o puesto:</label>
                          <div style="clear: both; height: 10px;"></div>
                        <div class="Divisiones2">
                            <asp:Label ID="lblPlazaPuesto" CssClass="Etiquetas" Text="Cargar Plaza o Puesto" runat="server" Width="150px" Font-Size="Small"></asp:Label>
                            <telerik:RadComboBox runat="server"  ID="cmbPlazaPuesto" EmptyMessage="Seleccione" OnSelectedIndexChanged="cmbPlazaPuesto_SelectedIndexChanged" AutoPostBack="true">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Por plaza" Value="plaza" />
                                    <telerik:RadComboBoxItem Text="Por puesto" Value="puesto" />
                                </Items>
                            </telerik:RadComboBox>
                        </div>
                          <div style="height:10px; clear:both;"></div>
                        <!-- Plazas-->
                        <div class="ctrlBasico">
                            <telerik:RadListBox ID="lstPlaza" Width="300" runat="server" OnClientItemDoubleClicking="OpenPlazasSelectionWindow" Visible="false">
                                <Items>
                                    <telerik:RadListBoxItem Text="Seleccione" Value="" />
                                </Items>
                            </telerik:RadListBox>
                            <telerik:RadButton ID="btnBuscarPuesto" runat="server" Text="B" OnClientClicked="OpenPlazasSelectionWindow" AutoPostBack="false" Visible="false"></telerik:RadButton>
                            <telerik:RadButton ID="btnLimpiarPuesto" runat="server" Text="X" OnClientClicked="CleanPlazasSelection" AutoPostBack="false" Visible="false"></telerik:RadButton>
                        </div>
                        <!-- Puestos-->
                        <div class="ctrlBasico">
                            <telerik:RadListBox ID="lstPuesto" Width="300" runat="server" OnClientItemDoubleClicking="OpenPuestoSelectionWindow" Visible="false">
                                <Items>
                                    <telerik:RadListBoxItem Text="Seleccione" Value="" />
                                </Items>
                            </telerik:RadListBox>
                            <telerik:RadButton ID="btnBuscarSeleccionPuesto" runat="server" Text="B" OnClientClicked="OpenPuestoSelectionWindow" AutoPostBack="false" Visible="false"></telerik:RadButton>
                            <telerik:RadButton ID="btnLimpiarSeleccionPuesto" runat="server" Text="X" OnClientClicked="CleanPuestoSelection" AutoPostBack="false" Visible="false"></telerik:RadButton>
                        </div>

                        <div class="ctrlBasico">
                            <asp:Label ID="lblEmpresa" CssClass="Etiquetas" runat="server" Text="Empresa:" Width="100px" Font-Size="Small" Visible="false"></asp:Label>
                            <telerik:RadComboBox ID="cmbEmpresas"  Width="200" runat="server" Visible="false"></telerik:RadComboBox>
                        </div>
                    </div>
                    <%--      <div class="ctrlBasico"></div>
                    <div class="ctrlBasico">
                        <telerik:RadCheckBox ID="chkMostrarEmpleados" runat="server" Text="Mostrar empleados" OnClick="chkMostrarEmpleados_Click"></telerik:RadCheckBox>
                    </div>--%>
                    <%--          <div class="ctrlBasico"> 
                    <telerik:RadButton ID="btnExportar"  runat="server" OnClientClicked="exportRadOrgChart" Text="Exportar a pdf" AutoPostBack="false" UseSubmitBehavior="false"></telerik:RadButton>
                    <telerik:RadClientExportManager runat="server" ID="RadClientExportManager1">
                    </telerik:RadClientExportManager>
                    </div>--%>
                    <div style="clear:both;"></div>
                    <label name="lblFiltros">Por favor indique por cuál de los siguientes criterios desea filtrar (seleccione solo uno):</label>
                    <div style="clear: both; height: 10px;"></div>
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda" style="width: 200px;">
                            1. Por área/departamento:
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadListBox ID="rlbArea" Width="200" runat="server" OnClientItemDoubleClicking="OpenDepartamentosSelectionWindow">
                                <Items>
                                    <telerik:RadListBoxItem Text="Seleccione" Value="" />
                                </Items>
                            </telerik:RadListBox>
                            <telerik:RadButton ID="RadButton1" runat="server" Text="B" OnClientClicked="OpenDepartamentosSelectionWindow" AutoPostBack="false"></telerik:RadButton>
                            <telerik:RadButton ID="RadButton2" runat="server" Text="X" OnClientClicked="CleanDepartamentosSelection" AutoPostBack="false"></telerik:RadButton>
                        </div>
                    </div>
                    <div style="clear: both;"></div>
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda" style="width: 200px;">
                            2. Por campo adicional:
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadListBox ID="RadListBox2" Width="200" runat="server" OnClientItemDoubleClicking="OpenCamposSelectionWindow">
                                <Items>
                                    <telerik:RadListBoxItem Text="Seleccione" Value="" />
                                </Items>
                            </telerik:RadListBox>
                            <telerik:RadButton ID="RadButton3" runat="server" Text="B" OnClientClicked="OpenCamposSelectionWindow" AutoPostBack="false"></telerik:RadButton>
                            <telerik:RadButton ID="RadButton4" runat="server" Text="X" OnClientClicked="CleanCamposSelection" AutoPostBack="false"></telerik:RadButton>
                        </div>
                    </div>
                    <div style="clear: both;"></div>
                    <label name="lblNiveles">Opcionalmente indique cuántos niveles desea mostrar a partir del puesto superior</label>
                    <div style="clear: both; height:10px;"></div>
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda" style="width: 200px;">
                            Número de niveles:
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadNumericTextBox ID="txtNiveles" runat="server" MaxLength="2" MaxValue="99" EmptyMessage="99" MinValue="1" NumberFormat-DecimalDigits="0" Width="50"></telerik:RadNumericTextBox>
                        </div>
                    </div>
                    <div style="clear: both; height: 10px;"></div>
                    <div class="ctrlBasico">
                        <telerik:RadButton ID="btnOrganigrama" runat="server" AutoPostBack="false" Text="Generar organigrama" OnClientClicked="OpenOrganigrama" ></telerik:RadButton>
                    </div>
                </div>
                <%--           <div style="height: calc(100% - 50px); overflow: auto;">
                    <div style="position: relative;">
                        <span style="position: absolute; display: none;">
                            <label name="lblNbAscendencia">Ascendencia:</label><br />
                            <label name="lblMensaje" runat="server" id="lblMensaje" style="">Selecciona un elemento de la lista para mostrar el organigrama.</label>
                            <telerik:RadListBox ID="lstAscendencia" runat="server" OnClientItemDoubleClicked="SelectPlazaOrigen"></telerik:RadListBox>
                        </span>
                        <telerik:RadOrgChart ID="rocPlazas" runat="server" DisableDefaultImage="true" Height="100%" OnNodeDataBound="rocPlazas_NodeDataBound">
                            <RenderedFields>
                                <ItemFields>--%>
                <%--<telerik:OrgChartRenderedField DataField="clItem" />--%>
                <%--                          <telerik:OrgChartRenderedField DataField="nbItem" />
                                </ItemFields>
                            </RenderedFields>
                        </telerik:RadOrgChart>
                        <telerik:RadContextMenu runat="server" ID="RadContextMenu1" OnClientItemClicked="onClientItemClicked">
                            <Items>
                                <telerik:RadMenuItem Text="Editar" Value="Plaza" />
                                <telerik:RadMenuItem Text="Editar" Value="Empleado" />
                            </Items>
                        </telerik:RadContextMenu>
                    </div>--%>
                <%--</div>--%>
            </telerik:RadPane>
            <telerik:RadPane ID="rpAyuda" runat="server" Scrolling="None" Width="22px" Height="50px">
                <telerik:RadSlidingZone ID="rszAyuda" runat="server" SlideDirection="Left" ClickToOpen="true" ExpandedPaneId="rspAyuda" Width="22px">
                    <telerik:RadSlidingPane ID="RadSlidingPane1" runat="server" Title="Ayuda" Width="270px" RenderMode="Mobile" Height="100%">
                        <div style="display: block; padding-left: 10px; padding-right: 10px; padding-top: 20px;">
                            <p align="justify">
                                Aquí podrás definir tu búsqueda para la generación del organigrama.
                                <br />
                                En caso de ingresar criterios de búsqueda, éstos serán utilizados para acotar el diagrama. 
                               <br />
                                <br />
                                <b>Nota:</b> Las opciones de Área/Departamento y de Campos extra son exclusivas, no pueden ser combinadas entre si.										
                            </p>
                        </div>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>

        </telerik:RadSplitter>
    </div>
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true" OnClientClose="returnDataToParentPopup" OnClientShow="centerPopUp">
        <Windows>
            <telerik:RadWindow ID="winEmpleado" runat="server" VisibleStatusbar="false" AutoSize="false" Modal="true" Behaviors="Close" NavigateUrl="~/Administracion/Empleado.aspx"></telerik:RadWindow>
            <telerik:RadWindow ID="winPlaza" runat="server" VisibleStatusbar="false" AutoSize="false" Modal="true" Behaviors="Close" NavigateUrl="~/Administracion/VentanaPlaza.aspx"></telerik:RadWindow>
            <telerik:RadWindow ID="winSeleccion" runat="server" Title="Seleccionar" Height="600px" Width="600px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">
        <script type="text/javascript">
            //<![CDATA[
            //Sys.Application.add_load(function () {
            //    window.orgChart = $find("<= rocPlazas.ClientID %>");
            //    window.winNodo = $find("<= winPlaza.ClientID%>");
            //    window.winItem = $find("<= winEmpleado.ClientID%>");
            //    window.contextMenu = $find("<= RadContextMenu1.ClientID %>");
            //    organigrama.initialize();
            //});
            //]]>
        </script>
        <script>

            //var $ = $telerik.$;

            //function exportRadOrgChart() {
            //    var v = $find('<=RadClientExportManager1.ClientID%>');
            //    v.exportPDF($(".RadOrgChart"));
            //}

        </script>
    </telerik:RadCodeBlock>
</asp:Content>
