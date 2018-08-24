<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="CuestionarioImpresion.aspx.cs" Inherits="SIGE.WebApp.FYD.CuestionarioImpresion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script type="text/javascript">
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div id="imprimir" runat="server" style="height: calc(100% - 40px); padding-top: 10px;">
                <label style="display: block !important; width: 100% !important; padding: 0 !important; margin-bottom: 20px !important; font-size: 21px !important; line-height: inherit !important; color: darkred !important; border: 0 !important; border-bottom: 1px solid #e5e5e5 !important;">
            Cuestionario de evaluación</label>
        <div class="ctrlBasico">
            <table>
                <tr>
                    <td>
                        <label>Período:</label></td>
                    <td colspan="2" style="border: 1px solid black; padding: 2px 2px 2px 2px; border-radius: 3px 3px;">
                        <div id="txtNoPeriodo" runat="server" style="min-width: 100px;"></div>
                    </td>
                    <td style="border: 1px solid black; padding: 2px 2px 2px 2px; border-radius: 3px 3px;">
                        <div id="txtNbPeriodo" runat="server" width="170" maxlength="1000" enabled="false"></div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>Persona a evaluar:</label></td>
                    <td colspan="2" style="border: 1px solid black; padding: 2px 2px 2px 2px; border-radius: 3px 3px;">
                        <div id="txtNombreEvaluado" runat="server" style="min-width: 100px;"></div>
                    </td>
                    <td style="border: 1px solid black; padding: 2px 2px 2px 2px;">
                        <div id="txtPuestoEvaluado" runat="server" width="170" maxlength="1000" enabled="false"></div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>Evaluador:</label></td>
                    <td colspan="2" style="border: 1px solid black; padding: 2px 2px 2px 2px; border-radius: 3px 3px;">
                        <div id="txtEvaluador" runat="server" style="min-width: 100px;"></div>
                    </td>
                    <td style="border: 1px solid black; padding: 2px 2px 2px 2px;">
                        <div id="txtTipo" runat="server" width="170" maxlength="1000" enabled="false"></div>
                    </td>
                </tr>
            </table>
        </div>
        <div style="clear: both;"></div>
        <div id="dvTabla" runat="server"></div>
        <div style="clear: both; height: 10px;"></div>
        <div id="divCamposExtras" runat="server" style="width: 100%; display: block;"></div>
    </div>
     <iframe src="#" style="width: 0; height: 0; border: none" id="ifrmPrint"></iframe>
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">
        <script type="text/javascript">

            setTimeout(function () {
                var myPrincipalHtml = document.getElementById('<%=imprimir.ClientID%>');
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
