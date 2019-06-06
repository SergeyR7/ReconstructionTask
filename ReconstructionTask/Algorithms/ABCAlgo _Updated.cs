using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReconstructionTask.Algorithms
{
    public class ABCAlgo_Updated
    {
        public class Hive
        {
            public List<Fabric> startFabtic;
            public List<int> restrictions;
            public List<Bee> bees = new List<Bee>();
            public List<Bee> bestbees = new List<Bee>() { new Bee(), new Bee(), new Bee() };
            Random rnd;

            public Hive(List<Fabric> fabrics, List<int> rest)
            {
                startFabtic = fabrics;
                restrictions = rest;
            }

            static bool Srav(List<int> a, List<int> b) // функция сравнения двух масивов поэлементно
            {
                if (a.Count != b.Count) return false;
                for (int i = 0; i < a.Count; i++)
                {
                    if (a[i] > b[i]) return false;
                }
                return true;
            }

            public void InitialPlan()
            {
                bees.Clear();
                rnd = new Random();
                while (bees.Count < 1000)
                {
                    int count = 0;
                    int[] allRec = new int[count];
                    foreach (Fabric f in startFabtic)
                    {
                        Array.Resize(ref allRec, allRec.Count() + f.Bool_Product_Reconstruction_Price.Count);
                        for (int j = count; j < count + f.Bool_Product_Reconstruction_Price.Count; j++) allRec[j] = 0;
                        allRec[rnd.Next(count, count + f.Bool_Product_Reconstruction_Price.Count)] = 1;
                        count += f.Bool_Product_Reconstruction_Price.Count;
                    }
                    Bee b = new Bee(allRec);

                    List<int> temprest = new List<int>();
                    foreach (var i in restrictions) temprest.Add(0);
                    int iter = -1;
                    for (int fab = 0; fab < startFabtic.Count; fab++)
                    {
                        for (int j = 0; j < startFabtic[fab].Bool_Product_Reconstruction_Price.Count; j++)
                        {
                            iter++;
                            for (int g = 1; g < restrictions.Count + 1; g++)
                            {
                                temprest[g - 1] += startFabtic[fab].Bool_Product_Reconstruction_Price[j][g] * b.plan[iter];
                            }

                        }
                    }
                    if (Srav(restrictions, temprest))
                    {
                        bees.Add(b);
                    }
                }
            }

            public void InitialLCF()
            {
                foreach (Bee b in bees)
                {
                    int iter = 0;
                    for (int fab = 0; fab < startFabtic.Count; fab++)
                    {
                        for (int i = 0; i < startFabtic[fab].Bool_Product_Reconstruction_Price.Count; i++)
                        {
                            b.LCF += startFabtic[fab].Bool_Product_Reconstruction_Price[i][startFabtic[fab].Bool_Product_Reconstruction_Price[i].Count - 1] * b.plan[iter++];
                        }
                    }
                }
            }


            public void BestInit()
            {
                foreach (Bee b in bees)
                {
                    if (b.LCF < bestbees[2].LCF)
                    {
                        bestbees[2] = b;
                        if (bestbees[2].LCF < bestbees[1].LCF)
                        {
                            Bee temp1 = new Bee(bestbees[1]);
                            Bee temp2 = new Bee(bestbees[2]);
                            bestbees[1] = temp2;
                            bestbees[2] = temp1;
                        }
                        if (bestbees[1].LCF < bestbees[0].LCF)
                        {
                            Bee temp1 = new Bee(bestbees[0]);
                            Bee temp2 = new Bee(bestbees[1]);
                            bestbees[0] = temp2;
                            bestbees[1] = temp1;
                        }
                    }
                }                
            }


            private bool SRAAAAAV(List<List<int>> l1, List<List<int>> l2)
            {
                if (l1.Count != l2.Count) return false;
                else
                {
                    for (int i = 0; i < l1.Count; i++)
                    {
                        if (l1[i].Count != l2[i].Count) return false;
                        else
                        {
                            for (int j = 0; j < l1[i].Count; j++)
                            {
                                if (l1[i][j] != l2[i][j]) return false;
                            }
                        }
                    }
                }


                return true;
            }

            public List<Bee> ForSolve()
            {
                rnd = new Random();
                int s = rnd.Next(0, startFabtic.Count);
                for (int random = 0; random < s; random++)
                {
                    rnd = new Random();
                    foreach (Bee eb in bestbees)
                    {
                        int g = rnd.Next(0, startFabtic.Count);
                        Fabric fabric = new Fabric(startFabtic[g]);
                        int count = 0;
                        for (int fabs = 0; fabs < startFabtic.Count; fabs++)
                        {
                            if (SRAAAAAV(startFabtic[fabs].Bool_Product_Reconstruction_Price, fabric.Bool_Product_Reconstruction_Price))
                            {
                                int min = 999999;
                                for (int fabsrow = count; fabsrow < count + startFabtic[fabs].Bool_Product_Reconstruction_Price.Count; fabsrow++)
                                {
                                    if (fabric.Bool_Product_Reconstruction_Price[fabsrow - count][fabric.Bool_Product_Reconstruction_Price[fabsrow - count].Count - 1] <= min)
                                    {
                                        min = fabric.Bool_Product_Reconstruction_Price[fabsrow - count][fabric.Bool_Product_Reconstruction_Price[fabsrow - count].Count - 1];
                                        for (int h = count; h < count + startFabtic[fabs].Bool_Product_Reconstruction_Price.Count; h++) eb.plan[h] = 0;
                                        eb.plan[fabsrow] = 1;
                                        eb.LCF -= 0 /*тут нужна стоимость реконструкции где стояла единичка*/;
                                        eb.LCF += 0 /*тут соответственно стоимость реконструкции где единичка стоит теперь*/;
                                        // после этого алгоритм будет лучшим в мире

                                    }
                                }
                            }
                            count += startFabtic[fabs].Bool_Product_Reconstruction_Price.Count;
                        }
                    }
                }
                return bestbees;

            }

            public Bee Solve()
            {
                List<List<Bee>> solutionMatrix = new List<List<Bee>>();
                for (int i = 0; i < 10; i++)
                {
                    InitialPlan();
                    InitialLCF();
                    BestInit();
                    solutionMatrix.Add(ForSolve());
                }
                var outListBee = solutionMatrix.Where(a => a[0].LCF == solutionMatrix.Min(b => b[0].LCF)).FirstOrDefault();
                return outListBee[0];
            }
        }



        public class Bee
        {
            public float LCF { get; set; }
            public int[] plan;
            public Bee(int[] plan)
            {
                this.plan = plan;
            }
            public Bee()
            {
                LCF = 999999;
            }
            public Bee(Bee b)
            {
                LCF = b.LCF;
                plan = b.plan;
            }
            
        }


        public AlgoResults Calculate(List<int> Product_in_total, List<int> Product_in_Command, List<int> inputdata, List<Fabric> fabrics)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            Hive hive = new Hive(fabrics, Product_in_Command);
            Bee bestBee=hive.Solve();
            var res = new List<bool>();
            foreach (var b in bestBee.plan) res.Add(System.Convert.ToBoolean(b));
            watch.Stop();
            return new AlgoResults(res.ToArray(), bestBee.LCF, watch.ElapsedMilliseconds);
        }
    }
}
