<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="VentanaComentariosEntrevista.aspx.cs" Inherits="SIGE.WebApp.IDP.VentanaComentariosEntrevista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script>
        function onCloseWindow() {
            GetRadWindow().close();
        }</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div style="height: calc(100% - 50px);">
        <div style="height: 10px; clear: both;"></div>
          <div class="ctrlBasico">

        <table class="ctrlTableForm">
            <tr>
                <td class="ctrlTableDataContext">
                    <label>Nombre del candidato: </label>
                </td>
                <td class="ctrlTableDataBorderContext">
                    <span runat="server" id="txtCandidato" style="width: 300px;"></span>
                </td>
                <td class="ctrlTableDataContext">
                    <label>Clave de la requisición: </label>
                </td>
                <td class="ctrlTableDataBorderContext">
                    <span runat="server" id="txtClaveRequisicion" style="width: 300px;"></span>
                </td>
               
            </tr>
        </table>

    </div>
                <div style="height: 5px; clear: both;"></div>
          <div style="height: calc(100% - 70px);">
        <telerik:RadGrid runat="server" ID="rgComentariosEntrevistas" AutoGenerateColumns="false"
            Width="100%" Height="100%" OnNeedDataSource="rgComentariosEntrevistas_NeedDataSource" ShowHeader="true"
            AllowFilteringByColumn="true">
            <ClientSettings>
                <Scrolling UseStaticHeaders="false" AllowScroll="true" />
                <Selecting AllowRowSelect="false" />
            </ClientSettings>
             <GroupingSettings CaseSensitive="false" />
            <MasterTableView ClientDataKeyNames="ID_ENTREVISTA" DataKeyNames="ID_ENTREVISTA" ShowHeadersWhenNoRecords="true">
                <Columns>

                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="30%" CurrentFilterFunction="Contains" FilterControlWidth="70%" DataField="NB_ENTREVISTADOR" UniqueName="NB_ENTREVISTADOR" HeaderText="Entrevistador"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="70%" CurrentFilterFunction="Contains" FilterControlWidth="70%" DataField="DS_OBSERVACIONES" UniqueName="DS_OBSERVACIONES" HeaderText="Comentarios"></telerik:GridBoundColumn>

                </Columns>
            </MasterTableView>
        </telerik:RadGrid>

        <div style="height: 10px; clear: both;"></div>


        <div class="divControlDerecha">
            <telerik:RadButton runat="server" ID="btnCancelar" Text="Salir" OnClientClicking="onCloseWindow"></telerik:RadButton>
        </div>
</div>
    </div>
</asp:Content>
