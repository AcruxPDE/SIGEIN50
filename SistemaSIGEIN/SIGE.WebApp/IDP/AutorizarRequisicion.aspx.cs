using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIGE.WebApp.Administracion
{
    public partial class VentanaAutorizarRequisicion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            radEditorAutorizar.Content = " <html>" +
                                          "<head> "+
                                         "<title>Hola mundo</title>"+
                                          "<meta charset=\"utf-8\">"+
                                          "</head>"+
                                          "<body>"+
                                          "<p>Estimado (a): __________________________________________	</p>"+
                                          "<p> Por medio del presente te informamos que _____________ creó una requisición de personal con  <br>"+
                                          "     la clave ________  para cubrir el puesto _______________ por ___________; para autorizar esta  <br>"+
                                           "    vacante y que el área de RR HH inicie el proceso de reclutamiento y selección, por favor da  <br>"+
                                           "    click en el siguiente enlace:  <br> <br>"+

                                          "<a href=\"https://www.google.com.mx\">link</a>	"+																									
                                          "</p>"+
                                           "<div style=\"float:left;\">¡Gracias! <br>	"+
                                        "Departamento de RR HH</div>"+
                                         " </body>"+
                                         " </html>";
        }
    }
}