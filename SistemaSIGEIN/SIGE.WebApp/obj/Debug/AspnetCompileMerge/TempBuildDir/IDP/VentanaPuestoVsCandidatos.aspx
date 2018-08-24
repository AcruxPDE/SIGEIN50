<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="VentanaPuestoVsCandidatos.aspx.cs" Inherits="SIGE.WebApp.IDP.VantanaPuestoVsCandidatos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
         <ClientEvents OnRequestStart="onRequestStart" OnResponseEnd="onResponseEnd" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnExcel">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rmpCandidatoPuestos" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rmpCandidatoPuestos" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>

         <telerik:RadCodeBlock ID="myCode" runat="server">

              <script type="text/javascript">

                  function getSvgContent(sender, args) {
                      var vLstDato = null;
                      vLstDato = {
                          svcImage: "Excel"
                      };

                      if (vLstDato != null) {
                          var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
                          ajaxManager.ajaxRequest(JSON.stringify(vLstDato) + "_" + "SVC");
                      }
                  }


                   function OpenImpresion() {
                       openChildDialog("GraficaPuestoVsCandidatos.aspx?vIdPuestoVsCandidatos=" + '<%= vIdPuestoVsCandidatos%>' + "&IdPuesto=" + '<%= vIdPuesto%>', "winImprimir", "Imprimir consulta");
                       //var myWindow = window.open("ReportePuestoVsCandidatos.aspx?vIdPuestoVsCandidatos=" + '<= vIdPuestoVsCandidatos%>' + "&IdPuesto=" + '<=vIdPuesto%>', "MsgWindow", "width=650,height=650");
                   }

                   function OpenImpresionReporte() {
                       openChildDialog("ReportePuestoVsCandidatos.aspx?vIdPuestoVsCandidatos=" + '<%= vIdPuestoVsCandidatos%>' + "&IdPuesto=" + '<%= vIdPuesto%>', "winImprimir", "Imprimir consulta");
                  }


                  function onRequestStart(sender, args) {
                      if ($find("<%=RadAjaxManager1.ClientID %>")._uniqueID == sender.__EVENTTARGET) {
                    args.set_enableAjax(false);
                }
            }


            function onResponseEnd(sender, args) {
                if ($find("<%=RadAjaxManager1.ClientID %>")._uniqueID == sender.__EVENTTARGET) {
                    args.set_enableAjax(true);
                }
            }

     </script>
     </telerik:RadCodeBlock>

   
    <telerik:RadTabStrip ID="rtsConsultas" runat="server" MultiPageID="rmpPuestoCandidatos" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab Text="Gráfica" SelectedIndex="0"></telerik:RadTab>
            <telerik:RadTab Text="Detalle de compatibilidad por competencias" SelectedIndex="1"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <div style="height: 10px;"></div>

    <div style="height: calc(100% - 80px);">
        <telerik:RadMultiPage ID="rmpPuestoCandidatos" runat="server" SelectedIndex="0" Height="100%">
                
            <telerik:RadPageView ID="rpvGrafica" runat="server" Height="100%">
                <div style="height: calc(100% - 55px);">
                <div class="ctrlBasico" style="width: 100%">
                    <telerik:RadHtmlChart runat="server" ID="rhcPuestoCandidatos" Width="100%" Height="650" Transitions="true" Skin="Silk">
                        <PlotArea>
                            <Series>
                                <telerik:ColumnSeries Stacked="false" Gap="1.5" Spacing="0.4">
                                    <Appearance>
                                        <FillStyle BackgroundColor="#d5a2bb"></FillStyle>
                                    </Appearance>
                                    <LabelsAppearance Position="OutsideEnd"></LabelsAppearance>
                                    <TooltipsAppearance Color="White"></TooltipsAppearance>
                                </telerik:ColumnSeries>
                            </Series>
                            <Appearance>
                                <FillStyle BackgroundColor="Transparent"></FillStyle>
                            </Appearance>
                        </PlotArea>
                        <Appearance>
                            <FillStyle BackgroundColor="Transparent"></FillStyle>
                        </Appearance>
                        <Legend>
                            <Appearance BackgroundColor="Transparent"  Position="Bottom" Width="100" >
                            </Appearance>
                        </Legend>
                    </telerik:RadHtmlChart>
                </div>

                <div style="clear: both; height: 10px"></div>
                <div class="ctrlBasico" style="width: 40%">
                    <telerik:RadGrid ID="rgdPromedios"
                        runat="server"
                        Height="200px"
                        AllowSorting="false"
                        HeaderStyle-Font-Bold="true"
                        AutoGenerateColumns="false"
                        BorderStyle="Solid"
                        BorderWidth="1px"
                         >
                        <ClientSettings>
                            <Scrolling UseStaticHeaders="false" AllowScroll="false" />
                            <Selecting AllowRowSelect="false" />
                        </ClientSettings>
                        <MasterTableView AllowFilteringByColumn="false" AllowPaging="false" ShowHeadersWhenNoRecords="true">
                            <Columns>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="400"  ItemStyle-BorderWidth="1px" ItemStyle-BorderStyle="Solid"  HeaderText="Persona" DataField="NB_PUESTO" UniqueName="NB_PUESTO"></telerik:GridBoundColumn>
                                <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" HeaderText="% Compatibilidad" DataField="PROMEDIO" UniqueName="PROMEDIO" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>--%>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" ItemStyle-BorderWidth="1px" ItemStyle-BorderStyle="Solid"  HeaderText="% Compatibilidad" DataField="PROMEDIO_TXT" UniqueName="PROMEDIO_TXT" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <div class="ctrlBasico" runat="server" id="divMensajeMayor130" visible ="false" style="width: 60%">
                    <label id="lblPuesto" name="lblPuesto" runat="server">(*) La persona tiene la capacidad para desempeñar el puesto, sin embargo supera en 30% o más los requerimientos de competencias del mismo. Esto es un factor que debe ser considerado para la toma de decisiones</label>
                </div>
                <div style="height:10px; clear:both;"></div>
                 <div class="divControlDerecha"> 
                     <telerik:RadButton ID="btnImprimir" runat="server" Text="Imprimir" OnClientClicked="OpenImpresion" AutoPostBack="false"></telerik:RadButton>
                     <telerik:RadButton ID="btnExcel" runat="server" Width="150" Text="Exportar a excel" OnClientClicked="getSvgContent" AutoPostBack="false"></telerik:RadButton>
                    </div>
                    </div>
            </telerik:RadPageView>
          <telerik:RadPageView ID="rpvDetalle" runat="server">
                <div style="height: calc(100% - 40px);">
                        <telerik:RadPivotGrid runat="server" ID="pgDetalleCompetencia" OnNeedDataSource="pgDetalleCompetencia_NeedDataSource" RowTableLayout="Tabular" OnCellDataBound="pgDetalleCompetencia_CellDataBound" 
                            ShowDataHeaderZone="false" ShowRowHeaderZone="false" ShowColumnHeaderZone="false" ShowFilterHeaderZone="false" AllowFiltering="false" AllowSorting="true" Height="100%" AllowNaturalSort="true" >
                            <ClientSettings>
                                <Scrolling AllowVerticalScroll="true" />
                                <Resizing AllowColumnResize="false" />
                            </ClientSettings>
                            
                            <ColumnHeaderCellStyle Width="50px" BorderStyle="Solid" BorderWidth="1px" />
                            <SortExpressions ></SortExpressions>
                            <TotalsSettings ColumnGrandTotalsPosition="None" GrandTotalsVisibility="None" ColumnsSubTotalsPosition="None" RowsSubTotalsPosition="None" />
                            <Fields >
                                 <telerik:PivotGridRowField  DataField="CL_COMPETENCIA"  CellStyle-Width="0" CellStyle-Font-Size="0" ></telerik:PivotGridRowField>
                                 <telerik:PivotGridRowField Caption="Color" DataField="CL_COLOR" CellStyle-BorderStyle="Solid" CellStyle-BorderWidth="1px"  CellStyle-Width="50" CellStyle-Height="100">
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
                                <telerik:PivotGridRowField Caption="Competencia" DataField="NB_COMPETENCIA" CellStyle-BorderStyle="Solid" CellStyle-BorderWidth="1px"   CellStyle-Width="200px"></telerik:PivotGridRowField>
                                <telerik:PivotGridRowField Caption="Descripción" DataField="DS_COMPETENCIA" CellStyle-BorderStyle="Solid" CellStyle-BorderWidth="1px"   CellStyle-Width="450px"></telerik:PivotGridRowField>
                                 <telerik:PivotGridColumnField DataField="NB_CANDIDATO" CellStyle-BorderStyle="Solid" CellStyle-BorderWidth="1px"   Caption="Factor" CellStyle-Height="30px">
                                    <CellTemplate>
                                <div style=" height: 60px; font-size: 8pt;text-align:center;"><%# GetDataItem() %></div>
                                    </CellTemplate>
                                </telerik:PivotGridColumnField>
                                <telerik:PivotGridAggregateField Aggregate="Average" DataField="PR_CANDIDATO" CellStyle-Width="80px" CellStyle-BorderStyle="Solid" CellStyle-BorderWidth="1px"  >
                                    <CellTemplate>
                                        <table style="border: 0px solid white !important; width: 100%">
                                            <tr>
                                                <td style="border-width: 0px; padding: 1px;">
                                                    <div runat="server" id="divPromedio" style="width: 95%; margin-bottom: auto; margin-top: auto; float: left; text-align: right;">
                                                        <%# String.Format("{0:N2}", Container.DataItem) %>%
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </CellTemplate>
                                </telerik:PivotGridAggregateField>
                            </Fields>
                        </telerik:RadPivotGrid>

                    </div>
              <div style="height:10px; clear:both;"></div>
              <div class="divControlDerecha"> 
              <telerik:RadButton ID="btnImpresion2" runat="server" Text="Imprimir" OnClientClicked="OpenImpresionReporte" AutoPostBack="false"></telerik:RadButton>
             </div>
         </telerik:RadPageView>
         </telerik:RadMultiPage>
         <iframe src="#" style="width: 0; height: 0; border: none" id="ifrmPrint"></iframe>
           </div>
    
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>

</asp:Content>
