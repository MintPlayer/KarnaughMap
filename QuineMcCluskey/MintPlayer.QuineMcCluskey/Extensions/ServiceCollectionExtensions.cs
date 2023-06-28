using Microsoft.Extensions.DependencyInjection;
using MintPlayer.QuineMcCluskey.Abstractions;

namespace MintPlayer.QuineMcCluskey.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddQuineMcCluskey(this IServiceCollection services)
    {
        return services.AddTransient<IQuineMcCluskey, QuineMcCluskey>();
    }
}
