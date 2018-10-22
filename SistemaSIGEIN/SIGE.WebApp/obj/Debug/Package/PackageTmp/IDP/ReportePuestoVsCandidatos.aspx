<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="ReportePuestoVsCandidatos.aspx.cs" Inherits="SIGE.WebApp.IDP.ReportePuestoVsCandidatos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
  <div id="dvImprimir" runat="server" style="width: 790px; padding-top: 10px; padding-bottom: 10px; padding-left: 10px; padding-right: 10px;">
        <div style="height: 10px;"></div>
        <label style="display: block !important; width: 100% !important; padding: 0 !important; margin-bottom: 20px !important; font-size: 21px !important; line-height: inherit !important; border: 0 !important; border-bottom: 1px solid #C6DB95 !important;">
            Consulta puesto vs candidatos</label>
          <%--  <div style="clear: both; height: 10px"></div>
            <label>Comparativo de puesto:</label>
            <div style="clear: both; height: 10px"></div>
            <div id="lbPuestoCom" runat="server"></div>
            <div style="clear: both; height: 10px"></div>
            <label>Contra las siguientes personas:</label>
            <div style="clear: both; height: 10px"></div>
            <div id="lbCandidatos" runat="server"></div>--%>
       <%-- <div style="clear: both; height: 10px"></div>
        <div style="height:1000px;">
            <telerik:RadHtmlChart runat="server" ID="rhcPuestoCandidatos" Height="650" Transitions="true" Skin="Silk">
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
                    <Appearance BackgroundColor="Transparent" Position="Bottom" Width="100">
                    </Appearance>
                </Legend>
            </telerik:RadHtmlChart>
        </div>
        <br /><br />--%>
      <%--  <div style="clear: both; height: 20px"></div>
        <div style="width: 60%; ">
            <telerik:RadGrid ID="rgdPromedios"
                runat="server"
                AllowSorting="false"
                HeaderStyle-Font-Bold="true"
                AutoGenerateColumns="false"
                BorderStyle="Solid"
                BorderWidth="1px">
                <ClientSettings>
                    <Scrolling UseStaticHeaders="false" AllowScroll="false" />
                    <Selecting AllowRowSelect="false" />
                </ClientSettings>
                <MasterTableView AllowFilteringByColumn="false" AllowPaging="false" ShowHeadersWhenNoRecords="true">
                    <Columns>
                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="400" ItemStyle-BorderWidth="1px" ItemStyle-BorderStyle="Solid" HeaderText="Persona" DataField="NB_PUESTO" UniqueName="NB_PUESTO"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" ItemStyle-BorderWidth="1px" ItemStyle-BorderStyle="Solid" HeaderText="% Compatibilidad" DataField="PROMEDIO_TXT" UniqueName="PROMEDIO_TXT" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </div>--%>
        <div style="clear: both; height: 20px"></div>
       <%-- <div runat="server" id="divMensajeMayor130" visible="false">
            <label id="lblPuesto" name="lblPuesto" runat="server">(*) La persona tiene la capacidad para desempeñar el puesto, sin embargo supera en 30% o más los requerimientos de competencias del mismo. Esto es un factor que debe ser considerado para la toma de decisiones</label>
        </div>
        <div style="clear: both; height: 10px"></div>--%>
        <%--  <telerik:RadPivotGrid runat="server" ID="pgDetalleCompetencia" OnNeedDataSource="pgDetalleCompetencia_NeedDataSource" RowTableLayout="Tabular" OnCellDataBound="pgDetalleCompetencia_CellDataBound" 
                            ShowDataHeaderZone="false" ShowRowHeaderZone="false" ShowColumnHeaderZone="false" ShowFilterHeaderZone="false" AllowFiltering="false" AllowSorting="true" Height="100%" AllowNaturalSort="true" >
                            <ClientSettings>
                                <Scrolling AllowVerticalScroll="true" />
                                <Resizing AllowColumnResize="false" />
                            </ClientSettings>
                            <ColumnHeaderCellStyle Width="50px" BorderStyle="Solid" BorderWidth="1px" />
                            <SortExpressions ></SortExpressions>
                            <TotalsSettings ColumnGrandTotalsPosition="None" GrandTotalsVisibility="None" ColumnsSubTotalsPosition="None" RowsSubTotalsPosition="None" />
                            <Fields >
                                 <telerik:PivotGridRowField  DataField="CL_COMPETENCIA"  CellStyle-Width="0%" CellStyle-Font-Size="0" >
                                 </telerik:PivotGridRowField>
                                 <telerik:PivotGridRowField Caption="Color" DataField="CL_COLOR" CellStyle-BorderStyle="Solid" CellStyle-BorderWidth="1px"  CellStyle-Width="10%">
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
                                <telerik:PivotGridRowField Caption="Competencia" UniqueName="NB_COMPETENCIA" DataField="NB_COMPETENCIA" CellStyle-BorderStyle="Solid" CellStyle-BorderWidth="1px"   CellStyle-Width="30%"></telerik:PivotGridRowField>
                                <telerik:PivotGridRowField Caption="Descripción" UniqueName="DS_COMPETENCIA" DataField="DS_COMPETENCIA" CellStyle-BorderStyle="Solid" CellStyle-BorderWidth="1px"   CellStyle-Width="50%"></telerik:PivotGridRowField>
                                 <telerik:PivotGridColumnField DataField="NB_CANDIDATO" CellStyle-BorderStyle="Solid" CellStyle-BorderWidth="1px" CellStyle-Width="15%"   Caption="Factor" >
                                    <CellTemplate>
                                <div style=" height: 60px; font-size: 8pt;text-align:center;"><%# GetDataItem() %></div>
                                    </CellTemplate>
                                </telerik:PivotGridColumnField>
                                <telerik:PivotGridAggregateField Aggregate="Average" DataField="PR_CANDIDATO" CellStyle-Width="15%" CellStyle-BorderStyle="Solid" CellStyle-BorderWidth="1px"  >
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
                        </telerik:RadPivotGrid>--%>
        <div id="divTabla" runat="server">
        </div>
    </div>
    <iframe src="#" style="width: 0; height: 0; border: none" id="ifrmPrint"></iframe>
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">
        <script type="text/javascript">
            setTimeout(function () {
                var myPrincipalHtml = document.getElementById('<%=dvImprimir.ClientID%>');
                var myIframe = document.getElementById('ifrmPrint');
                var pvContent = myPrincipalHtml.innerHTML;
                var myDoc = (myIframe.contentWindow || myIframe.contentDocument);
                if (myDoc.document) myDoc = myDoc.document;
                myDoc.write("<html>");
                myDoc.write("<body onload='this.focus(); this.print();'>");
                myDoc.write(pvContent + "</body></html>");
                myDoc.close(); (pvContent + "</body></html>");
                myDoc.close();
            }, 2000);
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
