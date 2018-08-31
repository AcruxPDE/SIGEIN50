<%@ Page Title="" Language="C#" MasterPageFile="~/MPC/ContextMC.master" AutoEventWireup="true" CodeBehind="ReporteTabuladorSueldos.aspx.cs" Inherits="SIGE.WebApp.MPC.ReporteTabuladorSueldos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div id="dvimp" runat="server" style="width: 790px; padding-top: 10px; padding-bottom: 10px; padding-left: 10px; padding-right: 10px;">
        <div style="height: 10px;"></div>
        <label style="display: block !important; width: 100% !important; padding: 0 !important; margin-bottom: 20px !important; font-size: 21px !important; line-height: inherit !important; border: 0 !important; border-bottom: 1px solid #0087CF !important;">
            Consulta Tabulador de sueldos</label>
        <div style="clear: both; height: 10px"></div>
        <div class="ctrlBasico">
            <table class="ctrlTableForm">
                <tr>
                    <td class="ctrlTableDataContext">
                        <label>Versión:</label></td>
                    <td colspan="2" class="ctrlTableDataBorderContext">
                        <div id="txtClaveTabulador" runat="server" style="min-width: 100px;"></div>
                    </td>
                </tr>
                <tr>
                    <td class="ctrlTableDataContext">
                        <label>Descipción:</label></td>
                    <td colspan="2" class="ctrlTableDataBorderContext">
                        <div id="txtNbTabulador" runat="server" style="min-width: 100px;"></div>
                    </td>
                </tr>
                <tr>
                    <td class="ctrlTableDataContext">
                        <label>Notas:</label></td>
                    <td colspan="2" class="ctrlTableDataBorderContext">
                        <div id="txtDescripción" runat="server" style="min-width: 100px;"></div>
                    </td>
                </tr>
                <tr>
                    <td class="ctrlTableDataContext">
                        <label>Fecha:</label></td>
                    <td colspan="2" class="ctrlTableDataBorderContext">
                        <div id="txtFecha" runat="server" style="min-width: 100px;"></div>
                    </td>
                </tr>
                <tr>
                    <td class="ctrlTableDataContext">
                        <label>Vigencia:</label></td>
                    <td colspan="2" class="ctrlTableDataBorderContext">
                        <div id="txtVigencia" runat="server" style="min-width: 100px;"></div>
                    </td>
                </tr>
                <tr>
                    <td class="ctrlTableDataContext">
                        <label>Tipo de puestos:</label></td>
                    <td colspan="2" class="ctrlTableDataBorderContext">
                        <div id="txtPuestos" runat="server" style="min-width: 100px;"></div>
                    </td>
                </tr>
            </table>
        </div>
        <div style="height: 10px;"></div>
        <div id="dvTabla" runat="server"></div>
    </div>
    <iframe src="#" style="width: 0; height: 0; border: none" id="ifrmPrint"></iframe>
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">
        <script type="text/javascript">
            setTimeout(function () {
                var myPrincipalHtml = document.getElementById('<%=dvimp.ClientID%>');
                var myIframe = document.getElementById('ifrmPrint');
                var pvContent = myPrincipalHtml.innerHTML;
                var myDoc = (myIframe.contentWindow || myIframe.contentDocument);
                if (myDoc.document) myDoc = myDoc.document;
                myDoc.write("<html>");
                myDoc.write("<body onload='this.focus(); this.print();'>");
                myDoc.write(pvContent + "</body></html>");
                myDoc.close(); (pvContent + "</body></html>");
                myDoc.close();
            }, 2000);
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
