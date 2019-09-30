using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Negocio.MetodologiaCompensacion
{
    internal static class LevelCalculatorIterationsManager
    {
        private static List<LevelCalculatorIterationsData> iterationsData;

        private static void InitializaData()
        {
            iterationsData = new List<LevelCalculatorIterationsData>();
            iterationsData.Add(new LevelCalculatorIterationsData { MinDataAmont = 1, MaxDataAmount = 200, Iterations = 20, InnerIterations = 100 });
            iterationsData.Add(new LevelCalculatorIterationsData { MinDataAmont = 201, MaxDataAmount = 500, Iterations = 40, InnerIterations = 200 });
            iterationsData.Add(new LevelCalculatorIterationsData { MinDataAmont = 501, MaxDataAmount = 1000, Iterations = 90, InnerIterations = 400 });
        }

        internal static LevelCalculatorIterationsData GetIterationsData(int dataAmount)
        {
            if (iterationsData == null)
                InitializaData();
            foreach (var data in iterationsData)
            {
                if (dataAmount >= data.MinDataAmont && dataAmount <= data.MaxDataAmount)
                    return data;
            }
            return iterationsData.Last();
        }
    }
}
