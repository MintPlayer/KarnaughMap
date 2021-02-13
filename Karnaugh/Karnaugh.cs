using Common;
using System.Collections.Generic;
using System.Threading.Tasks;

using Karnaugh.Abstractions;
using PetricksMethod.Abstractions;
using QuineMcCluskey.Abstractions;

namespace Karnaugh
{
    public class Karnaugh : IKarnaugh
    {
        private readonly IQuineMcCluskey quineMcCluskey;
        private readonly IPetricksMethod petricksMethod;
        public Karnaugh(IQuineMcCluskey quineMcCluskey, IPetricksMethod petricksMethod)
        {
            this.quineMcCluskey = quineMcCluskey;
            this.petricksMethod = petricksMethod;
        }

        public async Task<IEnumerable<Implicant>> Resolve(IEnumerable<int> minterms, IEnumerable<int> dontcares)
        {
            var primeImplicants = await quineMcCluskey.FindImplicants(minterms, dontcares);
            var essentialPrimeImplicants = await petricksMethod.FindEssentialImplicants(minterms, primeImplicants);
            return essentialPrimeImplicants;
        }
    }
}
