using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KarnaughMap.Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void BtnRandomFill_Click(object sender, System.EventArgs e)
        {
            await karnaughMap1.RandomFill();
        }

        private async void BtnSolve_Click(object sender, System.EventArgs e)
        {
            await karnaughMap1.Solve();
        }

        private void KarnaughMap1_KarnaughMapSolved(object sender, EventArgs.KarnaughMapSolvedEventArgs e)
        {
            lstLoopOnes.Items.Clear();
            lstLoopOnes.Items.AddRange(e.LoopsOnes.Select(l => l.ToString(karnaughMap1.InputVariables.ToArray())).ToArray());
            lstLoopZeros.Items.Clear();
            lstLoopZeros.Items.AddRange(e.LoopsZeros.Select(l => l.ToString(karnaughMap1.InputVariables.ToArray())).ToArray());
        }

        private void cmbMode_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            karnaughMap1.Mode = (Enums.eEditMode)cmbMode.SelectedIndex;
        }
    }
}
