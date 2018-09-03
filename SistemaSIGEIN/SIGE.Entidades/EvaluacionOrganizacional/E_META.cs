using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.EvaluacionOrganizacional
{
    [Serializable]
    public class E_META
    {
        public int ID_EVALUADO_FUNCION { get; set; }
        public string NB_FUNCION { get; set; }
        public int ID_EVALUADO { get; set; }
        public int ID_PERIODO { get; set; }
        public string NB_PERIODO { get; set; }
        public int ID_EVALUADO_META { get; set; }
        public string NB_INDICADOR { get; set; }
        public string NO_META { get; set; }
        public string DS_META { get; set; }
        public string DS_FUNCION { get; set; }
        public string CL_TIPO_META { get; set; }
        public bool FG_EVALUAR { get; set; }
        public bool FG_VALIDA_CUMPLIMIENTO { get; set; }
        public string NB_CUMPLIMIENTO_ACTUAL { get; set; }
        public string NB_CUMPLIMIENTO_MINIMO { get; set; }
        public string NB_CUMPLIMIENTO_SATISFACTORIO { get; set; }
        public string NB_CUMPLIMIENTO_SOBRESALIENTE { get; set; }
        public Nullable<decimal> PR_META { get; set; }
        public Nullable<decimal> PR_RESULTADO { get; set; }
        public Nullable<decimal> PR_CUMPLIMIENTO { get; set; }
        public string COLOR_NIVEL { get; set; }


        public bool FG_PORCENTUAL
        {
            get { return CL_TIPO_META == "Porcentual" ? true : false; }
        }
        public bool FG_MONTO
        {
            get { return CL_TIPO_META == "Monto" ? true : false; }
        }

        public bool FG_FECHA
        {
            get { return CL_TIPO_META == "Fecha" ? true : false; }
        }

        public bool SINO
        {
            get { return CL_TIPO_META == "Si/No" ? true : false; }
        }

        public decimal PR_CUMPLIMIENTO_ACTUAL
        {
            get { return CL_TIPO_META == "Porcentual" ? decimal.Parse(NB_CUMPLIMIENTO_ACTUAL) : decimal.Zero; }
        }

        public decimal PR_CUMPLIMIENTO_MINIMO
        {
            get { return CL_TIPO_META == "Porcentual" ? decimal.Parse(NB_CUMPLIMIENTO_MINIMO) : decimal.Zero; }
        }

        public decimal PR_CUMPLIMIENTO_SATISFACTORIO
        {
            get { return CL_TIPO_META == "Porcentual" ? decimal.Parse(NB_CUMPLIMIENTO_SATISFACTORIO) : decimal.Zero; }
        }

        public decimal PR_CUMPLIMIENTO_SOBRESALIENTE
        {
            get { return CL_TIPO_META == "Porcentual" ? decimal.Parse(NB_CUMPLIMIENTO_SOBRESALIENTE) : decimal.Zero; }
        }

        public decimal MN_CUMPLIMIENTO_ACTUAL
        {
            get { return CL_TIPO_META == "Monto" ? decimal.Parse(NB_CUMPLIMIENTO_ACTUAL) : decimal.Zero; }
        }

        public decimal MN_CUMPLIMIENTO_MINIMO
        {
            get { return CL_TIPO_META == "Monto" ? decimal.Parse(NB_CUMPLIMIENTO_MINIMO) : decimal.Zero; }
        }

        public decimal MN_CUMPLIMIENTO_SATISFACTORIO
        {
            get { return CL_TIPO_META == "Monto" ? decimal.Parse(NB_CUMPLIMIENTO_SATISFACTORIO) : decimal.Zero; }
        }

        public decimal MN_CUMPLIMIENTO_SOBRESALIENTE
        {
            get { return CL_TIPO_META == "Monto" ? decimal.Parse(NB_CUMPLIMIENTO_SOBRESALIENTE) : decimal.Zero; }
        }

        public DateTime? FE_CUMPLIMIENTO_ACTUAL
        {
            get
            {
                if (CL_TIPO_META == "Fecha")
                {
                    return DateTime.Parse(NB_CUMPLIMIENTO_ACTUAL);
                }
                else
                {
                    return null;
                }
            }
        }

        public DateTime? FE_CUMPLIMIENTO_MINIMO {
            get {
                if (CL_TIPO_META == "Fecha")
                {
                    return DateTime.Parse(NB_CUMPLIMIENTO_MINIMO);
                }
                else
                {
                    return null;
                }
            }
        }

        public DateTime? FE_CUMPLIMIENTO_SATISFACTORIO {
            get
            {
                if (CL_TIPO_META == "Fecha")
                {
                    return DateTime.Parse(NB_CUMPLIMIENTO_SATISFACTORIO);
                }
                else
                {
                    return null;
                }
            }
        }

        public DateTime? FE_CUMPLIMIENTO_SOBRESALIENTE {
            get
            {
                if (CL_TIPO_META == "Fecha")
                {
                    return DateTime.Parse(NB_CUMPLIMIENTO_SOBRESALIENTE);
                }
                else
                {
                    return null;
                }
            }
        }


        //private void AsignarValores()
        //{
        //    switch (CL_TIPO_META)
        //    {
        //        case "PORCENTUAL":

        //            //PR_CUMPLIMIENTO_ACTUAL = NB_CUMPLIMIENTO_ACTUAL != null ? decimal.Parse(NB_CUMPLIMIENTO_ACTUAL) : 0;
        //            //PR_CUMPLIMIENTO_MINIMO = NB_CUMPLIMIENTO_MINIMO != null ? decimal.Parse(NB_CUMPLIMIENTO_MINIMO) : 0;
        //            //PR_CUMPLIMIENTO_SATISFACTORIO = NB_CUMPLIMIENTO_SATISFACTORIO != null ? decimal.Parse(NB_CUMPLIMIENTO_SATISFACTORIO) : 0;
        //            //PR_CUMPLIMIENTO_SOBRESALIENTE = NB_CUMPLIMIENTO_SOBRESALIENTE != null ? decimal.Parse(NB_CUMPLIMIENTO_SOBRESALIENTE) : 0;

        //            break;

        //        case "MONTO":
        //            MN_CUMPLIMIENTO_ACTUAL = NB_CUMPLIMIENTO_ACTUAL != null ? decimal.Parse(NB_CUMPLIMIENTO_ACTUAL) : 0;
        //            MN_CUMPLIMIENTO_MINIMO = NB_CUMPLIMIENTO_MINIMO != null ? decimal.Parse(NB_CUMPLIMIENTO_MINIMO) : 0;
        //            MN_CUMPLIMIENTO_SATISFACTORIO = NB_CUMPLIMIENTO_SATISFACTORIO != null ? decimal.Parse(NB_CUMPLIMIENTO_SATISFACTORIO) : 0;
        //            MN_CUMPLIMIENTO_SOBRESALIENTE = NB_CUMPLIMIENTO_SOBRESALIENTE != null ? decimal.Parse(NB_CUMPLIMIENTO_SOBRESALIENTE) : 0;
        //            break;

        //        case "FECHA":

        //            if (NB_CUMPLIMIENTO_ACTUAL != null)
        //            {
        //                FE_CUMPLIMIENTO_ACTUAL = DateTime.Parse(NB_CUMPLIMIENTO_ACTUAL);
        //            }
        //            else
        //            {
        //                FE_CUMPLIMIENTO_ACTUAL = null;
        //            }

        //            if (NB_CUMPLIMIENTO_MINIMO != null)
        //            {
        //                FE_CUMPLIMIENTO_MINIMO = DateTime.Parse(NB_CUMPLIMIENTO_MINIMO);
        //            }
        //            else
        //            {
        //                FE_CUMPLIMIENTO_MINIMO = null;
        //            }

        //            if (NB_CUMPLIMIENTO_SATISFACTORIO != null)
        //            {
        //                FE_CUMPLIMIENTO_SATISFACTORIO = DateTime.Parse(NB_CUMPLIMIENTO_SATISFACTORIO);
        //            }
        //            else
        //            {
        //                FE_CUMPLIMIENTO_SATISFACTORIO = null;
        //            }


        //            if (NB_CUMPLIMIENTO_SOBRESALIENTE != null)
        //            {
        //                FE_CUMPLIMIENTO_SOBRESALIENTE = DateTime.Parse(NB_CUMPLIMIENTO_SOBRESALIENTE);
        //            }
        //            else
        //            {
        //                FE_CUMPLIMIENTO_SOBRESALIENTE = null;
        //            }
        //            break;
        //        default:
        //            break;
        //    }
        //}

        public E_META()
        {
            
        }

    }
}
