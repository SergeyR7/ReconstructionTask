﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReconstructionTask.Algorithms
{
    public class ABCAlgo
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


         

            public void Solve()
            {
                    InitialPlan();
                    InitialLCF();
                    BestInit();
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
            hive.Solve();
            var res = new List<bool>();
            foreach (var b in hive.bestbees[0].plan) res.Add(System.Convert.ToBoolean(b));
            watch.Stop();
            return new AlgoResults(res.ToArray(), hive.bestbees[0].LCF, watch.ElapsedMilliseconds);
        }
    }
}
