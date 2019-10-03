using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuineMcCluskey.Data.QuineMcCluskey.Table2
{
    internal class Table
    {
        public List<Row> Rows { get; set; }
        public List<Column> Columns { get; set; }

        public IEnumerable<Row> FindRowsForColumn(Column column, bool include_ignored = false)
        {
            return Rows
                .Where(r => include_ignored | new[] { eRowStatus.Neutral, eRowStatus.Required }.Contains(r.Status))
                .Where(r => r.Loop.MinTerms.Contains(column.Minterm));
        }
    }
}
