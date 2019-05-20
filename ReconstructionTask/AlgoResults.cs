using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReconstructionTask
{
    class AlgoResults
    {
        public bool[] ReconstructionPlan { get; set; }
        public float SummaryFunctionResult { get; set; }
        public List<int> ProductionSumm { get; set; }
        public AlgoResults(bool[] reconstructionPlan, float summaryFunctionResult)
        {
            ReconstructionPlan = reconstructionPlan;
            SummaryFunctionResult = summaryFunctionResult;
        }

        public AlgoResults(bool[] reconstructionPlan, float summaryFunctionResult,List<int> prodSumm)
        {
            ReconstructionPlan = reconstructionPlan;
            SummaryFunctionResult = summaryFunctionResult;
            ProductionSumm = prodSumm;
        }
    }
}
