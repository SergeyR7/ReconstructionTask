using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReconstructionTask.Algorithms
{
    public class InputGenerator
    {
        static public string GenerateAbsolutelyRandomFile()
        {
            var rand = new Random();
            var factoriesQty = rand.Next(5, 100);
            var typesOfProductsQty = rand.Next(3, 10);
            List<int> minSumms = new List<int>();
            for (int i = 0; i < typesOfProductsQty; i++)
            {
                minSumms.Add(rand.Next(factoriesQty * 100, factoriesQty * 1000));
            }
            int[][] A = new int[typesOfProductsQty + 1][];
            int count = 0;
            List<int> factoryReconstructionQtyList = new List<int>();
            for (int k = 0; k < factoriesQty; k++)
            {
                var qty = rand.Next(1, 5);
                factoryReconstructionQtyList.Add(qty);
                count += qty;
                for (int i = 0; i < A.Count(); i++)
                {
                    Array.Resize<int>(ref A[i], count);
                }
                for (int i = 0; i < count; i++)
                {
                    for (int j = 0; j < typesOfProductsQty; j++)
                    {
                        A[j][i] = rand.Next(minSumms[j] / factoriesQty, minSumms[j] / factoriesQty * 10);
                    }
                    A[typesOfProductsQty][i] = rand.Next(25, 200);
                }
            }
            var name = "Random_" + rand.Next(10000, 20000) + ".txt";
            FileStream fileStream = new FileStream(name, FileMode.OpenOrCreate, FileAccess.Write);
            using (var streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
            {
                streamWriter.WriteLine(factoriesQty);
                streamWriter.WriteLine(count);
                streamWriter.WriteLine(typesOfProductsQty);
                int counter = 0;
                foreach (var fact in factoryReconstructionQtyList)
                {
                    streamWriter.WriteLine(fact);
                    for (int i = counter; i < counter + fact; i++)
                    {
                        streamWriter.Write(0 + " ");
                        for (int j = 0; j < typesOfProductsQty; j++)
                        {
                            streamWriter.Write(A[j][i] + " ");
                        }
                        streamWriter.Write(A[typesOfProductsQty][i]);
                        streamWriter.WriteLine();
                    }
                    counter += fact;
                }
                for (int i = 0; i < typesOfProductsQty; i++)
                {
                    streamWriter.Write(minSumms[i] + " ");
                }

            }
            return name;
        }

    }
}
