using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.AdministracionSitio;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.Administracion
{
    public partial class VentanaModificarEmpleados : System.Web.UI.Page
    {
        string usuario = "";
        string programa = "";
        string cliente = "";

        #region Propiedades

        public List<E_PLANTILLA_LAYOUT> Mensajes
        {
            get { return (List<E_PLANTILLA_LAYOUT>)ViewState["vs_MENSAJES"]; }
            set { ViewState["vs_MENSAJES"] = value; }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            usuario = ContextoUsuario.oUsuario.CL_USUARIO.ToString();
            programa = ContextoUsuario.nbPrograma.ToString();
            cliente = ContextoApp.Licencia.clCliente;
            if (!Page.IsPostBack)
            {
                Mensajes = new List<E_PLANTILLA_LAYOUT>();
            }
        }

        protected void btnValidar_Click(object sender, EventArgs e)
        {
            ModificarEmpleado(true);
        }

        protected void btnProcesar_Click(object sender, EventArgs e)
        {
            ModificarEmpleado(false);

        }

        protected void GridErrores_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            GridErrores.DataSource = Mensajes;
        }

        private int numeroLineasEnArchivo(byte[] archivo)
        {
            int no_lineas = 0;

            using (StreamReader readfile = new StreamReader(new MemoryStream(archivo)))
            {
                while (readfile.ReadLine() != null)
                {
                    no_lineas++;
                }
            }

            return no_lineas;
        }

        private void ModificarEmpleado(bool? EsValidacion)
        {
            if (RadAsyncUpload1.UploadedFiles.Count > 0)
            {
                int i = 0;
                int totalLineas;
                RadProgressContext progress = RadProgressContext.Current;

                Mensajes = new List<E_PLANTILLA_LAYOUT>();
                //vCampos = new List<E_CAMPOS_K_PLANTILLA>();


                byte[] archivo = new byte[RadAsyncUpload1.UploadedFiles[0].ContentLength];
                RadAsyncUpload1.UploadedFiles[0].InputStream.Read(archivo, 0, int.Parse(RadAsyncUpload1.UploadedFiles[0].ContentLength.ToString()));

                totalLineas = numeroLineasEnArchivo(archivo);
                progress.SecondaryTotal = totalLineas;
                progress.SecondaryPercent = ((i * 100) / totalLineas);
                progress.SecondaryValue = i;

                string line;
                string[] row;
                string[] campos = new string[100];
                bool tieneDatos = false;
                CamposNominaNegocio negocio = new CamposNominaNegocio();

                using (StreamReader readfile = new StreamReader(new MemoryStream(archivo)))
                {
                    XElement xml = new XElement("ARCHIVO");
                    XElement xmlLinea = new XElement("LINEA");
                    XElement xmlCampos = new XElement("CL_CAMPOS");

                    while ((line = readfile.ReadLine()) != null)
                    {
                        if (i == 0) //VALIDAR LA LONGITUD DEL ARCHIVO Y QUE EXISTAN TODOS LOS CONCEPTOS EN EL CATALOGO APARTIR DE LA POSICION 2
                        {
                            row = line.Split(',');
                            if (row.Length > 2 && totalLineas > 1)
                            {
                                tieneDatos = true;
                                for (int x = 2; x < row.Length; x++)
                                {
                                    campos[x - 2] = row[x];
                                }
                            }
                        }
                        if (i >= 1)
                        {
                            row = line.Split(',');

                            for (int x = 2; x < row.Length; x++)
                            {
                                xml.Add(new XElement("LINEA", new XElement("CL_RAZON_SOCIAL", row[0]),
                                new XElement("CL_TRABAJADOR", row[1]),
                                new XElement("CL_CAMPOS", campos[x - 2]),
                                new XElement("VALOR_CAMPO", row[x])
                                ));
                            }
                            i++;
                            progress.SecondaryValue = i;
                            progress.SecondaryPercent = ((i * 100) / totalLineas);
                        }
                        else
                        {
                            i++;
                            progress.SecondaryValue = i;
                            progress.SecondaryPercent = ((i * 100) / totalLineas);
                        }
                    }

                    if (tieneDatos)
                    {
                        List<E_PLANTILLA_LAYOUT> respuesta = negocio.Actualiza_K_PLANTILLA_layout(xml.ToString(), EsValidacion, usuario, programa);
                        //Mensajes.Clear();
                        //foreach (var mensaje in respuesta)
                        //{
                        //    Mensajes.Add(new E_PLANTILLA_LAYOUT
                        //    {
                        //        VALOR = mensaje.VALOR,
                        //        MENSAJE_RETORNO = mensaje.MENSAJE_RETORNO
                        //    });
                        //}
                    }
                    else
                    {
                        Mensajes.Clear();

                        Mensajes.Add(new E_PLANTILLA_LAYOUT
                        {
                            VALOR = "",
                            MENSAJE_RETORNO = "No existen datos para evaluar"
                        });

                    }
                }
                GridErrores.Rebind();
            }
        }
    }
}