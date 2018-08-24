<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="VentanaImprimirSolicitud.aspx.cs" Inherits="SIGE.WebApp.IDP.VentanaImprimirSolicitud" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div id="dvImprimir" runat="server">
         <label style="display: block !important; width: 100% !important; padding: 0 !important; margin-bottom: 20px !important; font-size: 21px !important; line-height: inherit !important; color: darkred !important; border: 0 !important; border-bottom: 1px solid #e5e5e5 !important;">
              Solicitud de empleo</label>
            <div id="pvwPersonal" runat="server">
                <label style="display: block !important; width: 100% !important; padding: 0 !important; margin-bottom: 20px !important; font-size: 18px !important; line-height: inherit !important; color: darkred !important; border: 0 !important; border-bottom: 1px solid #e5e5e5 !important;">Información personal</label>
                  <table class="ctrlTableForm" >
                    <tr>
                        <td style="text-align: center;">
                            <telerik:RadBinaryImage ID="rbiFotoEmpleado" runat="server" Width="128" Height="128" ResizeMode="Fit" ImageUrl="~/Assets/images/LoginUsuario.png" />
                        </td>
                    </tr>
                </table>
            </div>
            <div style="clear: both; height: 15px"></div>
            <div id="pvwFamiliar" runat="server">
                <label style="display: block !important; width: 100% !important; padding: 0 !important; margin-bottom: 20px !important; font-size: 18px !important; line-height: inherit !important; color: darkred !important; border: 0 !important; border-bottom: 1px solid #e5e5e5 !important;">Información familiar</label>
            </div>
            <div style="clear: both; height: 15px"></div>
            <div id="pvwAcademica" runat="server">
                <label style="display: block !important; width: 100% !important; padding: 0 !important; margin-bottom: 20px !important; font-size: 18px !important; line-height: inherit !important; color: darkred !important; border: 0 !important; border-bottom: 1px solid #e5e5e5 !important;">Información académica</label>
            </div>
            <div style="clear: both; height: 15px"></div>
            <div id="pvwLaboral" runat="server">
                <label style="display: block !important; width: 100% !important; padding: 0 !important; margin-bottom: 20px !important; font-size: 18px !important; line-height: inherit !important; color: darkred !important; border: 0 !important; border-bottom: 1px solid #e5e5e5 !important;">Información laboral</label>
            </div>
            <div style="clear: both; height: 15px"></div>
            <div id="pvwCompetencias" runat="server">
                <label style="display: block !important; width: 100% !important; padding: 0 !important; margin-bottom: 20px !important; font-size: 18px !important; line-height: inherit !important; color: darkred !important; border: 0 !important; border-bottom: 1px solid #e5e5e5 !important;">Intereses y competencias</label>
            </div>
            <div style="clear: both; height: 15px"></div>
            <div id="pvwAdicional" runat="server">
                <label style="display: block !important; width: 100% !important; padding: 0 !important; margin-bottom: 20px !important; font-size: 18px !important; line-height: inherit !important; color: darkred !important; border: 0 !important; border-bottom: 1px solid #e5e5e5 !important;">Información adicional</label>
            </div>
            <div style="clear: both; height: 15px"></div>
            <div id="pvwDocumentos" runat="server">
                <label style="display: block !important; width: 100% !important; padding: 0 !important; margin-bottom: 20px !important; font-size: 18px !important; line-height: inherit !important; color: darkred !important; border: 0 !important; border-bottom: 1px solid #e5e5e5 !important;">Documentación</label>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <telerik:RadGrid ID="grdDocumentos" BorderStyle="Solid" BorderWidth="1px" HeaderStyle-Font-Size="9" ItemStyle-Font-Size="9" runat="server" HeaderStyle-Font-Bold="true" OnNeedDataSource="grdDocumentos_NeedDataSource" Width="580">
                        <ClientSettings>
                            <Scrolling UseStaticHeaders="true" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <MasterTableView ClientDataKeyNames="ID_ARCHIVO,ID_ITEM" DataKeyNames="ID_ARCHIVO,ID_ITEM" AutoGenerateColumns="false" ShowHeadersWhenNoRecords="true">
                            <Columns>
                                <telerik:GridBoundColumn HeaderText="Nombre del documento" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="1px" ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px" DataField="NB_DOCUMENTO" ></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Tipo de documento" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="1px" ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px" HeaderStyle-Width="200" DataField="CL_TIPO_DOCUMENTO" UniqueName="CL_TIPO_DOCUMENTO"></telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
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
