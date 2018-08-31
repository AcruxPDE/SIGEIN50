<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/MenuFYD.master" AutoEventWireup="true" CodeBehind="AutorizaProgramaCapacitacion.aspx.cs" Inherits="SIGE.WebApp.FYD.AutorizaProgramaCapacitacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script id="MyScript" type="text/javascript">
        function closeWindow() {
            GetRadWindow().close();
        }

        function onCloseWindow(oWnd, args) {
            $find("<%=pgridDetalle.ClientID%>").get_masterTableView().rebind();
        }


    </script>

    <style id="MyCss" type="text/css">
        .divRojo {
            background: Red;
        }

        .divAmarrillo {
            background: Yellow;
        }

        .divBlanco {
            background: White;
        }

        .divGris {
            background: Gray;
        }

        .divCeldas {
            border: 1px solid;
            padding: 1px;
            background: Yellow;
            margin: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramDocumentosAutorizar" runat="server" OnAjaxRequest="ramDocumentosAutorizar_AjaxRequest">

        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pgridDetalle" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="pgridDetalle">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pgridDetalle" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>
    <label class="labelTitulo">Autoriza programa de capacitación</label>

    <div style="height: calc(100% - 260px);">

        <table style="width: 100%;">
            <thead>
                <tr>
                    <th style="width: 80%;"></th>
                    <th style="width: 20%;"></th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                            <div style="width: 120px; margin-right: 15px; float: left;">
                                                <label id="lblPeriodo" name="lblPeriodo" runat="server">Período:</label>
                                            </div>
                                            <div class="divControlDerecha">
                                                <telerik:RadTextBox ID="txtPeriodo" runat="server" Width="200px" MaxLength="1000"></telerik:RadTextBox>
                                            </div>
                                        </div>
                                    </td>
                                    <td rowspan="3">

                                        <div class="ctrlBasico">
                                            <div style="width: 120px; float: left;">
                                                <label id="lblNotas" name="lblNotas" runat="server">Notas:</label>
                                            </div>
                                            <div class="divControlDerecha">
                                                <telerik:RadEditor Height="100" Width="500" AutoResizeHeight="false" ToolsWidth="400px" EditModes="Design"
                                                    ID="radEditorNotas" runat="server" ToolbarMode="ShowOnFocus" ToolsFile="~/Assets/BasicTools.xml">
                                                </telerik:RadEditor>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="clear: both;"></div>
                                        <div class="ctrlBasico">
                                            <div style="width: 120px; margin-right: 15px; float: left;">
                                                <label id="lblTipoEvaluacion" name="lblTipoEvaluacion" runat="server">Tipo de evaluación:</label>
                                            </div>
                                            <div class="divControlDerecha">
                                                <telerik:RadTextBox ID="txtTipoEvaluacion" runat="server" Width="200px" MaxLength="1000" Enabled="false"></telerik:RadTextBox>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="clear: both;"></div>
                                        <div class="ctrlBasico">
                                            <div style="width: 120px; margin-right: 15px; float: left;">
                                                <label id="lblDepartamento" name="lblDepartamento" runat="server">Departamento:</label>
                                            </div>
                                            <div class="divControlDerecha">
                                                <telerik:RadTextBox ID="txtNbDepartamento" runat="server" Width="200px" MaxLength="1000" Enabled="false"></telerik:RadTextBox>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                    <td>

                        <div class="divControlDerecha">
                            <table id="table1" style="border: 1px solid; background: #D8D8D8;">
                                <tbody>
                                    <tr>
                                        <td colspan="3" align="center" style="font-weight: bold;">Prioridades de Capacitación
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">Alta:</td>
                                        <td>&nbsp;</td>
                                        <td style="background: Red; width: 80px;"></td>
                                    </tr>
                                    <tr>
                                        <td align="right">Intermedia:</td>
                                        <td>&nbsp;</td>
                                        <td style="background: Yellow; width: 80px;"></td>
                                    </tr>
                                    <tr>
                                        <td align="right">No necesaria:</td>
                                        <td>&nbsp;</td>
                                        <td style="background: White; width: 80px;"></td>
                                    </tr>
                                    <tr>
                                        <td align="right">No Aplica:</td>
                                        <td>&nbsp;</td>
                                        <td style="background: Gray; width: 80px;"></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>

                    </td>
                </tr>
            </tbody>
        </table>



        <telerik:RadPivotGrid
            ID="pgridDetalle"
            runat="server"
            OnNeedDataSource="pgridDetalle_NeedDataSource"
            Height="100%"
            Width="100%"
            RowTableLayout="Tabular"
            ShowFilterHeaderZone="false"
            EmptyValue="-1"
            OnCellDataBound="pgridDetalle_CellDataBound"
            OnItemCommand="pgridDetalle_ItemCommand"
            ShowColumnHeaderZone="true" AllowFiltering="false"
             >

            <TotalsSettings ColumnGrandTotalsPosition="None" RowsSubTotalsPosition="None" GrandTotalsVisibility="None" ColumnsSubTotalsPosition="None" />
            <ClientSettings>
                <Scrolling AllowVerticalScroll="true" />
            </ClientSettings>

            <Fields>
                <telerik:PivotGridColumnField DataField="NB_EVALUADO" Caption="Evaluado" CellStyle-CssClass="Expandido"></telerik:PivotGridColumnField>
                <telerik:PivotGridColumnField DataField="NB_PUESTO" Caption="Puesto" CellStyle-CssClass="Expandido"></telerik:PivotGridColumnField>
                <telerik:PivotGridColumnField DataField="NB_DEPARTAMENTO" IsHidden="true"></telerik:PivotGridColumnField>

                <telerik:PivotGridRowField DataField="NB_CLASIFICACION_COMPETENCIA" Caption="Clasificacion" CellStyle-BackColor="White"></telerik:PivotGridRowField>
                <telerik:PivotGridRowField DataField="NB_COMPETENCIA" Caption="Competencia" CellStyle-BackColor="White"></telerik:PivotGridRowField>
                <telerik:PivotGridRowField DataField="CL_TIPO_COMPETENCIA" Caption="Categoría" CellStyle-BackColor="White"></telerik:PivotGridRowField>
                <telerik:PivotGridRowField DataField="CL_COLOR" IsHidden="true" Caption="Color"></telerik:PivotGridRowField>

                <telerik:PivotGridAggregateField DataField="PR_RESULTADO" Aggregate="Average" Caption="Resultado" >
                    
                </telerik:PivotGridAggregateField>
            </Fields>
        </telerik:RadPivotGrid>
        <div style="clear: both; height: 10px;" />
    </div>

    <div class="ctrlBasico">
        <div class="divControlIzquierda" style="width: 350px;">
            <label id="lblActivoTipo" name="lblDsTipo" runat="server">Estoy de acuerdo en autorizar el presente documento:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadButton ID="chkActivo" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false">
                <ToggleStates>
                    <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                    <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                </ToggleStates>
            </telerik:RadButton>
        </div>
    </div>

    <div class="ctrlBasico">
        <div style="width: 100px; float: left;">
            <label id="Label1" name="lblNotas" runat="server">Observaciones:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadEditor Height="40" Width="600px" AutoResizeHeight="false" ToolsWidth="400px" EditModes="Design"
                ID="radObservaciones" runat="server" ToolbarMode="ShowOnFocus" ToolsFile="~/Assets/BasicTools.xml">
            </telerik:RadEditor>
            <asp:RequiredFieldValidator Display="Dynamic" CssClass="validacion" ID="RequiredFieldValidator1" runat="server" Font-Names="Arial" Font-Size="Small" ControlToValidate="radObservaciones" ErrorMessage="Campo Obligatorio"></asp:RequiredFieldValidator>

        </div>
    </div>

    <div class="divControlDerecha" style="margin-right: 20px; margin-top: 10px;">
        <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" Width="100" OnClick="btnGuardar_Click"></telerik:RadButton>
    </div>

    <div style="clear: both;" />
    <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true">
    </telerik:RadWindowManager>
</asp:Content>
