using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class KarnaughMap : UserControl
    {
        public KarnaughMap()
        {
            InitializeComponent();

            Width = Height = 50;
            Paint += KarnaughMap_Paint;
        }

        private void KarnaughMap_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(System.Drawing.Color.Black);
        }
    }
}
