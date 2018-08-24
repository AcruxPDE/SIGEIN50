<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="VentanaConsultaGlobal.aspx.cs" Inherits="SIGE.WebApp.IDP.VentanaConsultaGlobal" %>

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

    <script>

        function OnClientClicked(sender, args) {
            var radtabstrip = $find('<%=rtsConsultas.ClientID %>');
            var editor = $find("<%= reComentarios.ClientID %>");
            var options = {
                trimText: true,
                removeMultipleSpaces: true
            }
            var oSelElem = editor.get_text(options);
            //if (oSelElem != null && oSelElem != "") {
            var count = radtabstrip.get_tabs().get_count();
            var currentindex = radtabstrip.get_selectedIndex();
            var nextindex = currentindex + 1;
            if (nextindex < count) {
                radtabstrip.set_selectedIndex(nextindex);
                // }
            }
        }


        function OpenImpresion() {
            //var myPageView = $find('<= rmpConsultas.ClientID %>');
            //var myIframe = document.getElementById('ifrmPrint');
            //var pvContent = myPageView.get_selectedPageView().get_element().innerHTML;
            //var myDoc = (myIframe.contentWindow || myIframe.contentDocument);
            //if (myDoc.document) myDoc = myDoc.document;
            //myDoc.write("<html><head><title>Consulta global</title>");
            //myDoc.write("</head><body onload='this.focus(); this.print();'>");
            //myDoc.write(pvContent + "</body></html>");
            //myDoc.close(); (pvContent + "</body></html>");
            //myDoc.close();
            var myWindow = window.open("ReporteConsultaGlobal.aspx?IdPuesto=" + '<%= vIdPuesto %>' + "&IdCandidato=" + '<%= vIdCandidato %>', "MsgWindow", "width=650,height=650");
        }


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">

    <telerik:RadAjaxLoadingPanel runat="server" ID="ralpConsultaGlobal"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager runat="server" ID="ramConsultaGlobal" DefaultLoadingPanelID="ralpConsultaGlobal">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnRecalcular">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgConsultaglobal" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgConsultaglobal" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rgdCompatibilidad" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rhcConsultaGlobal" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtComentarios" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rtsConsultas" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnReporte">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgdCompatibilidad" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rhcConsultaGlobal" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtComentarios" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <table class="ctrlTableForm">
        <tr>
            <td class="ctrlTableDataContext">
                <label>Folio de solicitud: </label>
            </td>
            <td class="ctrlTableDataBorderContext">
                <span runat="server" id="txtClSolicitud"></span>
            </td>
            <td class="ctrlTableDataContext">
                <label>Candidato: </label>
            </td>
            <td class="ctrlTableDataBorderContext">
                <span runat="server" id="txtNbCandidato"></span>
            </td>
            <td class="ctrlTableDataContext">
                <label>Puesto: </label>
            </td>
            <td class="ctrlTableDataBorderContext">
                <span runat="server" id="txtNbPuesto"></span>
            </td>
        </tr>
    </table>

    <telerik:RadSplitter ID="rsConsultas" runat="server" Width="100%" Height="95%" BorderSize="0">
        <telerik:RadPane ID="rpConsultas" runat="server" Width="100%" Height="100%">
            <telerik:RadTabStrip ID="rtsConsultas" runat="server" MultiPageID="rmpConsultas" SelectedIndex="0">
                <Tabs>
                    <telerik:RadTab Text="Ingrese los valores para el candidato/puesto " SelectedIndex="0"></telerik:RadTab>
                    <telerik:RadTab Text="Consulta Global" SelectedIndex="1"></telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <div style="height: calc(100% - 50px);">
                <telerik:RadMultiPage ID="rmpConsultas" runat="server" SelectedIndex="0" Height="100%">
                    <telerik:RadPageView ID="rpvParametrosConsulta" runat="server">
                        <div style="clear: both; height: 10px;"></div>
                        <div style="height: calc(100% - 180px);">
                            <telerik:RadGrid runat="server" Height="100%" ID="rgConsultaglobal" HeaderStyle-Font-Bold="true" OnNeedDataSource="rgConsultaglobal_NeedDataSource" AutoGenerateColumns="false" Width="100%">
                                <ClientSettings>
                                    <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                                </ClientSettings>
                                <MasterTableView EditMode="InPlace" DataKeyNames="ID_CONSULTA_GLOBAL_CALIFICACION" ShowFooter="true">
                                    <Columns>
                                        <telerik:GridBoundColumn UniqueName="NB_FACTOR" DataField="NB_FACTOR" HeaderText="Elemento">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="PR_FACTOR" DataField="PR_FACTOR" HeaderText="Ponderación" DataFormatString="{0:N2}%">
                                            <HeaderStyle Width="100px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn UniqueName="PR_CALIFICACION" DataField="PR_CALIFICACION" HeaderText="Calificación" FooterText="Compatibilidad total:" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                            <HeaderStyle Width="110px" />
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox runat="server" ID="txtCalificacion" Width="100%" MinValue="0" MaxValue="100" IncrementSettings-InterceptMouseWheel="false" AutoPostBack="false" Text='<%# Bind("PR_CALIFICACION") %>' Enabled='<%# HabilitarTextbox(Eval("NO_FACTOR").ToString(), Eval("FG_ASOCIADO_INGLES").ToString()) %>' NumberFormat-DecimalDigits="2" ShowSpinButtons="false">
                                                    <EnabledStyle HorizontalAlign="Right" />
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn UniqueName="PR_VALOR" DataField="PR_VALOR" HeaderText="Valor" FooterStyle-Font-Bold="true" Aggregate="Sum" DataFormatString="{0:N2}%">
                                            <HeaderStyle Width="100px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>
                        <div style="clear: both; height: 10px;"></div>
                        <label>Comentarios: </label>
                        <telerik:RadEditor Height="90px" Width="500px" ToolsWidth="400px" EditModes="Design" ID="reComentarios" runat="server" ToolbarMode="ShowOnFocus" ToolsFile="~/Assets/BasicTools.xml"></telerik:RadEditor>
                        <div class="divControlDerecha">
                            <div class="ctrlBasico">
                                <telerik:RadButton runat="server" ID="btnRecalcular" Text="Recalcular" OnClick="btnRecalcular_Click"></telerik:RadButton>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton runat="server" ID="btnGuardar" Text="Guardar" UseSubmitBehavior="false" OnClientClicked="OnClientClicked" OnClick="btnGuardar_Click"></telerik:RadButton>
                            </div>
                            <%-- <div class="ctrlBasico">
                        <telerik:RadButton AutoPostBack="true" runat="server" ID="btnReporte" Text="Reporte" OnClientClicked="OnClientClicked" OnClick="btnReporte_Click"></telerik:RadButton>
                    </div>--%>
                        </div>
                    </telerik:RadPageView>

                    <telerik:RadPageView ID="rpvConsulta" runat="server">
                        <div style="height: calc(100% - 20px); width: 100%">
                            <div style="float: left; width: 50%; height: 100%">
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
                            </div>
                            <div style="float: left; width: 50%; height: 100%">
                                <telerik:RadGrid ID="rgdCompatibilidad"
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
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="180" HeaderText="Elemento" DataField="NB_FACTOR" FooterText="Compatibilidad total:" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true" UniqueName="NB_FACTOR"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="70" HeaderText="Valor" DataFormatString="{0:N2}%" DataField="PR_VALOR" UniqueName="PR_VALOR" Aggregate="Sum" FooterStyle-Font-Bold="true" FooterAggregateFormatString="{0:N2}%" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                                <div style="clear: both;"></div>
                                <div>
                                    <table class="ctrlTableForm ctrlTableContext">
                                        <tr>
                                            <td>
                                                <label>Comentarios:</label></td>
                                            <td colspan="2">
                                                <div id="txtComentarios" runat="server" style="min-width: 100px; text-align: justify;"></div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="divControlDerecha">
                                <div class="ctrlBasico">
                                    <telerik:RadButton runat="server" ID="btnImprimir" Text="Imprimir" OnClientClicked="OpenImpresion" AutoPostBack="false"></telerik:RadButton>
                                </div>
                            </div>
                        </div>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
                <iframe src="#" style="width: 0; height: 0; border: none" id="ifrmPrint"></iframe>
            </div>
        </telerik:RadPane>
        <telerik:RadPane ID="rpAyuda" runat="server" Width="20px" Height="90%">
            <telerik:RadSlidingZone ID="rszAyuda" runat="server" SlideDirection="Left" Height="100%" ExpandedPaneId="rsConsultas" Width="20px" DockedPaneId="rsbConsultas">
                <telerik:RadSlidingPane ID="rsbAyuda" runat="server" CollapseMode="Forward" EnableResize="false" Width="250px" Title="Instrucciones" Height="100%">
                    <div id="divTabuladorMaestro" runat="server">
                        <p style="text-align: justify; margin-left: 10px; margin-right: 10px;">
                            <br />
                            En esta consulta puedes evaluar elementos adicionales. Establece la ponderación adecuada al puesto que estás evaluando de acuerdo a sus características. 
                            Califica del 0 al 100 cada uno de los elementos que estás evaluando. 
                            <br />
                            <br />
                            Una vez que hayas terminado  haz click en el botón recalcular para ver la calificación y guardar para ver el gráfico. 																	
                        </p>
                    </div>
                </telerik:RadSlidingPane>
            </telerik:RadSlidingZone>
        </telerik:RadPane>
    </telerik:RadSplitter>

    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
