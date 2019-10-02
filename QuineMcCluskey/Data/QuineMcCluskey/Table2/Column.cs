using System;

namespace QuineMcCluskey.Data.QuineMcCluskey.Table2
{
    internal class Column
    {
        public int MinTerm { get; set; }
        public Func<int> NumberOfLoops { get; set; }
        public bool Used { get; set; }
    }
}
