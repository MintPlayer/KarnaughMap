using System.Collections.Generic;

namespace QuineMcCluskey.Data
{
    /// <summary>Represents a single column in the Quine-McCluskey table.</summary>
    internal class Column
    {
        public Column()
        {
            Groups = new List<Group>();
        }

        /// <summary>Groups inside a column, each item inside a group has the same number of "ones"</summary>
        public List<Group> Groups { get; set; }
    }
}
