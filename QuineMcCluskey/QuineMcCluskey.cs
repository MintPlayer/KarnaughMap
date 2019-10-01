using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuineMcCluskey.Data.QuineMcCluskey.Table1;
using QuineMcCluskey.Enums;
//using QuineMcCluskey.Data;

namespace QuineMcCluskey
{
    public static class QuineMcCluskey
    {
        public static void QMC_Solve(IEnumerable<int> minterms, IEnumerable<int> dontcares)
        {
            //var table = QMC_CreateTable(minterms, dontcares);

            var table = CreateTable(minterms);

            for (int i = 0; i < table.Columns.Count - 1; i++)
            {
                for (int j = 0; j < table.Columns[i].Groups.Count - 1; j++)
                {
                    for (int k = 0; k < table.Columns[i].Groups[j].Records.Count; k++)
                    {
                        var term1 = table.Columns[i].Groups[j].Records[k];
                        for (int l = 0; l < table.Columns[i].Groups[j + 1].Records.Count; l++)
                        {
                            var term2 = table.Columns[i].Groups[j + 1].Records[l];
                            var res = Record.CompareItems(term1, term2);
                            if (res == null) continue;
                            if (table.Columns[i + 1].Groups[j].Records.Any(r => res.Data.SequenceEqual(r.Data))) continue;

                            table.Columns[i + 1].Groups[j].Records.Add(res);
                        }
                    }
                }
            }
        }

        private static Table CreateTable(IEnumerable<int> minterms)
        {
            var table = new Table();
            var bin_minterms = minterms.Select(m => Convert.ToString(m, 2));
            var bits = bin_minterms.Max(m => m.Length);
            var bin_minterms_padded = bin_minterms
                .Select(m => m.PadLeft(bits, '0'))
                .Select(m => m.Select(b =>
                {
                    switch (b)
                    {
                        case '0':
                            return Enums.LogicState.False;
                        case '1':
                            return Enums.LogicState.True;
                        default:
                            return Enums.LogicState.DontCare;
                    }
                }));
            
            for (int i = 0; i <= bits; i++)
            {
                var column = new Column();
                for (int j = 0; j < bits - i + 1; j++)
                    column.Groups.Add(new Group());

                table.Columns.Add(column);
            }

            foreach (var minterm in bin_minterms_padded)
                table.Columns[0].Groups[minterm.Count(n => n == Enums.LogicState.True)].Records.Add(new Record(minterm.ToArray()));

            return table;
        }
    }
}
