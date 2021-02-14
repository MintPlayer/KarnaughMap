using Common;
using QuineMcCluskey.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace QuineMcCluskey.Data
{
    /// <summary>Represents a group of minterms that belong together.</summary>
    internal class Loop : ILoop
    {
        /// <summary>Creates a new group of minterms.</summary>
        /// <param name="minterms">Minterms contained in this loop.</param>
        /// <param name="data">Representation inside the Quine-McCluskey table.</param>
        public Loop(IEnumerable<int> minterms, IEnumerable<LogicState> data)
        {
            Minterms = minterms.ToArray();
            Data = data.ToArray();
        }

        /// <summary>Contains the representation of this loop inside the Quine-McCluskey table (eg. 0X01X).</summary>
        public LogicState[] Data { get; private set; }

        /// <summary>Minterms contained in this loop.</summary>
        public int[] Minterms { get; private set; }

        /// <summary>Specifies whether this loop has been used during the Quine-McCluskey algorithm.</summary>
        public bool Used { get; set; }
    }
}
