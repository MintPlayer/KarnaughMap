using Microsoft.Extensions.DependencyInjection;
using MintPlayer.PetricksMethod.Abstractions;

namespace MintPlayer.PetricksMethod.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPetricksMethod(this IServiceCollection services)
    {
        return services.AddTransient<IPetricksMethod, PetricksMethod>();
    }
}
