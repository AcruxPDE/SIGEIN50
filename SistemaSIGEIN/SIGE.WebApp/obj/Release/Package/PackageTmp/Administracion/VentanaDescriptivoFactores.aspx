<%@ Page Title="" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="VentanaDescriptivoFactores.aspx.cs" Inherits="SIGE.WebApp.Administracion.VentanaDescriptivoFactores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">

    <script type="text/javascript">
        function closeWindow() {
            GetRadWindow().close();
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">

    <div style="height: 5px; clear: both;"></div>

    <telerik:RadTabStrip ID="rtsConsultaGlobal" runat="server" SelectedIndex="0" MultiPageID="rmpConsultaGlobal">
        <Tabs>
            <telerik:RadTab Text="Puestos" SelectedIndex="0"></telerik:RadTab>
            <telerik:RadTab Text="Configuración"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
     <telerik:RadMultiPage ID="rmpConsultaGlobal" runat="server" SelectedIndex="0" Height="85%">
                    <telerik:RadPageView ID="rpvContexto" runat="server" Height="100%">
                        <div style="height:calc(100% - 20px);">
                        <div style="height: 5px; clear: both;"></div>
                         <telerik:RadGrid ID="grdPuestos" HeaderStyle-Font-Bold="true"  OnNeedDataSource="grdPuestos_NeedDataSource" ShowHeader="true" Width="70%" Height="100%" runat="server" GridLines="None" AutoGenerateColumns="false">
                             <ClientSettings Scrolling-AllowScroll="true"></ClientSettings>
                                    <MasterTableView>
                                        <Columns>
                                            <telerik:GridBoundColumn HeaderText="Clave" DataField="CL_PUESTO" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn  HeaderText="Puesto" DataField="NB_PUESTO" HeaderStyle-Width="250"></telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                        </telerik:RadPageView>
         <telerik:RadPageView ID="rpvConfiguracion" runat="server" Height="100%">
              <div style="height:calc(100% - 60px);">
    <table class="ctrlTableForm" runat="server" id="tabla">
        <tr>
            <td class="ctrlTableDataContext">
                <label runat="server" id="lbPuesto">Puesto: </label>
            </td>
            <td class="ctrlTableDataBorderContext">
                <span runat="server" id="txtPuesto" style="width:300px;"></span>
            </td>
        </tr>
    </table>

    <div style="height: 5px; clear: both;"></div>
    <table border="0">
        <tr>
            <td style="width: 70px;">
                <label style="width: 100%; text-align: center;">Número</label>
            </td>
            <td style="width:50px;">
                <label style="width:100%; text-align:center;">Activo</label>
            </td>
            <td style="width: 410px;">
                <label style="width: 100%; text-align: center;">Nombre del elemento</label>
            </td>
            <td style="width: 120px;">
                <label style="width: 100%; text-align: center;">Ponderación %</label>
            </td>
            <td style="width: 150px;">
                <label style="width: 100%; text-align: center;">Asociado a inglés</label>
            </td>
        </tr>

        <%-- Primer factor --%>

        <tr>
            <td>
                <label style="width: 100%; text-align: center;">1</label>
            </td>
            <td style="text-align: center;">
                <telerik:RadCheckBox runat="server" Enabled="false"  ID="chkHabilitarF1" AutoPostBack="false" Style="margin-left: auto; margin-right: auto;"></telerik:RadCheckBox>
            </td>
            <td style="text-align: center;">
                <telerik:RadTextBox runat="server" ID="txtNombreF1" AutoPostBack="false" Width="400px" Enabled="false"></telerik:RadTextBox>
            </td>
            <td style="text-align: center;">
                <telerik:RadNumericTextBox runat="server" ID="txtPonderacionF1" AutoPostBack="false" Width="70px" MinValue="0" MaxValue="100">
                    <EnabledStyle HorizontalAlign="Right" />
                </telerik:RadNumericTextBox>
            </td>
            <td style="text-align: center;">
                <%--<telerik:RadCheckBox runat="server" ID="chkInglesF1" AutoPostBack="false"></telerik:RadCheckBox>--%>
                <%--<telerik:RadButton runat="server" ID="rbInglesF1" AutoPostBack="false" ButtonType="ToggleButton" ToggleType="Radio" GroupName="Ingles"></telerik:RadButton>--%>
            </td>
        </tr>

        <%-- Segundo  factor --%>

        <tr>
            <td>
                <label style="width: 100%; text-align: center;">2</label>
            </td>
            <td style="text-align: center;">
                <telerik:RadCheckBox runat="server" ID="chkHabilitarF2" AutoPostBack="false"></telerik:RadCheckBox>
            </td>
            <td style="text-align: center;">
                <telerik:RadTextBox runat="server" ID="txtNombreF2" AutoPostBack="false" Width="400px"></telerik:RadTextBox>
            </td>
            <td style="text-align: center;">
                <telerik:RadNumericTextBox runat="server" ID="txtPonderacionF2" AutoPostBack="false" Width="70px" MinValue="0" MaxValue="100">
                    <EnabledStyle HorizontalAlign="Right" />
                </telerik:RadNumericTextBox>
            </td>
            <td style="text-align: center;">
                <%--<telerik:RadCheckBox runat="server" ID="chkInglesF2" AutoPostBack="false"></telerik:RadCheckBox>--%>
                <telerik:RadButton runat="server" ID="rbInglesF2" AutoPostBack="false" ButtonType="ToggleButton" ToggleType="Radio" GroupName="Ingles"></telerik:RadButton>
            </td>
        </tr>

        <%-- Tercer factor --%>

        <tr>
            <td>
                <label style="width: 100%; text-align: center;">3</label>
            </td>
            <td style="text-align: center;">
                <telerik:RadCheckBox runat="server" ID="chkHabilitarF3" AutoPostBack="false"></telerik:RadCheckBox>
            </td>
            <td style="text-align: center;">
                <telerik:RadTextBox runat="server" ID="txtNombreF3" AutoPostBack="false" Width="400px"></telerik:RadTextBox>
            </td>
            <td style="text-align: center;">
                <telerik:RadNumericTextBox runat="server" ID="txtPonderacionF3" AutoPostBack="false" Width="70px" MinValue="0" MaxValue="100">
                    <EnabledStyle HorizontalAlign="Right" />
                </telerik:RadNumericTextBox>
            </td>
            <td style="text-align: center;">
                <%--<telerik:RadCheckBox runat="server" ID="chkInglesF3" AutoPostBack="false"></telerik:RadCheckBox>--%>
                <telerik:RadButton runat="server" ID="rbInglesF3" AutoPostBack="false" ButtonType="ToggleButton" ToggleType="Radio" GroupName="Ingles"></telerik:RadButton>
            </td>
        </tr>

        <%-- Cuarto factor --%>

        <tr>
            <td>
                <label style="width: 100%; text-align: center;">4</label>
            </td>
            <td style="text-align: center;">
                <telerik:RadCheckBox runat="server" ID="chkHabilitarF4" AutoPostBack="false"></telerik:RadCheckBox>
            </td>
            <td style="text-align: center;">
                <telerik:RadTextBox runat="server" ID="txtNombreF4" AutoPostBack="false" Width="400px"></telerik:RadTextBox>
            </td>
            <td style="text-align: center;">
                <telerik:RadNumericTextBox runat="server" ID="txtPonderacionF4" AutoPostBack="false" Width="70px" MinValue="0" MaxValue="100">
                    <EnabledStyle HorizontalAlign="Right" />
                </telerik:RadNumericTextBox>
            </td>
            <td style="text-align: center;">
                <%--<telerik:RadCheckBox runat="server" ID="chkInglesF4" AutoPostBack="false"></telerik:RadCheckBox>--%>
                <telerik:RadButton runat="server" ID="rbInglesF4" AutoPostBack="false" ButtonType="ToggleButton" ToggleType="Radio" GroupName="Ingles"></telerik:RadButton>
            </td>
        </tr>

        <%-- Quinto factor --%>

        <tr>
            <td>
                <label style="width: 100%; text-align: center;">5</label>
            </td>
            <td style="text-align: center;">
                <telerik:RadCheckBox runat="server" ID="chkHabilitarF5" AutoPostBack="false"></telerik:RadCheckBox>
            </td>
            <td style="text-align: center;">
                <telerik:RadTextBox runat="server" ID="txtNombreF5" AutoPostBack="false" Width="400px"></telerik:RadTextBox>
            </td>
            <td style="text-align: center;">
                <telerik:RadNumericTextBox runat="server" ID="txtPonderacionF5" AutoPostBack="false" Width="70px" MinValue="0" MaxValue="100">
                    <EnabledStyle HorizontalAlign="Right" />
                </telerik:RadNumericTextBox>
            </td>
            <td style="text-align: center;">
                <%--<telerik:RadCheckBox runat="server" ID="chkInglesF5" AutoPostBack="false"></telerik:RadCheckBox>--%>
                <telerik:RadButton runat="server" ID="rbInglesF5" AutoPostBack="false" ButtonType="ToggleButton" ToggleType="Radio" GroupName="Ingles"></telerik:RadButton>
            </td>
        </tr>

        <%-- Sexto factor --%>

        <tr>
            <td>
                <label style="width: 100%; text-align: center;">6</label>
            </td>
            <td style="text-align: center;">
                <telerik:RadCheckBox runat="server" ID="chkHabilitarF6" AutoPostBack="false"></telerik:RadCheckBox>
            </td>
            <td style="text-align: center;">
                <telerik:RadTextBox runat="server" ID="txtNombreF6" AutoPostBack="false" Width="400px"></telerik:RadTextBox>
            </td>
            <td style="text-align: center;">
                <telerik:RadNumericTextBox runat="server" ID="txtPonderacionF6" AutoPostBack="false" Width="70px" MinValue="0" MaxValue="100">
                    <EnabledStyle HorizontalAlign="Right" />
                </telerik:RadNumericTextBox>
            </td>
            <td style="text-align: center;">
                <%--<telerik:RadCheckBox runat="server" ID="chkInglesF6" AutoPostBack="false"></telerik:RadCheckBox>--%>
                <telerik:RadButton runat="server" ID="rbInglesF6" AutoPostBack="false" ButtonType="ToggleButton" ToggleType="Radio" GroupName="Ingles"></telerik:RadButton>
            </td>
        </tr>
         <tr>
            <td></td>
            <td style="text-align: center;"></td>
            <td style="text-align: center;"></td>
            <td style="text-align: center;"></td>
            <td style="text-align: center;">
                <telerik:RadButton runat="server" ID="rbSinIngles" Text="Sin Asociación"  AutoPostBack="false" ButtonType="ToggleButton" ToggleType="Radio" GroupName="Ingles"></telerik:RadButton>
            </td>
        </tr>


    </table>
   </div>
    <div style="height: 10px; clear: both;"></div>

    <div class="divControlDerecha">
        <div class="ctrlBasico">
            <telerik:RadButton runat="server" ID="btnGuardar" Text="Guardar" OnClick="btnGuardar_Click"></telerik:RadButton>
        </div>

        <div class="ctrlBasico">
            <telerik:RadButton runat="server" ID="btnCancelar" Text="Cancelar" AutoPostBack="false" OnClientClicked="closeWindow"></telerik:RadButton>
        </div>
    </div>

</telerik:RadPageView>
    </telerik:RadMultiPage>
  
    <telerik:RadWindowManager ID="rwmAlertas" runat="server"></telerik:RadWindowManager>
</asp:Content>
