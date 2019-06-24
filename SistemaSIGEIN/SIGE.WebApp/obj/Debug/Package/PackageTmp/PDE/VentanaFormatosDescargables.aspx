<%@ Page Title="" Language="C#" MasterPageFile="~/PDE/ContextPDE.master" AutoEventWireup="true" CodeBehind="VentanaFormatosDescargables.aspx.cs" Inherits="SIGE.WebApp.PDE.VentanaFormatosDescargables" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">

    <style>
        html {
            overflow: hidden;
        }
    </style>
    <script>
        function OpenVisorArchivosFormatosTramites(pIdArchivo, pNbArchivo) {
            var vTitulo = pNbArchivo;
            var vURL = "VisorDeFormatosTramites.aspx?IdArchivo=" + pIdArchivo + "&NbArchivo=" + pNbArchivo;
            var oWin = window.radopen(vURL, "rwVerArchivos", document.documentElement.clientWidth - 5, document.documentElement.clientHeight - 5);
            oWin.set_title(vTitulo);
        }

        function OpenVisorArchivosFormatos(pIdFormato, pNbArchivo) {
            var selectedItem = $find("<%=rgFormatos.ClientID %>").get_masterTableView().get_selectedItems()[0];

            if (selectedItem != undefined)
                OpenVisorArchivosFormatosTramites(pIdFormato, pNbArchivo);
            else
                radalert("Selecciona un formato.", 400, 150, "Aviso");

        }
    
        function OpenVisorArchivosTramites(pIdTramite, pNbArchivo) {
            var selectedItem = $find("<%=rgTramites.ClientID %>").get_masterTableView().get_selectedItems()[0];

             if (selectedItem != undefined)
                 OpenVisorArchivosFormatosTramites(pIdTramite, pNbArchivo);
             else
                 radalert("Selecciona un trámite.", 400, 150, "Aviso");

         }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div style="height: calc(100% - 60px);">
        <telerik:RadTabStrip ID="rtsFormatosDescargables" runat="server" MultiPageID="mpFormatosTramites">
            <Tabs>
                <telerik:RadTab TabIndex="0" Text="Formatos"></telerik:RadTab>
                <telerik:RadTab TabIndex="1" Text="Trámites"></telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <div style="height: 100%">
            <telerik:RadMultiPage ID="mpFormatosTramites" runat="server">
                <telerik:RadPageView ID="rpvFormatos" runat="server" Width="100%" Height="100%">
                    <telerik:RadGrid runat="server" ID="rgFormatos" EnableHeaderContextMenu="true" AllowSorting="true"
                        OnNeedDataSource="GridFormatos_NeedDataSource" Width="100%" >
                        <ClientSettings>
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <MasterTableView AutoGenerateColumns="false" DataKeyNames="ID_FORMATO_TRAMITE, NB_FORMATO_TRAMITE" EnableColumnsViewState="false" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true" >
                            <Columns>
                                <telerik:GridBoundColumn HeaderText="Clave" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="#F2DCDB" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" UniqueName="NB_FORMATO_TRAMITE" DataField="NB_FORMATO_TRAMITE" ItemStyle-Width="100px" FilterControlWidth="100px"></telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" HeaderStyle-BackColor="#F2DCDB" HeaderStyle-Font-Bold="true">
                                    <HeaderTemplate>Ver formato</HeaderTemplate>
                                    <ItemTemplate>
                                        <div style="padding-left: 50%; cursor: pointer;">
                                            <img id="imgArchivo" src="../Assets/images/pdf.png" onclick="OpenVisorArchivosFormatos(<%# Eval("ID_FORMATO_TRAMITE") %>,'<%# Eval("NB_FORMATO_TRAMITE") %>');" />

                                        </div>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                 <telerik:GridTemplateColumn  HeaderStyle-HorizontalAlign="Center"  AllowFiltering="false" HeaderStyle-BackColor="#F2DCDB" HeaderStyle-Font-Bold="true">
                                            <HeaderTemplate>Enviar por correo</HeaderTemplate>
                                    <ItemTemplate>
                                        <div id="EnviarCorreo" style="padding-left: 50%; cursor:pointer;">
                                               <asp:ImageButton ID="imagenFormato" runat="server" OnClick ="imagenFormato_Click" ImageUrl="../Assets/images/email.png" />
                                        </div>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                                         </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>

                </telerik:RadPageView>
                <telerik:RadPageView ID="rpvTramites" runat="server" Width="100%" Height="100%">
                    <telerik:RadGrid runat="server" ID="rgTramites" EnableHeaderContextMenu="true" AllowSorting="true"
                        OnNeedDataSource="GridTramites_NeedDataSource" Width="100%">
                        <ClientSettings>
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <MasterTableView AutoGenerateColumns="false" DataKeyNames="ID_FORMATO_TRAMITE, NB_FORMATO_TRAMITE" EnableColumnsViewState="false" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                            <Columns>
                                <telerik:GridBoundColumn HeaderText="Clave" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="#F2DCDB" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" UniqueName="NB_FORMATO_TRAMITE" DataField="NB_FORMATO_TRAMITE" ItemStyle-Width="100px" FilterControlWidth="100px"></telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" HeaderStyle-BackColor="#F2DCDB" HeaderStyle-Font-Bold="true">
                                    <HeaderTemplate>Ver trámite</HeaderTemplate>
                                    <ItemTemplate>
                                        <div style="padding-left: 50%; cursor: pointer;">
                                            <img id="imgArchivo" src="../Assets/images/pdf.png" onclick="OpenVisorArchivosTramites(<%# Eval("ID_FORMATO_TRAMITE") %>,'<%# Eval("NB_FORMATO_TRAMITE") %>' )" />

                                        </div>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" HeaderStyle-BackColor="#F2DCDB" HeaderStyle-Font-Bold="true">
                                    <HeaderTemplate>Enviar por correo</HeaderTemplate>
                                    <ItemTemplate>
                                        <div style="padding-left: 50%; cursor: pointer;">
                                            <asp:ImageButton ID="imagenTramite" runat="server" OnClick="imagenTramite_Click" ImageUrl="../Assets/images/email.png" />
                                        </div>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>

                </telerik:RadPageView>
            </telerik:RadMultiPage>
        </div>
        <telerik:RadEditor Height="10" Width="10px" ToolsWidth="0" EditModes="Design" ID="reTramite" runat="server" Visible="false" ToolbarMode="Floating" OnExportContent="reTramite_ExportContent" OnClientLoad="OnClientLoad" ToolsFile="~/Assets/AdvancedTools.xml"></telerik:RadEditor>

    </div>
    <telerik:RadWindowManager ID="rwmEdicionFormatos" runat="server" EnableShadow="true" OnClientClose="returnDataToParentPopup">
        <Windows>
            <telerik:RadWindow
                ID="rwVerArchivos"
                runat="server"
                VisibleStatusbar="false"
                ShowContentDuringLoad="true"
                Behaviors="Close"
                Modal="true"
                ReloadOnShow="false"
                AutoSize="false">
            </telerik:RadWindow>
        </Windows>

    </telerik:RadWindowManager>
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
