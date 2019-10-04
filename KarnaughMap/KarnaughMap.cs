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

        private int varsX;
        private int varsY;
        private int rowCount;
        private int columnCount;

        public ObservableCollection<string> InputVariables { get; private set; }
        public string OutputVariable { get; set; }

        private void InputVariables_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            varsY = InputVariables.Count >> 1;
            varsX = InputVariables.Count - varsY;

            rowCount = 1 << varsY;
            columnCount = 1 << varsX;

            Invalidate();
        }

        private void KarnaughMap_Paint(object sender, PaintEventArgs e)
        {
            const int gridSize = 40;
            var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

            Width = (columnCount + 1) * gridSize + 1;
            Height = (rowCount + 1) * gridSize + 1;
            e.Graphics.Clear(BackColor);

            for (int i = 0; i <= columnCount; i++)
                e.Graphics.DrawLine(System.Drawing.Pens.Black, gridSize * (i + 1), gridSize, gridSize * (i + 1), gridSize * (rowCount + 1));

            for (int j = 0; j <= rowCount; j++)
                e.Graphics.DrawLine(System.Drawing.Pens.Black, gridSize, gridSize * (j + 1), gridSize * (columnCount + 1), gridSize * (j + 1));

            e.Graphics.DrawLine(System.Drawing.Pens.Black, 0, 0, gridSize, gridSize);

            for (int i = 0; i < rowCount; i++)
            {
                var gray = GrayCodeConverter.Decimal2Gray(i);
                e.Graphics.DrawString(Convert.ToString(gray, 2).PadLeft(2, '0'), Font, Brushes.Black, new RectangleF(0, (i + 1) * gridSize, gridSize, gridSize), sf);
            }

            e.Graphics.RotateTransform(-90);

            for (int i = 0; i < columnCount; i++)
            {
                var gray = GrayCodeConverter.Decimal2Gray(i);
                e.Graphics.DrawString(Convert.ToString(gray, 2).PadLeft(3, '0'), Font, Brushes.Black, new RectangleF(-gridSize, (i + 1) * gridSize, gridSize, gridSize), sf);
            }

            e.Graphics.ResetTransform();

            for (int i = 0; i < columnCount; i++)
            {
                var i_gray = GrayCodeConverter.Decimal2Gray(i);
                for (int j = 0; j < rowCount; j++)
                {
                    var j_gray = GrayCodeConverter.Decimal2Gray(j);
                    var val = j_gray * (1 << varsX) + i_gray;
                    e.Graphics.DrawString(val.ToString(), Font, Brushes.Black, new RectangleF((i + 1) * gridSize, (j + 1) * gridSize, gridSize, gridSize), sf);
                }
            }
        }
    }
}
