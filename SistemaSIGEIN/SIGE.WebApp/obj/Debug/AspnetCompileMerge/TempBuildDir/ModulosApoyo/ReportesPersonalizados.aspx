﻿<%@ Page Language="C#" MasterPageFile="MenuMA.Master" AutoEventWireup="true" CodeBehind="ReportesPersonalizados.aspx.cs" Inherits="SIGE.WebApp.ModulosApoyo.ReportesPersonalizados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadPageLayout runat="server" ID="rplEO" GridType="Fluid" ShowGrid="true" HtmlTag="None">
        <telerik:LayoutRow RowType="Generic">
            <Rows>
                <telerik:LayoutRow>
                    <Content>
                        <div style="float: left; padding-left: 20px;">
                                <h3>REPORTES PERSONALIZADOS</h3>
                        </div>
                    </Content>
                </telerik:LayoutRow>
                <telerik:LayoutRow>
                    <Content>
                        <div class="col-xs-12 col-lg-6 col-md-6 col-sm-6" style="margin-top: 100px;">
                            <div style="padding-left: 20px;">
                                <h3><b style="color: #FF7400;">Bienvenido(a) al sistema</b> </h3>
                                <b><%= vNbUsuario %></b>
                                <br />
                                <br />
                                Bienvenido(a) a la aplicación <b>SIGEIN</b>.
                                    <br />
                                Para comenzar a utilizar el sistema, utiliza el menú de navegación en el panel superior. 
                               <div style="padding-left:20px; padding-top:20px;">
                               <telerik:RadBinaryImage ID="rbiLogoOrganizacion1" runat="server" Width="108" Height="108"  ResizeMode="Fit" />
                               </div>
                            </div>
                        </div>
                        <div class="col-xs-12 col-lg-6 col-md-6 col-sm-6" style="margin-top: 50px;">
                            <div style="position: relative; overflow: hidden;">
                                <img id="imgReportesPersonalizados" src="../Assets/images/menu/ReportesPersonalizados600x344.png" class="img-responsive" />
                            </div>
                        </div>
                    </Content>
                </telerik:LayoutRow>
            </Rows>
        </telerik:LayoutRow>
    </telerik:RadPageLayout>
</asp:Content>