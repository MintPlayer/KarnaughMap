using Microsoft.Extensions.DependencyInjection;
using QuineMcCluskey.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuineMcCluskey.Extensions
{
    public static class QuineMcCluskeyExtensions
    {
        public static IServiceCollection AddQuineMcCluskey(this IServiceCollection services)
        {
            return services
                .AddTransient<IQuineMcCluskey, QuineMcCluskey>()
                .AddTransient<ILoopCombiner, LoopCombiner>();
        }
    }
}
