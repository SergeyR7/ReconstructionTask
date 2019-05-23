using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReconstructionTask
{
    public class AlgoResults
    {
        public bool[] ReconstructionPlan { get; set; }
        public float SummaryFunctionResult { get; set; }
        public List<int> ProductionSumm { get; set; }
        public long Ms { get; set; }
        public AlgoResults(bool[] reconstructionPlan, float summaryFunctionResult,long ms)
        {
            ReconstructionPlan = reconstructionPlan;
            SummaryFunctionResult = summaryFunctionResult;
            Ms = ms;
        }

        public AlgoResults(bool[] reconstructionPlan, float summaryFunctionResult,long ms,List<int> prodSumm)
        {
            ReconstructionPlan = reconstructionPlan;
            SummaryFunctionResult = summaryFunctionResult;
            Ms = ms;
            ProductionSumm = prodSumm;
        }
    }
}
