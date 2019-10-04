using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections.ObjectModel;

namespace KarnaughMap
{
    public partial class KarnaughMap : UserControl
    {
        public KarnaughMap()
        {
            InitializeComponent();

            InputVariables = new ObservableCollection<string>();
            InputVariables.CollectionChanged += InputVariables_CollectionChanged;

            Paint += KarnaughMap_Paint;
        }

        private int rowCount;
        private int columnCount;

        public ObservableCollection<string> InputVariables { get; private set; }
        public string OutputVariable { get; set; }

        private void InputVariables_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var varsY = InputVariables.Count >> 1;
            var varsX = InputVariables.Count - varsY;

            rowCount = 1 << varsY;
            columnCount = 1 << varsX;

            Invalidate();
        }

        private void KarnaughMap_Paint(object sender, PaintEventArgs e)
        {
            const int gridSize = 40;

            Width = columnCount * gridSize + 1;
            Height = rowCount * gridSize + 1;
            e.Graphics.Clear(BackColor);

            for (int i = 0; i <= columnCount; i++)
                e.Graphics.DrawLine(System.Drawing.Pens.Black, gridSize * i, 0, gridSize * i, gridSize * rowCount);

            for (int j = 0; j <= rowCount; j++)
                e.Graphics.DrawLine(System.Drawing.Pens.Black, 0, gridSize * j, gridSize * columnCount, gridSize * j);
        }
    }
}
