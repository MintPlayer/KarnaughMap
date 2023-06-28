using MintPlayer.QuineMcCluskey.Abstractions.Enums;

namespace MintPlayer.QuineMcCluskey.Abstractions.Data;

public class Table
{
    public List<Column> Columns { get; init; } = new List<Column>();
}

public class Column
{
    public List<Group> Groups { get; init; } = new List<Group>();
}

public class Group
{
    public List<Implicant> Loops { get; init; } = new List<Implicant>();
}

public class Implicant
{
    public Implicant(int[] minterms, ELogicState[] data)
    {
        Minterms = minterms;
        Data = data;
    }

    public int[] Minterms { get; }
    public ELogicState[] Data { get; }
    public bool Used { get; set; }

    public override string ToString()
    {
        return new string(Data.Select(d =>
        {
            switch (d)
            {
                case ELogicState.False: return '0';
                case ELogicState.True: return '1';
                default: return 'X';
            }
        }).ToArray());
    }

    public string ToString(string[] inputVariables)
    {
        return string.Join(" ", inputVariables.Select((v, index) =>
        {
            int t = index - (inputVariables.Length - Data.Length);
            if (t >= 0)
            {
                switch (Data[t])
                {
                    case ELogicState.False:
                        return $"{v}!";
                    case ELogicState.True:
                        return v;
                    default:
                        return string.Empty;
                }
            }
            else
            {
                return $"{v}!";
            }
        }).Where(s => !string.IsNullOrEmpty(s)));
    }

    public static Implicant CompareItems(Implicant item1, Implicant item2)
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
    }
}