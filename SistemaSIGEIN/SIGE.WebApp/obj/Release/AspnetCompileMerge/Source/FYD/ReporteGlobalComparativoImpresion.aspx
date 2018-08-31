<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="ReporteGlobalComparativoImpresion.aspx.cs" Inherits="SIGE.WebApp.FYD.ReporteGlobalComparativoImpresion" %>

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
    <div style="clear: both; height: 10px;"></div>


    <div id="imprimir" runat="server" style="overflow: auto; height: calc(100% - 80px); width: 100%;">
        <label style="display: block !important; width: 100% !important; padding: 0 !important; margin-bottom: 20px !important; font-size: 21px !important; line-height: inherit !important; color: darkred !important; border: 0 !important; border-bottom: 1px solid #e5e5e5 !important;">
            Consulta global comparativa</label>
        <div style="clear: both; height: 10px;"></div>
        <div id="dvContexto" runat="server"></div>
        <div style="clear: both; height: 10px;"></div>
        <div id="dvReporteGeneral" runat="server"></div>
    </div>
    <label runat="server" id="lblAdvertencia" style="color: red;">* Alguno de los períodos que aparecen aún no ha sido cerrado por lo que alguna de las calificaciones podrían ser parciales</label>
    <iframe src="#" style="width: 0; height: 0; border: none" id="ifrmPrint"></iframe>
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">
        <script type="text/javascript">

            setTimeout(function () {
                var myPrincipalHtml = document.getElementById('<%= imprimir.ClientID%>');
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
