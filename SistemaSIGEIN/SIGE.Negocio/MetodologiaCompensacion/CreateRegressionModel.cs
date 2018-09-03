using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Telerik.Web.UI;
using System.Drawing;
using System.Data;
using System.Web.UI;

namespace SIGE.Negocio.MetodologiaCompensacion
{
    public enum RegressionType
    {
        Linear = 0,
        Logarithmic = 1
    }

    public class CreateRegressionModel
    {
        public static void Plot(RadHtmlChart HtmlChart, DataTable DataSource, string DataFieldX, string DataFieldY, RegressionType RegressionModelType)
        {
            double[] estimators = OrdinaryLeastSquares(DataSource, DataFieldX, DataFieldY, RegressionModelType);
            double slope = estimators[0];
            double intercept = estimators[1];
            double rSquared = estimators[2];
            string estYFieldName = "__Yest";
            string estXFieldName = "__Xest";
            AddRegressionFields(DataSource, DataFieldX, DataFieldY, estXFieldName, estYFieldName, slope, intercept, RegressionModelType);

            string equationSeriesName = FormatStringEquation(RegressionModelType, slope, intercept, rSquared);

            AddRegressionSeries(DataSource, HtmlChart, estXFieldName, estYFieldName, equationSeriesName);
            HtmlChart.DataSource = DataSource;
            HtmlChart.DataBind();

        }

        private static double[] OrdinaryLeastSquares(DataTable dt, string xField, string yField, RegressionType regressionType)
        {
            List<string> auxiliaryFields = new List<string>();
            if (regressionType == RegressionType.Logarithmic)
            {
                dt.Columns.Add("__LnX", typeof(double));
                dt.Rows.Cast<DataRow>().ToList().ForEach(r => r.SetField("__LnX", Math.Log((double)r[xField])));
                xField = "__LnX";
            }

            dt.Columns.Add("__XY", typeof(double), string.Format("{0} * {1}", xField, yField));
            dt.Columns.Add("__X2", typeof(double), string.Format("{0} * {0}", xField));
            dt.Columns.Add("__Y2", typeof(double), string.Format("{0} * {0}", yField));
            auxiliaryFields.AddRange(new string[] { "__XY", "__X2", "__Y2" });

            double xSum = 0;
            double ySum = 0;
            double xAvg = 0;
            double yAvg = 0;
            double xySum = 0;
            double x2Sum = 0;
            double y2Sum = 0;

            var vxSum = dt.Compute(string.Format("SUM([{0}])", xField), "");
            var vySum = dt.Compute(string.Format("SUM([{0}])", yField), "");
            var vxAvg = dt.Compute(string.Format("AVG([{0}])", xField), "");
            var vyAvg = dt.Compute(string.Format("AVG([{0}])", yField), "");
            var vxySum = dt.Compute("SUM([__XY])", "");
            var vx2Sum = dt.Compute("SUM([__X2])", "");
            var vy2Sum = dt.Compute("SUM([__Y2])", "");
            if (vxSum != null)
            {
                if (!String.IsNullOrEmpty(vxSum.ToString()))
                {
                    xSum = (double)vxSum;
                }
            }
            if (vySum != null)
            {
                if (!String.IsNullOrEmpty(vySum.ToString()))
                {

                    ySum = (double)vySum;
                }
            }
            if (vxAvg != null)
            {
                if (!String.IsNullOrEmpty(vxAvg.ToString()))
                {

                    xAvg = (double)vxAvg;
                }
            }
            if (vyAvg != null)
            {
                if (!String.IsNullOrEmpty(vyAvg.ToString()))
                {

                    yAvg = (double)vyAvg;
                }
            }
            if (vxySum != null)
            {
                if (!String.IsNullOrEmpty(vxySum.ToString()))
                {

                    xySum = (double)vxySum;
                }
            }
            if (vx2Sum != null)
            {
                if (!String.IsNullOrEmpty(vx2Sum.ToString()))
                {

                    x2Sum = (double)vx2Sum;
                }
            }
            if (vy2Sum != null)
            {
                if (!String.IsNullOrEmpty(vy2Sum.ToString()))
                {

                    y2Sum = (double)vy2Sum;
                }
            }
            //double xSum = (double)dt.Compute(string.Format("SUM([{0}])", xField), "");
            //double ySum = (double)dt.Compute(string.Format("SUM([{0}])", yField), "");

            //double xAvg = (double)dt.Compute(string.Format("AVG([{0}])", xField), "");
            //double yAvg = (double)dt.Compute(string.Format("AVG([{0}])", yField), "");

            //double xySum = (double)dt.Compute("SUM([__XY])", "");
            //double x2Sum = (double)dt.Compute("SUM([__X2])", "");
            //double y2Sum = (double)dt.Compute("SUM([__Y2])", "");

            int n = dt.Rows.Count;

            double slope = (n * xySum - xSum * ySum) / (n * x2Sum - xSum * xSum);

            bool isNaN = Double.IsNaN(slope);
            if (!isNaN)
            {
                double intercept = yAvg - slope * xAvg;

                bool isNaNIntercept = Double.IsNaN(intercept);
                if (!isNaNIntercept)
                {
                    dt.Columns.Add("__SSTField", typeof(double), string.Format("({0} - {1}) * ({0} - {1})", yField, yAvg));
                    dt.Columns.Add("__Yest_Orig", typeof(double), string.Format("{0} * {1} + {2}", slope, xField, intercept));
                    dt.Columns.Add("__SSEField", typeof(double), string.Format("({0} - __Yest_Orig) * ({0} - __Yest_Orig)", yField));

                    double SST = (double)dt.Compute("SUM([__SSTField])", "");
                    double SSE = (double)dt.Compute("SUM([__SSEField])", "");
                    auxiliaryFields.AddRange(new string[] { "__SSTField", "__SSEField" });

                    double rSquarred = 1 - SSE / SST;
                    bool isNaNrSquarred = Double.IsNaN(rSquarred);
                    if (!isNaNrSquarred)
                    {
                        RemoveAuxiliaryFields(dt, auxiliaryFields);

                        return new double[] { slope, intercept, rSquarred };
                    }
                    else
                    {
                        return new double[] { slope, intercept, 0.0 };
                    }
                }
                else
                {
                    return new double[] { slope, 0.0, 0.0 };
                }
            }
            else
            {
                return new double[] { 0.0, 0.0, 0.0 };
            }
        }

