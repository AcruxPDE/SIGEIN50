<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TooltipPlaneacionIncrementos.ascx.cs" Inherits="SIGE.WebApp.MPC.TooltipPlaneacionIncrementos" %>

<telerik:RadAjaxLoadingPanel ID="ralpTooltip" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramTooltip" runat="server" DefaultLoadingPanelID="ralpTooltip">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgSecuancias">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgSecuancias" UpdatePanelHeight="100%" />                    
                </UpdatedControls>
            </telerik:AjaxSetting>
         </AjaxSettings>
    </telerik:RadAjaxManager>
 
<telerik:RadGrid runat="server" ID="rgSecuancias" AutoGenerateColumns="false" >
    <MasterTableView ShowHeader="true">
        <Columns>
            <telerik:GridTemplateColumn AutoPostBackOnFilter="true"  HeaderStyle-Width="90" FilterControlWidth="10" HeaderText="Categoría" DataField="NO_CATEGORIA" UniqueName="NO_CATEGORIA" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                <%#((char)(int.Parse(Eval("NO_CATEGORIA").ToString()) + 64))%>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridBoundColumn UniqueName="CANTIDAD" DataField="CANTIDAD" DataFormatString="{0:C}"></telerik:GridBoundColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid>
   