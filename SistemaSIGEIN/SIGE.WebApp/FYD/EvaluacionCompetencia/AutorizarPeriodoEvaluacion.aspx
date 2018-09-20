<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="AutorizarPeriodoEvaluacion.aspx.cs" Inherits="SIGE.WebApp.FYD.EvaluacionCompetencia.AutorizarPeriodoEvaluacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">

    <style>
        .RadButton.rbSkinnedButton.uncheckedYes {
            background-color: #eee !important;
        }

            .RadButton.rbSkinnedButton.uncheckedYes > .rbDecorated {
                color: #eee !important;
            }

        .RadButton.rbSkinnedButton.checkedNo {
            background-color: #eee !important;
        }

            .RadButton.rbSkinnedButton.checkedNo > .rbDecorated {
                color: #333 !important;
            }

        .RadButton.rbSkinnedButton.uncheckedNo {
            background-color: #eee !important;
        }

            .RadButton.rbSkinnedButton.uncheckedNo > .rbDecorated {
                color: #eee !important;
            }

        .checkContainer {
            border-radius: 5px;
            border: 1px solid lightgray;
            background: #eee;
        }

        .RadListBox .rlbGroup {
            max-height: 300px;
            overflow: auto;
        }

        fieldset {
            padding-left: 10px;
            padding-right: 10px;
        }

        .AUTOEVALUACION {
            background-color: #CCFFFF !important;
            color: black;
        }

        .SUPERIOR {
            background-color: #CCFF66 !important;
            color: black;
        }

        .SUBORDINADO {
            background-color: #F5C9A6 !important;
            color: black;
        }

        .INTERRELACIONADO {
            background-color: whitesmoke !important;
            color: black;
        }

        .OTRO {
            background-color: #FCE981 !important;
            color: black;
        }

        .errBackground {
            background-color: red !important;
            color: black;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">

    <div style="clear: both; height: 10px;"></div>

    <div class="ctrlBasico">
        <label>Periodo:</label>
    </div>
    <div class="ctrlBasico">
        <telerik:RadTextBox runat="server" ID="txtClavePeriodo" Width="100px"></telerik:RadTextBox>
    </div>

    <div class="ctrlBasico">
        <telerik:RadTextBox runat="server" ID="txtNombrePeriodo" Width="300px"></telerik:RadTextBox>
    </div>

    <div style="clear: both; height: 10px;"></div>

    <div style="height: calc(100% - 150px); padding-bottom: 10px;">

        <telerik:RadGrid ID="grdCuestionarios" runat="server" HeaderStyle-Font-Bold="true" Height="100%" AutoGenerateColumns="false" EnableHeaderContextMenu="true" AllowSorting="true"
            OnNeedDataSource="grdCuestionarios_NeedDataSource"
            OnDetailTableDataBind="grdCuestionarios_DetailTableDataBind"
            OnItemDataBound="grdCuestionarios_ItemDataBound">
            <ClientSettings>
                <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" />
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView DataKeyNames="ID_EVALUADO" ClientDataKeyNames="ID_EVALUADO" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true" HierarchyDefaultExpanded="true" EnableHierarchyExpandAll="true">
                <Columns>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Evaluado" DataField="NB_EVALUADO" UniqueName="NB_EVALUADO"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="350" FilterControlWidth="280" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="250" FilterControlWidth="180" HeaderText="Área/Departamento" DataField="NB_DEPARTAMENTO" UniqueName="NB_DEPARTAMENTO"></telerik:GridBoundColumn>
                    <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="No. de cuestionarios" DataField="NO_CUESTIONARIOS" UniqueName="NO_CUESTIONARIOS" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>--%>
                </Columns>
                <DetailTables>
                    <telerik:GridTableView Name="gtvEvaluadores">
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="Evaluador">
                                <ItemTemplate>
                                    <span style="border: 1px solid gray; border-radius: 5px;" class="<%# Eval("CL_ROL_EVALUADOR") %>" title="<%# Eval("NB_ROL_EVALUADOR") %>">&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;<%# Eval("NB_EVALUADOR") %>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="350" FilterControlWidth="80" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO"></telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="Rol" HeaderStyle-Width="150">
                                <ItemTemplate>
                                    <div style="border: 1px solid gray; padding: 3px; text-align: center; min-width: 50px; border-radius: 5px;" class="<%# Eval("CL_ROL_EVALUADOR") %>"><%# Eval("NB_ROL_EVALUADOR") %></div>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </telerik:GridTableView>
                </DetailTables>
            </MasterTableView>
        </telerik:RadGrid>

    </div>

    <div class="ctrlBasico">
        <div class="divControlIzquierda" style="width: 350px;">
            <label id="lblActivoTipo" name="lblDsTipo" runat="server">Estoy de acuerdo en autorizar el presente documento:</label>
        </div>
        <div class="divControlDerecha">
            <%--            <telerik:RadButton ID="chkActivo" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false">
                <ToggleStates>
                    <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                    <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                </ToggleStates>
            </telerik:RadButton>--%>

            <div class="checkContainer">
                <telerik:RadButton ID="btnAutorizado" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpSuperior" AutoPostBack="false">
                    <ToggleStates>
                        <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                        <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                    </ToggleStates>
                </telerik:RadButton>
                <telerik:RadButton ID="btnRechazado" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpSuperior" AutoPostBack="false">
                    <ToggleStates>
                        <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                        <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                    </ToggleStates>
                </telerik:RadButton>
            </div>

        </div>
    </div>

    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="Label1" name="lblNotas" runat="server">Observaciones:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadEditor Height="40" Width="600px" AutoResizeHeight="false" ToolsWidth="400px" EditModes="Design"
                ID="radObservaciones" runat="server" ToolbarMode="ShowOnFocus" ToolsFile="~/Assets/BasicTools.xml">
            </telerik:RadEditor>
        </div>
    </div>

    <div class="divControlDerecha" style="margin-right: 20px; margin-top: 10px;">
        <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" Width="100" OnClick="btnGuardar_Click"></telerik:RadButton>
    </div>

    <div style="clear: both;" />
    <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true">
    </telerik:RadWindowManager>
    <%--    <div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnMostrarMatriz" runat="server" Text="Matriz de evaluadores" AutoPostBack="false" OnClientClicked="OpenMatrizEvaluadoresWindow"></telerik:RadButton>
        </div>
    </div>--%>
</asp:Content>
