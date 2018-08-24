<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="VentanaAnalisisCompetencias.aspx.cs" Inherits="SIGE.WebApp.IDP.VentanaAnalisisCompetencias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">

    <style>
        .CompatibilidadAlta {
            height: 100%;
            border-radius: 5px;
            background: green;
        }

        .CompatibilidadMedia {
            height: 100%;
            border-radius: 5px;
            background: gold;
        }

        .CompatibilidadBaja {
            height: 100%;
            border-radius: 5px;
            background: red;
        }
    </style>
    <script type="text/javascript">
    
        function OpenPreview() {
            var vURL = "../Administracion/VentanaVistaDescriptivo.aspx";
            var vTitulo = "Vista previa descriptivo";
            var vIdPuesto = '<%# vIdPuesto %>';

            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 20,
                height: browserWnd.innerHeight - 20
            };

            vURL = vURL + "?PuestoId=" + vIdPuesto;
            var wnd = openChildDialog(vURL, "winVistaPrevia", vTitulo, windowProperties);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div style="clear: both; height: 5px;"></div>

    <%--  <div class="ctrlBasico">

        <table class="ctrlTableForm">
            <tr>
                <td class="ctrlTableDataContext">
                    <label>Puesto a cubrir:</label>
                </td>
                <td class="ctrlTableDataBorderContext">
                    <span id="txtPuesto" runat="server"></span>
                </td>
                <td class="ctrlTableDataContext">
                    <label>Candidato:</label>
                </td>
                <td class="ctrlTableDataBorderContext">
                    <span runat="server" id="txtCandidato"></span>
                </td>
            </tr>
        </table>

    </div>--%>

    <table class="ctrlTableForm">
        <tr>
            <td class="ctrlTableDataContext">
                <label style="text-align: center;">Puesto: </label>
            </td>
            <td class="ctrlTableDataBorderContext">
                <a id="txtNombrePuesto" runat="server" href="javascript:OpenPreview();"></a>
                <%--<span id="txtNombrePuesto" runat="server"></span>--%>
            </td>
            <td class="ctrlTableDataContext">
                <label style="text-align: center;">Candidato: </label>
            </td>
            <td class="ctrlTableDataBorderContext">
                <span id="txtNombreCandidato" runat="server"></span>
            </td>
        </tr>
    </table>

    <div style="clear: both; height: 10px;"></div>

    <telerik:RadTabStrip runat="server" ID="rtsAnalisis" MultiPageID="rmpAnalisis" Width="100%" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" Text="Análisis de perfil" TabIndex="0"></telerik:RadTab>
            <telerik:RadTab runat="server" Text="Análisis de competencias" TabIndex="1"></telerik:RadTab>
            <telerik:RadTab runat="server" Text="Comparación de competencias Puesto/Candidato" TabIndex="3"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <div style="height: calc(100% - 100px); width: 100%;">

        <telerik:RadMultiPage runat="server" ID="rmpAnalisis" SelectedIndex="0" Width="100%" Height="100%">

            <telerik:RadPageView ID="rpvPerfil" runat="server">

                <div style="clear: both; height: 5px;"></div>

                <div class="ctrlBasico">
                    <table class="ctrlTableForm">

                        <%--<tr>
                            <td class="ctrlTableDataContext"></td>

                            <td class="ctrlTableDataBorderContext">
                                <div style="text-align: center; width: 100%;">
                                    <label style="text-align: center;">Puesto: </label>
                                    <span id="txtNombrePuesto" runat="server"></span>
                                </div>

                            </td>

                            <td class="ctrlTableDataBorderContext">

                                <div style="text-align: center; width: 100%;">
                                    <label style="text-align: center;">Candidato: </label>
                                    <span id="txtNombreCandidato" runat="server"></span>
                                </div>

                            </td>
                            <td class="ctrlTableDataBorderContext"></td>
                        </tr>--%>

                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Edad:</label>
                            </td>

                            <td class="ctrlTableDataBorderContext">
                                <span id="txtEdadPerfil" runat="server"></span>
                            </td>

                            <td class="ctrlTableDataBorderContext">
                                <span runat="server" id="txtEdadCandidato"></span>
                            </td>
                            <td class="ctrlTableDataBorderContext">
                                <div runat="server" id="divComparacionEdad" style="width: 20px;">&nbsp;</div>
                            </td>
                        </tr>

                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Género</label>
                            </td>

                            <td class="ctrlTableDataBorderContext">
                                <span id="txtGeneroPerfil" runat="server"></span>
                            </td>

                            <td class="ctrlTableDataBorderContext">
                                <span runat="server" id="txtGeneroCandidato"></span>
                            </td>
                            <td class="ctrlTableDataBorderContext">
                                <div runat="server" id="divComparacionGenero">&nbsp;</div>
                            </td>
                        </tr>

                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Estado civil:</label>
                            </td>

                            <td class="ctrlTableDataBorderContext">
                                <span id="txtEdoCivilPerfil" runat="server"></span>
                            </td>

                            <td class="ctrlTableDataBorderContext">
                                <span runat="server" id="txtEdoCivilCandidato"></span>
                            </td>
                            <td class="ctrlTableDataBorderContext">
                                <div runat="server" id="divComparacionEdoCivil">&nbsp;</div>
                            </td>
                        </tr>

                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Escolaridades: </label>
                            </td>

                            <td class="ctrlTableDataBorderContext">
                                <span id="txtEscolaridadesPerfil" runat="server"></span>
                            </td>

                            <td class="ctrlTableDataBorderContext">
                                <span runat="server" id="txtEscolaridadesCandidato"></span>
                            </td>
                            <td class="ctrlTableDataBorderContext">
                                <div runat="server" id="divComparacionEscolaridades">&nbsp;</div>
                            </td>
                        </tr>

                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Competencias especificas:</label>
                            </td>

                            <td class="ctrlTableDataBorderContext">
                                <span id="txtComEspPerfil" runat="server"></span>
                            </td>

                            <td class="ctrlTableDataBorderContext">
                                <span runat="server" id="txtCompEspCandidato"></span>
                            </td>
                            <td class="ctrlTableDataBorderContext">
                                <div runat="server" id="divComparacionCompEsp">&nbsp;</div>
                            </td>
                        </tr>

                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Areas de interes:</label>
                            </td>
                            <td class="ctrlTableDataBorderContext">
                                <span id="txtAreaInteresPerfil" runat="server"></span>
                            </td>
                            <td class="ctrlTableDataBorderContext">
                                <span runat="server" id="txtAreaInteresCandidato"></span>
                            </td>
                            <td class="ctrlTableDataBorderContext">
                                <div runat="server" id="divComparacionAreaInteres">&nbsp;</div>
                            </td>
                        </tr>
                    </table>
                </div>
                 <div class="divControlDerecha" style="width: 15%; text-align: left; margin-right:20px;">
                        <fieldset>
                            <legend>
                                <label>Compatibilidad</label>
                            </legend>

                            <table class="ctrlTableForm">
                                <tr>
                                    <td>
                                        <div style="background: green; width: 50px; border-radius: 1px;">
                                            <br />
                                        </div>
                                    </td>
                                    <td>
                                        <label>Compatible</label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="background: red; width: 50px; border-radius: 1px;">
                                            <br />
                                        </div>
                                    </td>
                                    <td>
                                        <label>No compatible</label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style=" width: 50px; border-radius: 1px;">
                                            N/A
                                        </div>
                                    </td>
                                    <td>
                                        <label>No aplica</label></td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                <div style="clear: both; height: 10px;"></div>
                <table class="ctrlTableForm">
                    <tr>
                        <td class="ctrlTableDataContext">
                            <label>Porcentaje de compatibilidad con el perfil: </label>
                        </td>
                        <td class="ctrlTableDataBorderContext">
                            <span runat="server" id="txtNoElementosCompatibles"></span>
                        </td>
                        <%--<td class="ctrlTableDataContext">
                            <label>Promedio de compatibilidad: </label>
                        </td>
                        <td class="ctrlTableDataBorderContext">
                            <span runat="server" id="txtPromedioCompatibilidad"></span>
                        </td>--%>
                    </tr>
                </table>
               
            </telerik:RadPageView>
            <telerik:RadPageView ID="rpvCompetencias" runat="server">
                <div style="height: calc(100% - 10px); width: 100%;">
                    <telerik:RadGrid runat="server" ID="rgCompetencias" HeaderStyle-Font-Bold="true" AutoGenerateColumns="false" Height="100%" OnNeedDataSource="rgCompetencias_NeedDataSource" EnableHeaderContextMenu="true">
                        <ClientSettings>
                            <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                        </ClientSettings>
                        <MasterTableView ShowFooter="true">
                            <Columns>
                                <telerik:GridTemplateColumn UniqueName="CL_COLOR" DataField="CL_COLOR">
                                    <HeaderStyle Width="40px" />
                                    <ItemTemplate>
                                        <table style="height: 100%; width: 20px;">
                                            <tr>
                                                <td style="border-width: 0px; padding: 0px;">
                                                    <div style="height: 100%; border-radius: 5px; background: <%# Eval("CL_COLOR") %>;">&nbsp;</div>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn UniqueName="NB_CLASIFICACION_COMPETENCIA" DataField="NB_CLASIFICACION_COMPETENCIA" HeaderText="Clasificación">
                                    <HeaderStyle Width="150px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="NB_COMPETENCIA" DataField="NB_COMPETENCIA" HeaderText="Competencia">
                                    <HeaderStyle Width="200px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="DS_COMPETENCIA" DataField="DS_COMPETENCIA" HeaderText="Descripción"></telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="PR_CANDIDATO" DataField="PR_CANDIDATO" HeaderText="% de cumplimiento" >
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle Width="150px" />
                                    <ItemTemplate>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="padding-right: 10px; width: 90%; text-align: right;">
                                                    <%# string.Format("{0:N2}", Eval("PR_CANDIDATO")) %>
                                                </td>
                                                <td>
                                                    <div class="<%# Eval("CL_COMPATIBILIDAD") %>">&nbsp;</div>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <%--<telerik:GridBoundColumn UniqueName="PR_CUMPLIMIENTO_COMPETENCIA" DataField="PR_CUMPLIMIENTO_COMPETENCIA" HeaderText="% de cumplimiento" DataFormatString="{0:N2}%">
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>--%>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
            </telerik:RadPageView>

            <telerik:RadPageView ID="rpvGrafica" runat="server">
                <div class="ctrlBasico" style="width: 70%">
                    <telerik:RadHtmlChart runat="server" ID="rhcPuestoCandidatos" Width="100%" Height="500" Transitions="true" Skin="Silk">
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
                            <Appearance BackgroundColor="Transparent" Position="Bottom">
                            </Appearance>
                        </Legend>
                    </telerik:RadHtmlChart>
                </div>

                  <div class="ctrlBasico" style="width: 30%">
                    <telerik:RadGrid ID="rgdPromedios"
                        runat="server"
                        AllowSorting="false"
                        HeaderStyle-Font-Bold="true"
                        AutoGenerateColumns="false">
                        <ClientSettings>
                            <Scrolling UseStaticHeaders="false" AllowScroll="false" />
                            <Selecting AllowRowSelect="false" />
                        </ClientSettings>
                        <MasterTableView AllowFilteringByColumn="false" AllowPaging="false" ShowHeadersWhenNoRecords="true">
                            <Columns>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="100" HeaderText="Persona" DataField="NB_PUESTO" UniqueName="NB_PUESTO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="50" HeaderText="% Compatibilidad" DataField="PROMEDIO_TXT" UniqueName="PROMEDIO_TXT" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                 <div class="ctrlBasico" runat="server" id="divMensajeMayor130" visible ="false" style="width: 60%">
                    <label id="lblPuesto" name="lblPuesto" runat="server">(*) La persona tiene la capacidad para desempeñar el puesto, sin embargo supera en 30% o más los requerimientos de competencias del mismo. Esto es un factor que debe ser considerado para la toma de decisiones</label>
                </div>
            </telerik:RadPageView>

        </telerik:RadMultiPage>

    </div>


</asp:Content>
