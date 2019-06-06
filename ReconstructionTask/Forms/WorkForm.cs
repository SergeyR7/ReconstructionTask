using ReconstructionTask.Algorithms;
using ReconstructionTask.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReconstructionTask
{
    public partial class WorkForm : Form
    {
        Form father;
        InputData inputData = new InputData();
        public WorkForm(Form f)
        {
            father = f;
            InitializeComponent();
        }



        private void WorkForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.father.Show();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "input files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var filePath = openFileDialog1.FileName;
                inputData.Clear();
                try
                {
                    inputData.ReadDataFromPath(filePath);
                    textBox1.Text = openFileDialog1.FileName;
                    button3.Enabled = true;
                }
                catch(Exception ex)
                {
                    richTextBox1.Text = ex.Message;
                    richTextBox2.Text = ex.Message;
                    richTextBox3.Text = ex.Message;
                    button3.Enabled = false;
                }
                
            }
        }

        public void InputDataForGenerate(int fact,int pr)
        {
            try
            {
                inputData.Clear();
                var nameFile = InputGenerator.GenerateFile(fact,pr);
                var filePath = nameFile;
                inputData.ReadDataFromPath(filePath);
                textBox1.Text = "(bin->debug)/" + nameFile;
                button3.Enabled = true;
                button3.ForeColor = Color.Black;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            if (textBox1.Text.Length != 0 && openFileDialog1.CheckFileExists)
            {
                button3.Enabled = false;
                var inpdata2 = inputData.Clone();
                var inpdata3 = inputData.Clone();
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                Task<AlgoResults> t1 = Task<AlgoResults>.Run(() => new GreedyAlgo().Calculate(inputData.Product_in_total, inputData.Product_in_Command, inputData.inputdata, inputData.fabrics));
                Task<AlgoResults> t2 = Task<AlgoResults>.Run(() => new ABCAlgo().Calculate(inpdata2.Product_in_total, inpdata2.Product_in_Command, inpdata2.inputdata, inpdata2.fabrics));
                Task<AlgoResults> t3 = Task<AlgoResults>.Run(() => new ABCAlgo_Updated().Calculate(inpdata3.Product_in_total, inpdata3.Product_in_Command, inpdata3.inputdata, inpdata3.fabrics));
                await Task.WhenAll(new[] { t1, t2, t3 });
                var algoResults = t1.Result;
                richTextBox1.Text = "Reconstruction plan: \n";
                string recons = "";
                foreach (var s in algoResults.ReconstructionPlan) recons += s.ToString();
                var array = recons.Replace("True", " 1 ").Replace("False", " 0 ");
                richTextBox1.Text += array + "\n";
                richTextBox1.Text += "Summary cost of reconstructions: ";
                richTextBox1.Text += algoResults.SummaryFunctionResult.ToString();
                richTextBox1.Text += "\nTime Elapsed :  " + algoResults.Ms + " ms";
                algoResults = t2.Result;
                richTextBox2.Text = "Reconstruction plan: \n";
                recons = "";
                foreach (var s in algoResults.ReconstructionPlan) recons += s.ToString();
                array = recons.Replace("True", " 1 ").Replace("False", " 0 ");
                richTextBox2.Text += array + "\n";
                richTextBox2.Text += "Summary cost of reconstructions: ";
                richTextBox2.Text += algoResults.SummaryFunctionResult.ToString();
                richTextBox2.Text += "\nTime Elapsed :  " + algoResults.Ms + " ms";
                algoResults = t3.Result;
                richTextBox3.Text = "Reconstruction plan: \n";
                recons = "";
                foreach (var s in algoResults.ReconstructionPlan) recons += s.ToString();
                array = recons.Replace("True", " 1 ").Replace("False", " 0 ");
                richTextBox3.Text += array + "\n";
                richTextBox3.Text += "Summary cost of reconstructions: ";
                richTextBox3.Text += algoResults.SummaryFunctionResult.ToString();
                richTextBox3.Text += "\nTime Elapsed :  " + algoResults.Ms + " ms";
                inputData.Clear();
                this.Cursor = System.Windows.Forms.Cursors.Default;

            }
            else richTextBox1.Text = "Error! File not found or data incorrect";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                inputData.Clear();
                var filePath = "../../file.txt";
                inputData.ReadDataFromPath(filePath);
                textBox1.Text = "../../file.txt";
                button3.Enabled = true;
                button3.ForeColor = Color.Black;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form aboutInput = new AboutInput(this);
            aboutInput.Show();
            linkLabel1.LinkVisited = true;
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                inputData.Clear();
                var nameFile = InputGenerator.GenerateAbsolutelyRandomFile(false);
                var filePath = nameFile;
                inputData.ReadDataFromPath(filePath);
                textBox1.Text = "(bin->debug)/"+nameFile ;
                button3.Enabled = true;
                button3.ForeColor = Color.Black;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                inputData.Clear();
                button3.Enabled = false;
                this.Cursor= System.Windows.Forms.Cursors.WaitCursor;
                var nameFile = InputGenerator.GenerateAbsolutelyRandomFile(true);
                var filePath = nameFile;
                inputData.ReadDataFromPath(filePath);
                button3.Enabled = true;
                this.Cursor = Cursors.Default;

                textBox1.Text = "(bin->debug)/" + nameFile;
                button3.Enabled = true;
                button3.ForeColor = Color.Black;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            AboutForm form = new AboutForm(this);
            form.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            AboutForm form = new AboutForm(this);
            form.Show();
            this.Hide();

        }

        private void label3_Click(object sender, EventArgs e)
        {
            AboutForm form = new AboutForm(this);
            form.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            InputFormPopUp popUp = new InputFormPopUp(this);
            popUp.Show();
            this.Enabled = false;
        }

        private async void button7_Click(object sender, EventArgs e)
        {
            int factoriesQty = 4;
            int typesOfProductsQty = 5;
            const int iterations = 100;  //change here the quantity of tasks
            long[] msForGreedyAlgo = new long[iterations];
            long[] msForABCAlgo = new long[iterations];
            long[] msForABCUAlgo = new long[iterations];
            float[] SFRForGreedyAlgo = new float[iterations];
            float[] SFRForABCAlgo = new float[iterations];
            float[] SFRForABCUAlgo = new float[iterations];

            this.Cursor = Cursors.WaitCursor;
            for (int i = 0; i < iterations; i++)
            {
                inputData.Clear();
                var path = InputGenerator.GenerateFile(factoriesQty + i, typesOfProductsQty);
                inputData.ReadDataFromPath(path);
                textBox1.Text = path;
                var inpdata2 = inputData.Clone();
                var inpdata3 = inputData.Clone();
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                Task<AlgoResults> t1 = Task<AlgoResults>.Run(() => new GreedyAlgo().Calculate(inputData.Product_in_total, inputData.Product_in_Command, inputData.inputdata, inputData.fabrics));
                Task<AlgoResults> t2 = Task<AlgoResults>.Run(() => new ABCAlgo().Calculate(inpdata2.Product_in_total, inpdata2.Product_in_Command, inpdata2.inputdata, inpdata2.fabrics));
                Task<AlgoResults> t3 = Task<AlgoResults>.Run(() => new ABCAlgo_Updated().Calculate(inpdata3.Product_in_total, inpdata3.Product_in_Command, inpdata3.inputdata, inpdata3.fabrics));
                await Task.WhenAll(new[] { t1, t2, t3 });
                msForGreedyAlgo[i] = t1.Result.Ms;
                msForABCAlgo[i] = t2.Result.Ms;
                msForABCUAlgo[i] = t3.Result.Ms;
                SFRForGreedyAlgo[i] = t1.Result.SummaryFunctionResult;
                SFRForABCAlgo[i] = t2.Result.SummaryFunctionResult;
                SFRForABCUAlgo[i] = t3.Result.SummaryFunctionResult;
                File.Delete(path);
            }
            ChartForm chart = new ChartForm(msForABCUAlgo);
            chart.Show();
            FileStream fileStream = new FileStream("GREEDY FOR EXCEL.txt", FileMode.OpenOrCreate, FileAccess.Write);
            using (var streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
            {
                for (int j = 0; j < iterations; j++)
                {
                    streamWriter.WriteLine(SFRForGreedyAlgo[j] + "                         " + msForGreedyAlgo[j]);
                }
            }
            FileStream fileStream2 = new FileStream("ABC FOR EXCEL.txt", FileMode.OpenOrCreate, FileAccess.Write);
            using (var streamWriter = new StreamWriter(fileStream2, Encoding.UTF8))
            {
                for (int j = 0; j < iterations; j++)
                {
                    streamWriter.WriteLine(SFRForABCAlgo[j] + "                         " + msForABCAlgo[j]);
                }
            }
            FileStream fileStream3 = new FileStream("ABCU FOR EXCEL.txt", FileMode.OpenOrCreate, FileAccess.Write);
            using (var streamWriter = new StreamWriter(fileStream3, Encoding.UTF8))
            {
                for (int j = 0; j < iterations; j++)
                {
                    streamWriter.WriteLine(SFRForABCUAlgo[j] + "                        " + msForABCUAlgo[j]);
                }
            }
            this.Cursor = Cursors.Default;
        }

        private void WorkForm_Load(object sender, EventArgs e)
        {

        }

    }
}
