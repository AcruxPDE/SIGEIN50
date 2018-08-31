<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="VentanaEventoEvaluacionResultados.aspx.cs" Inherits="SIGE.WebApp.FYD.EvaluacionCompetencia.VentanaEventoEvaluacionResultados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">

    <style>
        .centrado {
            display: block;
            margin-left: auto;
            margin-right: auto;
        }

        .headerCentrado {
            text-align: center;
            width: 100%;
            float: left;
        }

        .lblNivel span {
            border: 1px solid gray;
            padding: 2px 7px !important;
            background-color: white;
            border-radius: 14px;
        }

        .rslItemsWrapper {
            z-index: 10 !important;
        }

        
        .RadToolTip {
            border: 1px solid #666 !important;
            background-color: whitesmoke !important;
        }

            .RadToolTip table.rtWrapper td.rtWrapperContent {
                color: #333 !important;
            }
    </style>

    <script type="text/javascript">
        function closeWindow(oWnd, args) {
                GetRadWindow().close();
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">

    <style>
        .rbToggleButton {
            line-height: 15px;
        }

        .RadSlider_Default .rslHorizontal .rslSelectedregion {
            background: transparent;
            border-color: transparent;
        }

        .RadSlider_Default .rslHorizontal .rslTrack {
            background: transparent;
            border-color: transparent;
        }

        .RadSlider_Default .rslHorizontal .rslItem {
            background: transparent;
        }

        .RadSlider_Default .rslHorizontal a.rslHandle {
            background-image: none;
        }
    </style>

    <telerik:RadToolTipManager ID="rtmEvaluacion" runat="server" AutoTooltipify="true" BackColor="WhiteSmoke" RelativeTo="Element" AutoCloseDelay="60000" Position="TopCenter" Width="300" HideEvent="LeaveTargetAndToolTip"></telerik:RadToolTipManager>

    <div style="height: calc(100% - 50px); width: 100%; overflow: auto;">
        <telerik:RadGrid runat="server" ID="rgResultados" HeaderStyle-Font-Bold="true" AutoGenerateColumns="false" OnNeedDataSource="rgResultados_NeedDataSource" Height="100%" OnDataBound="rgResultados_DataBound" OnItemDataBound="rgResultados_ItemDataBound">
            <ClientSettings>
                <Scrolling AllowScroll="true" UseStaticHeaders="true"></Scrolling>
            </ClientSettings>
             <GroupingSettings CaseSensitive="false" />

            <MasterTableView ShowHeadersWhenNoRecords="true" DataKeyNames="ID_EVENTO_PARTICIPANTE_COMPETENCIA, CL_PARTICIPANTE">
                <ColumnGroups>
                    <telerik:GridColumnGroup HeaderText="Niveles" Name="htNiveles" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center"></telerik:GridColumnGroup>
                </ColumnGroups>
                <Columns>
           <%--         <telerik:GridBoundColumn UniqueName="CL_PARTICIPANTE" DataField="CL_PARTICIPANTE" HeaderText="Clave">
                        <HeaderStyle Width="100" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="NB_PARTICIPANTE" DataField="NB_PARTICIPANTE" HeaderText="Nombre">
                        <HeaderStyle Width="300" />
                    </telerik:GridBoundColumn>--%>
                    <telerik:GridTemplateColumn DataField="NB_COMPETENCIA" HeaderText="Competencias" UniqueName="NB_COMPETENCIA" ReadOnly="true">
                        <HeaderStyle Width="300" />
                        <ItemTemplate>
                            <div title="<%# Eval("DS_COMPETENCIA") %>"><%# Eval("NB_COMPETENCIA") %></div>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <%--                    <telerik:GridTemplateColumn DataField="NO_EVALUACION" HeaderText="Calificación" UniqueName="NO_EVALUACION">
                        <HeaderTemplate>
                            <div style="padding-left: 40px;">
                                <telerik:RadSlider ID="rsEncabezado" AutoPostBack="false" runat="server" AnimationDuration="400" Skin="Default" ShowDecreaseHandle="false"
                                    ShowIncreaseHandle="false" Value="5" ShowDragHandle="false" Height="60px" ItemType="item" ThumbsInteractionMode="Free" Width="720">
                                    <Items>
                                        <telerik:RadSliderItem Text="No competente<br />0<br />0%" Value="0" ToolTip="No competente" />
                                        <telerik:RadSliderItem Text="Baja<br />1<br />20%" Value="1" ToolTip="Competencia baja" />
                                        <telerik:RadSliderItem Text="Media Baja<br />2<br />40%" Value="2" ToolTip="Competencia media baja" />
                                        <telerik:RadSliderItem Text="Media alta<br />3<br />60%" Value="3" ToolTip="Competencia media alta" />
                                        <telerik:RadSliderItem Text="Alta<br />4<br />80%" Value="4" ToolTip="Competencia alta" />
                                        <telerik:RadSliderItem Text="Óptima<br />5<br />100%" Value="5" ToolTip="Competencia óptima" />
                                    </Items>
                                </telerik:RadSlider>
                            </div>
                        </HeaderTemplate>

                        <ItemTemplate>
                            <div style="height: 100%; width: calc(100% + 15px); margin: -8px; padding: 8px;">
                                <telerik:RadSlider ID="rsNivel1" AutoPostBack="false" runat="server" AnimationDuration="400" TrackMouseWheel="false"
                                    Value='<%# decimal.Parse(Eval("NO_EVALUACION").ToString()) %>' CssClass="ItemsSlider"
                                    Height="22" ItemType="item" ThumbsInteractionMode="Free" Width="800px">
                                    <Items>
                                        <telerik:RadSliderItem Text="0" Value="0" Height="22" CssClass="lblNivel" />
                                        <telerik:RadSliderItem Text="1" Value="1" CssClass="lblNivel" />
                                        <telerik:RadSliderItem Text="2" Value="2" CssClass="lblNivel" />
                                        <telerik:RadSliderItem Text="3" Value="3" CssClass="lblNivel" />
                                        <telerik:RadSliderItem Text="4" Value="4" CssClass="lblNivel" />
                                        <telerik:RadSliderItem Text="5" Value="5" CssClass="lblNivel" />
                                    </Items>
                                </telerik:RadSlider>
                            </div>
                        </ItemTemplate>

                    </telerik:GridTemplateColumn>--%>

                    <telerik:GridTemplateColumn DataField="FG_CALIFICACION0" ColumnGroupName="htNiveles" UniqueName="FG_CALIFICACION0" HeaderStyle-Width="90" ItemStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            <div style="width: 100%; height: 100%; text-align: center;">
                                No competente<br />0<br />0%
                            </div>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <telerik:RadButton runat="server" ID="rbNivel0" Text="0" ToggleType="Radio" AutoPostBack="false" GroupName="respuestas" Checked='<%# Eval("FG_CALIFICACION0") %>'>
                                <ToggleStates>
                                    <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                    <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                </ToggleStates>
                            </telerik:RadButton>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn DataField="FG_CALIFICACION1" ColumnGroupName="htNiveles"  UniqueName="FG_CALIFICACION1" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            <div style="width: 100%; height: 100%; text-align: center;">
                               Baja<br />1<br />20%
                            </div>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <telerik:RadButton runat="server" ID="rbNivel1" Text="1" ToggleType="Radio" AutoPostBack="false" GroupName="respuestas" Checked='<%# Eval("FG_CALIFICACION1") %>'>
                                <ToggleStates>
                                    <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                    <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                </ToggleStates>
                            </telerik:RadButton>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn DataField="FG_CALIFICACION2" ColumnGroupName="htNiveles"  UniqueName="FG_CALIFICACION2" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            <div style="width: 100%; height: 100%; text-align: center;">
                                Media Baja<br />2<br />40%
                            </div>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <telerik:RadButton runat="server" ID="rbNivel2" Text="2" ToggleType="Radio" AutoPostBack="false" GroupName="respuestas" Checked='<%# Eval("FG_CALIFICACION2") %>'>
                                <ToggleStates>
                                    <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                    <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                </ToggleStates>
                            </telerik:RadButton>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn DataField="FG_CALIFICACION3" ColumnGroupName="htNiveles"  UniqueName="FG_CALIFICACION3" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            <div style="width: 100%; height: 100%; text-align: center;">
                               Media alta<br />3<br />60%
                            </div>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <telerik:RadButton runat="server" ID="rbNivel3" Text="3" ToggleType="Radio" AutoPostBack="false" GroupName="respuestas" Checked='<%# Eval("FG_CALIFICACION3") %>'>
                                <ToggleStates>
                                    <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                    <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                </ToggleStates>
                            </telerik:RadButton>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn DataField="FG_CALIFICACION4" ColumnGroupName="htNiveles"  UniqueName="FG_CALIFICACION4" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            <div style="width: 100%; height: 100%; text-align: center;">
                              Alta<br />4<br />80%
                            </div>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <telerik:RadButton runat="server" ID="rbNivel4" Text="4" ToggleType="Radio" AutoPostBack="false" GroupName="respuestas" Checked='<%# Eval("FG_CALIFICACION4") %>'>
                                <ToggleStates>
                                    <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                    <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                </ToggleStates>
                            </telerik:RadButton>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn DataField="FG_CALIFICACION5" ColumnGroupName="htNiveles"  UniqueName="FG_CALIFICACION5" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            <div style="width: 100%; height: 100%; text-align: center;">
                               Óptima<br />5<br />100%
                            </div>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <telerik:RadButton runat="server" ID="rbNivel5" Text="5" ToggleType="Radio" AutoPostBack="false" GroupName="respuestas" Checked='<%# Eval("FG_CALIFICACION5") %>'>
                                <ToggleStates>
                                    <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                    <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                </ToggleStates>
                            </telerik:RadButton>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <%--<telerik:GridTemplateColumn UniqueName="FG_CALIFICACION0" DataField="FG_CALIFICACION0">
                        <ItemStyle Width="10%" />
                        <HeaderTemplate>
                            <span class="headerCentrado">No competente</span>
                            <span class="headerCentrado">0</span>
                            <span class="headerCentrado">0%</span>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <telerik:RadCheckBox runat="server" ID="chkCalificacion0" AutoPostBack="false" CssClass="centrado"></telerik:RadCheckBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn UniqueName="FG_CALIFICACION1" DataField="FG_CALIFICACION1">
                        <ItemStyle Width="10%" />
                        <HeaderTemplate>
                            <span class="headerCentrado">Baja</span>
                            <span class="headerCentrado">1</span>
                            <span class="headerCentrado">20%</span>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <telerik:RadCheckBox runat="server" ID="chkCalificacion1" AutoPostBack="false" CssClass="centrado"></telerik:RadCheckBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn UniqueName="FG_CALIFICACION2" DataField="FG_CALIFICACION2">
                        <ItemStyle Width="10%" />
                        <HeaderTemplate>
                            <span class="headerCentrado">Media baja</span>
                            <span class="headerCentrado">2</span>
                            <span class="headerCentrado">40%</span>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <telerik:RadCheckBox runat="server" ID="chkCalificacion2" AutoPostBack="false" CssClass="centrado"></telerik:RadCheckBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn UniqueName="FG_CALIFICACION3" DataField="FG_CALIFICACION3">
                        <ItemStyle Width="10%" />
                        <HeaderTemplate>
                            <span class="headerCentrado">Media alta</span>
                            <span class="headerCentrado">3</span>
                            <span class="headerCentrado">60%</span>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div style="border: 1px solid red; width: 50%;">
                                <asp:RadioButton runat="server" ID="rbCalificacion3" AutoPostBack="false" CssClass="centrado" Width="50%" />
                            </div>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn UniqueName="FG_CALIFICACION4" DataField="FG_CALIFICACION4">
                        <ItemStyle Width="10%" />
                        <HeaderTemplate>
                            <span class="headerCentrado">Alta</span>
                            <span class="headerCentrado">4</span>
                            <span class="headerCentrado">80%</span>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <telerik:RadButton runat="server" ID="rbCalificacion4" AutoPostBack="false" CssClass="centrado" ButtonType="LinkButton" ToggleType="Radio" GroupName="calificacion">
                                <ToggleStates>
                                    <telerik:RadButtonToggleState Text="Alta" PrimaryIconCssClass="rbToggleRadioChecked" />
                                    <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleRadio" />
                                </ToggleStates>
                            </telerik:RadButton>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn UniqueName="FG_CALIFICACION5" DataField="FG_CALIFICACION5">
                        <ItemStyle Width="10%" />
                        <HeaderTemplate>
                            <span class="headerCentrado">Óptima</span>
                            <span style="text-align: center; width: 100%; float: left;">5</span>
                            <span style="text-align: center; width: 100%; float: left;">100%</span>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div style="border: 1px solid red;">
                                <telerik:RadButton runat="server" ID="rbCalificacion5" AutoPostBack="false" CssClass="centrado" ButtonType="ToggleButton" ToggleType="Radio" GroupName="calificacion">
                                </telerik:RadButton>
                            </div>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>--%>


                    <%--<telerik:GridTemplateColumn UniqueName="DS_SIGNIFICADO" DataField="FG_CALIFICACION5">
                    <ItemTemplate>
                        <telerik:RadTextBox runat="server" ID="txtSignificado"></telerik:RadTextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>--%>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
    <div style="clear: both; height: 10px;"></div>
    <div class="divControlDerecha">

        <telerik:RadButton runat="server" ID="btnAceptar" Text="Aceptar" OnClick="btnAceptar_Click"></telerik:RadButton>

        <telerik:RadButton runat="server" ID="btnCancelar" Text="Cancelar" OnClientClicked="closeWindow"></telerik:RadButton>
         <telerik:RadButton runat="server" ID="btnCancelarInvitado" Text="Cancelar" OnClick="RadButton1_Click" AutoPostBack="true" Visible="false"></telerik:RadButton>
    </div>

    <telerik:RadWindowManager ID="rwmResultados" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
