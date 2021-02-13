using Common;
using QuineMcCluskey.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuineMcCluskey
{
    internal class QuineMcCluskey : IQuineMcCluskey
    {
        public QuineMcCluskey()
        {
        }

        public Task<IEnumerable<Implicant>> FindImplicants(IEnumerable<int> minterms, IEnumerable<int> dontcares)
        {
            throw new System.NotImplementedException();
        }
    }
}
