using Common;
using PetricksMethod.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetricksMethod
{
    internal class PetricksMethod : IPetricksMethod
    {
        public PetricksMethod()
        {
        }

        public Task<Implicant> FindEssentialImplicants(IEnumerable<int> minterms, IEnumerable<Implicant> implicants)
        {
            throw new System.NotImplementedException();
        }
    }
}
