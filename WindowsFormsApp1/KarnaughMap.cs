using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;

namespace WindowsFormsApp1
{
    [DesignerSerializer(typeof(KarnaughMapSerializer), typeof(CodeDomSerializer))]
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
        public ObservableCollection<int> PlusTerms { get; set; }
        public ObservableCollection<int> MinTerms { get; set; }

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
                e.Graphics.DrawLine(System.Drawing.Pens.Black, gridSize * (i + 1), 0, gridSize * (i + 1), gridSize * rowCount);

            for (int j = 0; j <= rowCount; j++)
                e.Graphics.DrawLine(System.Drawing.Pens.Black, 0, gridSize * (j + 1), gridSize * columnCount, gridSize * (j + 1));

            e.Graphics.DrawLine(System.Drawing.Pens.Black, 0, 0, gridSize, gridSize);
        }
    }
}
