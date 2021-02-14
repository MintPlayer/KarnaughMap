using QuineMcCluskey.Abstractions;
using System.Collections.Generic;

namespace QuineMcCluskey.Data
{
    /// <summary>Represents a group inside a table-column.</summary>
    internal class Group
    {
        public Group()
        {
            Loops = new List<ILoop>();
        }

        /// <summary>Records inside this group.</summary>
        public List<ILoop> Loops { get; }
    }
}
