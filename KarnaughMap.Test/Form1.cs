﻿using System;
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
            await karnaughMap1.SolveAutomatically();
        }

        private List<QuineMcCluskey.RequiredLoop> loopsOnes;
        private List<QuineMcCluskey.RequiredLoop> loopsZeros;
        private void KarnaughMap1_KarnaughMapSolved(object sender, EventArgs.KarnaughMapSolvedEventArgs e)
        {
            loopsOnes = e.LoopsOnes;
            loopsZeros = e.LoopsZeros;

            lstLoopOnes.Items.Clear();
            lstLoopOnes.Items.AddRange(e.LoopsOnes.Select(l => l.ToString(karnaughMap1.InputVariables.ToArray())).ToArray());
            lstLoopZeros.Items.Clear();
            lstLoopZeros.Items.AddRange(e.LoopsZeros.Select(l => l.ToString(karnaughMap1.InputVariables.ToArray())).ToArray());
        }

        private bool ignoreSelectedModeChanging = false;
        private void cmbMode_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (!ignoreSelectedModeChanging)
            {
                karnaughMap1.Mode = (Enums.eEditMode)cmbMode.SelectedIndex;
                btnRandomFill.Enabled = cmbMode.SelectedIndex == 0;
                btnSolve.Enabled = cmbMode.SelectedIndex == 1;
                lstLoopOnes.Items.Clear();
                lstLoopZeros.Items.Clear();
            }
        }

        private void KarnaughMap1_ModeChanging(object sender, EventArgs.ModeChangingEventArgs e)
        {
            if (karnaughMap1.HasLoops)
            {
                if (e.NewValue == Enums.eEditMode.Edit)
                {
                    if (MessageBox.Show("This will remove all loops. Are you sure?", "Edit mode", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                    {
                        e.Cancel = true;
                        ignoreSelectedModeChanging = true;
                        cmbMode.SelectedIndex = (int)e.OldValue;
                        ignoreSelectedModeChanging = false;
                    }
                }
            }
        }

        private void LstLoopZeros_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            karnaughMap1.SelectedLoop = loopsZeros[lstLoopZeros.SelectedIndex];
        }

        private void LstLoopOnes_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            karnaughMap1.SelectedLoop = loopsOnes[lstLoopOnes.SelectedIndex];
        }
    }
}
