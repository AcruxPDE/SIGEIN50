<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ToolTipEmpleado.ascx.cs" Inherits="SIGE.WebApp.FYD.ToolTipEmpleado" %>

<telerik:RadAjaxLoadingPanel ID="ralpTooltip" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramTooltip" runat="server" DefaultLoadingPanelID="ralpTooltip">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgDatos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgDatos" UpdatePanelHeight="100%" />                    
                </UpdatedControls>
            </telerik:AjaxSetting>
         </AjaxSettings>
    </telerik:RadAjaxManager>

<telerik:RadGrid runat="server" ID="rgDatos" AutoGenerateColumns="false" OnNeedDataSource="rgDatos_NeedDataSource">
    
    <MasterTableView ShowHeader="False">
        <Columns>
            <telerik:GridBoundColumn UniqueName="ID_ELEMENTO" DataField="NB_COMPETENCIA"></telerik:GridBoundColumn>
            <telerik:GridBoundColumn UniqueName="PR_COMPATIBILIDAD" DataField="PR_COMPATIBILIDAD"></telerik:GridBoundColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid>