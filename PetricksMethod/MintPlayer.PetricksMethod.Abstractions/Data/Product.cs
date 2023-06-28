using MintPlayer.QuineMcCluskey.Abstractions.Data;

namespace MintPlayer.PetricksMethod.Abstractions.Data;

public class Product
{
    public List<Sum> Sums { get; init; }
}

public class Sum
{
    public List<ImplicantGroup> Groups { get; init; }
}

public class ImplicantGroup
{
    public List<Implicant> Implicants { get; init; }
}