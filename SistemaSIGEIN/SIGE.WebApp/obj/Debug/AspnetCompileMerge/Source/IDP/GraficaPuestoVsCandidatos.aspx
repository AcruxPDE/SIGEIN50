<%@ Page Title="" Language="C#" MasterPageFile="~/MPC/ContextMC.master" AutoEventWireup="true" CodeBehind="GraficaPuestoVsCandidatos.aspx.cs" Inherits="SIGE.WebApp.IDP.GraficaPuestoVsCandidatos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
     <div id="dvImprimir" runat="server" style="width: 790px; padding-top: 10px; padding-bottom: 10px; padding-left: 10px; padding-right: 10px;">
        <div style="height: 10px;"></div>
        <label style="display: block !important; width: 100% !important; padding: 0 !important; margin-bottom: 20px !important; font-size: 21px !important; line-height: inherit !important; border: 0 !important; border-bottom: 1px solid #C6DB95 !important;">
            Consulta puesto vs candidatos</label>
            <div style="clear: both; height: 10px"></div>
            <label>Comparativo de puesto:</label>
            <div style="clear: both; height: 10px"></div>
            <div id="lbPuestoCom" runat="server"></div>
            <div style="clear: both; height: 10px"></div>
            <label>Contra las siguientes personas:</label>
            <div style="clear: both; height: 10px"></div>
            <div id="lbCandidatos" runat="server"></div>
                 <div style="clear: both; height: 10px"></div>
            <telerik:RadHtmlChart runat="server" ID="rhcPuestoCandidatos" Height="750" Width="100%" Transitions="true" Skin="Silk">
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
        <br /><br />
        <div style="clear: both; height: 20px"></div>
         <div id="dvTablaCandidatosResul" runat="server"></div>
           <div runat="server" id="divMensajeMayor130" visible="false">
            <label id="lblPuesto" name="lblPuesto" runat="server">(*) La persona tiene la capacidad para desempeñar el puesto, sin embargo supera en 30% o más los requerimientos de competencias del mismo. Esto es un factor que debe ser considerado para la toma de decisiones</label>
        </div>
<%--        <div style="clear: both; height: 10px"></div>--%>
       <%-- <div style="width: 60%; ">
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
