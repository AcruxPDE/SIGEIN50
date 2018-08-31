<%@ Page Title="" Language="C#" MasterPageFile="~/EO/ContextEO.master" AutoEventWireup="true" CodeBehind="DisenoMetasDescriptivo.aspx.cs" Inherits="SIGE.WebApp.EO.DisenoMetasDescriptivo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div style="height: 10px"></div>
    <div class="ctrlBasico" style="text-align: center">

        <div class="divControlIzquierda">
            <label id="lblPeriodoDiseno">Período:</label>
        </div>
        <div class="divControlIzquierda">
            <telerik:RadTextBox ID="txtPeriodoDiseno" Enabled="false" InputType="Text" Width="100" Height="30" runat="server"></telerik:RadTextBox>
        </div>
        <div class="divControlIzquierda">
            <label id="lblDsPeriodoDiseno">Descripción:</label>
        </div>
        <div class="divControlIzquierda">
            <telerik:RadTextBox ID="txtDsPeriodoDiseno" Enabled="false" InputType="Text" Width="150" Height="30" runat="server"></telerik:RadTextBox>
        </div>
        <div style="height: 40px;"></div>
    </div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="lblEmpleado">Empleado:</label>
        </div>
        <div class="divControlIzquierda">
            <telerik:RadTextBox ID="txtEmpleado" Enabled="false" InputType="Text" Width="200" Height="30" runat="server"></telerik:RadTextBox>
        </div>
    </div>
    <div style="height: 10px"></div>

    <div style="height: 10px;"></div>
    <div class="ctrlBasico">
        <div style="height: 50px;"></div>
        <div class="ctrlBasico">
            <label id="lblAdministrarProcesos" style="font-weight: bold">Administrar procesos de capital humano</label>
        </div>
        <table id="Table2" cellspacing="2" cellpadding="1" width="100%" border="0" rules="none"
            class="ctrlTableForm" style="border-color: #eee">
            <tr>
                <td>
                    <table id="Table3" width="450px" border="1" class="module" style="border-color: #eee">
                        <tr>
                            <td class="tablaDisenoMetas">
                                <label style="width: 150px; color: white; text-align: center">Indicador</label>
                            </td>
                            <td class="tablaDisenoMetas">
                                <label id="lblMeta" style="width: 150px; color: white; text-align: center">Meta</label>
                            </td>
                            <td class="tablaDisenoMetas">
                                <label id="lblPorcentaje" style="width: 10px; color: white; text-align: center">%</label>
                            </td>
                            <td class="tablaDisenoMetas">
                                <label id="lblNumero" style="width: 10px; color: white; text-align: center">#</label>
                            </td>
                            <td class="tablaDisenoMetas">
                                <label id="lblFecha" style="width: 90px; color: white; text-align: center">dd-mm-aa</label>
                            </td>
                            <td class="tablaDisenoMetas">
                                <label id="lblActual" style="width: 70px; color: white; text-align: center">Actual</label>
                            </td>
                            <td class="tablaDisenoMetas">
                                <label id="lblMinima" style="width: 70px; color: white; text-align: center">Mínima</label>
                            </td>
                            <td class="tablaDisenoMetas">
                                <label id="lblSatisfactoria" style="width: 100px; color: white; text-align: center">Satisfactoria</label>
                            </td>
                            <td class="tablaDisenoMetas">
                                <label id="lblSobresaliente" style="width: 110px; color: white; text-align: center">Sobresaliente</label>
                            </td>
                            <td class="tablaDisenoMetas">
                                <label id="lblPonderacion" style="width: 100px; color: white; text-align: center">Ponderación</label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadLabel runat="server" Text="Llenado correcto de reportes de cierre de actividades" Width="150"></telerik:RadLabel>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtMeta" runat="server" TabIndex="2" Width="150">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtPorcentaje" runat="server" TabIndex="2" Width="50">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtNumero" runat="server" TabIndex="2" Width="50">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtFecha" runat="server" TabIndex="2" Width="90">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtActual" runat="server" TabIndex="2" Width="70">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtMinima" runat="server" TabIndex="2" Width="70">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtSatisfactoria" runat="server" TabIndex="2" Width="100">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtSobresaliente" runat="server" TabIndex="2" Width="110">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtPonderacion" runat="server" TabIndex="2" Width="100">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
