using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.Negocio.MetodologiaCompensacion
{
    public class LevelCalculator
    {
        #region Variables
        private int levels;
        private int seed;
        internal decimal[] representatives;
        #endregion

        #region Metodos
        public LevelCalculator(int levels, int seed)
        {
            this.levels = levels;
            this.seed = seed;
            representatives = new decimal[levels];
        }

        internal void AdjustLevels(int newLevels)
        {
            this.levels = newLevels;
        }

        public XElement GetSerializedData()
        {
            return new XElement("LevelModel", new XAttribute("Levels", levels)
                                            , new XAttribute("Seed", seed)
                                            , from representative in representatives
                                              select new XElement("Level", new XAttribute("Representative", representative))
                               );
        }

        public static LevelCalculator LoadFromSerializedData(XElement serializedData, out string message)
        {
            message = String.Empty;
            XElement levelModelElem = serializedData.Name == "LevelModel" ? serializedData : serializedData.Descendants("LevelModel").FirstOrDefault();
            if (levelModelElem == null)
                throw new ArgumentException("El argumento proporcionado no contiene un nodo LevelModel.");
            int levels, seed;
            if (levelModelElem.Attribute("Levels") == null || !int.TryParse(levelModelElem.Attribute("Levels").Value, out levels))
            {
                throw new ArgumentException("El nodo LevelModel no contiene un atributo Levels válido.");
            }
            if (levelModelElem.Attribute("Seed") == null || !int.TryParse(levelModelElem.Attribute("Seed").Value, out seed))
            {
                throw new ArgumentException("El nodo LevelModel no contiene un atributo Seed válido.");
            }
            var levelCalculator = new LevelCalculator(levels, seed);
            int index = 0;
            decimal representative;
            foreach (var elem in levelModelElem.Descendants("Level"))
            {
                if (elem.Attribute("Representative") != null && decimal.TryParse(elem.Attribute("Representative").Value, out representative))
                {
                    if (index >= levels)
                        index++;
                    else
                        levelCalculator.representatives[index++] = representative;
                }
            }
            if (index < levels)
            {
                levelCalculator.AdjustLevels(index);
                message = "El modelo de niveles cargado tiene discrepancias, la cantidad de niveles es mayor que la cantidad de representantes.";
            }
            else if (index > levels)
            {
                message = "El modelo de niveles cargado tiene discrepancias, la cantidad de niveles es inferior a la cantidad de representates. El número de representates cargados corresponde con la cantidad de niveles del modelo.";
            }
            return levelCalculator;
        }

        public int[] CalculateModel(decimal[] data, out decimal minError)
        {
            minError = 0M;
            int[] clusterTag = new int[data.Length];
            int[] iterationClusterTags;
            decimal[] iterationRepresentatives = new decimal[levels];
            LevelCalculatorIterationsData iterationData = LevelCalculatorIterationsManager.GetIterationsData(data.Length);
            Dictionary<decimal, bool> representativesSelection = new Dictionary<decimal, bool>();
            decimal selectedRepresentative, minMeanError = decimal.MaxValue, iterationError;
            Random rand = new Random(this.seed);
            int index;
            for (int iteration = 0; iteration < iterationData.Iterations; iteration++)
            {
                index = 0;
                while (index < levels)
                {
                    selectedRepresentative = data[rand.Next(data.Length)];
                    if (!representativesSelection.ContainsKey(selectedRepresentative))
                    {
                        representativesSelection.Add(selectedRepresentative, true);
                        index++;
                    }
                }
                index = 0;
                foreach (var keyValuePair in representativesSelection)
                    iterationRepresentatives[index++] = keyValuePair.Key;
                iterationClusterTags = CalculateModelIteration(data, iterationRepresentatives, iterationData.InnerIterations, out iterationError);
                if (iterationError < minMeanError)
                {
                    Array.Copy(iterationClusterTags, clusterTag, iterationClusterTags.Length);
                    Array.Copy(iterationRepresentatives, representatives, levels);
                    minMeanError = iterationError;
                }
                if (minMeanError == 0M)
                    break;
                representativesSelection.Clear();
            }
            minError = minMeanError;
            SortRepresentatives(clusterTag);
            return clusterTag;
        }

        private void SortRepresentatives(int[] clusterTag)
        {
            Dictionary<int, decimal> representativesFormerOrder = new Dictionary<int, decimal>();
            Dictionary<decimal, int> representativesSorted = new Dictionary<decimal, int>();
            for (int index = 0; index < representatives.Length; index++)
                representativesFormerOrder.Add(index, representatives[index]);
            Array.Sort(representatives);
            for (int index = 0; index < representatives.Length; index++)
                representativesSorted.Add(representatives[index], index);
            for (int index = 0; index < clusterTag.Length; index++)
                clusterTag[index] = representativesSorted[representativesFormerOrder[clusterTag[index]]];
        }

        private int[] CalculateModelIteration(decimal[] data, decimal[] iterationRepresentatives, int totalIterations, out decimal totalMeanError)
        {
            //inicializacion
            totalMeanError = 0M;
            int[] clusterCount = new int[levels];
            int calculatedTag;
            bool change;
            decimal minDistance, candidateDistance;
            int[] clusterTags = new int[data.Length];
            for (int iteration = 0; iteration < totalIterations; iteration++)
            {
                change = false;
                //calcular los cluster tags y determinar si hay cambio de etiqueta
                for (int dataIndex = 0; dataIndex < data.Length; dataIndex++)
                {
                    minDistance = Math.Abs(data[dataIndex] - iterationRepresentatives[0]);
                    calculatedTag = 0;
                    for (int tagCandidate = 1; tagCandidate < levels; tagCandidate++)
                    {
                        candidateDistance = Math.Abs(data[dataIndex] - iterationRepresentatives[tagCandidate]);
                        if (candidateDistance < minDistance)
                        {
                            minDistance = candidateDistance;
                            calculatedTag = tagCandidate;
                        }
                    }
                    if (clusterTags[dataIndex] != calculatedTag)
                    {
                        clusterTags[dataIndex] = calculatedTag;
                        change = true;
                    }
                }
                if (!change)
                {
                    break;
                }
                //calcular los representantes
                for (int clusterIndex = 0; clusterIndex < levels; clusterIndex++)
                {
                    iterationRepresentatives[clusterIndex] = 0M;
                    clusterCount[clusterIndex] = 0;
                }
                for (int dataIndex = 0; dataIndex < data.Length; dataIndex++)
                {
                    iterationRepresentatives[clusterTags[dataIndex]] += data[dataIndex];
                    clusterCount[clusterTags[dataIndex]]++;
                }
                for (int clusterIndex = 0; clusterIndex < levels; clusterIndex++)
                {
                    if (clusterCount[clusterIndex] > 0)
                        iterationRepresentatives[clusterIndex] /= clusterCount[clusterIndex];
                }
            }
            //calcular error medio total
            for (int dataIndex = 0; dataIndex < data.Length; dataIndex++)
            {
                totalMeanError += Math.Abs(data[dataIndex] - iterationRepresentatives[clusterTags[dataIndex]]);
            }
            totalMeanError /= data.Length;
            return clusterTags;
        }

        public decimal[] GetRepresentatives()
        {
            decimal[] representativesCloned = new decimal[levels];
            Array.Copy(representatives, representativesCloned, levels);
            return representativesCloned;
        }
        #endregion
    }
}
