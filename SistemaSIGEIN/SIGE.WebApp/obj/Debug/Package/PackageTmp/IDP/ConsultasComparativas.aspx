<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/MenuIDP.master" AutoEventWireup="true" CodeBehind="ConsultasComparativas.aspx.cs" Inherits="SIGE.WebApp.IDP.ConsultasComparativas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        var vIdPuesto = 0;
        var vIdCandidato = 0;
        var vIdPuestoGlobal = 0;
        var vIdCandidatoGlobal = 0;
        var datos = "";
        var vJsonPeriodo = "";

        function GetSelectionPuesto() {
            var wnd = GetWindowProperties();
            wnd.vTitulo = "Selección de puesto";
            wnd.vURL = "../Comunes/SeleccionPuesto.aspx";
            wnd.vRadWindowId = "winSeleccion";
            return wnd;
        }

        function OpenSelectionPuesto() {
         OpenWindow(GetSelectionPuesto());
        }

        function GetSelectionPuestoCandidatos() {
            var wnd = GetWindowProperties();
            wnd.vTitulo = "Selección de puesto";
            wnd.vURL = "../Comunes/SeleccionPuesto.aspx?CatalogoCl=PUESTO_CANDIDATOS&mulSel=1";
            wnd.vRadWindowId = "winSeleccion";
            return wnd;
        }

        function OpenSelectionPuestoCandidatos() {
          OpenWindow(GetSelectionPuestoCandidatos());
        }
         
        function GetSelectionPuestoGlobal() {
            var wnd = GetWindowProperties();
            wnd.vTitulo = "Selección de puesto";
            wnd.vURL = "../Comunes/SeleccionPuesto.aspx?CatalogoCl=PUESTO_GLOBAL";
            wnd.vRadWindowId = "winSeleccion";
            return wnd;
        }

        function OpenSelectionPuestoGlobal() {
            OpenWindow(GetSelectionPuestoGlobal())
        }

        function GetSelectionCandidato() {
            var wnd = GetWindowProperties();
            wnd.vTitulo = "Selección de candidato";
            wnd.vURL = "../Comunes/SeleccionCandidato.aspx?mulSel=1";
            wnd.vRadWindowId = "winSeleccion";
            return wnd;
        }

        function OpenSelectionCandidato() {
            OpenWindow(GetSelectionCandidato())
        }

        function GetSelectionCandidatoGlobal() {
            var wnd = GetWindowProperties();
            wnd.vTitulo = "Selección de candidato";
            wnd.vURL = "../Comunes/SeleccionCandidato.aspx?CatalogoCl=CANDIDATO_GLOBAL";
            wnd.vRadWindowId = "winSeleccion";
            return wnd;
        }

        function OpenSelectionCandidatoGlobal() {
            OpenWindow(GetSelectionCandidatoGlobal())
        }

        function GetSelectionCandidatos() {
            var wnd = GetWindowProperties();
            wnd.vTitulo = "Selección de candidato";
            wnd.vURL = "../Comunes/SeleccionCandidato.aspx?CatalogoCl=CANDIDATOS";
            wnd.vRadWindowId = "winSeleccion";
            return wnd;
        }

        function OpenSelectionCandidatos() {
            OpenWindow(GetSelectionCandidatos())
        }

        function GetVentanaPuestoVsCandidatos() {
            var wnd = GetWindowPropertiesConsultas();
            wnd.vTitulo = "Consulta Puesto vs. N Personas";
            wnd.vURL = "VentanaPuestoVsCandidatos.aspx?vIdPuestoVsCandidatos=" + '<%= vIdPuestoVsCandidatos%>' + "&IdPuesto=" + vIdPuesto;
            wnd.vRadWindowId = "rwConsulta";
            return wnd;
        }

        function OpenPuestoVsCandidatos() {
            if (vIdPuesto === 0) {
                radalert("Selecciona un puesto.", 400, 150);
                return;
            }
            if (datos === "") {
                radalert("Selecciona por lo menos un candidato.", 400, 150);
                return;
            }
            OpenWindow(GetVentanaPuestoVsCandidatos());
        }

        function GetVentanaCandidatoVsPuestos() {
            var wnd = GetWindowPropertiesConsultas();
            wnd.vTitulo = "Consulta Persona vs. N Puestos";
            wnd.vURL = "VentanaCandidatoVsPuestos.aspx?vIdCandidatoVsPuestos=" + '<%= vIdCandidatoVsPuestos%>' + "&IdCandidato=" + vIdCandidato;
            wnd.vRadWindowId = "rwConsulta";
            return wnd;
        }

        function OpenCandidatoVsPuestos() {
            if (vIdCandidato === 0) {
                radalert("Selecciona un candidato.", 400, 150);
                return;
            }

            if (vJsonPeriodo === "") {
                radalert("Selecciona por lo menos un puesto.", 400, 150);
                return;
            }
            OpenWindow(GetVentanaCandidatoVsPuestos());
         }

        function GetConfiguracionWindowProperties() {
            var wnd = GetWindowProperties();
            wnd.vTitulo = "Consulta global";
            wnd.vURL = "VentanaConsultaGlobal.aspx?IdPuesto=" + vIdPuestoGlobal + "&IdCandidato=" + vIdCandidatoGlobal;
            wnd.vRadWindowId = "winSeleccion";
            return wnd;
        }

        function GetWindowPropertiesConsultas() {
            return {
                width: document.documentElement.clientWidth - 100,
                height: document.documentElement.clientHeight - 10
            };
        }

        function GetWindowProperties() {
            return {
                width: document.documentElement.clientWidth - 350,
                height: document.documentElement.clientHeight - 10
            };
        }

        function OpenWindow(pWindowProperties) {
            openChildDialog(pWindowProperties.vURL, pWindowProperties.vRadWindowId, pWindowProperties.vTitulo, pWindowProperties);
        }


        function OpenConsultaGlobalWindow() {

            if (vIdPuestoGlobal === 0) {
                radalert("Selecciona un puesto.", 400, 150);
                return;
            }

            if (vIdCandidatoGlobal === 0) {
                radalert("Selecciona un candidato.", 400, 150);
                return;
            }
            OpenWindow(GetConfiguracionWindowProperties());
        }

        function DeletePuesto() {
            var vListBox = $find("<%=lstPuesto.ClientID %>");
             Delete(vListBox);
         }

         function DeletePuestoGlobal() {
             var vListBox = $find("<%=rlbPuestoGlobal.ClientID %>");
             Delete(vListBox);
         }

         function DeleteCandidatoGlobal() {
             var vListBox = $find("<%=rlbCandidatoGlobal.ClientID %>");
            Delete(vListBox);
         }

        function DeleteCandidato() {
            var vListBox = $find("<%=lstCandidato.ClientID %>");
             Delete(vListBox);
         }

        function Delete(vListBox) {
            var vSelectedItems = vListBox.get_selectedItems();
            vListBox.trackChanges();
            if (vSelectedItems)
                vSelectedItems.forEach(function (item) {
                    vListBox.get_items().remove(item);
                });
            vListBox.commitChanges();
        }

        function OnCloseWindow() {
            GetRadWindow().close();
        }

        function useDataFromChild(pDato) {
            if (pDato != null) {
                var arr = [];
                var vDatosSeleccionados = pDato[0];
                switch (pDato[0].clTipoCatalogo) {
                    case "PUESTO":
                        list = $find("<%=lstPuesto.ClientID %>");
                         InsertItemList(list, vDatosSeleccionados.nbPuesto, vDatosSeleccionados.idPuesto);
                         vIdPuesto = vDatosSeleccionados.idPuesto;
                         break;
                     case "CANDIDATO":
                         for (var i = 0; i < pDato.length; ++i) {
                             arr.push(pDato[i].idCandidato);
                         }
                         datos = JSON.stringify({ clTipo: vDatosSeleccionados.clTipoCatalogo, arrCandidatos: arr });
                         InsertDato(datos);

                         break;

                     case "PUESTO_GLOBAL":
                         list = $find("<%= rlbPuestoGlobal.ClientID %>");
                         vIdPuestoGlobal = vDatosSeleccionados.idPuesto;
                         InsertItemList(list, vDatosSeleccionados.nbPuesto, vDatosSeleccionados.idPuesto);
                         break;

                     case "CANDIDATO_GLOBAL":
                         list = $find("<%= rlbCandidatoGlobal.ClientID %>");
                         vIdCandidatoGlobal = vDatosSeleccionados.idCandidato;
                         InsertItemList(list, vDatosSeleccionados.nbCandidato, vDatosSeleccionados.idCandidato);

                    case "CANDIDATOS":
                         list = $find("<%= lstCandidato.ClientID %>");
                         InsertItemList(list, vDatosSeleccionados.nbCandidato, vDatosSeleccionados.idCandidato);
                         vIdCandidato = vDatosSeleccionados.idCandidato;
                         break;
                    case "PUESTO_CANDIDATOS":
                         vJsonPeriodo = JSON.stringify({ clTipo: vDatosSeleccionados.clTipoCatalogo, oPuestos: pDato });
                        InsertDato(vJsonPeriodo);
                        break;
                 }
             }
         }

         function InsertDato(pDato) {
             var ajaxManager = $find('<%= ramConsultas.ClientID%>');
             ajaxManager.ajaxRequest(pDato);
         }

         function InsertItemList(pList, pTexto, pValor) {
             if (list != undefined) {
                 list.trackChanges();
                 var items = list.get_items();
                 items.clear();
                 var item = new Telerik.Web.UI.RadListBoxItem();
                 item.set_text(pTexto);
                 item.set_value(pValor);

                 item.set_selected(true);
                 items.add(item);
                 list.commitChanges();
             }
         }


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramConsultas" runat="server" OnAjaxRequest="ramConsultas_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ramConsultas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgdCandidatos" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rgdPuestos" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadTabStrip ID="rtsConsultas" runat="server" MultiPageID="rmpConsultas">
        <Tabs>
            <telerik:RadTab Text="Consulta Puesto vs. N Personas" SelectedIndex="0"></telerik:RadTab>
            <telerik:RadTab Text="Consulta Persona vs. N Puestos" SelectedIndex="1"></telerik:RadTab>
            <telerik:RadTab Text="Consulta Global" SelectedIndex="2"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <div style="height: calc(100% - 90px);">
        <telerik:RadMultiPage ID="rmpConsultas" runat="server" SelectedIndex="0" Height="100%">

            <%-- inicio de consulta Puesto vs. N Personas --%>
            <telerik:RadPageView ID="rpvPuestoPersonas" runat="server">
                <div style="clear: both; height: 10px"></div>

                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblPuesto" name="lblPuesto" runat="server">Puesto:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadListBox ID="lstPuesto" Width="280px" Height="35px" runat="server" ValidationGroup="vgPuesto"></telerik:RadListBox>
                        <telerik:RadButton ID="btnSeleccionarPuesto" runat="server" Text="B" AutoPostBack="false" ValidationGroup="vgPuesto" OnClientClicked="OpenSelectionPuesto"></telerik:RadButton>
                        <telerik:RadButton ID="btnEliminarPuesto" runat="server" Text="X" AutoPostBack="false" ValidationGroup="vgPuesto" OnClientClicked="DeletePuesto"></telerik:RadButton>
                    </div>
                </div>

               <div style="clear: both; height: 10px"></div>
               <div style="height: calc(100% - 70px);">
                            <telerik:RadGrid ID="rgdCandidatos"
                                runat="server"
                                Height="100%"
                                Width="1100"
                                AllowSorting="true"
                                AllowFilteringByColumn="true"
                                ShowHeader="true"
                                HeaderStyle-Font-Bold="true"
                                OnNeedDataSource="rgdCandidatos_NeedDataSource"
                                OnItemDataBound="rgdCandidatos_ItemDataBound"
                                AllowMultiRowSelection ="true"
                                OnItemCommand="rgdCandidatos_ItemCommand">
                                <ClientSettings>
                                    <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>
                                <PagerStyle AlwaysVisible="true" />
                                <GroupingSettings CaseSensitive="false" />
                                <MasterTableView DataKeyNames="ID_CANDIDATO" ClientDataKeyNames="ID_CANDIDATO" AutoGenerateColumns="false" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true">
                                    <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="True" ShowExportToCsvButton="false" ShowRefreshButton="false"
                                        AddNewRecordText="Insertar" />
                                    <Columns>
                                        <%--<telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35"></telerik:GridClientSelectColumn>--%>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="100" FilterControlWidth="60%" HeaderText="Folio de solicitud" DataField="CL_SOLICITUD" UniqueName="CL_SOLICITUD"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="250" FilterControlWidth="70%" HeaderText="Nombre completo" DataField="NB_CANDIDATO" UniqueName="NB_CANDIDATO"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="190" FilterControlWidth="70%" HeaderText="Correo" DataField="CL_CORREO_ELECTRONICO" UniqueName="CL_CORREO_ELECTRONICO"></telerik:GridBoundColumn>
                                        <telerik:GridButtonColumn UniqueName="ELIMINAR" Text="Eliminar" CommandName="Delete" ButtonType="ImageButton">
                                    <ItemStyle Width="25" />
                                    <HeaderStyle Width="25" />
                                </telerik:GridButtonColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>
                <div style="clear: both; height: 10px"></div>
               <div class="ctrlBasico">
                   <telerik:RadButton ID="btnAgregarCandidatos" runat="server" Text="Agregar candidato" AutoPostBack="false" Width ="160px" OnClientClicked="OpenSelectionCandidato"></telerik:RadButton>
                   <%--<telerik:RadButton ID="btnEliminarCandidato" runat="server" Text="Eliminar" AutoPostBack="false" Width ="80px"></telerik:RadButton>--%>
               </div>
                <%--<div style="clear: both; height: 10px"></div>--%>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnReportePuestoCandidatos" runat="server" Text="Reporte" AutoPostBack="false" Width="80px" OnClientClicked="OpenPuestoVsCandidatos"></telerik:RadButton>
                </div>

            </telerik:RadPageView>
            <%-- fin de consulta Puesto vs. N Personas --%>


            <%-- inicio de consulta Persona vs. N Puestos --%>
            <telerik:RadPageView ID="rpvPersonaPuestos" runat="server">

                 <div style="clear: both; height: 10px"></div>

                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblCandidato" name="lblCandidato" runat="server">Candidato:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadListBox ID="lstCandidato" Width="280px" Height="35px" runat="server" ValidationGroup="vgCandidato"></telerik:RadListBox>
                        <telerik:RadButton ID="btnAgregarCandidato" runat="server" Text="B" AutoPostBack="false" ValidationGroup="vgCandidato" OnClientClicked="OpenSelectionCandidatos"></telerik:RadButton>
                        <telerik:RadButton ID="btnEliminarCandidato" runat="server" Text="X" AutoPostBack="false" ValidationGroup="vgCandidato" OnClientClicked="DeleteCandidato"></telerik:RadButton>
                    </div>
                </div>

               <div style="clear: both; height: 10px"></div>
               <div style="height: calc(100% - 70px);">
                            <telerik:RadGrid ID="rgdPuestos"
                                runat="server"
                                Height="100%"
                                Width="1100"
                                AllowSorting="true"
                                AllowFilteringByColumn="true"
                                ShowHeader="true"
                                HeaderStyle-Font-Bold="true"
                                OnNeedDataSource="rgdPuestos_NeedDataSource"
                                AllowMultiRowSelection ="true"
                                OnItemCommand="rgdPuestos_ItemCommand"
                                OnItemDataBound="rgdPuestos_ItemDataBound">
                                <ClientSettings>
                                    <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>
                                <PagerStyle AlwaysVisible="true" />
                                <GroupingSettings CaseSensitive="false" />
                                <MasterTableView DataKeyNames="ID_PUESTO" ClientDataKeyNames="ID_PUESTO" AutoGenerateColumns="false" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true">
                                    <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="True" ShowExportToCsvButton="false" ShowRefreshButton="false"
                                        AddNewRecordText="Insertar" />
                                    <Columns>
                                        <%--<telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35"></telerik:GridClientSelectColumn>--%>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="100" FilterControlWidth="70%" HeaderText="Clave" DataField="CL_PUESTO" UniqueName="CL_PUESTO"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="70%" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO"></telerik:GridBoundColumn>
                                        <telerik:GridButtonColumn UniqueName="ELIMINAR" Text="Eliminar" CommandName="Delete" ButtonType="ImageButton">
                                    <ItemStyle Width="25" />
                                    <HeaderStyle Width="25" />
                                </telerik:GridButtonColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>
                <div style="clear: both; height: 10px"></div>
               <div class="ctrlBasico">
                   <telerik:RadButton ID="btnAgregarPuestos" runat="server" Text="Agregar puesto" AutoPostBack="false" Width ="160px" OnClientClicked="OpenSelectionPuestoCandidatos"></telerik:RadButton>
               </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnReporteCandidatoPuestos" runat="server" Text="Reporte" AutoPostBack="false" Width="80px" OnClientClicked="OpenCandidatoVsPuestos"></telerik:RadButton>
                </div>
            </telerik:RadPageView>
            <%-- fin de consulta Persona vs. N Puestos --%>
            <%-- inicio de consulta Global --%>
            <telerik:RadPageView ID="rpvConsultaGlobal" runat="server">
                <div style="clear: both; height: 10px;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="Label1" name="lblPuestoGlobal" runat="server">Puesto:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadListBox ID="rlbPuestoGlobal" Width="280px" Height="35px" runat="server" ValidationGroup="vgPuesto"></telerik:RadListBox>
                        <telerik:RadButton ID="btnSeleccionPuestoglobal" runat="server" Text="B" AutoPostBack="false" ValidationGroup="vgPuesto" OnClientClicked="OpenSelectionPuestoGlobal"></telerik:RadButton>
                        <telerik:RadButton ID="btnQuitarPuestoGlobal" runat="server" Text="X" AutoPostBack="false" ValidationGroup="vgPuesto" OnClientClicked="DeletePuestoGlobal"></telerik:RadButton>
                    </div>
                </div>
                <div style="clear: both; height: 10px;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="Label2" name="lblCandidatoGlobal" runat="server">Candidato:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadListBox ID="rlbCandidatoGlobal" Width="280px" Height="35px" runat="server" ValidationGroup="vgPuesto"></telerik:RadListBox>
                        <telerik:RadButton ID="btnSeleccionCandidatoGlobal" runat="server" Text="B" AutoPostBack="false" ValidationGroup="vgGlobal" OnClientClicked="OpenSelectionCandidatoGlobal"></telerik:RadButton>
                        <telerik:RadButton ID="btnQuitarCandidatoGlobal" runat="server" Text="X" AutoPostBack="false" ValidationGroup="vgGlobal" OnClientClicked="DeleteCandidatoGlobal"></telerik:RadButton>
                    </div>
                </div>
                <div style="height: 10px; clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <div class="divControlDerecha">
                            <telerik:RadButton runat="server" ID="btnConsultaglobal" Text="Reporte" AutoPostBack="false" OnClientClicked="OpenConsultaGlobalWindow"></telerik:RadButton>
                        </div>
                    </div>
                </div>
            </telerik:RadPageView>
            <%-- fin de consulta Global --%>
        </telerik:RadMultiPage>
    </div>

    <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true" OnClientClose="returnDataToParentPopup">
        <Windows>
            <telerik:RadWindow
                ID="winSeleccion"
                runat="server"
                VisibleStatusbar="false"
                VisibleTitlebar="true"
                ShowContentDuringLoad="false"
                Modal="true"
                ReloadOnShow="true"
                Behaviors="Close">
            </telerik:RadWindow>

            <telerik:RadWindow
                ID="rwConsulta"
                runat="server"
                VisibleStatusbar="false"
                VisibleTitlebar="true"
                ShowContentDuringLoad="false"
                Modal="true"
                ReloadOnShow="true"
                Behaviors="Close">
            </telerik:RadWindow>

          
            <telerik:RadWindow 
                ID="winReportes" 
                runat="server" 
                VisibleStatusbar="false" 
                VisibleTitlebar="true" 
                ShowContentDuringLoad="false" 
                Modal="true" 
                ReloadOnShow="true" 
                Behaviors="Close"
                ></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

</asp:Content>
