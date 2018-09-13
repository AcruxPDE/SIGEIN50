<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="ConsultasPersonales.aspx.cs" Inherits="SIGE.WebApp.IDP.ConsultasPersonales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script type="text/javascript">

        function OpenResumida() {
            var vIdBateria = '<%= vIdBateria %>';
            var vFgParcial = '<%= vFgConsultaparcial %>';
             var vURL = "ReporteadorConsultas.aspx";
             var vTitulo = "Impresión consultas";

             vURL = vURL + "?IdBateria=" + vIdBateria + "&FgParcial="+ vFgParcial + "&ClConsulta=PERSONAL-RESUMIDA";
             var win = window.open(vURL, '_blank');
             win.focus();
        }

        function OpenDetallada() {
            var vIdBateria = '<%= vIdBateria %>';
            var vURL = "ReporteadorConsultas.aspx";
            var vTitulo = "Impresión consultas";

            vURL = vURL + "?IdBateria=" + vIdBateria + "&ClConsulta=PERSONAL-DETALLADA";
            var win = window.open(vURL, '_blank');
            win.focus();
        }

    </script>
    <style>
        .Expandido {
            text-align: center;
            font-size: 14pt;
            height: 10px;
        }

        .Contraido {
            text-align: center;
            font-size: 14pt;
            height: 10px;
            writing-mode: tb-rl;
        }

        .ContenedorBaremo {
            width: 35px;
            height: 20px;
        }

        .ColorBaremo1 {
            width: 33%;
            height: 100%;
            background-color: red;
            float: left;
            border: 1px solid black;
        }

        .BlankBaremo1 {
            width: 67%;
            height: 100%;
            background-color: white;
            float: left;
            border: 1px solid black;
        }

        .ColorBaremo2 {
            width: 67%;
            height: 100%;
            float: left;
            border: 1px solid black;
            background-color: yellow;
        }

        .BlankBaremo2 {
            width: 33%;
            height: 100%;
            float: left;
            border: 1px solid black;
            background-color: white;
        }

        .ColorBaremo3 {
            width: 100%;
            height: 100%;
            float: left;
            border: 1px solid black;
            background-color: green;
        }

        .BlankBaremo3 {
            width: 0%;
            height: 100%;
            float: left;
            background-color: white;
        }

        .ColorBaremo4 {
            width: 100%;
            height: 100%;
            float: left;
            border: 1px solid black;
            background-color: gray;
        }

        .BlankBaremo3 {
            width: 0%;
            height: 100%;
            float: left;
            background-color: white;
        }

        .Justificado {
            text-align: justify;
        }

        .Centrado {
            text-align: center;
        }

        .rgCollapse {
            display: none !important;
        }

        .rgExpand {
            display: none !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="dgvResumen">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="dgvResumen" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="pgDetallada">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pgDetallada" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <%-- <div style="clear: both; height: 10px;"></div>--%>
    <%--   <div class="ctrlBasico">--%>
    <div class="ctrlBasico">
        <table class="ctrlTableForm">
            <tr>
                <td class="ctrlTableDataContext">
                    <label>Folio de solicitud:</label></td>
                <td class="ctrlTableDataBorderContext">
                    <div id="txtFolio" runat="server" width="170" maxlength="1000" enabled="false"></div>
                </td>
                <td class="ctrlTableDataContext">
                    <label>Candidato:</label></td>
                <td colspan="2" class="ctrlTableDataBorderContext">
                    <div id="txtCandidato" runat="server" style="min-width: 100px;"></div>
                </td>
                
            </tr>
        </table>
    </div>
    <%--    </div>--%>
<%--    <telerik:RadTabStrip runat="server" ID="tbConsultas" SelectedIndex="0" MultiPageID="mpConsultas" Visible="false">
        <Tabs>
            <telerik:RadTab Text="Resumida"></telerik:RadTab>
            <telerik:RadTab Text="Detallada"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>--%>
      <div style="clear:both; height:10px;"></div>
    <div style="height: calc(100% - 70px);">
        <telerik:RadMultiPage runat="server" ID="mpConsultas" SelectedIndex="0" Width="100%" Height="100%">
            <telerik:RadPageView ID="pvResumen" runat="server" TabIndex="0" Width="100%" Height="100%">
                <div style="height: calc(100% - 35px); width: 100%;">

                    <telerik:RadGrid
                        ID="dgvResumen" runat="server" Height="99%" Width="99%" AutoGenerateColumns="false"
                        EnableHeaderContextMenu="true" ShowGroupPanel="false" AllowSorting="true" HeaderStyle-Font-Bold="true"
                        OnNeedDataSource="dgvResumen_NeedDataSource" OnItemDataBound="dgvResumen_ItemDataBound" OnItemCreated="dgvResumen_ItemCreated" OnItemCommand="dgvResumen_ItemCommand">
                        <ClientSettings EnableAlternatingItems="false">
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <%--<ExportSettings HideStructureColumns="false" ExportOnlyData="true" IgnorePaging="true" FileName="Presupuesto" Excel-FileExtension="xlsx"></ExportSettings>--%>
                        <MasterTableView EnableColumnsViewState="false" AllowPaging="false" ShowFooter="true" AllowFilteringByColumn="false" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true" DataKeyNames="CL_COLOR,ID_COMPETENCIA" EnableHeaderContextMenu="true">
                            <%--<CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false" ShowExportToExcelButton="true" ExportToExcelText="Exportar a Excel" />--%>

                            <Columns>
                                <telerik:GridTemplateColumn DataField="CL_COLOR" UniqueName="CL_COLOR" ItemStyle-HorizontalAlign="Center" HeaderText="">
                                    <ItemStyle Width="10px" Height="15px" HorizontalAlign="Center" />
                                    <HeaderStyle Width="25px" Height="20px" />
                                    <ItemTemplate>
                                        <div class="ctrlBasico" style="height: 60px; width: 30px; float: left; background: <%# Eval("CL_COLOR") %>; border-radius: 5px;"></div>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="false" Display="true" HeaderStyle-Width="50" FilterControlWidth="60" HeaderText="Clasificación" DataField="CL_CLASIFICACION" UniqueName="CL_CLASIFICACION" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>--%>
                                <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="false" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="ID_COMPETENCIA" DataField="ID_COMPETENCIA" UniqueName="ID_COMPETENCIA"></telerik:GridBoundColumn>--%>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="130" FilterControlWidth="80" HeaderText="Competencia" DataField="NB_COMPETENCIA" UniqueName="NB_COMPETENCIA" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="120" HeaderText="Descripción competencia" DataField="DS_COMPETENCIA" UniqueName="DS_COMPETENCIA" ItemStyle-CssClass="Justificado" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Color" DataField="CL_COLOR" UniqueName="CL_COLOR2"></telerik:GridBoundColumn>
                                <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="false" Display="true" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Promedio" DataField="NO_BAREMO_PROMEDIO" UniqueName="NO_BAREMO_PROMEDIO" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>--%>
                                <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="false" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Factor" DataField="NO_BAREMO_FACTOR" UniqueName="NO_BAREMO_FACTOR" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>--%>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Descripción del nivel" DataField="DS_NIVEL_COMPETENCIA_PERSONA" UniqueName="DS_NIVEL_COMPETENCIA_PERSONA" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="90" FilterControlWidth="80" HeaderText="Porcentaje" DataField="NO_BAREMO_PORCENTAJE" UniqueName="NO_BAREMO_PORCENTAJE" Aggregate="Avg" DataFormatString="{0:N2}%" FooterAggregateFormatString="Compatibilidad: {0:N2}%" ItemStyle-CssClass="Derecha" HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>

                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>

                <div style="clear:both; height:5px;"></div>
                <div class="divControlDerecha">

                        <telerik:RadButton runat="server" ID="btnImprimir" Text="Imprimir" Width="100px" AutoPostBack="false" OnClientClicked="OpenResumida"></telerik:RadButton>
                        <telerik:RadButton runat="server" ID="btnExportResumen" Text="Exportar a Excel" Width="150px" UseSubmitBehavior="false" OnClick="btnExportResumen_Click"></telerik:RadButton>
         </div>

            </telerik:RadPageView>

            <telerik:RadPageView ID="pvDetallada" runat="server" TabIndex="1">

                <div style="height: calc(100% - 35px); width: 100%;">

                    <telerik:RadGrid ID="grdDetallada" runat="server" Height="100%" AutoGenerateColumns="true" OnItemCreated="grdDetallada_ItemCreated"
                        OnNeedDataSource="grdDetallada_NeedDataSource" AllowFilteringByColumn="false" HeaderStyle-Font-Bold="true" OnColumnCreated="grdDetallada_ColumnCreated" AllowMultiRowSelection="true" AllowPaging="false">
                        <ClientSettings EnablePostBackOnRowClick="false">
                            <%--<Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true" FrozenColumnsCount="2" CountGroupSplitterColumnAsFrozen="false"></Scrolling>--%>
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <MasterTableView ClientDataKeyNames="ID_COMPETENCIA" DataKeyNames="ID_COMPETENCIA" EnableColumnsViewState="false" AllowFilteringByColumn="false" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                            <Columns></Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>

                <div style="clear:both; height:5px;"></div>
                <div class="divControlDerecha">
                        <telerik:RadButton runat="server" ID="btnImprimirDetallada" Text="Imprimir" Width="100px" AutoPostBack="false" OnClientClicked="OpenDetallada"></telerik:RadButton>
                        <telerik:RadButton runat="server" ID="btnExportarDetalle" Text="Exportar a Excel" Width="150" UseSubmitBehavior="false" OnClick="btnExportarDetalle_Click"></telerik:RadButton>
                </div>

               



                <%--<telerik:RadPivotGrid ID="pgDetallada" runat="server" 
                    OnNeedDataSource="pgDetallada_NeedDataSource" Width="99%" Height="99%" RowTableLayout="Tabular" ShowFilterHeaderZone="false" EmptyValue="4" 
                    OnCellDataBound="pgDetallada_CellDataBound"  ShowRowHeaderZone="false" ShowColumnHeaderZone="false" AllowFiltering="false"    ShowDataHeaderZone="false">

                    <TotalsSettings ColumnGrandTotalsPosition="None" RowsSubTotalsPosition="None" GrandTotalsVisibility="None" ColumnsSubTotalsPosition="None"  />
                    <ClientSettings  >
                        
                        <Scrolling AllowVerticalScroll="true" />                        
                    </ClientSettings>

                    <ColumnHeaderCellStyle Width="50px"/>
                    <DataCellStyle Height="50px" />
                    
                    <Fields>

                        <telerik:PivotGridColumnField DataField="NB_PRUEBA" Caption="Prueba" ></telerik:PivotGridColumnField>
                       
                        <telerik:PivotGridColumnField DataField="NB_FACTOR" Caption="Factor">
                            <CellTemplate>
                                <div style="writing-mode: tb-rl; height: 150px; font-size: 8pt"><%# GetDataItem() %></div>
                            </CellTemplate>
                        </telerik:PivotGridColumnField>
                        <telerik:PivotGridColumnField DataField="NB_ABREVIATURA" IsHidden="true"></telerik:PivotGridColumnField>
                        
                        
                      
                        <telerik:PivotGridRowField Caption="Color" DataField="CL_COLOR" CellStyle-Width="50">
                                    <CellTemplate>
                                        <table style="height: 100%;">
                                            <tr>
                                                <td style="border-width: 0px; padding: 0px;">
                                                    <div style="height: 100%; border-radius: 5px; background: <%# Container.DataItem.ToString() %>;">&nbsp;</div>
                                                </td>
                                            </tr>
                                        </table>
                                    </CellTemplate>
                                </telerik:PivotGridRowField>
                          <telerik:PivotGridRowField DataField="NB_CLASIFICACION_COMPETENCIA" Caption="Clasificacion" CellStyle-Width="120"></telerik:PivotGridRowField>
                        <telerik:PivotGridRowField DataField="NB_COMPETENCIA" Caption="Competencia" CellStyle-Width="130"></telerik:PivotGridRowField>

                          
                        <telerik:PivotGridRowField DataField="CL_COLOR"  Caption="Color" CellStyle-ForeColor="#F5F5F5" CellStyle-Height="1px" CellStyle-Font-Size="1px" ></telerik:PivotGridRowField>

                        <telerik:PivotGridAggregateField DataField="NO_VALOR"  Caption="Valor">
                            <CellTemplate>
                             
                                <div class="ContenedorBaremo">
                                   <span><img src='/Assets/images/Baremos<%# Eval("ICONO") %>.png' /></span>
                                   <div class="ColorBaremo<%#  Math.Round(decimal.Parse((GetDataItem() ?? 5).ToString())) %>"></div>
                                    <div class="BlankBaremo<%#  Math.Round(decimal.Parse((GetDataItem() ?? 5).ToString())) %>"></div>
                                </div>
                            </CellTemplate>
                        </telerik:PivotGridAggregateField>
                    </Fields>
                </telerik:RadPivotGrid>--%>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </div>
     <telerik:RadWindowManager ID="rwmAlertas" runat="server"></telerik:RadWindowManager>
</asp:Content>
