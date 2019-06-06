using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReconstructionTask.Forms
{
    public partial class InputFormPopUp : Form
    {
        WorkForm parent;
        public InputFormPopUp(WorkForm f)
        {
            parent = f;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                int factoriesQty = int.Parse(textBox1.Text);
                int topQty = int.Parse(textBox2.Text);
                if (factoriesQty >= 1000 || factoriesQty <= 2 || topQty >= 50 || topQty <= 2) throw new Exception("Input Correct data");
                parent.Enabled = true;
                parent.InputDataForGenerate(factoriesQty, topQty);
                this.Close();
            }
            catch (Exception ex)
            {
                textBox1.Clear();
                textBox2.Clear();
                label1.Text = ex.Message;
            }

            
        }

        private void InputFormPopUp_FormClosing(object sender, FormClosingEventArgs e)
        {
            parent.Enabled = true;
        }
    }
}
