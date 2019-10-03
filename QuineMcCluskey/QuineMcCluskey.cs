using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuineMcCluskey.Enums;
//using QuineMcCluskey.Data;

namespace QuineMcCluskey
{
    public static class QuineMcCluskeySolver
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
                .Where(r => !r.Used);

            CreateTable2(minterms.ToList(), unused.ToList());


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
                    .Records.Add(new Data.QuineMcCluskey.Table1.Loop(new[] { minterm.Decimal }, minterm.Binary.ToArray()));

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
                            var res = Data.QuineMcCluskey.Table1.Loop.CompareItems(term1, term2);
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

        private static void CreateTable2(List<int> minterms, List<Data.QuineMcCluskey.Table1.Loop> loops)
        {
            int loopCount = loops.Count, mintermCount = minterms.Count;

            var table = new Data.QuineMcCluskey.Table2.Table
            {
                Rows = loops.Select(l => new Data.QuineMcCluskey.Table2.Row { Loop = l, Status = Data.QuineMcCluskey.Table2.eRowStatus.Neutral }).ToList(),
                Columns = minterms.Select(m => new Data.QuineMcCluskey.Table2.Column { Minterm = m, Status = Data.QuineMcCluskey.Table2.eColumnStatus.NotUsed }).ToList()
            };


            // Loop through all columns
            for (int i = 0; i < table.Columns.Count; i++)
            {
                // Check if column only has one row
                var associated_rows = table.FindRowsForColumn(table.Columns[i]);
                if(associated_rows.Count() == 1)
                {
                    var associated_rows_list = associated_rows.ToList();
                 
                    // Mark row as required
                    associated_rows_list[0].Status = Data.QuineMcCluskey.Table2.eRowStatus.Required;

                    // Mark columns for this row as used.
                    foreach (var minterm in associated_rows_list[0].Loop.MinTerms)
                        table.Columns.First(c => c.Minterm == minterm).Status = Data.QuineMcCluskey.Table2.eColumnStatus.Used;
                }
            }


            //var data = new char[mintermCount][];

            //for (int i = 0; i < mintermCount; i++)
            //{
            //    //for (int j = 0; j < loopCount; j++)
            //    //{
            //    //    if (loops[j].MinTerms.Contains(minterms[i]))
            //    //        data[i][j] = '*';
            //    //}
            //    data[i] = loops.Select(l => l.MinTerms.Contains(minterms[i]) ? '*' : char.MinValue).ToArray();
            //}

            //for (int i = 0; i < mintermCount; i++)
            //{
            //    if(data[i].Count(r => r == '*') == 1)
            //        data
            //}

            //var columns_with_one_star = data.Where(c => c.Count(r => r == '*') == 1);
            //if(columns_with_one_star.Any())
            //{
            //    foreach (var c in columns_with_one_star)
            //    {
                    
            //    }
            //}
        }
    }
}
