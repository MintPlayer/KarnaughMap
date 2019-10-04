using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Linq;

namespace KarnaughMap
{
    public partial class KarnaughMap : UserControl
    {
        public KarnaughMap()
        {
            InitializeComponent();
            DoubleBuffered = true;

            InputVariables = new ObservableCollection<string>();
            InputVariables.CollectionChanged += InputVariables_CollectionChanged;

            Paint += KarnaughMap_Paint;
            MouseClick += KarnaughMap_MouseClick;
            KeyDown += KarnaughMap_KeyDown;
        }

        const int gridSize = 40;
        private string[] varsX;
        private string[] varsY;
        private int rowCount;
        private int columnCount;

        private Point focusedCell = new Point();

        public ObservableCollection<string> InputVariables { get; private set; }
        public string OutputVariable { get; set; }

        private List<int> ones = new List<int>();
        private List<int> zeros = new List<int>();

        private void InputVariables_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var varsYcount = InputVariables.Count >> 1;
            var varsXcount = InputVariables.Count - varsYcount;

            varsY = InputVariables.Take(varsYcount).ToArray();
            varsX = InputVariables.Skip(varsYcount).ToArray();

            rowCount = 1 << varsYcount;
            columnCount = 1 << varsXcount;

            Invalidate();
        }

        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:
                    return true;
                default:
                    return base.IsInputKey(keyData);
            }
        }

        private void KarnaughMap_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    if (--focusedCell.X < 0) focusedCell.X = columnCount - 1;
                    break;
                case Keys.Right:
                    if (++focusedCell.X >= columnCount) focusedCell.X = 0;
                    break;
                case Keys.Up:
                    if (--focusedCell.Y < 0) focusedCell.Y = rowCount - 1;
                    break;
                case Keys.Down:
                    if (++focusedCell.Y >= rowCount) focusedCell.Y = 0;
                    break;
                case Keys.Space:
                    ToggleNumber(GridIndexToNumber(focusedCell));
                    break;
                default:
                    return;
            }
            Invalidate();
        }

        private void KarnaughMap_Paint(object sender, PaintEventArgs e)
        {
            var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

            // Set control size
            Width = (columnCount + 1) * gridSize + 1 + Font.Height;
            Height = (rowCount + 1) * gridSize + 1 + Font.Height;

            // Clear the background
            e.Graphics.Clear(BackColor);

            // Draw the output variable
            e.Graphics.DrawString(OutputVariable, Font, Brushes.Black, 0, 0);

            // Translate the transform for the titles
            e.Graphics.TranslateTransform(Font.Height, Font.Height);

            // Draw the x-axis title
            e.Graphics.DrawString(string.Join(", ", varsX), Font, Brushes.Black, new RectangleF(gridSize, -Font.Height, columnCount * gridSize, Font.Height), sf);
            
            // Draw the grid
            for (int i = 0; i <= columnCount; i++)
                e.Graphics.DrawLine(System.Drawing.Pens.Black, gridSize * (i + 1), gridSize, gridSize * (i + 1), gridSize * (rowCount + 1));
            for (int j = 0; j <= rowCount; j++)
                e.Graphics.DrawLine(System.Drawing.Pens.Black, gridSize, gridSize * (j + 1), gridSize * (columnCount + 1), gridSize * (j + 1));

            // Draw the diagonal line
            e.Graphics.DrawLine(System.Drawing.Pens.Black, 0, 0, gridSize, gridSize);

            // Draw the y-axis binary codes
            for (int i = 0; i < rowCount; i++)
            {
                var gray = GrayCodeConverter.Decimal2Gray(i);
                e.Graphics.DrawString(Convert.ToString(gray, 2).PadLeft(2, '0'), Font, Brushes.Black, new RectangleF(0, (i + 1) * gridSize, gridSize, gridSize), sf);
            }

            // Save the transform, rotate
            var grid_transform = e.Graphics.Transform;
            e.Graphics.RotateTransform(-90);

            // Draw the y-axis title
            e.Graphics.DrawString(string.Join(", ", varsY), base.Font, Brushes.Black, new RectangleF(-Height + Font.Height, -Font.Height, Height - Font.Height - gridSize, Font.Height), sf);

            // Draw the x-axis binary codes
            for (int i = 0; i < columnCount; i++)
            {
                var gray = GrayCodeConverter.Decimal2Gray(i);
                e.Graphics.DrawString(Convert.ToString(gray, 2).PadLeft(3, '0'), Font, Brushes.Black, new RectangleF(-gridSize, (i + 1) * gridSize, gridSize, gridSize), sf);
            }

            // Restore the transform
            e.Graphics.Transform = grid_transform;

            // Draw the box values
            for (int i = 0; i < columnCount; i++)
            {
                for (int j = 0; j < rowCount; j++)
                {
                    var index = GridIndexToNumber(i, j);
                    string text;
                    if (ones.Contains(index))
                    {
                        if (zeros.Contains(index))
                            text = "X";
                        else
                            text = "1";
                    }
                    else
                    {
                        if (zeros.Contains(index))
                            text = "0";
                        else
                            text = "-";
                    }

                    e.Graphics.DrawString(text, Font, Brushes.Black, new RectangleF((i + 1) * gridSize, (j + 1) * gridSize, gridSize, gridSize), sf);
                }
            }

            // Draw the focused cell
            var rct = new Rectangle((focusedCell.X + 1) * gridSize, (focusedCell.Y + 1) * gridSize, gridSize, gridSize);
            rct.Inflate(-2, -2);
            var pen = new Pen(Color.Black, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dot };
            e.Graphics.DrawRectangle(pen, rct);
        }

        private void KarnaughMap_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.X < gridSize + Font.Height) return;
            if (e.Y < gridSize + Font.Height) return;

            var x = (e.X - gridSize - Font.Height) / gridSize;
            var y = (e.Y - gridSize - Font.Height) / gridSize;

            var i_gray = GrayCodeConverter.Decimal2Gray(x);
            var j_gray = GrayCodeConverter.Decimal2Gray(y);
            var index = j_gray * (1 << varsX.Length) + i_gray;
            ToggleNumber(index);

            Invalidate();
        }

        #region Helper methods
        private int GridIndexToNumber(Point gridIndex)
        {
            return GridIndexToNumber(gridIndex.X, gridIndex.Y);
        }
        private int GridIndexToNumber(int x, int y)
        {
            var x_gray = GrayCodeConverter.Decimal2Gray(x);
            var y_gray = GrayCodeConverter.Decimal2Gray(y);

            var result = y_gray * (1 << varsX.Length) + x_gray;
            return result;
        }
        private void ToggleNumber(int index)
        {
            if (zeros.Contains(index))
            {
                if (ones.Contains(index))
                {
                    ones.Remove(index);
                    zeros.Remove(index);
                }
                else
                {
                    zeros.Remove(index);
                    ones.Add(index);
                }
            }
            else
            {
                if (ones.Contains(index))
                {
                    zeros.Add(index);
                }
                else
                {
                    zeros.Add(index);
                }
            }
        }
        #endregion
    }
}
