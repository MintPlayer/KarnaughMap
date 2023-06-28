using MintPlayer.QuineMcCluskey.Abstractions.Data;

namespace MintPlayer.QuineMcCluskey.Abstractions;

public interface IQuineMcCluskey
{
    Task<Implicant[]> Solve(IEnumerable<int> minterms, IEnumerable<int> dontcares);
}