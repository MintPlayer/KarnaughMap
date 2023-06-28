using MintPlayer.QuineMcCluskey.Abstractions.Data;

namespace MintPlayer.PetricksMethod.Abstractions;

public interface IPetricksMethod
{
    Task<Implicant[]> Solve(IEnumerable<int> minterms, IEnumerable<Implicant> unused);
}