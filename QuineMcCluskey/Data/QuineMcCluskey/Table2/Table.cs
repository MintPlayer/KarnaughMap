using QuineMcCluskey.Data.QuineMcCluskey.Table1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuineMcCluskey.Data.QuineMcCluskey.Table2
{
    internal class Table
    {
        public Table(List<int> minterms, List<Record> loops)
        {
            Rows = loops.Select(l => new Row
            {
                Record = l,
                Status = eRowStatus.Neutral
            }).ToList();

            Columns = minterms.Select(m => new Column 
            {
                MinTerm = m,
                Status = eColumnStatus.Unused,
                Rows = Rows.Where(r => r.Record.MinTerms.Contains(m)).ToList()
            }).ToList();
        }

        public List<Row> Rows { get; private set; }
        public List<Column> Columns { get; private set; }
    }
}
