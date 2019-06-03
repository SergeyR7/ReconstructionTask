using ReconstructionTask.Algorithms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        private void label6_Click(object sender, EventArgs e)
        {

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

    

        private async void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            if (textBox1.Text.Length != 0 && openFileDialog1.CheckFileExists)
            {
                button3.Enabled = false;
                label2.Visible = true;
                var inpdata2 = new InputData();
                inpdata2.ReadDataFromPath(inputData.Path);
                Task<AlgoResults> t1 = Task<AlgoResults>.Run(() => GreedyAlgo.Calculate(inputData.Product_in_total, inputData.Product_in_Command, inputData.inputdata, inputData.fabrics));
                Task<AlgoResults> t2 = Task<AlgoResults>.Run(() => new ABCAlgo().Calculate(inpdata2.Product_in_total, inpdata2.Product_in_Command, inpdata2.inputdata, inpdata2.fabrics));
                //Task<AlgoResults> t3 = Task<AlgoResults>.Run(() => CapitalAlgo.Calculate(inputData));
                await Task.WhenAll(new[] { t1, t2/*, t3 */});
                label2.Visible = false;
                var algoResults1 = t1.Result;
                richTextBox1.Text = "Reconstruction plan: \n";
                string recons = "";
                foreach (var s in algoResults1.ReconstructionPlan) recons += s.ToString();
                var array = recons.Replace("True", " 1 ").Replace("False", " 0 ");
                richTextBox1.Text += array + "\n";
                richTextBox1.Text += "Summary cost of reconstructions: ";
                richTextBox1.Text += algoResults1.SummaryFunctionResult.ToString();
                richTextBox1.Text += "\nTime Elapsed :  " + algoResults1.Ms + " ms";
                var algoResults2 = t2.Result;
                richTextBox2.Text = "Reconstruction plan: \n";
                recons = "";
                foreach (var s in algoResults2.ReconstructionPlan) recons += s.ToString();
                array = recons.Replace("True", " 1 ").Replace("False", " 0 ");
                richTextBox2.Text += array + "\n";
                richTextBox2.Text += "Summary cost of reconstructions: ";
                richTextBox2.Text += algoResults2.SummaryFunctionResult.ToString();
                richTextBox2.Text += "\nTime Elapsed :  " + algoResults2.Ms + " ms";
                inputData.Clear();
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
    }
}
