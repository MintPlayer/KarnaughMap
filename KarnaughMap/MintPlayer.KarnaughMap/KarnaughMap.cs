using MintPlayer.PetricksMethod.Abstractions;
using MintPlayer.QuineMcCluskey.Abstractions;
using MintPlayer.ObservableCollection;
using MintPlayer.GrayConverter.Abstractions;

namespace MintPlayer.KarnaughMap
{
    public partial class KarnaughMap : Control
    {
        public KarnaughMap()
        {
            InitializeComponent();
            DoubleBuffered = true;
            InputVariables = new ObservableCollection<string>();

            GotFocus += (sender, e) => Invalidate();
            LostFocus += (sender, e) => Invalidate();
            InputVariables.CollectionChanged += InputVariables_CollectionChanged;
        }

        /// <summary>Defines the width of a cell in the Karnaugh map.</summary>
        const int gridSize = 40;

        /// <summary>Holds the variable names that are projected on top of the Karnaugh map.</summary>
        private string[] varsX;
        /// <summary>Holds the variable names that are projected on the left of the Karnaugh map.</summary>
        private string[] varsY;
        /// <summary>The number of actual rows in the Karnaugh map.</summary>
        private int rowCount;
        /// <summary>The number of actual columns in the Karnaugh map.</summary>
        private int columnCount;
        /// <summary>The position of the currently focused cell.</summary>
        private Point focusedCell = new Point();
        /// <summary>Holds the minterms for "high".</summary>
        private List<int> ones = new List<int>();
        /// <summary>Holds the minterms for "low".</summary>
        private List<int> zeros = new List<int>();
        /// <summary>Holds the minterms that are selected</summary>
        private List<int> selectedCells = new List<int>();
        /// <summary>Holds the required loops for "high".</summary>
        private readonly ObservableCollection<MintPlayer.QuineMcCluskey.Abstractions.Data.Implicant> loops_ones;
        /// <summary>Holds the required loops for "low".</summary>
        private readonly ObservableCollection<MintPlayer.QuineMcCluskey.Abstractions.Data.Implicant> loops_zeros;


        public IQuineMcCluskey? QuineMcCluskey { get; set; }
        public IPetricksMethod? PetricksMethod { get; set; }
        public IGrayConverter? GrayConverter { get; set; }
        public ObservableCollection<string> InputVariables { get; private set; }


        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
