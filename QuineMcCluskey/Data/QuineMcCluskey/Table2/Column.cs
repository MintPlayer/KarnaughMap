using System.Collections.Generic;

namespace QuineMcCluskey.Data.QuineMcCluskey.Table2
{
    internal class Column
    {
        public int MinTerm { get; set; }
        public eColumnStatus Status { get; set; }
        public List<Row> Rows { get; set; }
    }

    internal enum eColumnStatus
    {
        Unused,
        Covered
    }
}
