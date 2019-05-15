<%@ Page Title="" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="AnalisisCompetencias.aspx.cs" Inherits="SIGE.WebApp.IDP.AnalisisCompetencias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div style="clear: both; height: 10px;"></div>

    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="lblNbIdioma" name="lblNbIdioma" runat="server">Puesto a cubrir:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadTextBox ID="txtClCatalogo" runat="server" Width="250px" MaxLength="1000"></telerik:RadTextBox>
        </div>
    </div>

    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="Label1" name="lblNbIdioma" runat="server">Candidato:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadTextBox ID="RadTextBox1" runat="server" Width="250px" MaxLength="1000"></telerik:RadTextBox>
        </div>
    </div>

    <telerik:RadTabStrip runat="server" ID="rtsReportes" MultiPageID="rmpReportes" Width="100%" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" Text="Análisis de perfil" TabIndex="0"></telerik:RadTab>
            <telerik:RadTab runat="server" Text="Análisis de competencias" TabIndex="1"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <div style="height: calc(100% - 100px); width: 100%;">
        <telerik:RadMultiPage runat="server" ID="rmpReportes" SelectedIndex="0" Width="100%" Height="100%">

            <telerik:RadPageView ID="rpvPerfil" runat="server" TabIndex="0">

                <div style="clear: both;" />

                <table>
                    <tr>
                        <td>
                            <div class="ctrlBasico">
                                <div class="divControlIzquierda">
                                    <label id="Label2" name="lblNbIdioma" runat="server">Edad:</label>
                                </div>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="RadTextBox2" runat="server" Width="250px" MaxLength="1000"></telerik:RadTextBox>
                                </div>
                            </div>
                        </td>
                        <td>
                            <div class="ctrlBasico">
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="RadTextBox3" runat="server" Width="250px" MaxLength="1000"></telerik:RadTextBox>
                                </div>
                            </div>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <div class="ctrlBasico">
                                <div class="divControlIzquierda">
                                    <label id="Label3" name="lblNbIdioma" runat="server">Género:</label>
                                </div>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="RadTextBox4" runat="server" Width="250px" MaxLength="1000"></telerik:RadTextBox>

                                </div>
                            </div>
                        </td>

                        <td>
                            <div class="ctrlBasico">
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="RadTextBox5" runat="server" Width="250px" MaxLength="1000"></telerik:RadTextBox>
                                </div>
                            </div>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <div class="ctrlBasico">
                                <div class="divControlIzquierda">
                                    <label id="Label4" name="lblNbIdioma" runat="server">Estado civil:</label>
                                </div>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="RadTextBox6" runat="server" Width="250px" MaxLength="1000"></telerik:RadTextBox>
                                </div>
                            </div>

                        </td>

                        <td>
                            <div class="ctrlBasico">
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="RadTextBox7" runat="server" Width="250px" MaxLength="1000"></telerik:RadTextBox>
                                </div>
                            </div>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <div class="ctrlBasico">
                                <div class="divControlIzquierda">
                                    <label id="Label5" name="lblNbIdioma" runat="server">Postgrado:</label>
                                </div>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="RadTextBox8" runat="server" Width="250px" MaxLength="1000"></telerik:RadTextBox>
                                </div>
                            </div>
                        </td>

                        <td>
                            <div class="ctrlBasico">
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="RadTextBox9" runat="server" Width="250px" MaxLength="1000"></telerik:RadTextBox>
                                </div>
                            </div>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <div class="ctrlBasico">
                                <div class="divControlIzquierda">
                                    <label id="Label6" name="lblNbIdioma" runat="server">Carrera Profesional:</label>
                                </div>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="RadTextBox10" runat="server" Width="250px" MaxLength="1000"></telerik:RadTextBox>

                                </div>
                            </div>
                        </td>

                        <td>
                            <div class="ctrlBasico">
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="RadTextBox11" runat="server" Width="250px" MaxLength="1000"></telerik:RadTextBox>
                                </div>
                            </div>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <div class="ctrlBasico">
                                <div class="divControlIzquierda">
                                    <label id="Label7" name="lblNbIdioma" runat="server">Carrera Técnica:</label>
                                </div>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="RadTextBox12" runat="server" Width="250px" MaxLength="1000"></telerik:RadTextBox>
                                </div>
                            </div>
                        </td>

                        <td>
                            <div class="ctrlBasico">
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="RadTextBox13" runat="server" Width="250px" MaxLength="1000"></telerik:RadTextBox>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>

            </telerik:RadPageView>

            <telerik:RadPageView ID="rpvCompetencia" runat="server" TabIndex="1">

                <telerik:RadGrid ID="grdCompetencias" ShowHeader="true" runat="server" AllowPaging="true" AllowSorting="true" Width="100%" Height="100%"
                    GridLines="None" AllowFilteringByColumn="true" ClientSettings-EnablePostBackOnRowClick="false" OnNeedDataSource="grdCompetencias_NeedDataSource" OnItemDataBound="grdCompetencias_ItemDataBound">
                     <GroupingSettings CaseSensitive="false" />
                    <ClientSettings AllowKeyboardNavigation="true">
                        <Selecting AllowRowSelect="true" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="true" />
                    <MasterTableView ClientDataKeyNames="ID_COMPETENCIA" DataKeyNames="ID_COMPETENCIA" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" PageSize="10" HorizontalAlign="NotSet">
                        <Columns>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Competencia" DataField="NB_COMPETENCIA" UniqueName="NB_COMPETENCIAS" HeaderStyle-Width="100" FilterControlWidth="30"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Descripción" DataField="DS_COMPETENCIA" UniqueName="DS_COMPETENCIA" HeaderStyle-Width="400" FilterControlWidth="330"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Grado de compatibilidad" DataField="" UniqueName="" HeaderStyle-Width="0" FilterControlWidth="0"></telerik:GridBoundColumn>
                        </Columns>

                    </MasterTableView>
                </telerik:RadGrid>


            </telerik:RadPageView>

        </telerik:RadMultiPage>
    </div>
</asp:Content>
