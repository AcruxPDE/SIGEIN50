<%@ Page Title="" Language="C#" MasterPageFile="~/MPC/ContextMC.master" AutoEventWireup="true" CodeBehind="TabuladorNivel.aspx.cs" Inherits="SIGE.WebApp.MPC.TabuladorNivel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function closeWindow() {
                var pDatos = [{
                    accion: "<%= vCL_GUARDAR %>"
                   
                }];
                cerrarVentana(pDatos);
            }

            function cerrarVentana(recargarGrid) {
                sendDataToParent(recargarGrid);
            }

         

             </script>
    </telerik:RadCodeBlock>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
           <telerik:AjaxSetting AjaxControlID="grdNiveles">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdNiveles" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadTabStrip ID="rtsTabuladorSueldos" runat="server" SelectedIndex="0" MultiPageID="rmpTabuladorNiveles">
        <Tabs>
            <telerik:RadTab Text="Contexto" SelectedIndex="0"></telerik:RadTab>
            <telerik:RadTab Text="Niveles" SelectedIndex ="1"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="rmpTabuladorNiveles" runat="server" SelectedIndex="0" Height="90%">
        <telerik:RadPageView ID="rpvContexto" runat="server" Height="100%">
          <div style="clear: both; height: 10px;"></div>
            <div class="ctrlBasico">
                        <table class="ctrlTableForm">
                            <tr>
                                <td class="ctrlTableDataContext">
                                    <label>Versión:</label></td>
                                <td colspan="2" class="ctrlTableDataBorderContext">
                                    <div id="txtClTabulador" runat="server" style="min-width: 100px;"></div>
                                </td>
                            </tr>
                             <tr>
                                <td class="ctrlTableDataContext">
                                    <label>Descripción:</label></td>
                                <td colspan="2" class="ctrlTableDataBorderContext">
                                    <div id="txtNbTabulador" runat="server" style="min-width: 100px;"></div>
                                </td>
                            </tr>
                            <tr>
                                <td class="ctrlTableDataContext">
                                    <label>Notas:</label></td>
                                <td colspan="2" class="ctrlTableDataBorderContext">
                                    <div id="txtDescripción" runat="server" style="min-width: 100px;"></div>
                                </td>
                            </tr>
                            <tr>
                                <td class="ctrlTableDataContext">
                                    <label>Fecha:</label></td>
                                <td colspan="2" class="ctrlTableDataBorderContext">
                                    <div id="txtFecha" runat="server" style="min-width: 100px;"></div>
                                </td>
                            </tr>
                            <tr>
                                <td class="ctrlTableDataContext">
                                    <label>Vigencia:</label></td>
                                <td colspan="2" class="ctrlTableDataBorderContext">
                                    <div id="txtVigencia" runat="server" style="min-width: 100px;"></div>
                                </td>
                            </tr>
                            <tr>
                                <td class="ctrlTableDataContext">
                                    <label>Tipo de puestos:</label></td>
                                <td colspan="2" class="ctrlTableDataBorderContext">
                                    <div id="txtPuestos" runat="server" style="min-width: 100px;"></div>
                                </td>
                            </tr>
                        </table>
                    </div>
               <%-- <div class="ctrlBasico">
                    <label id="lbTabulador"
                        name="lbTabulador"
                        runat="server">
                        Tabulador:</label>
                    <telerik:RadTextBox ID="txtClTabulador"
                        runat="server"
                        Width="150px"
                        MaxLength="800"
                        Enabled="false">
                    </telerik:RadTextBox>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtNbTabulador"
                        runat="server"
                        Width="260px"
                        MaxLength="800"
                        Enabled="false">
                    </telerik:RadTextBox>
                </div>--%>
            </telerik:RadPageView>
          <telerik:RadPageView ID="rpvNiveles" runat="server" Height="100%">
                <div style="height: calc(100% - 50px);">
                    <telerik:RadGrid ID="grdNiveles"
                        runat="server"
                        Width="850px"
                        Height="100%"
                        AllowSorting="true"
                        AllowFilteringByColumn="true"
                        ShowHeader="true"
                        HeaderStyle-Font-Bold="true"
                        OnItemDataBound="grdNiveles_ItemDataBound"
                        OnNeedDataSource ="grdNiveles_NeedDataSource">
                        <GroupingSettings CaseSensitive="false" />
                        <ClientSettings>
                            <Scrolling AllowScroll="true" UseStaticHeaders="true"></Scrolling>
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <MasterTableView AutoGenerateColumns="false" DataKeyNames="ID_TABULADOR_NIVEL" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true">
                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="Clave" HeaderStyle-Width="100" FilterControlWidth="60" AutoPostBackOnFilter="false" DataField="CL_TABULADOR_NIVEL" UniqueName="CL_TABULADOR_NIVEL">
                                    <ItemTemplate>
                                        <telerik:RadTextBox ID="txtClNivel" runat="server" Enabled="false" Width="90px" MaxLength="800" Text='<%#Eval("CL_TABULADOR_NIVEL") %>'  > </telerik:RadTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Nombre" HeaderStyle-Width="180" FilterControlWidth="150" AutoPostBackOnFilter="false" DataField="NB_TABULADOR_NIVEL" UniqueName="NB_TABULADOR_NIVEL">
                                    <ItemTemplate>
                                        <telerik:RadTextBox ID="txtNbNivel" runat="server" Width="220px" MaxLength="800" Text='<%#Eval("NB_TABULADOR_NIVEL") %>'  > </telerik:RadTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true"  HeaderStyle-Width="70" FilterControlWidth="40" HeaderText="Nivel" DataField="NO_NIVEL" UniqueName="NO_NIVEL" ItemStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="Orden" HeaderStyle-Width="70" FilterControlWidth="50" AutoPostBackOnFilter="false" DataField="NO_ORDEN" UniqueName="NO_ORDEN">
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox runat="server" ID="txnOrden" Name="txnOrden" Width="60px" MinValue="1" ShowSpinButtons="true"  Text='<%#Eval("NO_ORDEN") %>' NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                 <telerik:GridTemplateColumn HeaderText="Progresión" HeaderStyle-Width="70" FilterControlWidth="50" AutoPostBackOnFilter="false" DataField="PR_PROGRESION" UniqueName="PR_PROGRESION">
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox runat="server" ID="txnProgresion" Name="txtProgresion" Width="60px" MinValue="0" ShowSpinButtons="true"  Text='<%#Eval("PR_PROGRESION") %>' NumberFormat-DecimalDigits="0"  ToolTip="Indica el porcentaje para el incremento entre las posibilidades por nivel."></telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                    </div>
    <div style="clear: both; height: 10px;"></div>
        <div class="divControlesBoton">
        <telerik:RadButton ID="btnGuardar" AutoPostBack="true" OnClick="btnGuardar_Click" runat="server"  Text="Guardar" Width="100"></telerik:RadButton>
            </div>
              </telerik:RadPageView>
        </telerik:RadMultiPage>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server" ></telerik:RadWindowManager>
</asp:Content>
