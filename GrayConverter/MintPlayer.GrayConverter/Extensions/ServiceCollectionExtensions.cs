using Microsoft.Extensions.DependencyInjection;
using MintPlayer.GrayConverter.Abstractions;

namespace MintPlayer.GrayConverter.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGrayConverter(this IServiceCollection services)
    {
        return services
            .AddTransient<IGrayConverter, GrayConverter>();
    }
}
