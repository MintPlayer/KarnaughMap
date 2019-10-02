using System;
using System.Collections.Generic;
using System.Text;

namespace QuineMcCluskey.Data.QuineMcCluskey.Table2
{
    internal class Row
    {
        public eRowStatus Status { get; set; }
    }

    internal enum eRowStatus
    {
        Neutral,
        Required
    }
}
