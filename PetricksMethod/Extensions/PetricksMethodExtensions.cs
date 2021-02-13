using Microsoft.Extensions.DependencyInjection;
using PetricksMethod.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetricksMethod.Extensions
{
    public static class PetricksMethodExtensions
    {
        public static IServiceCollection AddPetricksMethod(this IServiceCollection services)
        {
            return services
                .AddTransient<IPetricksMethod, PetricksMethod>();
        }
    }
}
