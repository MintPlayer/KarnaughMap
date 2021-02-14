using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Common;
using QuineMcCluskey.Abstractions;

namespace QuineMcCluskey
{
    internal class QuineMcCluskey : IQuineMcCluskey
    {
        private readonly ILoopCombiner loopCombiner;
        public QuineMcCluskey(ILoopCombiner loopCombiner)
        {
            this.loopCombiner = loopCombiner;
        }

        public async Task<ImplicantsResult> FindImplicants(IEnumerable<int> minterms, IEnumerable<int> dontcares)
        {
            var table = CreateTable(minterms.Union(dontcares), out var numberOfBits);
            await SolveTable(table);

            var unused = table.Columns
                .SelectMany(c => c.Groups)
                .SelectMany(g => g.Loops)
                .Cast<Data.Loop>()
                .Where(l => !l.Used)
                .Select(l => new Implicant(l.Data, l.Minterms));

            return new ImplicantsResult
            {
                Implicants = unused,
                NumberOfBits = numberOfBits
            };
        }

        private Data.Table CreateTable(IEnumerable<int> minterms, out int numberOfBits)
        {
            var table = new Data.Table();

            // If there are no minterms at all, return empty table
            if (!minterms.Any())
            {
                numberOfBits = 0;
                return table;
            }

            // Convert to binary
            var bin_minterms = minterms.Select(m => new
            {
                Binary = Convert.ToString(m, 2),
                Decimal = m
            });

            // Compute number of bits
            var bits = numberOfBits = bin_minterms.Max(m => m.Binary.Length);

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

            // Add columns to the table
            for (int i = 0; i <= bits; i++)
            {
                var column = new Data.Column();
                for (int j = 0; j < bits - i + 1; j++)
                    column.Groups.Add(new Data.Group());

                table.Columns.Add(column);
            }

            // Add data to the first column
            foreach (var minterm in bin_minterms_padded)
            {
                table
                    .Columns[0]
                    .Groups[minterm.Binary.Count(n => n == LogicState.True)]
                    .Loops.Add(new Data.Loop(new[] { minterm.Decimal }, minterm.Binary.ToArray()));
            }

            return table;
        }

        private async Task SolveTable(Data.Table table)
        {
            for (int i = 0; i < table.Columns.Count - 1; i++)
            {
                for (int j = 0; j < table.Columns[i].Groups.Count - 1; j++)
                {
                    for (int k = 0; k < table.Columns[i].Groups[j].Loops.Count; k++)
                    {
                        var term1 = table.Columns[i].Groups[j].Loops[k] as Data.Loop;
                        for (int l = 0; l < table.Columns[i].Groups[j + 1].Loops.Count; l++)
                        {
                            var term2 = table.Columns[i].Groups[j + 1].Loops[l] as Data.Loop;
                            var res = await loopCombiner.CompareLoops(term1, term2);
                            if (res == null) continue;

                            // Mark records as used
                            term1.Used = term2.Used = true;

                            if (table.Columns[i + 1].Groups[j].Loops.Any(r => res.Data.SequenceEqual(r.Data))) continue;

                            table.Columns[i + 1].Groups[j].Loops.Add(res);
                        }
                    }
                }
            }
        }
    }
}
