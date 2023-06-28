using MintPlayer.PetricksMethod.Abstractions;
using MintPlayer.PetricksMethod.Abstractions.Data;
using MintPlayer.QuineMcCluskey.Abstractions.Data;

namespace MintPlayer.PetricksMethod;

internal class PetricksMethod : IPetricksMethod
{
    public Task<Implicant[]> Solve(IEnumerable<int> minterms, IEnumerable<Implicant> unused)
    {
        var product = minterms.Select(m => new Sum
        {
            Groups = unused
                .Where(i => i.Minterms.Contains(m))
                .Select(i => new ImplicantGroup { Implicants = new List<Implicant> { i } })
                .ToList(),
        }).ToArray();


    }
}