<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="ReporteGlobalImpresion.aspx.cs" Inherits="SIGE.WebApp.FYD.ReporteGlobalImpresion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <style>
        .divRojo {
            height: 80%;
            width: 15px;
            border-radius: 5px;
            background: red;
        }

        .divAmarillo {
            height: 80%;
            width: 15px;
            border-radius: 5px;
            background: gold;
        }

        .divVerde {
            height: 80%;
            width: 15px;
            border-radius: 5px;
            background: green;
        }

        .divNa {
            height: 80%;
            width: 15px;
            border-radius: 5px;
            background: gray;
        }

        table.tablaColor {
            width: 100%;
        }

        td.porcentaje {
            width: 80%;
            padding: 1px;
        }

        td.puesto {
            width: 75%;
            padding: 1px;
            font-weight: bold;
        }

        td.color {
            width: 10%;
            padding: 1px 1px 1px 1px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div id="dvImprimir" runat="server">
        <label style="display: block !important; width: 100% !important; padding: 0 !important; margin-bottom: 20px !important; font-size: 21px !important; line-height: inherit !important; color: darkred !important; border: 0 !important; border-bottom: 1px solid #e5e5e5 !important;">
            Consulta global</label>
        <div style="clear: both; height: 10px;"></div>
        <div style="overflow: auto; height: calc(100% - 90px); width: 100%;">
            <div class="ctrlBasico">
                <table class="ctrlTableForm ctrlTableContext">
                    <tr>
                        <td>
                            <label>Periodo:</label>
                        </td>
                        <td>
                            <span id="txtClave" runat="server" style="width: 300px;"></span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Descripción:</label>
                        </td>
                        <td colspan="2">
                            <span id="txtDescripcion" runat="server" style="width: 300px;"></span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Tipo de evaluación:</label></td>
                        <td colspan="2">
                            <ul id="txtTiposEvaluacion" runat="server" style="margin-bottom: 0px;"></ul>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="clear: both;"></div>
            <div>
                <table class="ctrlTableForm ctrlTableContext">
                    <tr>
                        <td>
                            <label>Notas:</label></td>
                        <td colspan="2">
                            <div id="txtDsNotas" runat="server" style="min-width: 100px;"></div>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="clear: both; height: 10px;"></div>
            <div id="dvReporteGeneral" runat="server"></div>
        </div>
    </div>
    <iframe src="#" style="width: 0; height: 0; border: none" id="ifrmPrint"></iframe>
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">
        <script type="text/javascript">

            setTimeout(function () {
                var myPrincipalHtml = document.getElementById('<%=dvImprimir.ClientID%>');
                var myIframe = document.getElementById('ifrmPrint');
                var pvContent = myPrincipalHtml.innerHTML;
                var myDoc = (myIframe.contentWindow || myIframe.contentDocument);
                if (myDoc.document) myDoc = myDoc.document;
                myDoc.write("<html>");
                myDoc.write("<body onload='this.focus(); this.print();'>");
                myDoc.write(pvContent + "</body></html>");
                myDoc.close(); (pvContent + "</body></html>");
                myDoc.close();
            }, 3000);

        </script>
    </telerik:RadCodeBlock>
</asp:Content>
