using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReconstructionTask
{
    class GreedyAlgo
    {
        static bool Compare(List<int> a, List<int> b)
        {
            int acc = 0;
            for (int i = 0; i < a.Count; i++)
            {
                if (a[i] < b[i]) acc++;
            }
            if (acc == a.Count) return true;
            return false;
        }
        public static AlgoResults Calculate(InputData inputData)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            var Product_in_total = inputData.Product_in_total;
            var Product_in_Command = inputData.Product_in_Command;
            var inputdata = inputData.inputdata;
            var fabrics = inputData.fabrics;
            int C = 0;
            
            while (Compare(Product_in_total, Product_in_Command))
            {
                C = 0;
                for (int i = 0; i < inputdata[2]; i++) Product_in_total[i] = 0;
                foreach (Fabric f in fabrics)
                {
                    int min = 9999;
                    for (int i = 0; i < f.Bool_Product_Reconstruction_Price.Count; i++)
                    {
                        if (f.Bool_Product_Reconstruction_Price[i][inputdata[2] + 1] < min)
                        {
                            foreach (var s in f.Bool_Product_Reconstruction_Price)
                            {
                                s[0] = 0;
                            }
                            min = f.Bool_Product_Reconstruction_Price[i][inputdata[2] + 1];
                            f.Bool_Product_Reconstruction_Price[i][0] = 1;

                        }
                    }

                    for (int i = 0; i < f.Bool_Product_Reconstruction_Price.Count; i++)
                    {
                        for (int x = 1; x < inputdata[2] + 1; x++)
                        {
                            Product_in_total[x - 1] += f.Bool_Product_Reconstruction_Price[i][x] * f.Bool_Product_Reconstruction_Price[i][0];
                        }
                        C += f.Bool_Product_Reconstruction_Price[i][inputdata[2] + 1] * f.Bool_Product_Reconstruction_Price[i][0];
                    }


                    for (int i = 0; i < f.Bool_Product_Reconstruction_Price.Count; i++)
                    {
                        if (f.Bool_Product_Reconstruction_Price[i][0] == 1)
                        {
                            f.Bool_Product_Reconstruction_Price[i][inputdata[2] + 1] = 99999;
                        }
                    }
                }
            }
            List<bool> arr = new List<bool>();

            foreach (Fabric f in fabrics)
            {
                foreach (var t in f.Bool_Product_Reconstruction_Price)
                {
                    arr.Add(System.Convert.ToBoolean(t[0]));
                }
            }
            watch.Stop();
            return new AlgoResults(arr.ToArray(), C, watch.ElapsedMilliseconds,Product_in_total);
        }
    }
}

