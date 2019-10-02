using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuineMcCluskey.Enums;
//using QuineMcCluskey.Data;

namespace QuineMcCluskey
{
    public static class QuineMcCluskey
    {
        public static void QMC_Solve(IEnumerable<int> minterms, IEnumerable<int> dontcares)
        {
            //var table = QMC_CreateTable(minterms, dontcares);

            // For table 1, include the don't cares
            var table1 = CreateTable1(minterms.Union(dontcares));
            QMC_SolveTable1(table1);

            var unused = table1.Columns
                .SelectMany(c => c.Groups)
                .SelectMany(g => g.Records)
                .Where(r => !r.Used).ToArray();




        }

        private static Data.QuineMcCluskey.Table1.Table CreateTable1(IEnumerable<int> minterms)
        {
            var table = new Data.QuineMcCluskey.Table1.Table();

            // Convert to binary
            var bin_minterms = minterms.Select(m => new
            {
                Binary = Convert.ToString(m, 2),
                Decimal = m
            });

            // Compute number of bits
            var bits = bin_minterms.Max(m => m.Binary.Length);

            // Pad the binary numbers, convert to logic states
            var bin_minterms_padded = bin_minterms
                .Select(m => new
                {
                    Binary = m.Binary.PadLeft(bits, '0').Select(b =>
                    {
                        switch (b)
                        {
                            case '0':
                                return LogicState.False;
                            case '1':
                                return LogicState.True;
                            default:
                                return LogicState.DontCare;
                        }
                    }),
                    m.Decimal
                });

            for (int i = 0; i <= bits; i++)
            {
                var column = new Data.QuineMcCluskey.Table1.Column();
                for (int j = 0; j < bits - i + 1; j++)
                    column.Groups.Add(new Data.QuineMcCluskey.Table1.Group());

                table.Columns.Add(column);
            }

            foreach (var minterm in bin_minterms_padded)
                table
                    .Columns[0]
                    .Groups[minterm.Binary.Count(n => n == LogicState.True)]
                    .Records.Add(new Data.QuineMcCluskey.Table1.Record(new[] { minterm.Decimal }, minterm.Binary.ToArray()));

            return table;
        }

        private static void QMC_SolveTable1(Data.QuineMcCluskey.Table1.Table table1)
        {
            for (int i = 0; i < table1.Columns.Count - 1; i++)
            {
                for (int j = 0; j < table1.Columns[i].Groups.Count - 1; j++)
                {
                    for (int k = 0; k < table1.Columns[i].Groups[j].Records.Count; k++)
                    {
                        var term1 = table1.Columns[i].Groups[j].Records[k];
                        for (int l = 0; l < table1.Columns[i].Groups[j + 1].Records.Count; l++)
                        {
                            var term2 = table1.Columns[i].Groups[j + 1].Records[l];
                            var res = Data.QuineMcCluskey.Table1.Record.CompareItems(term1, term2);
                            if (res == null) continue;

                            // Mark records as used
                            term1.Used = term2.Used = true;

                            if (table1.Columns[i + 1].Groups[j].Records.Any(r => res.Data.SequenceEqual(r.Data))) continue;

                            table1.Columns[i + 1].Groups[j].Records.Add(res);
                        }
                    }
                }
            }
        }

        private static Data.QuineMcCluskey.Table2.Table CreateTable2(IEnumerable<int> minterms, IEnumerable<Data.QuineMcCluskey.Table1.Record> loops)
        {
            var columns = minterms.Select(m => new Data.QuineMcCluskey.Table2.Column
            {
                MinTerm = m,
                NumberOfLoops = () => loops.Count(l => l.MinTerms.Contains(m)),
                Used = false
            }).ToList();

            var covered_minterms = new List<int>();

            // Find columns with only one loop
            var columns_with_one_loop = columns.Where(c => c.NumberOfLoops() == 1);
            if (columns_with_one_loop.Any())
            {
                //https://codeblog.jonskeet.uk/2006/01/20/foreachperf/
                //columns_with_one_loop.ToList().ForEach((colmn) =>
                //{

                //})

                foreach (var column in columns_with_one_loop)
                {
                    foreach (var required_loop in loops.Where(l => l.MinTerms.Contains(column.MinTerm)))
                    {
                        required_loop.Used = true;
                        covered_minterms.AddRange(required_loop.MinTerms.Except(covered_minterms));
                        covered_minterms.ForEach()
                    }
                }
            }
            else
            {

            }
        }
    }
}
