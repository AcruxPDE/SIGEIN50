﻿<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="ReporteConsultaGlobal.aspx.cs" Inherits="SIGE.WebApp.IDP.ReporteConsultaGlobal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <style>
        .Nombre {
            padding-left: 10px;
        }

        .AltoDatos {
            height: 50px;
        }

        .CentrarTexto {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
   <div id="dvImprimir" runat="server" style="width: 790px; padding-top: 10px; padding-bottom: 10px; padding-left: 10px; padding-right: 10px;">
        <div style="height: 10px;"></div>
        <label style="display: block !important; width: 100% !important; padding: 0 !important; margin-bottom: 20px !important; font-size: 21px !important; line-height: inherit !important; border: 0 !important; border-bottom: 1px solid #C6DB95 !important;">
            Consulta global</label>
            <div style="clear: both; height: 10px"></div>
        <table style="border-collapse:collapse; border: solid 1px;">
            <tr style="border-collapse:collapse; border: solid 1px;">
                <td style="border-collapse:collapse; border: solid 1px; padding:10px;">
                    <label>Folio de solicitud: </label>
                </td>
                <td style="border-collapse:collapse; border: solid 1px; padding:10px;">
                    <span runat="server" id="txtClSolicitud"></span>
                </td>
                  </tr>
            <tr style="border-collapse:collapse; border: solid 1px;">
                <td style="border-collapse:collapse; border: solid 1px; padding:10px;">
                    <label>Candidato: </label>
                </td>
                <td style="border-collapse:collapse; border: solid 1px; padding:10px;">
                    <span runat="server" id="txtNbCandidato"></span>
                </td>
                  </tr>
            <tr style="border-collapse:collapse; border: solid 1px;">
                <td style="border-collapse:collapse; border: solid 1px; padding:10px;">
                    <label>Puesto: </label>
                </td>
                <td style="border-collapse:collapse; border: solid 1px; padding:10px;">
                    <span runat="server" id="txtNbPuesto"></span>
                </td>
            </tr>
        </table>
            <div style="clear: both; height: 10px;"></div>
            <telerik:RadHtmlChart runat="server" ID="rhcConsultaGlobal" Width="100%" Height="480" Transitions="true" Skin="Silk">
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
                    <YAxis>
                        <LabelsAppearance DataFormatString="{0}%"></LabelsAppearance>
                    </YAxis>
                </PlotArea>
                <Appearance>
                    <FillStyle BackgroundColor="Transparent"></FillStyle>
                </Appearance>
                <Legend>
                    <Appearance BackgroundColor="Transparent" Position="Bottom">
                    </Appearance>
                </Legend>
            </telerik:RadHtmlChart>
                <div style="clear: both; height: 15px;"></div>
            <div style="width:100%;">
            <div style="width:50%; float:left;">
            <div id="dvCompatibilidad" runat="server"></div>
                </div>
           <%-- <telerik:RadGrid ID="rgdCompatibilidad"
                runat="server"
                AllowSorting="false"
                AutoGenerateColumns="false"
                HeaderStyle-Font-Bold="true"
                OnNeedDataSource="rgdCompatibilidad_NeedDataSource"
                BorderStyle="Solid"
                BorderWidth="1px">
                <ClientSettings>
                    <Scrolling UseStaticHeaders="false" AllowScroll="false" />
                    <Selecting AllowRowSelect="false" />
                </ClientSettings>
                <MasterTableView AllowFilteringByColumn="false" AllowPaging="false" ShowHeadersWhenNoRecords="true" ShowFooter="true">
                    <Columns>
                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="180" HeaderText="Elemento" DataField="NB_FACTOR" FooterText="Compatibilidad total:" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true" UniqueName="NB_FACTOR" ColumnValidationSettings-ModelErrorMessage-BorderStyle="Solid"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="70" HeaderText="Valor" DataFormatString="{0:N2}%" DataField="PR_VALOR" UniqueName="PR_VALOR" Aggregate="Sum" FooterStyle-Font-Bold="true" FooterAggregateFormatString="{0:N2}%" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" ColumnValidationSettings-ModelErrorMessage-BorderStyle="Solid"></telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>--%>
 <div style="width:50%; float:left;">
                <table style="border-collapse:collapse; border: solid 1px;">
                    <tr>
                        <td style="border-collapse:collapse; border:1px solid; padding:10px;">
                            <label>Comentarios:</label></td>
                        <td colspan="2" style="border-collapse:collapse; border:1px solid; padding:10px;">
                            <div id="txtComentarios" runat="server" style="min-width: 100px; text-align: justify;"></div>
                        </td>
                    </tr>
                </table>
     </div>
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
