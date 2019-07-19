<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="VentanaReporteMaximosMinimos.aspx.cs" Inherits="SIGE.WebApp.FYD.VentanaReporteMaximosMinimos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
     <div style="clear:both; height:10px;"></div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label>Puesto objetivo:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadTextBox runat="server" ID="txtClavePuesto" Width="300px" ReadOnly="true"></telerik:RadTextBox>        
        </div>
    </div>
    
    <div style="clear:both; height:10px;"></div>

    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label>Descripción:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadTextBox runat="server" ID="txtNombrePuesto" Width="300px" ReadOnly="true"></telerik:RadTextBox>
        </div>
    </div>

    <div style="clear:both; height:10px;"></div>

    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label>Número de ocupantes:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadTextBox runat="server" ID="txtNoOcupantes" Width="100px" ReadOnly="true" >
                <ReadOnlyStyle HorizontalAlign="Right" />
            </telerik:RadTextBox>
        </div>
    </div>

    <div style="clear:both; height:10px;"></div>

    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label>Punto de reorden:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadTextBox runat="server" ID="txtReorden" Width="100px" ReadOnly="true">
                <ReadOnlyStyle HorizontalAlign="Right" />
            </telerik:RadTextBox>
        </div>
    </div>

    <div style="clear:both; height:10px;"></div>

    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label>Stock de reemplazo:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadTextBox runat="server" ID="txtStock" Width="100px" ReadOnly="true">
                <ReadOnlyStyle HorizontalAlign="Right" />
            </telerik:RadTextBox>
        </div>
    </div>

    <div style="clear:both; height:10px;"></div>

    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label>Cantidad a capacitar:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadTextBox runat="server" ID="txtCapacitar" Width="100px" ReadOnly="true">
                <ReadOnlyStyle HorizontalAlign="Right" />
            </telerik:RadTextBox>
        </div>
    </div>

    <div style="clear:both; height:10px;"></div>

    <telerik:RadGrid runat="server" ID="rgReporte" AutoGenerateColumns="false" HeaderStyle-Font-Bold="true" OnNeedDataSource="rgReporte_NeedDataSource">
        <MasterTableView ShowHeadersWhenNoRecords="true">
            <Columns>
                <telerik:GridBoundColumn UniqueName="NB_EVENTO" DataField="NB_EVENTO" HeaderText="Evento"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="NB_CURSO" DataField="NB_CURSO" HeaderText="Curso"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="DS_COMPETENCIAS" DataField="DS_COMPETENCIAS" HeaderText="Competencias"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="NO_PERSONAL" DataField="NO_PERSONAL_ENTRENAMIENTO" ItemStyle-HorizontalAlign="Right" HeaderText="No. personal en entrenamiento"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="FE_TERMINO" DataField="FE_TERMINO" HeaderText="Fecha de liberación" DataFormatString="{0:dd/MM/yyyy}"></telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>

    <label id="lblMensaje" runat="server" style="font-weight:bold; color:red;" visible="false"></label>
</asp:Content>
