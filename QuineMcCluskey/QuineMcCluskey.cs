using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuineMcCluskey.Data.QuineMcCluskey.Table1;
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
                            if (table[i + 1][j].Any(r => r.Text == res)) continue;

                            table[i + 1][j].Add(new Record(res));
                        }
                    }
                }
            }
        }

        private static string CompareItems(Record item1, Record item2)
        {
            var res = "";
            var differences = 0;
            for (int m = 0; m < item1.Text.Length; m++)
            {
                if ((item1.Text[m] == 'X') ^ (item2.Text[m] == 'X')) return null;

                if (item1.Text[m] == item2.Text[m]) res += item1.Text[m];
                else if (++differences > 1) return null;
                else res += 'X';
            }

            return res;
        }

        private static Table CreateTable(IEnumerable<int> minterms)
        {
            var table = new Table();
            var bin_minterms = minterms.Select(m => Convert.ToString(m, 2));
            var bits = bin_minterms.Max(m => m.Length);
            var bin_minterms_padded = bin_minterms.Select(m => m.PadLeft(bits, '0'));

            for (int i = 0; i <= bits; i++)
            {
                var column = new Column();
                for (int j = 0; j < bits - i + 1; j++)
                    column.Add(new Group());

                table.Add(column);
            }

            foreach (var number in bin_minterms_padded)
                table[0][number.Count(n => n == '1')].Add(new Record(number));

            return table;
        }
    }
}
