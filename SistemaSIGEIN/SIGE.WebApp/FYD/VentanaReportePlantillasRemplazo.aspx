﻿<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="VentanaReportePlantillasRemplazo.aspx.cs" Inherits="SIGE.WebApp.FYD.VentanaReportePlantillasRemplazo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    
    <style>
        .PotencialBajo {
            background-color: red;
            text-align: center;
            color: white;
        }

        .PotencialIntermedio {
            background-color: gold;
            text-align: center;
        }

        .PotencialAlto {
            background-color: green;
            text-align: center;
        }

        .CeldaNumerica {
            text-align: right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">

    <div style="clear:both; height:10px;"></div>

    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label>Puesto: </label>
        </div>
        <div class="divControlDerecha">
           <telerik:RadTextBox runat="server" ID="txtPuesto" Enabled="false" Width="300px"></telerik:RadTextBox>
        </div>
    </div>

    <div style="clear:both; height:10px;"></div>

    <div style="height:calc(100% - 80px); overflow:auto;">
    <telerik:RadPivotGrid runat="server" ID="rpgReporte" OnNeedDataSource="rpgReporte_NeedDataSource" TotalsSettings-GrandTotalText="Compatibilidad" OnCellDataBound="rpgReporte_CellDataBound" ShowFilterHeaderZone="false" AllowFiltering="false">
        
        <TotalsSettings ColumnGrandTotalsPosition="Last" ColumnsSubTotalsPosition="None" RowsSubTotalsPosition="None" RowGrandTotalsPosition="None" />
        
        <Fields>
            <telerik:PivotGridRowField UniqueName="CL_EMPLEADO" DataField="CL_EMPLEADO" Caption="Clave"></telerik:PivotGridRowField>
            <telerik:PivotGridRowField UniqueName="NB_EMPLEADO" DataField="NB_EMPLEADO" Caption="Empleado"></telerik:PivotGridRowField>
            <telerik:PivotGridRowField UniqueName="NB_PUESTO" DataField="NB_PUESTO" Caption="Puesto"></telerik:PivotGridRowField>
            <telerik:PivotGridRowField UniqueName="NB_DEPARTAMENTO" DataField="NB_DEPARTAMENTO" Caption="Departamento"></telerik:PivotGridRowField>

            <telerik:PivotGridColumnField UniqueName="NB_COMPETENCIA" DataField="NB_COMPETENCIA"  Caption="Competencia"></telerik:PivotGridColumnField>
            
            <telerik:PivotGridAggregateField UniqueName="PR_COMPATIBILIDAD" DataFormatString="{0:N2}%" DataField="PR_COMPATIBILIDAD" Caption="Compatibilidad" Aggregate="Average" CellStyle-CssClass="CeldaNumerica">
                
            </telerik:PivotGridAggregateField>
        </Fields>
    </telerik:RadPivotGrid>
    </div>    
</asp:Content>