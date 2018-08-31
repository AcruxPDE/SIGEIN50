<%@ Page Title="" Language="C#" MasterPageFile="~/MPC/ContextMC.master" AutoEventWireup="true" CodeBehind="VentanaTabuladorCompetencia.aspx.cs" Inherits="SIGE.WebApp.MPC.VentanaTabuladorCompetencia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function closeWindow() {
                var pDatos = [{
                    clTipoCatalogo: "TabCompetencia"
                }];
                cerrarVentana(pDatos);
            }

            function cerrarVentana(recargarGrid) {
                sendDataToParent(recargarGrid);
            }

            function cancelarAccion() {
                cerrarVentana(null);
            }
        </script>
    </telerik:RadCodeBlock>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadPanelBar ID="rad" runat="server" Height="270" Width="100%" ExpandMode="FullExpandedItem">
        <Items>
            <telerik:RadPanelItem Text="Factor" PostBack="false" Expanded="true">
                <ContentTemplate>
                    <div style="clear: both; height: 10px;"></div>
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda">
                            <label id="lbNombre"
                                name="lbNombre"
                                runat="server">
                                Nombre:</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadTextBox ID="txtNombre"
                                runat="server"
                                Width="550px"
                                MaxLength="800">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator
                                Display="Dynamic"
                                CssClass="validacion"
                                ID="RequiredFieldValidator2"
                                runat="server"
                                Font-Names="Arial"
                                Font-Size="Small"
                                ControlToValidate="txtNombre"
                                ErrorMessage="Campo Obligatorio">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div style="clear: both;" />
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda">
                            <label id="lbDescripcion"
                                name="lbNombre"
                                runat="server">
                                Descripción:</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadTextBox ID="txtDescripccion"
                                runat="server"
                                Width="550px"
                                MaxLength="1000"
                                Height="100px"
                                TextMode="MultiLine">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator
                                Display="Dynamic"
                                CssClass="validacion"
                                ID="RequiredFieldValidator1"
                                runat="server"
                                Font-Names="Arial"
                                Font-Size="Small"
                                ControlToValidate="txtDescripccion"
                                ErrorMessage="Campo Obligatorio">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                </ContentTemplate>
            </telerik:RadPanelItem>
            <telerik:RadPanelItem Text="Definición de niveles" PostBack="false" Expanded="false">
                <ContentTemplate>
                    <div style="clear: both;"></div>
                    <table style="width: 98%;">
                        <thead>
                            <tr>
                                <th style="width: 10%;">
                                    <div></div>
                                </th>
                                <th style="width: 45%;">
                                    <div>Por Puesto</div>
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td style="text-align: center;">Nivel 0</td>
                                <td>
                                    <telerik:RadEditor
                                        Height="150px"
                                        Width="100%"
                                        ToolsWidth="310px"
                                        EditModes="Design"
                                        ID="radEditorPuesto0"
                                        runat="server"
                                        ToolbarMode="Default"
                                        ToolsFile="~/Assets/BasicTools.xml">
                                    </telerik:RadEditor>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center;">Nivel 1</td>
                                <td>
                                    <telerik:RadEditor
                                        Height="150px"
                                        Width="100%"
                                        ToolsWidth="310px"
                                        EditModes="Design"
                                        ID="radEditorPuesto1"
                                        runat="server"
                                        ToolbarMode="Default"
                                        ToolsFile="~/Assets/BasicTools.xml">
                                    </telerik:RadEditor>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center;">Nivel 2</td>
                                <td>
                                    <telerik:RadEditor
                                        Height="150px"
                                        Width="100%"
                                        ToolsWidth="310px"
                                        EditModes="Design"
                                        ID="radEditorPuesto2"
                                        runat="server"
                                        ToolbarMode="Default"
                                        ToolsFile="~/Assets/BasicTools.xml">
                                    </telerik:RadEditor>

                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center;">Nivel 3</td>
                                <td>
                                    <telerik:RadEditor
                                        Height="150px"
                                        Width="100%"
                                        ToolsWidth="310px"
                                        EditModes="Design"
                                        ID="radEditorPuesto3"
                                        runat="server"
                                        ToolbarMode="Default"
                                        ToolsFile="~/Assets/BasicTools.xml">
                                    </telerik:RadEditor>

                                </td>
                             
                            </tr>
                            <tr>
                                <td style="text-align: center;">Nivel 4</td>
                                <td>
                                    <telerik:RadEditor
                                        Height="150px"
                                        Width="100%"
                                        ToolsWidth="310px"
                                        EditModes="Design"
                                        ID="radEditorPuesto4"
                                        runat="server"
                                        ToolbarMode="Default"
                                        ToolsFile="~/Assets/BasicTools.xml">
                                    </telerik:RadEditor>
                                </td>
                               
                            </tr>
                            <tr>
                                <td style="text-align: center;">Nivel 5</td>
                                <td>
                                    <telerik:RadEditor
                                        Height="150px"
                                        Width="100%"
                                        ToolsWidth="310px"
                                        EditModes="Design"
                                        ID="radEditorPuesto5"
                                        runat="server"
                                        ToolbarMode="Default"
                                        ToolsFile="~/Assets/BasicTools.xml">
                                    </telerik:RadEditor>
                                </td>
                            </tr>
                        </tbody>
                    </table>

                </ContentTemplate>
            </telerik:RadPanelItem>
        </Items>
    </telerik:RadPanelBar>
    <div style="height:10px;"></div>
    <div style=" clear: both;">
        <div class="divControlesBoton">
            <telerik:RadButton ID="btnGuardar"
                runat="server"
                Width="100px"
                Text="Guardar"
                AutoPostBack="true"
                OnClick="btnGuardar_Click">
            </telerik:RadButton>
            <telerik:RadButton ID="btnCancelar"
                runat="server"
                Width="100px"
                Text="Cancelar"
                AutoPostBack="true"
                OnClientClicking="cancelarAccion">
            </telerik:RadButton>
        </div>
    </div>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server"></telerik:RadWindowManager>
</asp:Content>
