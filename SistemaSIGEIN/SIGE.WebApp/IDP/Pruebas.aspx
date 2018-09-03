<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/MenuIDP.master" AutoEventWireup="true" CodeBehind="Pruebas.aspx.cs" Inherits="SIGE.WebApp.IDP.Pruebas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script id="MyScript" type="text/javascript">

            var idPrueba = "";
            var vclTokenExterno = "";
            var clTokenExterno = "";

            function obtenerIdFila() {
                var grid = $find("<%=grdPruebas.ClientID %>");
                var MasterTable = grid.get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();

                if (selectedRows.length != 0) {
                    var row = selectedRows[0];
                    var SelectDataItem = MasterTable.get_dataItems()[row._itemIndex];
                    console.info(SelectDataItem);
                    clTipoPrueba = MasterTable.getCellByColumnUniqueName(row, "CL_PRUEBA");
                    idPrueba = SelectDataItem.getDataKeyValue("ID_PRUEBA");
                    vclTokenExterno = SelectDataItem.getDataKeyValue("CL_TOKEN_EXTERNO");
                }
            }

            function IniciarPrueba() {
                obtenerIdFila();
                if ((idPrueba != "")) {

                    if (clTipoPrueba.innerHTML == "LABORAL-1") {
                        var win = window.open("VentanaPersonalidadLaboralI.aspx?ID=" + idPrueba + "&&T=" + vclTokenExterno, '_blank');
                        win.add_close(prueba);
                        win.focus();
                    } else if (clTipoPrueba.innerHTML == "INTERES") {
                        var win = window.open("VentanaInteresesPersonales.aspx?ID=" + idPrueba + "&&T=" + vclTokenExterno, '_blank');
                        win.focus();
                    }
                    else if (clTipoPrueba.innerHTML == "LABORAL-2") {
                        var win = window.open("VentanaPersonalidadLaboralII.aspx?ID=" + idPrueba + "&&T=" + vclTokenExterno, '_blank');
                        win.focus();
                    }
                    else if (clTipoPrueba.innerHTML == "PENSAMIENTO") {
                        var win = window.open("VentanaEstiloDePensamiento.aspx?ID=" + idPrueba + "&&T=" + vclTokenExterno, '_blank');
                        win.focus();
                    }

                    else if (clTipoPrueba.innerHTML == "APTITUD-1") {
                        var win = window.open("VentanaAptitudMentalI.aspx?ID=" + idPrueba + "&&T=" + vclTokenExterno, '_blank');
                        win.focus();
                    }
                    else if (clTipoPrueba.innerHTML == "APTITUD-2") {
                        var win = window.open("VentanaAptitudMentalII.aspx?ID=" + idPrueba + "&&T=" + vclTokenExterno, '_blank');
                        win.focus();
                    }
                    else if (clTipoPrueba.innerHTML == "TIVA") {
                        var win = window.open("VentanaTIVA.aspx?ID=" + idPrueba + "&&T=" + vclTokenExterno, '_blank');
                        win.focus();
                    }

                    else if (clTipoPrueba.innerHTML == "ORTOGRAFIA-2") {
                        var win = window.open("VentanaOrtografiaII.aspx?ID=" + idPrueba + "&&T=" + vclTokenExterno, '_blank');
                        win.focus();
                    }

                    else if (clTipoPrueba.innerHTML == "ORTOGRAFIA-3") {
                        var win = window.open("VentanaOrtografiaIII.aspx?ID=" + idPrueba + "&&T=" + vclTokenExterno, '_blank');
                        win.focus();
                    }

                    else if (clTipoPrueba.innerHTML == "TECNICAPC") {
                        var win = window.open("VentanaTecnicaPC.aspx?ID=" + idPrueba + "&&T=" + vclTokenExterno, '_blank');
                        win.focus();
                    }

                    else if (clTipoPrueba.innerHTML == "REDACCION") {
                        var win = window.open("VentanaRedaccion.aspx?ID=" + idPrueba + "&&T=" + vclTokenExterno, '_blank');
                        win.focus();
                    }

                    else if (clTipoPrueba.innerHTML == "INGLES") {
                        var win = window.open("VentanaInglesI.aspx?ID=" + idPrueba + "&&T=" + vclTokenExterno, '_blank');
                        win.focus();
                    }
                  else {alert("No hay Prueba que aplicar");}
                }
                else {radalert("No has seleccionado una prueba.", 400, 150, "");}
            }
    

            function IniciarPruebaManual() {
                obtenerIdFila();
                if ((idPrueba != "")) {

                    if (clTipoPrueba.innerHTML == "LABORAL-1") {
                        var win = window.open("VentanaPersonalidadLabIManual.aspx?ID=" + idPrueba + "&&T=" + vclTokenExterno, '_blank');
                        win.focus();
                    } else if (clTipoPrueba.innerHTML == "INTERES") {
                        var win = window.open("VentanaInteresesPersonalesManual.aspx?ID=" + idPrueba + "&&T=" + vclTokenExterno, '_blank');
                        win.focus();
                    }
                    else if (clTipoPrueba.innerHTML == "LABORAL-2") {
                        var win = window.open("VentanaPersonalidadLaboralII.aspx?ID=" + idPrueba + "&&T=" + vclTokenExterno, '_blank');
                        win.focus();
                    }
                    else if (clTipoPrueba.innerHTML == "PENSAMIENTO") {
                        var win = window.open("VentanaEstiloDePensamiento.aspx?ID=" + idPrueba + "&&T=" + vclTokenExterno, '_blank');
                        win.focus();
                    }

                    else if (clTipoPrueba.innerHTML == "APTITUD-1") {
                        var win = window.open("VentanaAptitudMentalI.aspx?ID=" + idPrueba + "&&T=" + vclTokenExterno, '_blank');
                        win.focus();
                    }
                    else if (clTipoPrueba.innerHTML == "APTITUD-2") {
                        var win = window.open("VentanaAptitudMentalIIManual.aspx?ID=" + idPrueba + "&&T=" + vclTokenExterno, '_blank');
                        win.focus();
                    }
                    else if (clTipoPrueba.innerHTML == "TIVA") {
                        var win = window.open("VentanaTIVA.aspx?ID=" + idPrueba + "&&T=" + vclTokenExterno, '_blank');
                        win.focus();
                    }

                    else if (clTipoPrueba.innerHTML == "ORTOGRAFIA-2") {
                        var win = window.open("VentanaOrtografiaII.aspx?ID=" + idPrueba + "&&T=" + vclTokenExterno, '_blank');
                        win.focus();
                    }

                    else if (clTipoPrueba.innerHTML == "ORTOGRAFIA-3") {
                        var win = window.open("VentanaOrtografiaIII.aspx?ID=" + idPrueba + "&&T=" + vclTokenExterno, '_blank');
                        win.focus();
                    }

                    else if (clTipoPrueba.innerHTML == "TECNICAPC") {
                        var win = window.open("VentanaTecnicaPC.aspx?ID=" + idPrueba + "&&T=" + vclTokenExterno, '_blank');
                        win.focus();
                    }

                    else if (clTipoPrueba.innerHTML == "REDACCION") {
                        var win = window.open("VentanaRedaccion.aspx?ID=" + idPrueba + "&&T=" + vclTokenExterno, '_blank');
                        win.focus();
                    }

                    else if (clTipoPrueba.innerHTML == "INGLES") {
                        var win = window.open("VentanaInglesI.aspx?ID=" + idPrueba + "&&T=" + vclTokenExterno, '_blank');
                        win.focus();
                    }
                    else if (clTipoPrueba.innerHTML == "ADAPTACION") {
                        var win = window.open("VentanaAdaptacionMedioManual.aspx?ID=" + idPrueba + "&&T=" + vclTokenExterno, '_blank');
                        win.focus();
                    }

                    else { alert("No hay Prueba que aplicar"); }
                }
                else { radalert("No has seleccionado una prueba.", 400, 150, ""); }
            }

        </script>
    </telerik:RadCodeBlock>

    <label class="labelTitulo">Candidatos</label>
     <div class="ctrlBasico">
                <telerik:RadButton runat="server" Text="Agregar Prueba" ID="btnAddTest" OnClick="btnAddTest_Click" AutoPostBack="true" />
     </div>

     <div style="clear:both;"></div>
   <div style="height: calc(100% - 140px);">
        <telerik:RadSplitter ID="splSolicitudes" runat="server" Width="100%" Height="100%" BorderSize="0" Orientation="Vertical">
            <telerik:RadPane ID="rpnGridPruebas" runat="server">
                    <telerik:RadGrid 
                        ID="grdPruebas" 
                        runat="server" 
                        Height="100%" 
                        AutoGenerateColumns="false" 
                        EnableHeaderContextMenu="true" 
                        ShowGroupPanel="true" 
                        AllowSorting="true" 
                        OnNeedDataSource="grdPruebas_NeedDataSource">
                        <ClientSettings>
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView ClientDataKeyNames="ID_PRUEBA,CL_TOKEN_EXTERNO" EnableColumnsViewState="false" DataKeyNames="ID_PRUEBA,CL_TOKEN_EXTERNO" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true" PageSize="20">
                            
                             <Columns>
                                <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35"></telerik:GridClientSelectColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Tipo prueba" DataField="CL_PRUEBA" UniqueName="CL_PRUEBA"></telerik:GridBoundColumn> <%--1--%>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Nombre prueba" DataField="NB_PRUEBA" UniqueName="NB_PRUEBA"></telerik:GridBoundColumn> <%--1--%>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Nombre Candidato" DataField="NB_CANDIDATO_COMPLETO" UniqueName="NB_CANDIDATO_COMPLETO"></telerik:GridBoundColumn><%-- 2--%>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="No. requisición" DataField="NO_REQUISICION" UniqueName="NO_REQUISICION"></telerik:GridBoundColumn> <%--1--%>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="300" FilterControlWidth="230" HeaderText="Clave empleado" DataField="CL_EMPLEADO" UniqueName="CL_EMPLEADO"></telerik:GridBoundColumn><%-- 2--%>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="300" FilterControlWidth="230" HeaderText="Status" DataField="CL_ESTADO" UniqueName="CL_ESTADO"></telerik:GridBoundColumn> <%--1--%>
                               <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="300" FilterControlWidth="230" HeaderText="Tiempo de prueba" DataField="NO_TIEMPO_PRUEBA" UniqueName="NO_TIEMPO_PRUEBA" DataFormatString="{0:F2}"></telerik:GridBoundColumn> <%--1--%>    
                                  </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
        </telerik:RadPane>
              <telerik:RadPane ID="rpnOpciones" runat="server" Width="30">
                <telerik:RadSlidingZone ID="slzOpciones" runat="server" Width="30" ClickToOpen="true" SlideDirection="Left">
                    <telerik:RadSlidingPane ID="RSPHelp" runat="server" Title="Mail" Width="100%" Height="100%">
                        <div style="padding: 20px;">
                        </div>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>
    
        </telerik:RadSplitter>
           </div>
     <div style="clear:both; height:10px;"></div>
         <div class="ctrlBasico">
                    <telerik:RadButton runat="server" Text="Resultados" ID="RadButton1"  AutoPostBack="false" />
         </div>
          <div class="ctrlBasico">
                    <telerik:RadButton runat="server" Text="Captura Manual" ID="RadButton2"   OnClientClicked="IniciarPruebaManual"  AutoPostBack="false" />
         </div>
          <div class="ctrlBasico">
                    <telerik:RadButton runat="server" Text="Aplicación" ID="RadButton3" OnClientClicked="IniciarPrueba"  AutoPostBack="false" />
         </div>
          <div class="ctrlBasico">
                    <telerik:RadButton runat="server" Text="Enviar" ID="RadButton4"  AutoPostBack="false" />
         </div>
        
    <!-- Fin Secciones de niveles -->

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="RWpruebas"
                runat="server"
                Title="Pruebas"
                Height="500"
                Width="900"
                Left="5%"
                ReloadOnShow="true"
                ShowContentDuringLoad="false"
                VisibleStatusbar="false"
                VisibleTitlebar="true"
                Behaviors="Close"
                Modal="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true" Height="100%"></telerik:RadWindowManager>
</asp:Content>
