using SIGE.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.Negocio.Utilerias
{
    public class Utilerias
    {

        //private Enum 

        public DataTable ConvertToDataTable<T>(IList<T> data, bool rotateData)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }
                table.Rows.Add(row);
            }

            if (rotateData)
            {
                return RotateData(table);
            }
            else
            {
                return table;
            }

        }

        public DataTable RotateData(DataTable dtOrigen)
        {

            DataTable dtDestino = new DataTable();

            for (int i = 0; i <= dtOrigen.Rows.Count - 1; i++)
            {
                dtDestino.Columns.Add(dtOrigen.Rows[i][0].ToString());
            }

            for (int i = 0; i < dtOrigen.Columns.Count - 1; i++)
            {
                dtDestino.Rows.Add();
            }

            for (int i = 1; i <= dtOrigen.Columns.Count - 1; i++)
            {
                for (int j = 0; j <= dtOrigen.Rows.Count - 1; j++)
                {
                    dtDestino.Rows[i - 1][j] = dtOrigen.Rows[j][i];
                }
            }

            return dtDestino;
        }


        //public DataTable PivotData(string RowField, string DataField, AggregateFunction Aggregate, params string[] ColumnFields)
        //{
        //    DataTable dt = new DataTable();
        //    string Separator = ".";
        //    var RowList = (from x in _SourceTable.AsEnumerable()
        //                   select new { Name = x.Field<object>(RowField) }).Distinct();
        //    var ColList = (from x in _SourceTable.AsEnumerable()
        //                   select new
        //                   {
        //                       Name = ColumnFields.Select(n => x.Field<object>(n))
        //                           .Aggregate((a, b) => a += Separator + b.ToString())
        //                   })
        //                       .Distinct()
        //                       .OrderBy(m => m.Name);

        //    dt.Columns.Add(RowField);
        //    foreach (var col in ColList)
        //    {
        //        dt.Columns.Add(col.Name.ToString());
        //    }

        //    foreach (var RowName in RowList)
        //    {
        //        DataRow row = dt.NewRow();
        //        row[RowField] = RowName.Name.ToString();
        //        foreach (var col in ColList)
        //        {
        //            string strFilter = RowField + " = '" + RowName.Name + "'";
        //            string[] strColValues =
        //              col.Name.ToString().Split(Separator.ToCharArray(),
        //                                        StringSplitOptions.None);
        //            for (int i = 0; i < ColumnFields.Length; i++)
        //                strFilter += " and " + ColumnFields[i] +
        //                             " = '" + strColValues[i] + "'";
        //            row[col.Name.ToString()] = GetData(strFilter, DataField, Aggregate);
        //        }
        //        dt.Rows.Add(row);
        //    }
        //    return dt;
        //}

    }
}
