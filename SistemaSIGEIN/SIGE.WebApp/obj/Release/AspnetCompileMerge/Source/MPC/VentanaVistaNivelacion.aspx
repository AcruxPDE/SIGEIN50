<%@ Page Title="" Language="C#" MasterPageFile="~/MPC/ContextMC.master" AutoEventWireup="true" CodeBehind="VentanaVistaNivelacion.aspx.cs" Inherits="SIGE.WebApp.MPC.VentanaVistaNivelacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function closeWindow() {
                GetRadWindow().close();
            }

            
             </script>
    </telerik:RadCodeBlock>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">

     <div style="clear: both; height: 10px;"></div>
                <div class="ctrlBasico">
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
                </div>
     <div style="clear: both; height: 10px;"></div>
                <div style="height: calc(100% - 150px);">
    <telerik:RadGrid ID="grdValuacionPuesto"
                        runat="server"
                        Height="100%"
                        AllowSorting="true"
                        AutoGenerateColumns="true"
                           HeaderStyle-Font-Bold="true"
                        OnNeedDataSource="grdValuacionPuesto_NeedDataSource"
                        OnItemDataBound="grdValuacionPuesto_ItemDataBound">
                        <ClientSettings>
                            <Selecting AllowRowSelect="true" />
                            <Scrolling AllowScroll="true" UseStaticHeaders="true"></Scrolling>
                        </ClientSettings>
                        <GroupingSettings CaseSensitive="false" />
                        <PagerStyle AlwaysVisible="true" />
                        <MasterTableView AutoGenerateColumns="false" DataKeyNames ="ID_TABULADOR_PUESTO" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true">
                            <Columns>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="130" FilterControlWidth="100" HeaderText="Clave"  DataField="CL_PUESTO"  UniqueName="CL_PUESTO" ></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="220" FilterControlWidth="180" HeaderText="Puesto"  DataField="NB_PUESTO" UniqueName="NB_PUESTO"></telerik:GridBoundColumn>
                                <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="120" FilterControlWidth="100" HeaderText="Valuación"  DataField="NO_PROMEDIO_VALUACION" UniqueName="NO_PROMEDIO_VALUACION" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>--%>
                                <telerik:GridTemplateColumn HeaderText="Valuación" HeaderStyle-Width="120" FilterControlWidth="100" AutoPostBackOnFilter="false" DataField="NO_PROMEDIO_VALUACION" UniqueName="NO_PROMEDIO_VALUACION">
                                    <ItemTemplate>
                                         <span title="Promedio"><%#Eval("NO_PROMEDIO_VALUACION") %></span> 
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Nivel" HeaderStyle-Width="120" FilterControlWidth="100" AutoPostBackOnFilter="false" DataField="NO_NIVEL" UniqueName="NO_NIVEL">
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox runat="server" ID="txnNivel" Name="txnNivel" Width="80px" MinValue="0"  ShowSpinButtons="true"  Text='<%#Eval("NO_NIVEL") %>' NumberFormat-DecimalDigits="0" CssClass="RightAligned" ToolTip="Nivel"></telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                    </div>
    <div style="clear: both; height: 10px;"></div>
        <div class="divControlesBoton">
        <telerik:RadButton ID="btnAceptar" AutoPostBack="false" OnClientClicked="closeWindow" runat="server" Text="Aceptar" Width="100"></telerik:RadButton>
        <telerik:RadButton ID="btnGuardar" AutoPostBack="true"  runat="server"  Text="Guardar" OnClick="btnGuardar_Click" Width="100"></telerik:RadButton>

            </div>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server" ></telerik:RadWindowManager>
    
</asp:Content>
