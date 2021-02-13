using Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Karnaugh.Abstractions
{
    public interface IKarnaugh
    {
        Task<IEnumerable<Implicant>> Resolve(IEnumerable<int> minterms, IEnumerable<int> dontcares);
    }
}
