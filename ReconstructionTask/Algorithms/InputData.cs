using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReconstructionTask
{
    public class Fabric // клас произвотства(фабрики)
    {
        public List<List<int>> Bool_Product_Reconstruction_Price;
        public Fabric()
        {
            Bool_Product_Reconstruction_Price = new List<List<int>>();
        }
        public Fabric(Fabric f)
        {
            Bool_Product_Reconstruction_Price = new List<List<int>>(f.Bool_Product_Reconstruction_Price);
        }

    }
    public class InputData
    {
        public Fabric fabric = new Fabric();
        public List<int> inputdata = new List<int>(); 
        public List<Fabric> fabrics = new List<Fabric>(); 
        public List<int> Product_in_total = new List<int>();
        public List<int> Product_in_Command = new List<int>();
        public string Path = "";
        public void ReadDataFromPath(string filePath)
        {
            Path = filePath;
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;

                for (int i = 0; i < 3; i++)
                {
                    line = streamReader.ReadLine();
                    inputdata.Add(Convert.ToInt32(line));
                }
                for (int i = 0; i < inputdata[2]; i++) Product_in_total.Add(0);
                List<int> temp;
                for (int i = 0; i < inputdata[0]; i++)
                {
                    fabric = new Fabric();
                    int ss = Convert.ToInt32(streamReader.ReadLine());
                    for (int j = 0; j < ss; j++)
                    {
                        temp = new List<int>();
                        line = streamReader.ReadLine();
                        string[] line_elements = line.Split(' ');
                        for (int g = 0; g < inputdata[2] + 2; g++)
                        {
                            int s = Convert.ToInt32(line_elements[g]);
                            temp.Add(s);
                        }
                        fabric.Bool_Product_Reconstruction_Price.Add(temp);
                    }

                    fabrics.Add(fabric);
                }

                line = streamReader.ReadLine();
                string[] bnumbrs = line.Split(' ');
                for (int h = 0; h < inputdata[2]; h++)
                {
                    Product_in_Command.Add(Convert.ToInt32(bnumbrs[h]));
                }
            }
        }

        public InputData Clone()
        {
            var newdata = new InputData();
            newdata.ReadDataFromPath(Path);
            return newdata;
        }
        public void Clear()
        {
            inputdata.Clear();
            fabrics.Clear();
            Product_in_total.Clear();
            Product_in_Command.Clear();
        }
    }
}
