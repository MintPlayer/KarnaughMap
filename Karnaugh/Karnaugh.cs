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

        public Task<IEnumerable<Implicant>> Resolve(IEnumerable<int> minterms, IEnumerable<int> dontcares)
        {
            throw new System.NotImplementedException();
        }
    }
}
