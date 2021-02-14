using Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuineMcCluskey.Abstractions
{
    public interface IQuineMcCluskey
    {
        Task<ImplicantsResult> FindImplicants(IEnumerable<int> minterms, IEnumerable<int> dontcares);
    }
}
