<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="VentanaTemas.aspx.cs" Inherits="SIGE.WebApp.FYD.VentanaTemas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script id="MyScript" type="text/javascript">
        function generateDataForParent() {
            var info = null;
            var vDatos = []; var vCompetencias = []; var vMateriales = [];
            var masterTableCompetencia = $find("<%=grdTemaCompetencia.ClientID %>").get_masterTableView();
            var dataItemsCompetencia = masterTableCompetencia.get_dataItems();
            var masterTableMaterial = $find("<%=grdTemaMaterial.ClientID %>").get_masterTableView();
            var dataItemsMaterial = masterTableMaterial.get_dataItems();
            var clTema = $find("<%=txtcl_tema.ClientID %>");
            var nbTema = $find("<%=txtTema.ClientID %>");
            var duracion = $find("<%=txtDuracion.ClientID %>");
            var descripcion = $find("<%=txtDescripcion.ClientID %>");

            for (i = 0; i < dataItemsCompetencia.length; i++) {
                var vCompetencia = {
                    idCompetencia: masterTableCompetencia.getCellByColumnUniqueName(dataItemsCompetencia[i], "ID_COMPETENCIA").innerHTML,
                    clTipoCompetencia: masterTableCompetencia.getCellByColumnUniqueName(dataItemsCompetencia[i], "NB_COMPETENCIA").innerHTML,
                    nbCompetencia: masterTableCompetencia.getCellByColumnUniqueName(dataItemsCompetencia[i], "CL_TIPO_COMPETENCIA").innerHTML
                };
                vCompetencias.push(vCompetencia);
            }

            for (i = 0; i < dataItemsMaterial.length; i++) {
                var vMaterial = {
                    clMaterial: masterTableMaterial.getCellByColumnUniqueName(dataItemsMaterial[i], "CL_MATERIAL").innerHTML,
                    nbMaterial: masterTableMaterial.getCellByColumnUniqueName(dataItemsMaterial[i], "NB_MATERIAL").innerHTML,
                    mnMaterial: masterTableMaterial.getCellByColumnUniqueName(dataItemsMaterial[i], "MN_MATERIAL").innerHTML
                };
                vMateriales.push(vMaterial);
            }

            var material = JSON.stringify(vMaterial);
            var competencia = JSON.stringify(vCompetencias);

            var vDato = {
                clTema: clTema.get_value(),
                nbTema: nbTema.get_value(),
                duracion: duracion.get_value(),
                descripcion: descripcion.get_value(),
                vLstCompetencia: competencia,
                vLstMaterial: material,
                clTipoCatalogo: "TEMA"
            };

            vDatos.push(vDato);
            sendDataToParent(vDatos);

        }

        function OpenSelectionWindow(sender, args) {
            var vBtnGuardarCompetencia = $find("<%= rpvVentanaCompetencia.FindControl("radBtnGuardarCompetencia").ClientID %>");
            var vBtnGuradarMaterial = $find("<%= rpvVentanaCompetencia.FindControl("radBtnGuardarMaterial").ClientID %>");

            if (sender == vBtnGuardarCompetencia)
                openChildDialog("../Comunes/SeleccionCompetencia.aspx", "winSeleccionCompetencia", "Selección de competencia");

            if (sender == vBtnGuradarMaterial)
                openChildDialog("VentanaTemaMateriales.aspx", "winMaterial", "Agregar material");
        }

        function useDataFromChild(pData) {
            if (pData != null) {
                var vSelectedData = pData[0];
                var list;
                switch (vSelectedData.clTipoCatalogo) {
                    case "COMPETENCIA":
                        var arrCompetencia = [];
                        for (var i = 0; i < pData.length; i++)
                            arrCompetencia.push({
                                //ID_INSTRUCTOR_COMPETENCIA : 0,
                                ID_COMPETENCIA: pData[i].idCompetencia,
                                CL_COMPETENCIA: pData[i].clCompetencia,
                                NB_COMPETENCIA: pData[i].nbCompetencia,
                                CL_TIPO_COMPETENCIA: pData[i].clTipoCompetencia
                            });
                        var datosCompetencia = JSON.stringify(arrCompetencia);
                        __doPostBack(vSelectedData.clTipoCatalogo, datosCompetencia);
                        break;
                    case "MATERIAL":
                        var arrMaterial = [];
                        arrMaterial.push({
                            //ID_INSTRUCTOR_COMPETENCIA : 0,
                            NB_MATERIAL: vSelectedData.nbConcepto,
                            MN_MATERIAL: vSelectedData.mnCosto
                        });
                        var datosMaterial = JSON.stringify(arrMaterial);
                        __doPostBack(vSelectedData.clTipoCatalogo, datosMaterial);
                        break;
                }
            }
        }

        function closeWindow() {
            //   GetRadWindow().close();
            var vDatos = [];
            var vDato = {
                clTipoCatalogo: "TEMA"
            };

            vDatos.push(vDato);
            sendDataToParent(vDatos);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="rmpVentanaTema" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab Text="Tema"></telerik:RadTab>
            <telerik:RadTab Text="Competencias"></telerik:RadTab>
            <telerik:RadTab Text="Materiales"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <div style="height: calc(100% - 100px); padding: 10px 10px 10px 10px;">
        <telerik:RadMultiPage ID="rmpVentanaTema" runat="server" SelectedIndex="0">
            <telerik:RadPageView ID="rpvVentanaTema" runat="server">
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblNo" name="lblNo" runat="server">No.:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtcl_tema" runat="server" Width="80px" MaxLength="20"></telerik:RadTextBox>
                        <asp:RequiredFieldValidator CausesValidation="True" ValidationGroup="ValTema" Display="Dynamic" CssClass="validacion" ID="RequiredFieldValidator4" runat="server" Font-Names="Arial" Font-Size="Small" ControlToValidate="txtcl_tema" ErrorMessage="Campo Obligatorio"></asp:RequiredFieldValidator>

                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblTema" name="lblTema" runat="server">Tema:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtTema" runat="server" Width="250px" MaxLength="512"></telerik:RadTextBox>
                        <asp:RequiredFieldValidator CausesValidation="True" ValidationGroup="ValTema" Display="Dynamic" CssClass="validacion" ID="RequiredFieldValidator1" runat="server" Font-Names="Arial" Font-Size="Small" ControlToValidate="txtTema" ErrorMessage="Campo Obligatorio"></asp:RequiredFieldValidator>

                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblDuracion" name="lblDuracion" runat="server">Duración:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadNumericTextBox ID="txtDuracion" MinValue="1" DataType="Decimal" runat="server" Width="150px" MaxLength="13" NumberFormat-DecimalDigits="0" ShowSpinButtons="true"></telerik:RadNumericTextBox>
                        <asp:RequiredFieldValidator CausesValidation="True" ValidationGroup="ValTema" Display="Dynamic" CssClass="validacion" ID="RequiredFieldValidator2" runat="server" Font-Names="Arial" Font-Size="Small" ControlToValidate="txtDuracion" ErrorMessage="Campo Obligatorio"></asp:RequiredFieldValidator>

                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblDescripcion" name="lblDescripcion" runat="server">Descripción:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtDescripcion" runat="server" Width="450px" Height="130px" TextMode="MultiLine" MaxLength="2048"></telerik:RadTextBox>
                    </div>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="rpvVentanaCompetencia" runat="server">
                <telerik:RadGrid ID="grdTemaCompetencia" runat="server" ShowHeader="true" AllowPaging="false"
                    AllowSorting="true" GroupPanelPosition="Top" Width="605px" GridLines="None"
                    Height="290px" HeaderStyle-Font-Bold="true"
                    AllowFilteringByColumn="true"
                    ClientSettings-EnablePostBackOnRowClick="false" OnNeedDataSource="grdTemaCompetencia_NeedDataSource">
                    <GroupingSettings CaseSensitive="false" />
                    <ClientSettings AllowKeyboardNavigation="true">
                        <Selecting AllowRowSelect="true" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="false" />
                    <MasterTableView ClientDataKeyNames="ID_ITEM" DataKeyNames="ID_COMPETENCIA" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" PageSize="10"
                        HorizontalAlign="NotSet" EditMode="EditForms">
                        <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="false" ShowExportToCsvButton="false"
                            RefreshText="Actualizar" AddNewRecordText="Insertar" />
                        <Columns>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="id" DataField="ID_COMPETENCIA" UniqueName="ID_COMPETENCIA" HeaderStyle-Width="0" FilterControlWidth="80"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave" DataField="CL_TIPO_COMPETENCIA" UniqueName="CL_TIPO_COMPETENCIA" HeaderStyle-Width="175" FilterControlWidth="100"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre" DataField="NB_COMPETENCIA" UniqueName="NB_COMPETENCIA" FilterControlWidth="225"></telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                <div style="clear: both; height: 10px;"></div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="radBtnGuardarCompetencia" OnClientClicked="OpenSelectionWindow" AutoPostBack="false" runat="server" Text="Agregar" Width="100"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="radBtnEliminaCompetencia" OnClick="BtnEliminaCompetencia_Click" AutoPostBack="true" runat="server" Text="Eliminar" Width="100"></telerik:RadButton>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="rpvVentanaMateriales" runat="server">
                <telerik:RadGrid ID="grdTemaMaterial" runat="server" ShowHeader="true" AllowPaging="false"
                    AllowSorting="true" GroupPanelPosition="Top" Width="605px" GridLines="None"
                    Height="290px" HeaderStyle-Font-Bold="true"
                    AllowFilteringByColumn="true"
                    ClientSettings-EnablePostBackOnRowClick="false" OnNeedDataSource="grdTemaMaterial_NeedDataSource">
                    <GroupingSettings CaseSensitive="false" />
                    <ClientSettings AllowKeyboardNavigation="true">
                        <Selecting AllowRowSelect="true" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="false" />
                    <MasterTableView ClientDataKeyNames="ID_ITEM" DataKeyNames="ID_ITEM" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" PageSize="10" HorizontalAlign="NotSet" EditMode="EditForms">
                        <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="false" ShowExportToCsvButton="false" RefreshText="Actualizar" AddNewRecordText="Insertar" />
                        <Columns>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="No." DataField="CL_MATERIAL" UniqueName="CL_MATERIAL" HeaderStyle-Width="100" FilterControlWidth="15"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre" DataField="NB_MATERIAL" UniqueName="NB_MATERIAL" HeaderStyle-Width="340" FilterControlWidth="250"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Costo" DataField="MN_MATERIAL" UniqueName="MN_MATERIAL" FilterControlWidth="70" DataFormatString="{0:C2}">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                <div style="clear: both; height: 10px;"></div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="radBtnGuardarMaterial" OnClientClicked="OpenSelectionWindow" AutoPostBack="false" runat="server" Text="Agregar" Width="100"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="radBtnEliminarMaterial" OnClick="radBtnEliminarMaterial_Click" AutoPostBack="true" runat="server" Text="Eliminar" Width="100"></telerik:RadButton>
                </div>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </div>
    <div style="clear: both; height: 5px;"></div>
    <div class="divControlDerecha">
        <div class="ctrlBasico">
            <telerik:RadButton ID="radBtnGuardar" AutoPostBack="true" runat="server" Text="Aceptar" Width="100" OnClick="radBtnGuardar_Click"></telerik:RadButton>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="radBtnCancelar" AutoPostBack="false" runat="server" Text="Cancelar" Width="100" OnClientClicked="closeWindow"></telerik:RadButton>
        </div>
    </div>

    <telerik:RadWindowManager ID="rwmAlertas" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="winSeleccionCompetencia" runat="server" Title="Seleccionar competencia" Height="620px" Width="880px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
</asp:Content>
