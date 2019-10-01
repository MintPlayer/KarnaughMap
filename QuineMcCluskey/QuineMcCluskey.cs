using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
//using QuineMcCluskey.Data;

namespace QuineMcCluskey
{
    public static class QuineMcCluskey
    {
        public static void QMC_Solve(IEnumerable<int> minterms, IEnumerable<int> dontcares)
        {
            //var table = QMC_CreateTable(minterms, dontcares);

            var table = CreateTable(minterms);

            for (int i = 0; i < table.Count - 1; i++)
            {
                for (int j = 0; j < table[i].Count - 1; j++)
                {
                    for (int k = 0; k < table[i][j].Count; k++)
                    {
                        var term1 = table[i][j][k];
                        for (int l = 0; l < table[i][j + 1].Count; l++)
                        {
                            var term2 = table[i][j + 1][l];
                            var res = CompareItems(term1, term2);
                            if (res == null) continue;
                            if (table[i + 1][j].Contains(res)) continue;

                            table[i + 1][j].Add(res);
                        }
                    }
                }
            }
        }

        private static string CompareItems(string item1, string item2)
        {
            var res = "";
            var differences = 0;
            for (int m = 0; m < item1.Length; m++)
            {
                if ((item1[m] == 'X') ^ (item2[m] == 'X')) return null;

                if (item1[m] == item2[m]) res += item1[m];
                else if (++differences > 1) return null;
                else res += 'X';
            }

            return res;
        }

        private static List<List<List<string>>> CreateTable(IEnumerable<int> minterms)
        {
            var table = new List<List<List<string>>>();
            var bin_minterms = minterms.Select(m => Convert.ToString(m, 2));
            var bits = bin_minterms.Max(m => m.Length);
            var bin_minterms_padded = bin_minterms.Select(m => m.PadLeft(bits, '0'));

            for (int i = 0; i <= bits; i++)
            {
                var column = new List<List<string>>();
                for (int j = 0; j < bits - i + 1; j++)
                    column.Add(new List<string>());

                table.Add(column);
            }

            foreach (var number in bin_minterms_padded)
                table[0][number.Count(n => n == '1')].Add(number);

            return table;
        }

        //private static Table QMC_CreateTable(IEnumerable<int> minterms, IEnumerable<int> dontcares)
        //{
        //    var minterm_records = minterms.Select(m => new Record(m));
        //    var groups = minterm_records.GroupBy(m => m.NumberOfOnes).Select(g => new Group(g.Key, g.ToArray()));
        //    var column0 = new Column(0, groups);

        //    var table = new Table();
        //    table.Add(column0);
        //    return table;
        //}
    }
}
