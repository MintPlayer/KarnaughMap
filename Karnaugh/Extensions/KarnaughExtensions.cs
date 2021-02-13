using Karnaugh.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Karnaugh.Extensions
{
    public static class KarnaughExtensions
    {
        public static IServiceCollection AddKarnaugh(this IServiceCollection services)
        {
            return services
                .AddTransient<IKarnaugh, Karnaugh>();
        }
    }
}
