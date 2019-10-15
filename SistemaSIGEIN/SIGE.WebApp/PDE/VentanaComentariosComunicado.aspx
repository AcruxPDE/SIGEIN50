<%@ Page Title="" Language="C#" MasterPageFile="~/PDE/ContextPDE.master" AutoEventWireup="true" CodeBehind="VentanaComentariosComunicado.aspx.cs" Inherits="SIGE.WebApp.PDE.VentanaComentariosComunicado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <style>
            .RadListViewContainer {
                border: 1px solid lightgray;
                margin: 6px;
                padding: 2px;
                border-radius: 5px;
            }

            h4 {
            }
        </style>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function closeWindow() {
                GetRadWindow().close();
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
     <div style="clear: both; height: 10px;"></div>
    <div>
        <telerik:RadTextBox ID="txtComentario"
            TextMode="MultiLine"
            runat="server"
            Width="100%"
            Height="140px">
        </telerik:RadTextBox>
    </div>
    <div style="clear: both; height: 10px;"></div>
    <div class="divControlDerecha">
        <telerik:RadButton ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" AutoPostBack="true">
        </telerik:RadButton>
        </div>
    <div style="clear: both; height: 10px;"></div>
                    <label class="labelTitulo">Comentarios:</label>
     <telerik:RadLabel runat="server" ID="rlMensajePrivado" Text="No se permiten comentarios en este comunicado porque es privado, si desea agregar un comentario puede editar este estatus." Visible="false"></telerik:RadLabel>
                    <telerik:RadListView ID="rlvComentarios" runat="server" DataKeyNames="ID_COMENTARIO_COMUNICADO" OnNeedDataSource="rlvComentarios_NeedDataSource"
                        ClientDataKeyNames="ID_COMENTARIO_COMUNICADO" ItemPlaceholderID="EventosHolder" Visible="true">
                        <LayoutTemplate>
                            <div style="overflow: auto; overflow-y: auto; max-height: 200px;">
                                <asp:Panel ID="EventosHolder" runat="server"></asp:Panel>
                            </div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <div class="RadListViewContainer" >
                                <div style ='background-color: <%# (int) Eval("ID_COMENTARIO_COMUNICADO") % 2 == 0 ? "#E3E3E3" : "#FBB7CE" %>'>
                                    <div style="text-align: left">
                                        <h4 style="font-weight: bold"><%# Eval("NOMBRE") %> <%# Eval("FE_COMENTARIO") %></h4>
                                    </div>
                                    <div>
                                        <%# Eval("DS_COMENTARIO") %>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </telerik:RadListView>
    <telerik:RadWindowManager ID="rnMensaje" runat="server"></telerik:RadWindowManager>
</asp:Content>
