<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="VentanaContenedorPruebas.aspx.cs" Inherits="SIGE.WebApp.IDP.VentanaContenedorPruebas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">

    <div style="border-bottom: 1px solid #ddd;padding-top: 15px;">
        <div class="ctrlBasico">
            <div class="ctrlBasico">
                <div class="divControlIzquierda">
                    <label id="lblFolio" name="lblFolio">Folio:</label>
                </div>
                <div class="divControlDerecha">
                    <telerik:RadTextBox  runat="server" ID="txtFolio" ReadOnly="true" />
                </div>
            </div>
            <div class="ctrlBasico">
                <div class="divControlIzquierda">
                    <label id="lblNombre" name="lblNombre">Nombre:</label>
                </div>
                <div class="divControlDerecha">
                    <telerik:RadTextBox runat="server" ID="txtNombre" ReadOnly="true" Width="350px" />
                </div>
            </div>
            <div style="clear: both"></div>
        </div>

        <div style="clear: both"></div>
    </div>
    <div style="clear:both;height:10px"></div>
    <div id="contenidoPruebas">

        <div class="divPrueba">
            <label id="lblPersonalidadLab1" name="lblPersonalidadLab1">Personalidad laboral I</label>
            <img src="../Assets/images/Eliminar.png" alt="Eliminar" width="17px" />
        </div>
        
        <div class="divPrueba">
            <label id="lblInteresesPersonales" name="lblInteresesPersonales">Intereses personales</label>
            <img src="../Assets/images/Eliminar.png" alt="Eliminar" width="17px" />
        </div>
        

        <div class="divPrueba">
            <label id="lblEstiloPensamiento" name="lblEstiloPensamiento">Estilo de pensamiento</label>
            <img src="../Assets/images/Eliminar.png" alt="Eliminar" width="17px" />
        </div>
        

        <div class="divPrueba">
            <label id="lblAptitupMental" name="lblAptitupMental">Aptitud mental I</label>
            <img src="../Assets/images/Eliminar.png" alt="Eliminar" width="17px" />
        </div>
        
        <div class="divPrueba">
            <label id="lblAptitupMentalII" name="lblAptitupMentalII">Aptitud mental II</label>
            <img src="../Assets/images/Eliminar.png" alt="Eliminar" width="17px" />
        </div>

        <div class="divPrueba">
            <label id="lblPersonalidadLaboralII" name="lblPersonalidadLaboralII">Personalidad laboral II</label>
            <img src="../Assets/images/Eliminar.png" alt="Eliminar" width="17px" />
        </div>


        <div class="divPrueba">
            <label id="lblAdaptacionMiedo" name="lblAdaptacionMiedo">Adaptación al medio</label>
            <img src="../Assets/images/Eliminar.png" alt="Eliminar" width="17px" />
        </div>

        <div class="divPrueba">
            <label id="lblTiva" name="lblTiva">TIVA</label>
            <img src="../Assets/images/Eliminar.png" alt="Eliminar" width="17px" />
        </div>


        <div class="divPrueba">
            <label id="lblOrtografiaI" name="lblOrtografiaI">Ortografía I</label>
            <img src="../Assets/images/Eliminar.png" alt="Eliminar" width="17px" />
        </div>


        <div class="divPrueba">
            <label id="lblOrtografiaII" name="lblOrtografiaII">Ortografía II</label>
            <img src="../Assets/images/Eliminar.png" alt="Eliminar" width="17px" />
        </div>


         <div class="divPrueba">
            <label id="lblOrtografiaIII" name="lblOrtografiaIII">Ortografía III</label>
            <img src="../Assets/images/Eliminar.png" alt="Eliminar" width="17px" />
        </div>


        <div class="divPrueba">
            <label id="lblTecnicaPC" name="lblTecnicaPC">Técnica (PC)</label>
            <img src="../Assets/images/Eliminar.png" alt="Eliminar" width="17px" />
        </div>


        <div class="divPrueba">
            <label id="lblRedaccion" name="lblRedaccion">Redacción</label>
            <img src="../Assets/images/Eliminar.png" alt="Eliminar" width="17px" />
        </div>


        <div class="divPrueba" >
            <label id="lblPruebaIngles" name="lblPruebaIngles">Prueba de inglés</label>
            <img src="../Assets/images/Eliminar.png" alt="Eliminar" width="17px" />
        </div>


        <div style="clear:both"></div>        

    </div>

</asp:Content>
