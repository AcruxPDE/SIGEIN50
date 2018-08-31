<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="VentanaReporteCursosRealizadosDetalle.aspx.cs" Inherits="SIGE.WebApp.FYD.VentanaReporteCursosRealizadosDetalle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadTabStrip runat="server" ID="rtsdetalle" Width="100%" SelectedIndex="0" MultiPageID="rmpDetalle">
        <Tabs>
            <telerik:RadTab runat="server" Text="Evento"></telerik:RadTab>
            <telerik:RadTab runat="server" Text="Participantes"></telerik:RadTab>
            <telerik:RadTab runat="server" Text="Competencias"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <div style="height:calc(100% - 100px); overflow:auto;">
        <telerik:RadMultiPage runat="server" ID="rmpDetalle" Width="100%" Height="100%" SelectedIndex="0">
            <telerik:RadPageView runat="server" ID="rpvGeneral">
                <div style="clear: both; height: 5px;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label>Evento:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox runat="server" ID="txtEvento" Width="300px" Enabled="false"></telerik:RadTextBox>
                    </div>
                </div>
                <div style="clear: both; height: 5px;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label>Curso:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox runat="server" ID="txtCurso" Width="300px" Enabled="false"></telerik:RadTextBox>
                    </div>
                </div>
                <div style="clear: both; height: 5px;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label>Descripción:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox runat="server" ID="txtDescripcion" Width="300px" Enabled="false"></telerik:RadTextBox>
                    </div>
                </div>
                <div style="clear: both; height: 5px;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label>Tipo:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox runat="server" ID="txtTipoCurso" Width="300px" Enabled="false"></telerik:RadTextBox>
                    </div>
                </div>
                <div style="clear: both; height: 5px;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label>Duración</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox runat="server" ID="txtDuracion" Width="300px" Enabled="false"></telerik:RadTextBox>
                    </div>
                </div>
                <div style="clear: both; height: 5px;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label>Puesto objetivo:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox runat="server" ID="txtPuesto" Width="300px" Enabled="false"></telerik:RadTextBox>
                    </div>
                </div>
                <div style="clear: both; height: 5px;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label>Notas:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadEditor runat="server" ID="reNotas" EditModes="Design" NewLineMode="Br" ToolsWidth="310px" ToolbarMode="Default" ToolsFile="~/Assets/AdvancedTools.xml"></telerik:RadEditor>
                    </div>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView runat="server" ID="rpvParticipantes">
                <telerik:RadGrid runat="server" ID="rgParticipantes" HeaderStyle-Font-Bold="true" AutoGenerateColumns="false" OnNeedDataSource="rgParticipantes_NeedDataSource">
                    <MasterTableView ShowHeadersWhenNoRecords="true">
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="NO_FILA" DataField="NO_FILA" HeaderText="#"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CL_PARTICIPANTE" DataField="CL_PARTICIPANTE" HeaderText="Clave"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="NB_PARTICIPANTE" DataField="NB_PARTICIPANTE" HeaderText="Nombre"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="NB_PUESTO" DataField="NB_PUESTO" HeaderText="Puesto"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="DS_CUMPLIMIENTO" DataField="DS_CUMPLIMIENTO" HeaderText="Promedio"></telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </telerik:RadPageView>
            <telerik:RadPageView runat="server" ID="rpvCompetencias">
                <telerik:RadGrid runat="server" ID="rgCompetencias" AutoGenerateColumns="false" HeaderStyle-Font-Bold="true" OnNeedDataSource="rgCompetencias_NeedDataSource">
                    <MasterTableView ShowHeadersWhenNoRecords="true">
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="NO_FILA" DataField="NO_FILA" HeaderText="#"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CL_TIPO_COMPETENCIA" DataField="CL_TIPO_COMPETENCIA" HeaderText="Categoria"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="NB_COMPETENCIA" DataField="NB_COMPETENCIA" HeaderText="Competencia"></telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </div>
</asp:Content>
