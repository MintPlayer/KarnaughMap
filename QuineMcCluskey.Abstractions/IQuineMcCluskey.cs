using Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuineMcCluskey.Abstractions
{
    public interface IQuineMcCluskey
    {
        Task<IEnumerable<Implicant>> FindImplicants(IEnumerable<int> minterms, IEnumerable<int> dontcares);
    }
}