        private static void RemoveAuxiliaryFields(DataTable dt, List<string> auxiliaryField)
        {
            foreach (string field in auxiliaryField)
            {
                dt.Columns.Remove(field);
            }
        }

        private static void AddRegressionFields(DataTable dt, string xField, string yField, string XestField, string YestField, double slope, double intercept, RegressionType RegressionModelType)
        {
            int n = dt.Rows.Count;
           
                double xMin = 0;
                double xMax = 0;
                double step = 0; 
                var vxMin = dt.Compute(string.Format("Min([{0}])", xField), "");
                var vxMax = dt.Compute(string.Format("Max([{0}])", xField), "");

                //double xMin = (double)dt.Compute(string.Format("Min([{0}])", xField), "");
                //double xMax = (double)dt.Compute(string.Format("Max([{0}])", xField), "");
                if (vxMin != null)
                {
                    if (!String.IsNullOrEmpty(vxMin.ToString()))
                    {
                        xMin = (double)vxMin;
                    }
                }
                if (vxMax != null)
                {
                    if (!String.IsNullOrEmpty(vxMax.ToString()))
                    {

                        xMax = (double)vxMax;
                    }
                }
                step = (xMax - xMin) / (n - 1);
                double cumulative = xMin;
                dt.Columns.Add(XestField, typeof(double));
                dt.Columns.Add(YestField, typeof(double));
                for (int i = 0; i < n; i++)
                {
                    if (RegressionModelType == RegressionType.Logarithmic)
                    {
                        dt.Rows[i][YestField] = Math.Log(cumulative) * slope + intercept;
                    }
                    else
                    {
                        dt.Rows[i][YestField] = cumulative * slope + intercept;

                    }
                    dt.Rows[i][XestField] = cumulative;

                    cumulative += step;
                }
            }

        private static string FormatStringEquation(RegressionType regressionType, double slope, double intercept, double rSquared)
        {
            string XName = "X";
            if (regressionType == RegressionType.Logarithmic)
            {
                XName = "Ln(X)";
            }

            return string.Format("Y = {0} * {3} + {1}\\nR-Squared: {2}", Math.Round(slope, 4), Math.Round(intercept, 4), Math.Round(rSquared, 4), XName);
        }

        private static void AddRegressionSeries(DataTable dt, RadHtmlChart chart, string xField, string yField, string seriesName)
        {
            ScatterLineSeries scatterLineSeries1 = new ScatterLineSeries();
            //scatterLineSeries1.DataFieldX = xField;
            //scatterLineSeries1.DataFieldY = yField;


            foreach (DataRow item in dt.Rows)
            {
                scatterLineSeries1.SeriesItems.Add(decimal.Parse(item["__Xest"].ToString()), decimal.Parse(item["__Yest"].ToString()));
            }
            scatterLineSeries1.Name = "Tendencia";
            scatterLineSeries1.LabelsAppearance.Visible = false;
            scatterLineSeries1.MarkersAppearance.Visible = false;
            chart.PlotArea.Series.Add(scatterLineSeries1);


            //vPrimerScatterSeries.SeriesItems.Add(iEmp.NO_NIVEL, iEmp.MN_SUELDO_ORIGINAL);
            //vPrimerScatterSeries.LabelsAppearance.Visible = false;
            //vPrimerScatterSeries.Name = iEmp.NB_TABULADOR.ToString();
            //dt.Rows.Add(iEmp.NO_NIVEL, iEmp.MN_SUELDO_ORIGINAL);


        }
    }
}
