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

            Width = Height = 50;
            Paint += KarnaughMap_Paint;
        }

        public ObservableCollection<string> InputVariables { get; private set; } = new ObservableCollection<string>();
        public string OutputVariable { get; set; }

        private void KarnaughMap_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(System.Drawing.Color.Black);
        }
    }
}
