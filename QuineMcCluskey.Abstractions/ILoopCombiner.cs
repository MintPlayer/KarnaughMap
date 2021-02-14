using System.Threading.Tasks;

namespace QuineMcCluskey.Abstractions
{
    /// <summary>Contains logic that lets you compare and merge 2 loops.</summary>
    public interface ILoopCombiner
    {
        /// <summary>Merges 2 loops if possible. Returns null if the loops cannot be joined.</summary>
        /// <param name="loop1">First loop.</param>
        /// <param name="loop2">Second loop.</param>
        /// <param name="variableCount">Supply the number of variables yourself to increase speed.</param>
        Task<ILoop> CompareLoops(ILoop loop1, ILoop loop2, int variableCount = 0);
    }
}
