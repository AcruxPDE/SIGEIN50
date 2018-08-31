<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeBehind="Funciones.aspx.cs" Inherits="SIGE.WebApp.Administracion.Funciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdFunciones">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdFunciones" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <label class="labelTitulo">Funciones</label>
    <div style="height:calc(100% - 70px)">
        <telerik:RadTreeList ID="grdFunciones" runat="server" DataKeyNames="ID_FUNCION" ParentDataKeyNames="ID_FUNCION_PADRE" AutoGenerateColumns="false" OnNeedDataSource="grdFunciones_NeedDataSource"
            AllowMultiItemSelection="true" AllowRecursiveSelection="false" Height="100%">
            <ClientSettings>
                <Scrolling UseStaticHeaders="true" AllowScroll="true"/>
                <Selecting AllowItemSelection="true" />
            </ClientSettings>
            <Columns>
                <telerik:TreeListBoundColumn DataField="NB_FUNCION" HeaderText="Nombre" UniqueName="NB_FUNCION"></telerik:TreeListBoundColumn>
                <telerik:TreeListBoundColumn DataField="CL_FUNCION" HeaderText="Clave" UniqueName="CL_FUNCION"></telerik:TreeListBoundColumn>
                <telerik:TreeListBoundColumn DataField="CL_TIPO_FUNCION" HeaderText="Tipo" UniqueName="CL_TIPO_FUNCION"></telerik:TreeListBoundColumn>
            </Columns>
        </telerik:RadTreeList>
    </div>
</asp:Content>
