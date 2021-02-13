using Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetricksMethod.Abstractions
{
    public interface IPetricksMethod
    {
        Task<IEnumerable<Implicant>> FindEssentialImplicants(IEnumerable<int> minterms, IEnumerable<Implicant> implicants);
    }
}
