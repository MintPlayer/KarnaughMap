using System.Collections.Generic;

namespace QuineMcCluskey.Data
{
    /// <summary>Table holding the data for the Quine-McCluskey algorithm.</summary>
    internal class Table
    {
        public Table()
        {
            Columns = new List<Column>();
        }

        /// <summary>Columns inside the table, representing a cycle of the algorithm.</summary>
        public List<Column> Columns { get; }
    }
}
