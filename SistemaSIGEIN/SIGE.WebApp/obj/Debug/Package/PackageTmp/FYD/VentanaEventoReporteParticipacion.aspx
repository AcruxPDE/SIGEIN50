<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="VentanaEventoReporteParticipacion.aspx.cs" Inherits="SIGE.WebApp.FYD.VentanaEventoReporteParticipacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">

    <div style="clear: both; height: 5px;"></div>

   <%-- <div class="ctrlBasico">
        <label>Evento:</label>
        <telerik:RadTextBox runat="server" ID="txtEvento" Width="300px" Enabled="false"></telerik:RadTextBox>
    </div>

    <div class="ctrlBasico">
        <label>Curso:</label>
        <telerik:RadTextBox runat="server" ID="txtCurso" Width="300px" Enabled="false"></telerik:RadTextBox>
    </div>--%>
    <div class="ctrlBasico">
                    <table class="ctrlTableForm">
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Evento:</label></td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="txtEvento" runat="server" width="170" maxlength="1000" enabled="false"></div>
                            </td>
                              <td class="ctrlTableDataContext">
                                <label>Curso:</label></td>
                            <td class="ctrlTableDataBorderContext">
                                <div id="txtCurso" runat="server" width="170" maxlength="1000" enabled="false"></div>
                            </td>
                        </tr>
                    </table>
                </div>


    <div style="clear: both; height: 5px;"></div>

    <div style="height: calc(100% - 70px);">
        <telerik:RadGrid runat="server" ID="rgReporte" HeaderStyle-Font-Bold="true" AutoGenerateColumns="false" OnNeedDataSource="rgReporte_NeedDataSource" ShowFooter="true" EnableHeaderContextMenu="true" Height="100%" Width="100%" AllowFilteringByColumn="true" OnItemDataBound="rgReporte_ItemDataBound" >
            <ClientSettings>
                <Scrolling UseStaticHeaders="true" AllowScroll="true" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID_PARTICIPANTE">
                <Columns>
                    <telerik:GridBoundColumn UniqueName="CL_PARTICIPANTE" DataField="CL_PARTICIPANTE" HeaderText="Clave" CurrentFilterFunction="Contains" FilterControlWidth="30">
                        <HeaderStyle Width="100" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="NB_PARTICIPANTE" DataField="NB_PARTICIPANTE" HeaderText="Nombre" CurrentFilterFunction="Contains" FilterControlWidth="130"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="NB_PUESTO" DataField="NB_PUESTO" HeaderText="Puesto" CurrentFilterFunction="Contains" FilterControlWidth="130">
                        <HeaderStyle Width="200" />
                    </telerik:GridBoundColumn>
<%--                    <telerik:GridBoundColumn UniqueName="NB_DEPARTAMENTO" DataField="NB_DEPARTAMENTO" HeaderText="Departamento" CurrentFilterFunction="Contains" FilterControlWidth="130">
                        <HeaderStyle Width="200" />
                    </telerik:GridBoundColumn>--%>
                    <telerik:GridBoundColumn UniqueName="PR_EVALUACION_PARTICIPANTE" DataField="PR_EVALUACION_PARTICIPANTE" HeaderText="Promedio" DataFormatString="{0:N2}" Aggregate="Avg" FooterAggregateFormatString="Promedio grupal: {0:N2}" CurrentFilterFunction="Contains" FilterControlWidth="130">
                        <HeaderStyle Width="200" />
                        <ItemStyle HorizontalAlign="Right" />
                        <FooterStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn UniqueName="LST_COMPETENCIAS" HeaderText="Competencias" AllowFiltering="false">
                        <ItemTemplate>
                            <telerik:RadGrid ID="grdCompetencias" HeaderStyle-Font-Bold="true" runat="server" AutoGenerateColumns="false">
                                <MasterTableView>
                                    <Columns>
                                        <telerik:GridBoundColumn HeaderText="Clave" DataField="CL_COMPETENCIA" HeaderStyle-Width="100" UniqueName="CL_COMPETENCIA"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Nombre" DataField="NB_COMPETENCIA" UniqueName="NB_COMPETENCIA"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Nivel de competencia" DataField="PR_EVALUACION_COMPETENCIA" UniqueName="PR_EVALUACION_COMPETENCIA" DataFormatString="{0:N2}">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Width="100px" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>

</asp:Content>
