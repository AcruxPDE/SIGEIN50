<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="MatrizCuestionarios.aspx.cs" Inherits="SIGE.WebApp.FYD.MatrizCuestionarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">

    <style>
        .matriz {
            height: 100%;
            width: 19%;
            min-width: 250px;
            float: left;
        }

        fieldset {
            height: calc(100% - 20px);
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
    </style>

    <script type="text/javascript">
        function OpenAgregarSuperiorioWindow() {
            OpenNuevoCuestionarioWindow("SUPERIOR");
        }

        function OpenAgregarSubordinadoioWindow() {
            OpenNuevoCuestionarioWindow("SUBORDINADO");
        }

        function OpenAgregarInterWindow() {
            OpenNuevoCuestionarioWindow("INTERRELACIONADO");
        }

        function OpenAgregarOtroWindow() {
            OpenNuevoCuestionarioWindow("OTRO");
        }

        function ObtenerDataKeyNameValue(pGrid) {
            var masterTable = pGrid.get_masterTableView();
            var selectedItems = masterTable.get_selectedItems();
            if (selectedItems.length > 0)
                return selectedItems[0].getDataKeyValue("ID_EVALUADO_EVALUADOR");
            else
                return null;
        }


        function OpenNuevoCuestionarioWindow(pClrol) {
            var windowProperties = {
                //width: 550,
                //height: 280
                width: 600,
                height: 650
            };

            var cRol = "";
            if (pClrol != undefined)
                pClrol = "&ClRol=" + pClrol;

            OpenSelectionWindow("AgregarCuestionario.aspx?PeriodoId=<%= vIdPeriodo %>&EvaluadoId=<%= vIdEvaluado %>&AccionCerrarCl=REBIND" + pClrol, "winAgregarCuestionario", "Agregar/Editar cuestionario", windowProperties);
        }


        function OpenEditarCuestionarioWindow() {
            var vIdEvaluadoEvaluador = null;

            if (vIdEvaluadoEvaluador == null)
                vIdEvaluadoEvaluador = ObtenerDataKeyNameValue($find('<%= grdSupervisor.ClientID %>'));
            if (vIdEvaluadoEvaluador == null)
                vIdEvaluadoEvaluador = ObtenerDataKeyNameValue($find('<%= grdSubordinado.ClientID %>'));
            if (vIdEvaluadoEvaluador == null)
                vIdEvaluadoEvaluador = ObtenerDataKeyNameValue($find('<%= grdInterrelacionado.ClientID %>'));
            if (vIdEvaluadoEvaluador == null)
                vIdEvaluadoEvaluador = ObtenerDataKeyNameValue($find('<%= grdOtrosEvaluadores.ClientID %>'));

            if (vIdEvaluadoEvaluador == null)
                radalert("Selecciona a un evaluador.", 400, 150);
            else
                OpenCuestionarioWindow(vIdEvaluadoEvaluador);
        }

        function OpenCuestionarioWindow(pIdEvaluadoEvaluador) {
            var windowProperties = {
                //width: 550,
                //height: 280
                width: 500,
                height: 650
            };

            var vIdEvaluadoEvaluador = "";
            if (pIdEvaluadoEvaluador != undefined)
                vIdEvaluadoEvaluador = "&EvaluadoEvaluadorId=" + pIdEvaluadoEvaluador;

            OpenSelectionWindow("AgregarCuestionario.aspx?PeriodoId=<%= vIdPeriodo %>&EvaluadoId=<%= vIdEvaluado %>&AccionCerrarCl=REBIND" + vIdEvaluadoEvaluador, "winAgregarCuestionario", "Agregar/Editar cuestionario", windowProperties);
        }



        function OpenSelectionWindow(pURL, pIdWindow, pTitle, pWindowProperties) {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 20,
                height: browserWnd.innerHeight - 20
            };

            if (pWindowProperties)
                windowProperties = pWindowProperties;

            openChildDialog(pURL, pIdWindow, pTitle, windowProperties)
        }

        function SelectRow(sender, args) {
            var grdSuperior = $find('<%= grdSupervisor.ClientID %>');
            var grdSubordinado = $find('<%= grdSubordinado.ClientID %>');
            var grdInterrelacionado = $find('<%= grdInterrelacionado.ClientID %>');
            var grdOtro = $find('<%= grdOtrosEvaluadores.ClientID %>');

            if (sender != grdSuperior)
                grdSuperior.clearSelectedItems();
            if (sender != grdSubordinado)
                grdSubordinado.clearSelectedItems();
            if (sender != grdInterrelacionado)
                grdInterrelacionado.clearSelectedItems();
            if (sender != grdOtro)
                grdOtro.clearSelectedItems();

        }

        function useDataFromChild(pDato) {
            if (pDato != null) {
                switch (pDato[0].clTipoCatalogo) {
                    case "REBIND":
                        $find('<%= ramMatriz.ClientID %>').ajaxRequest(pDato.clAccionCerrar);
                        break;
                }
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">

    <div style="height: calc(100% - 45px); padding-bottom: 10px;">
        <telerik:RadSplitter ID="rsCuestionarios" runat="server" Width="100%" Height="100%" BorderSize="0">

        <telerik:RadPane ID="rpCuestionarios" runat="server">

        <telerik:RadAjaxLoadingPanel ID="ralpMatriz" runat="server"></telerik:RadAjaxLoadingPanel>
        <telerik:RadAjaxManager ID="ramMatriz" runat="server" DefaultLoadingPanelID="ralpMatriz" OnAjaxRequest="ramMatriz_AjaxRequest">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="ramMatriz">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="grdAutoevaluador" UpdatePanelHeight="100%" />
                        <telerik:AjaxUpdatedControl ControlID="grdSupervisor" UpdatePanelHeight="100%" />
                        <telerik:AjaxUpdatedControl ControlID="grdSubordinado" UpdatePanelHeight="100%" />
                        <telerik:AjaxUpdatedControl ControlID="grdInterrelacionado" UpdatePanelHeight="100%" />
                        <telerik:AjaxUpdatedControl ControlID="grdOtrosEvaluadores" UpdatePanelHeight="100%" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="grdAutoevaluador">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="grdAutoevaluador" UpdatePanelHeight="100%" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="grdSupervisor">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="grdSupervisor" UpdatePanelHeight="100%" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="grdSubordinado">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="grdSubordinado" UpdatePanelHeight="100%" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="grdInterrelacionado">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="grdInterrelacionado" UpdatePanelHeight="100%" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="grdOtrosEvaluadores">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="grdOtrosEvaluadores" UpdatePanelHeight="100%" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>

        <div style="clear: both; height: 5px;"></div>

        <div class="ctrlBasico">
            <div class="divControlIzquierda">
                <label>Evaluado:</label>
            </div>

            <div class="divControlDerecha">
                <telerik:RadTextBox runat="server" ID="txtEvaluado" Width="400px" Enabled="false"></telerik:RadTextBox>
            </div>
        </div>
        
        <div style="clear: both; height: 5px;"></div>
        <div style="height: calc(100% - 110px);">

            <div class="matriz">
                <telerik:RadGrid ID="grdAutoevaluador" runat="server" HeaderStyle-Font-Bold="true" Width="100%" Height="100%" OnNeedDataSource="grdAutoevaluador_NeedDataSource" OnDeleteCommand="grdAutoevaluador_DeleteCommand">
                    <ClientSettings>
                        <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                    </ClientSettings>
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView DataKeyNames="ID_CUESTIONARIO,ID_EVALUADO_EVALUADOR" ClientDataKeyNames="ID_CUESTIONARIO,ID_EVALUADO_EVALUADOR" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false">
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="Autoevaluación" ItemStyle-CssClass="AUTOEVALUACION">
                                <ItemTemplate>
                                    <span title="<%# ObtenerToolTipEvaluador((string)Eval("CL_EVALUADOR"), (string)Eval("CL_PUESTO")) %>"><%# Eval("NB_EVALUADOR") %><br />
                                        <span style="font-weight: bold;"><%# Eval("NB_PUESTO") %></span></span>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridButtonColumn CommandName="Delete" Text="Eliminar" UniqueName="DeleteColumn" ButtonType="ImageButton" HeaderStyle-Width="30" ConfirmTextFields="NB_EVALUADOR" ConfirmTextFormatString="¿Desea eliminar el cuestionario del evaluador {0}?" ConfirmDialogWidth="400" ConfirmDialogHeight="150" ConfirmDialogType="RadWindow" />
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
            <div class="matriz">
                <telerik:RadGrid ID="grdSupervisor" runat="server" HeaderStyle-Font-Bold="true" Width="100%" Height="100%" OnNeedDataSource="grdSupervisor_NeedDataSource" OnDeleteCommand="grdSupervisor_DeleteCommand">
                    <ClientSettings ClientEvents-OnRowClick="SelectRow">
                        <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView DataKeyNames="ID_CUESTIONARIO,ID_EVALUADO_EVALUADOR" ClientDataKeyNames="ID_CUESTIONARIO,ID_EVALUADO_EVALUADOR" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false">
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="Superior" ItemStyle-CssClass="SUPERIOR">
                                <ItemTemplate>
                                    <span title="<%# ObtenerToolTipEvaluador((string)Eval("CL_EVALUADOR"), (string)Eval("CL_PUESTO")) %>"><%# Eval("NB_EVALUADOR") %><br />
                                        <span style="font-weight: bold;"><%# Eval("NB_PUESTO") %></span></span>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridButtonColumn CommandName="Delete" Text="Eliminar" UniqueName="DeleteColumn" ButtonType="ImageButton" HeaderStyle-Width="30" ConfirmTextFields="NB_EVALUADOR" ConfirmTextFormatString="¿Desea eliminar el cuestionario del evaluador {0}?" ConfirmDialogWidth="400" ConfirmDialogHeight="150" ConfirmDialogType="RadWindow" />
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
            <div class="matriz">
                <telerik:RadGrid ID="grdSubordinado" runat="server" HeaderStyle-Font-Bold="true" Width="100%" Height="100%" OnNeedDataSource="grdSubordinado_NeedDataSource" OnDeleteCommand="grdSubordinado_DeleteCommand">
                    <ClientSettings ClientEvents-OnRowClick="SelectRow">
                        <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView DataKeyNames="ID_CUESTIONARIO,ID_EVALUADO_EVALUADOR" ClientDataKeyNames="ID_CUESTIONARIO,ID_EVALUADO_EVALUADOR" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false">
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="Subordinado" ItemStyle-CssClass="SUBORDINADO">
                                <ItemTemplate>
                                    <span title="<%# ObtenerToolTipEvaluador((string)Eval("CL_EVALUADOR"), (string)Eval("CL_PUESTO")) %>"><%# Eval("NB_EVALUADOR") %><br />
                                        <span style="font-weight: bold;"><%# Eval("NB_PUESTO") %></span></span>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridButtonColumn CommandName="Delete" Text="Eliminar" UniqueName="DeleteColumn" ButtonType="ImageButton" HeaderStyle-Width="30" ConfirmTextFields="NB_EVALUADOR" ConfirmTextFormatString="¿Desea eliminar el cuestionario del evaluador {0}?" ConfirmDialogWidth="400" ConfirmDialogHeight="150" ConfirmDialogType="RadWindow" />
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
            <div class="matriz">
                <telerik:RadGrid ID="grdInterrelacionado" runat="server" HeaderStyle-Font-Bold="true" Width="100%" Height="100%" OnNeedDataSource="grdInterrelacionado_NeedDataSource" OnDeleteCommand="grdInterrelacionado_DeleteCommand">
                    <ClientSettings ClientEvents-OnRowClick="SelectRow">
                        <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView DataKeyNames="ID_CUESTIONARIO,ID_EVALUADO_EVALUADOR" ClientDataKeyNames="ID_CUESTIONARIO,ID_EVALUADO_EVALUADOR" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false">
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="Interrelacionado" ItemStyle-CssClass="INTERRELACIONADO">
                                <ItemTemplate>
                                    <span title="<%# ObtenerToolTipEvaluador((string)Eval("CL_EVALUADOR"), (string)Eval("CL_PUESTO")) %>"><%# Eval("NB_EVALUADOR") %><br />
                                        <span style="font-weight: bold;"><%# Eval("NB_PUESTO") %></span></span>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridButtonColumn CommandName="Delete" Text="Eliminar" UniqueName="DeleteColumn" ButtonType="ImageButton" HeaderStyle-Width="30" ConfirmTextFields="NB_EVALUADOR" ConfirmTextFormatString="¿Desea eliminar el cuestionario del evaluador {0}?" ConfirmDialogWidth="400" ConfirmDialogHeight="150" ConfirmDialogType="RadWindow" />
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
            <div class="matriz">
                <telerik:RadGrid ID="grdOtrosEvaluadores" runat="server" HeaderStyle-Font-Bold="true" Width="100%" Height="100%" OnNeedDataSource="grdOtrosEvaluadores_NeedDataSource" OnDeleteCommand="grdOtrosEvaluadores_DeleteCommand">
                    <ClientSettings ClientEvents-OnRowClick="SelectRow">
                        <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView DataKeyNames="ID_CUESTIONARIO,ID_EVALUADO_EVALUADOR" ClientDataKeyNames="ID_CUESTIONARIO,ID_EVALUADO_EVALUADOR" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false">
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="Otro" ItemStyle-CssClass="OTRO">
                                <ItemTemplate>
                                    <span title="<%# ObtenerToolTipEvaluador((string)Eval("CL_EVALUADOR"), (string)Eval("CL_PUESTO")) %>"><%# Eval("NB_EVALUADOR") %><br />
                                        <span style="font-weight: bold;"><%# Eval("NB_PUESTO") %></span></span>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridButtonColumn CommandName="Delete" Text="Eliminar" UniqueName="DeleteColumn" ButtonType="ImageButton" HeaderStyle-Width="30" ConfirmTextFields="NB_EVALUADOR" ConfirmTextFormatString="¿Desea eliminar el cuestionario del evaluador {0}?" ConfirmDialogWidth="400" ConfirmDialogHeight="150" ConfirmDialogType="RadWindow" />
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>

        </div>
        <div style="height: 10px;">
        </div>
    <%--    <div class="ctrlBasico">
            <telerik:RadButton ID="btnAgregarCuestionario" runat="server" Text="Agregar" AutoPostBack="false" OnClientClicked="OpenAgregarCuestionarioWindow"></telerik:RadButton>
        </div>--%>

        <div class="ctrlBasico">
            <telerik:RadButton ID="btnAutoevaluacion" runat="server" Text="Agregar Autoevaluación" AutoPostBack="true" OnClick="btnAutoevaluacion_Click"></telerik:RadButton>
        </div>
        
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnSuperior" runat="server" Text="Agregar superior" AutoPostBack="false" OnClientClicked="OpenAgregarSuperiorioWindow"></telerik:RadButton>
        </div>

        <div class="ctrlBasico">
            <telerik:RadButton ID="btnSubordinado" runat="server" Text="Agregar subordinado" AutoPostBack="false" OnClientClicked="OpenAgregarSubordinadoioWindow"></telerik:RadButton>
        </div>

        <div class="ctrlBasico">
            <telerik:RadButton ID="btnInterrelacionado" runat="server" Text="Agregar interrelacionado" AutoPostBack="false" OnClientClicked="OpenAgregarInterWindow"></telerik:RadButton>
        </div>

        <div class="ctrlBasico">
            <telerik:RadButton ID="btnOrtos" runat="server" Text="Agregar otros" AutoPostBack="false" OnClientClicked="OpenAgregarOtroWindow"></telerik:RadButton>
        </div>

        <div class="ctrlBasico">
            <telerik:RadButton ID="btnEditarCuestionario" runat="server" Text="Editar" AutoPostBack="false" OnClientClicked="OpenEditarCuestionarioWindow"></telerik:RadButton>
        </div>
        <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>

        </telerik:RadPane>

                            <telerik:RadPane ID="rpAyudaCuestionario" runat="server" Scrolling="None" Width="22px">
                                <%--<telerik:RadSlidingZone ID="rszAyudaCuestionario" runat="server" SlideDirection="Left" Width="22px" DockedPaneId="rspAyudaCuestionario">--%>
                                <telerik:RadSlidingZone ID="rszAyudaCuestionario" runat="server" SlideDirection="Left" Width="22px" >
                                    <telerik:RadSlidingPane ID="rspAyudaCuestionario" RenderMode="Mobile" runat="server" Title="Ayuda" Width="400px">
                                        <div style="text-align: justify; padding: 10px;">
                                            <p>Utiliza esta página para definir la programación de cuestionarios.</p>
                                            <p>Esta página te permite definir a los evaluadores que recibirán cuestionarios para calificar las competencias de las personas a evaluar en el proceso de Deteccion de Necesidades de Capacitación. Para facilitarte esta decisión te proponemos la siguiente matriz, revisa detenidamente esta propuesta y utiliza, en su caso, el icono para quitar aquellas personas que no deseas que reciban un cuestionario de evaluación. Utiliza el botón para agregar más personas si lo deseas. Una vez que hayas concluido la revisión y selección haz clic en el botón Crear cuestionarios y la matriz se ajustará a tus requerimientos.</p>
                                        </div>
                                    </telerik:RadSlidingPane>
                                </telerik:RadSlidingZone>
                            </telerik:RadPane>
        </telerik:RadSplitter>

    </div>
</asp:Content>
