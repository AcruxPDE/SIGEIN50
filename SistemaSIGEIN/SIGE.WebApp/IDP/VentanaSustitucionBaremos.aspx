<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="VentanaSustitucionBaremos.aspx.cs" Inherits="SIGE.WebApp.IDP.SustitucionBaremos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script id="MyScript" type="text/javascript">

        function GetRadWindow() {
            var oWindow = null;
            if
                (window.radWindow) oWindow = window.radWindow;
            else
                if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function closeWindow() {
            GetRadWindow().close();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnReinicio">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="dgvBaremos" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="dgvBaremos" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div style="height: calc(100% - 70px);">
        <div style="clear:both; height:10px;"></div>   
        <telerik:RadGrid
            ID="dgvBaremos" runat="server" ShowStatusBar="False"
            AutoGenerateColumns="False" AllowPaging="false"
            AllowSorting="True" Width="100%" Height="100%"
            HeaderStyle-Font-Bold="true"
            OnNeedDataSource="dgvBaremos_NeedDataSource">
            <ClientSettings>
                <Scrolling UseStaticHeaders="true" AllowScroll="true" />
            </ClientSettings>
            <MasterTableView AllowMultiColumnSorting="true"
                ShowHeadersWhenNoRecords="true"
                Name="Sustitución de Baremos" DataKeyNames="ID_VARIABLE">
                <CommandItemSettings ShowAddNewRecordButton="false" />
                <Columns>
                    <telerik:GridBoundColumn DataField="CL_VARIABLE" HeaderText="Variable" UniqueName="CL_VARIABLE" ReadOnly="true" HeaderStyle-Width="300px"></telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn DataField="NO_VALOR" HeaderText="Nivel" UniqueName="NO_VALOR">
                        <ItemTemplate>
                            <telerik:RadSlider
                                ID="rsNivel1"
                                AutoPostBack="false"
                                runat="server"
                                AnimationDuration="400"
                                Value='<%# int.Parse(Eval("NO_VALOR").ToString()) +  1%>'
                                CssClass="fItemsSlider"
                                Height="70px" ItemType="item" ThumbsInteractionMode="Free" Width="500px">
                                <Items>
                                    <telerik:RadSliderItem Text="N/A" Value="0" />
                                    <telerik:RadSliderItem Text="0" Value="1" />
                                    <telerik:RadSliderItem Text="1" Value="2" />
                                    <telerik:RadSliderItem Text="2" Value="3" />
                                    <telerik:RadSliderItem Text="3" Value="4" />
                                </Items>
                            </telerik:RadSlider>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    <div style="clear: both; height: 10px;"></div>
    <div class="divControlesBoton">
        <telerik:RadButton ID="btnReinicio" runat="server" Width="100px" Text="Reinicializar" OnClick="btnReinicio_Click" AutoPostBack="true"></telerik:RadButton>
        <telerik:RadButton ID="btnGuardar" runat="server" Width="100px" Text="Guardar" OnClick="btnGuardar_Click" AutoPostBack="true"></telerik:RadButton>
        <telerik:RadButton ID="btnCancelar" runat="server" Width="100px" Text="Cancelar" AutoPostBack="false" OnClientClicking="closeWindow"></telerik:RadButton>
    </div>
        </div>
    <telerik:RadWindowManager ID="rwmAlertas" runat="server">
    </telerik:RadWindowManager>
</asp:Content>
