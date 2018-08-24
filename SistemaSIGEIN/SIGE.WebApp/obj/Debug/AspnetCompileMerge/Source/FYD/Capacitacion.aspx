<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/MenuFYD.master" AutoEventWireup="true" CodeBehind="Capacitacion.aspx.cs" Inherits="SIGE.WebApp.FYD.Capacitacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .rectangle {
            padding-top:30px !important;
            height: 100px !important;
            width: 170px !important;
        }

        .process {
            background-image: url('/Assets/images/Process.png');
            background-repeat: no-repeat;
            text-align: center;
            padding-top: 20px;
            vertical-align: middle;
            width: 140px;
            height: 100px;
        }

        td {
            text-align: center;
        }

        .puntoUno {
            border-radius: 10px;
            border: 1px solid grey;
            width: 150px;
            height: 100px;
            padding: 10px;
            vertical-align: middle;
            background-color: lightgray;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="align-content: center">
        <table>
            <tr>
                <td>
                    <telerik:RadButton ID="btnPeriodosEvaluacion" runat="server" Text="Períodos de evaluación" CssClass="rectangle" ButtonType="LinkButton" NavigateUrl="/FYD/PeriodosEvaluacion.aspx"></telerik:RadButton>
                </td>
                <td>
                    <img src="../Assets/images/FlechaDerecha.png" />
                </td>
                <td>
                    <div class="process">Necesidades de capacitación</div>
                </td>
                <td>
                    <img src="../Assets/images/FlechaDerecha.png" />
                </td>
                <td>
                    <telerik:RadButton ID="btnProgramasCapacitacion" runat="server" Text="Programas de capacitación" CssClass="rectangle" ButtonType="LinkButton" NavigateUrl="/FYD/ProgramasCapacitacion.aspx"></telerik:RadButton>
                </td>
                <td>
                    <img src="../Assets/images/FlechaIzquierda.png" />
                </td>
                <td>
                    <div class="puntoUno">
                        Programas de capacitación<br />
                        desde 0
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="4"></td>
                <td>
                    <img src="../Assets/images/FlechaAbajo.png" />
                </td>
                <td colspan="2"></td>
            </tr>
            <tr>
                <td colspan="4"></td>
                <td>
                    <telerik:RadButton ID="btnEventosCapacitacion" runat="server" Text="Eventos de capacitación" CssClass="rectangle" ButtonType="LinkButton" NavigateUrl="/FYD/EventosCapacitacion.aspx"></telerik:RadButton>
                </td>
                <td>
                    <img src="../Assets/images/FlechaIzquierda.png" />
                </td>
                <td>
                    <div class="puntoUno">
                        Eventos de capacitación sin DNC ni programas
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="4"></td>
                <td>
                    <img src="../Assets/images/FlechaAbajo.png" />
                </td>
                <td colspan="2"></td>
            </tr>
            <tr>
                <td colspan="4"></td>
                <td>
                    <telerik:RadButton ID="btnConsultas" runat="server" Text="Consultas de Formación y Desarrollo" CssClass="rectangle" ButtonType="LinkButton" NavigateUrl="/FYD/Consultas.aspx"></telerik:RadButton>
                </td>
                <td colspan="2"></td>
            </tr>
        </table>
    </div>
</asp:Content>
