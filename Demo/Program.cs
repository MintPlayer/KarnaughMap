using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

using Karnaugh.Abstractions;
using Karnaugh.Extensions;
using PetricksMethod.Extensions;
using QuineMcCluskey.Extensions;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection()
                .AddQuineMcCluskey()
                .AddPetricksMethod()
                .AddKarnaugh()
                .BuildServiceProvider();

            var minterms = new List<int> { 0, 1, 5, 7, 8, 10, 14, 15 };
            var dontcares = new List<int>();
            var karnaugh = services.GetService<IKarnaugh>();

            var implicants = karnaugh.Resolve(minterms, dontcares).Result;
        }
    }
}
