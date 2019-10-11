<%@ Page Title="" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="VentanaImportarEmpleados.aspx.cs" Inherits="SIGE.WebApp.Administracion.VentanaImportarEmpleados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <style>
        .RadButton.rbSkinnedButton.uncheckedYes {
            background-color: #eee !important;
        }

            .RadButton.rbSkinnedButton.uncheckedYes > .rbDecorated {
                color: #eee !important;
            }

        .RadButton.rbSkinnedButton.checkedNo {
            background-color: #eee !important;
        }

            .RadButton.rbSkinnedButton.checkedNo > .rbDecorated {
                color: #333 !important;
            }

        .RadButton.rbSkinnedButton.uncheckedNo {
            background-color: #eee !important;
        }

            .RadButton.rbSkinnedButton.uncheckedNo > .rbDecorated {
                color: #eee !important;
            }

        .checkContainer {
            border-radius: 5px;
            border: 1px solid lightgray;
            background: #eee;
        }
    </style>
    <script type="text/javascript">

        function closeWindow(estatus) {
            var oArg = new Object();
            oArg.estatus = estatus;
            var window = GetRadWindow();
            window.close(estatus);
        }

        function closePopup() {
            var window = GetRadWindow();
            window.close();
        }

        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }


        function importarEmpleados() {
            var wnd = $find("<%= rwCargar.ClientID%>");
            wnd.show();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">

    <div style="width: 39%; height: 90%; float: left;">

        <div style="margin-top: 15px;">
            <telerik:RadAsyncUpload ID="RadAsyncUpload1"
                runat="server"
                Width="270px"
                MaxFileInputsCount="1"
                Localization-Cancel="Cancelar"
                Localization-Remove="Quitar"
                Localization-Select="Buscar">
            </telerik:RadAsyncUpload>
        </div>

        <div id="divVersion" style="margin-top: 15px;">
            <telerik:RadComboBox Width="250px" runat="server" ID="cmbVersion" EmptyMessage="Seleccione..." CssClass="btnMediano">
                <Items>
                    <telerik:RadComboBoxItem Value="1.0" Text="1.0" />
                    <telerik:RadComboBoxItem Value="1.1" Text="1.1" />
                </Items>
            </telerik:RadComboBox>
        </div>

        <div id="divValidar" style="margin-top: 15px;">
            <telerik:RadButton CssClass="btnMediano" ID="btnValidar" runat="server" Text="Validar archivo" OnClick="btnValidar_Click">
                <Icon PrimaryIconCssClass="rbOk" />
            </telerik:RadButton>

            <telerik:RadButton CssClass="btnMediano" ID="btnProcesar" runat="server" Text="Cargar archivo">
                <Icon PrimaryIconCssClass="rbUpload" />
            </telerik:RadButton>
        </div>
    </div>

    <div id="divGrid" style="width: 59%; height: 90%; float: left;">
        <div style="margin-top: 15px">
            <telerik:RadGrid ID="GridErrores" ShowHeader="true"
                OnNeedDataSource="GridErrores_NeedDataSource"
                runat="server" AllowPaging="false" AllowSorting="true" GroupPanelPosition="Top" Width="99%" Height="470" ClientSettings-EnablePostBackOnRowClick="false" AllowFilteringByColumn="false" PagerStyle-AlwaysVisible="false">
                <ClientSettings AllowKeyboardNavigation="true">
                    <Selecting AllowRowSelect="true" />
                    <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                </ClientSettings>
                <MasterTableView ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" CommandItemDisplay="Top">
                    <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="false" ShowExportToCsvButton="false" />
                    <Columns>
                        <telerik:GridBoundColumn HeaderText="Linea" DataField="clave_retorno" UniqueName="clave_retorno" HeaderStyle-Width="50px" ItemStyle-Width="50px" Display="true" ReadOnly="true" ItemStyle-HorizontalAlign="left"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Error" DataField="mensaje_retorno" UniqueName="mensaje_retorno" HeaderStyle-Width="200px" ItemStyle-Width="200px" Display="true" ReadOnly="true" ItemStyle-HorizontalAlign="left"></telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </div>
    </div>

    <div class="divBotoneraDerecha" style="float: right; margin-right:20px">
        <telerik:RadButton CssClass="btnCancelar" ID="btnCerrar" runat="server" Text=" Cerrar" OnClientClicked="closeWindow" Width="90px">
            <Icon PrimaryIconCssClass="rbCancel" />
        </telerik:RadButton>
    </div>

    <telerik:RadWindow ID="rwCargar" runat="server" Width="500" Height="200" BorderWidth="0" Modal="true" VisibleStatusbar="false" VisibleOnPageLoad="false" Title="Importando empleados" Behaviors="None">
        <ContentTemplate>
            <telerik:RadProgressManager ID="RadProgressManager2" runat="server" />
            <telerik:RadProgressArea ID="RadProgressArea2" runat="server" Width="460" Height="150"
                ProgressIndicators="FilesCountBar, FilesCount, FilesCountPercent, TimeElapsed, SelectedFilesCount"
                Localization-ElapsedTime="Tiempo transcurrido: " Localization-TotalFiles="Lineas totales: " Localization-UploadedFiles="Lineas procesadas: " />
        </ContentTemplate>
    </telerik:RadWindow>

    <telerik:RadWindowManager ID="rwMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
