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
    public partial class AboutInput : Form
    {
        Form father;
        public AboutInput(Form f)
        {
            this.father = f;
            InitializeComponent();
        }

        private void AboutInput_FormClosing(object sender, FormClosingEventArgs e)
        {
            father.Show();
        }
    }
}
