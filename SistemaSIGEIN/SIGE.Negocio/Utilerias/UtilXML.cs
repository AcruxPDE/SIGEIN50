using SIGE.Entidades.Externas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.Negocio.Utilerias
{
    public static class UtilXML
    {
        public static object ValorAtributo(XAttribute pAtributo, E_TIPO_DATO pTipoDato)
        {
            object oAtributo = null;

            if (pAtributo != null)
            {
                switch (pTipoDato)
                {
                    case E_TIPO_DATO.INT:
                        int intValue = -1;
                        if (int.TryParse(pAtributo.Value, out intValue))
                            oAtributo = intValue;
                        else
                            oAtributo = null;
                        break;

                    case E_TIPO_DATO.DECIMAL:
                        decimal decValue = -1;
                        if (decimal.TryParse(pAtributo.Value, out decValue))
                            oAtributo = decValue;
                        else
                            oAtributo = null;
                        break;

                    case E_TIPO_DATO.DATETIME:
                        DateTime temp;
                        if (DateTime.TryParse(pAtributo.Value, out temp))
                            oAtributo = temp;
                        else
                            oAtributo = null;
                        break;

                    case E_TIPO_DATO.BOOLEAN:
                        switch (pAtributo.Value)
                        {
                            case "1":
                            case "true":
                                oAtributo = true;
                                break;
                            case "0":
                            case "false":
                                oAtributo = false;
                                break;
                            default:
                                oAtributo = null;
                                break;
                        }
                        break;

                    case E_TIPO_DATO.STRING:
                        oAtributo = pAtributo.Value;
                        break;
                }
            }

            return oAtributo;
        }

        public static T ValorAtributo<T>(XAttribute pAtributo)
        {
            E_TIPO_DATO pTipoDato = E_TIPO_DATO.INT;

            Type vTipo = typeof(T);

            object oAtributo = null;
            if (vTipo == typeof(int) || vTipo == typeof(int?))
                pTipoDato = E_TIPO_DATO.INT;

            if (vTipo == typeof(decimal) || vTipo == typeof(decimal?))
                pTipoDato = E_TIPO_DATO.DECIMAL;

            if (vTipo == typeof(bool) || vTipo == typeof(bool?))
                pTipoDato = E_TIPO_DATO.BOOLEAN;

            if (vTipo == typeof(DateTime) || vTipo == typeof(DateTime?))
                pTipoDato = E_TIPO_DATO.DATETIME;

            if (vTipo == typeof(string))
                pTipoDato = E_TIPO_DATO.STRING;

            oAtributo = ValorAtributo(pAtributo, pTipoDato);

            if (!(vTipo.IsGenericType && vTipo.GetGenericTypeDefinition() == typeof(Nullable<>)) && oAtributo == null)
                oAtributo = default(T);

            return (T)oAtributo;
        }

        public static void AsignarValorAtributo(XElement pXmlNodo, string pNbAtributo, string pNbValor)
        {
            XAttribute vXmlValor = pXmlNodo.Attribute(pNbAtributo);
            if (vXmlValor == null)
                pXmlNodo.Add(new XAttribute(pNbAtributo, pNbValor ?? String.Empty));
            else
                vXmlValor.Value = pNbValor;
        }

        public static void AsignarValorCampo(XElement pXmlCampo, XElement pXmlValores)
        {
            XElement vXmlValor = pXmlValores.Elements("VALOR").FirstOrDefault(f => UtilXML.ValorAtributo<string>(f.Attribute("ID_CAMPO")) == UtilXML.ValorAtributo<string>(pXmlCampo.Attribute("ID_CAMPO")));
            //UtilXML.ValorAtributo<string>(pXmlCampo.Attribute("ID_CAMPO")));
            //XElement vXmlValor = pXmlValores.Element(UtilXML.ValorAtributo<string>(pXmlCampo.Attribute("ID_CAMPO")));
            switch (UtilXML.ValorAtributo<string>(pXmlCampo.Attribute("CL_TIPO")))
            {
                case "TEXTBOX":
                case "CHECKBOX":
                case "NUMERICBOX":
                case "MASKBOX":
                case "DATEPICKER":
                case "DATEAGE":
                    if (vXmlValor != null)
                        AsignarValorAtributo(pXmlCampo, "NB_VALOR", vXmlValor.Value);
                    break;
                case "TEXTBOXCP":
                    if (vXmlValor != null)
                        AsignarValorAtributo(pXmlCampo, "NB_VALOR", vXmlValor.Value);
                    break;
                case "COMBOBOX":
                    if (vXmlValor != null)
                        pXmlCampo.Add((vXmlValor != null) ? vXmlValor.Element("ITEMS") : new XElement("DATOS"));
                    break;
                case "LISTBOX":
                    if (vXmlValor != null)
                    {
                        string vValor = UtilXML.ValorAtributo<string>(vXmlValor.Attribute("NB_VALOR"));
                        AsignarValorAtributo(pXmlCampo, "CL_VALOR", vValor);

                        string vTexto = UtilXML.ValorAtributo<string>(vXmlValor.Attribute("NB_TEXTO"));
                        AsignarValorAtributo(pXmlCampo, "NB_VALOR", vTexto);
                    }
                    break;
                case "GRID":
                    if (vXmlValor != null)
                    {
                        if (vXmlValor.Element("DATOS") != null)
                            pXmlCampo.Element("GRID").Add(vXmlValor.Element("DATOS"));
                        else
                            pXmlCampo.Element("GRID").Add(new XElement("DATOS"));
                    }

                    foreach (XElement vXmlCampoGrid in pXmlCampo.Element("FORMULARIO").Elements("CAMPO"))
                        AsignarValorCampo(vXmlCampoGrid, pXmlValores);
                    break;
            }
        }
    }
}
