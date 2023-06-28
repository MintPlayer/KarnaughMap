using MintPlayer.PetricksMethod.Abstractions;
using MintPlayer.QuineMcCluskey.Abstractions;
using MintPlayer.QuineMcCluskey.Abstractions.Enums;
using MintPlayer.QuineMcCluskey.Abstractions.Data;

namespace MintPlayer.QuineMcCluskey;

internal class QuineMcCluskey : IQuineMcCluskey
{
    #region Constructor
    private readonly IPetricksMethod petricksMethod;
    public QuineMcCluskey(IPetricksMethod petricksMethod)
    {
        this.petricksMethod = petricksMethod;
    }
    #endregion

    public async Task<Implicant[]> Solve(IEnumerable<int> minterms, IEnumerable<int> dontcares)
    {
        var table = await CreateTable(minterms.Union(dontcares));
        var solved = await SolveTable(table);
        var unused = table.Columns
            .SelectMany(c => c.Groups)
            .SelectMany(g => g.Loops)
            .Where(l => !l.Used)
            .ToArray();

        var primeImplicants = await petricksMethod.Solve(minterms.Except(dontcares), unused);

        return primeImplicants;
    }

    private async Task<Table> CreateTable(IEnumerable<int> minterms)
    {
        var table = new Table();

        // If there are no minterms at all, return empty table
        if (!minterms.Any()) return table;

        // Compute number of bits
        var numberOfBits = Convert.ToString(minterms.Max(), 2).Length;

        var binaryFirstColumn = minterms
            .Select(m =>
            {
                var mbin = Convert.ToString(m, 2)
                    .PadLeft(numberOfBits, '0')
                    .Select(b => b switch { '0' => ELogicState.False, '1' => ELogicState.True, _ => ELogicState.DontCare })
                    .ToArray();

                return new PrimeImplicant { Binary = mbin, Decimal = m, Group = mbin.Count(b => b == ELogicState.True) };
            })
            .GroupBy(m => m.Group)
            .ToDictionary(m => m.Key, m => m.ToArray());

        var columns = Enumerable.Range(0, numberOfBits + 1)
            .Select(i => new
            {
                Groups = Enumerable.Range(0, numberOfBits - i + 1)
                    .Select(j =>
                    {
                        if (i == 0) return binaryFirstColumn[j];
                        else return new PrimeImplicant[0];
                    })
                    .ToArray()
            })
            .ToArray();

        return table;
    }

    private async Task<Table> SolveTable(Table table)
    {
        for (int i = 0; i < table.Columns.Count - 1; i++)
        {
            for (int j = 0; j < table.Columns[i].Groups.Count - 1; j++)
            {
                for (int k = 0; k < table.Columns[i].Groups[j].Loops.Count; k++)
                {
                    var term1 = table.Columns[i].Groups[j].Loops[k];
                    for (int l = 0; l < table.Columns[i].Groups[j + 1].Loops.Count; l++)
                    {
                        var term2 = table.Columns[i].Groups[j + 1].Loops[l];
                        var res = await CompareItems(term1, term2);
                        if (res == null) continue;

                        // Mark records as used
                        term1.Used = term2.Used = true;

                        if (!table.Columns[i + 1].Groups[j].Loops.Any(r => res.Data.SequenceEqual(r.Data)))
                        {
                            table.Columns[i + 1].Groups[j].Loops.Add(res);
                        }
                    }
                }
            }
        }

        return table;
    }

    private Task<Implicant?> CompareItems(Implicant item1, Implicant item2)
    {
        return Task.Run<Implicant?>(() =>
        {
            var result = new List<ELogicState>();
            var differences = 0;
            for (int m = 0; m < item1.Data.Length; m++)
            {
                if ((item1.Data[m] == ELogicState.DontCare) ^ (item2.Data[m] == ELogicState.DontCare)) return null;

                if (item1.Data[m] == item2.Data[m]) result.Add(item1.Data[m]);
                else if (++differences > 1) return null;
                else result.Add(ELogicState.DontCare);
            }

            return new Implicant(item1.Minterms.Union(item2.Minterms).ToArray(), result.ToArray());
        });
    }

    class PrimeImplicant
    {
        public ELogicState[] Binary { get; init; }
        public int Decimal { get; init; }
        public int Group { get; init; }
    }
}