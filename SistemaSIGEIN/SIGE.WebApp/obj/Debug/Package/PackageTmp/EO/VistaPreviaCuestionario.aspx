<%@ Page Title="" Language="C#" MasterPageFile="~/EO/ContextEO.master" AutoEventWireup="true" CodeBehind="VistaPreviaCuestionario.aspx.cs" Inherits="SIGE.WebApp.EO.VistaPreviaCuestionario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script type="text/javascript">

        function OpenImpresion() {
            var myPrincipalHtml = document.getElementById('<%=dvimp.ClientID%>');
            var myIframe = document.getElementById('ifrmPrint');
            var pvContent = myPrincipalHtml.innerHTML;
            var myDoc = (myIframe.contentWindow || myIframe.contentDocument);
            if (myDoc.document) myDoc = myDoc.document;
            myDoc.write("<html><head><title>Cuestionario clima laboral</title>");
            myDoc.write("</head><body onload='this.focus(); this.print();'>");
            myDoc.write(pvContent + "</body></html>");
            myDoc.close(); (pvContent + "</body></html>");
            myDoc.close();
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div id="dvimp" runat="server" style="width: 790px; padding-top: 10px; padding-bottom: 10px; padding-left: 10px; padding-right: 10px;">
        <div style="clear: both; height: 5px;"></div>
        <div class="ctrlBasico">
            <label id="lbPeriodo" name="lbTabulador" runat="server" style="width: 100px;">Período:</label>
            <input id="txtClPeriodo" runat="server" type="text" name="txtClPeriodo" readonly="true" style="background-color: #cccccc; border-style: solid; border-width: 1px; border-radius: 5px; width: 420px">
        </div>
        <div id="dvMostrar" runat="server">
            <div style="clear: both; height: 2px;"></div>
            <div class="ctrlBasico">
                <label id="lbedad" name="LbFiltros" runat="server" visible="false" style="width: 100px;">Edad:</label>
                <input id="txtEdaddes" runat="server" type="text" name="txtEdaddes" visible="false" readonly="true" style="background-color: #cccccc; border-style: solid; border-width: 1px; border-radius: 5px; width: 120px">
                <label id="lbAntiguedad" name="LbFiltros" runat="server" visible="false" style="width: 120px;">Fecha de ingreso:</label>
                <input id="txtAntiguedades" runat="server" type="text" value="        /        /" name="txtAntiguedades" visible="false" readonly="true" style="background-color: #cccccc; border-style: solid; border-width: 1px; border-radius: 5px; width: 110px">
            </div>
            <div class="ctrlBasico">
                <label id="lbGenero" name="LbFiltros" runat="server" visible="false" style="width: 50px;">Género:</label>
            </div>
            <div id="dvGeneros" runat="server">
            </div>
            <div style="clear: both; height: 5px;"></div>
            <div class="ctrlBasico">
                <label id="lbDepartamento" name="LbFiltros" runat="server" visible="false" style="width: 150px;">Área/Departamento:</label>
            </div>
            <div id="dvAreas" runat="server">
            </div>
            <div style="clear: both; height: 5px;"></div>
            <div class="ctrlBasico">
                <table id="tbAdscripciones" runat="server">
                </table>
            </div>
        </div>
        <div style="clear: both; height: 5px;"></div>
        <div style="text-align: justify;">
            <fieldset style="padding: 10px;">
                <legend>
                    <label>Instrucciones:</label>
                </legend>
                <literal id="lbInstrucciones" runat="server"></literal>
            </fieldset>
        </div>
        <div style="clear: both; height: 5px;"></div>
        <div id="dvCuestionario" runat="server"></div>
        <div style="clear: both; height: 5px;"></div>
        <div id="dvPreguntasAbiertas" runat="server"></div>
        <%--     <telerik:RadGrid runat="server" ID="rgCuestionario" HeaderStyle-Font-Bold="true" AutoGenerateColumns="false" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="1px" Height="100%" Width="100%" OnNeedDataSource="rgCuestionario_NeedDataSource">
            <ClientSettings EnablePostBackOnRowClick="false">
                <Scrolling UseStaticHeaders="true" AllowScroll="false" />
                <Selecting AllowRowSelect="false" />
            </ClientSettings>
            <MasterTableView ShowHeadersWhenNoRecords="false">
                <Columns>
                    <telerik:GridBoundColumn HeaderText="#" HeaderStyle-Width="40" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px" DataField="NO_SECUENCIA" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Pregunta" DataField="NB_PREGUNTA" HeaderStyle-Width="210" ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="80px" ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px" ItemStyle-HorizontalAlign="Center" ReadOnly="true" HeaderText="Totalmente de acuerdo" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                        <ItemTemplate>
                            <input id="rbTotalmenteAcuerdo" runat="server" name="rbTotalmenteAcuerdo" value="" type="radio">
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="80px" ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px" ItemStyle-HorizontalAlign="Center" ReadOnly="true" HeaderText="Casi siempre de acuerdo" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                        <ItemTemplate>
                            <input id="rbCasiAcuerdo" runat="server" name="rbTotalmenteAcuerdo" value="" type="radio">
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px" ReadOnly="true" HeaderText="Casi siempre en desacuerdo" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                        <ItemTemplate>
                            <input id="rbCasiDesacuerdo" runat="server" name="rbTotalmenteAcuerdo" value="" type="radio">
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px" ReadOnly="true" HeaderText="Totalmente en desacuerdo" HeaderStyle-Font-Bold="true">
                        <ItemTemplate>
                            <input id="rbTotalmenteDesacuerdo" runat="server" name="rbTotalmenteAcuerdo" value="" type="radio">
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
        <telerik:RadGrid runat="server" ID="rgPreguntasAbiertas" HeaderStyle-Font-Bold="true" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="1px" AutoGenerateColumns="false" Height="100%" Width="100%" OnNeedDataSource="rgPreguntasAbiertas_NeedDataSource">
            <ClientSettings EnablePostBackOnRowClick="false">
                <Scrolling UseStaticHeaders="true" AllowScroll="false" />
                <Selecting AllowRowSelect="false" />
            </ClientSettings>
            <MasterTableView ShowHeadersWhenNoRecords="false">
                <Columns>
                    <telerik:GridBoundColumn HeaderText="Pregunta" HeaderStyle-Width="200" HeaderStyle-Height="0" ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px" DataField="NB_PREGUNTA"></telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="390" HeaderStyle-Height="0" ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px" ItemStyle-HorizontalAlign="Center" HeaderText="Respuesta" ReadOnly="true">
                        <ItemTemplate>
                            <telerik:RadTextBox ID="txtRespuesta" runat="server" Height="30" Width="100%" BorderStyle="Solid" BorderWidth="0"></telerik:RadTextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>--%>
    </div>
    <iframe src="#" style="width: 0; height: 0; border: none" id="ifrmPrint"></iframe>
    <div class="divControlDerecha" style="padding:20px;">
        <telerik:RadButton ID="btnImpresion2" runat="server" Text="Imprimir" OnClientClicked="OpenImpresion" AutoPostBack="false"></telerik:RadButton>
    </div>
</asp:Content>
