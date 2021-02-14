using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using QuineMcCluskey.Abstractions;
using Common;

namespace QuineMcCluskey
{
    internal class LoopCombiner : ILoopCombiner
    {
        public LoopCombiner()
        {
        }

        public Task<ILoop> CompareLoops(ILoop loop1, ILoop loop2, int variableCount = 0)
        {
            // Build the result
            var result = new List<LogicState>();

            // Count the deviations between the two loops
            var differences = 0;

            // Determine the number of variables
            if (variableCount == 0)
            {
                variableCount = Math.Max(loop1.Data.Length, loop2.Data.Length);
            }

            // Loop through every bit
            for (int m = 0; m < variableCount; m++)
            {
                // Check if only one of the values is a dont-care.
                if ((loop1.Data[m] == LogicState.DontCare) ^ (loop2.Data[m] == LogicState.DontCare))
                {
                    // Cannot be combined
                    return Task.FromResult<ILoop>(null);
                }

                if (loop1.Data[m] == loop2.Data[m])
                {
                    // Copy the value to the result
                    result.Add(loop1.Data[m]);
                }
                else if (++differences > 1)
                {
                    // Allow only 1 diversion
                    return Task.FromResult<ILoop>(null);
                }
                else
                {
                    // Merge 0 and 1 into dont-care
                    result.Add(LogicState.DontCare);
                }
            }

            return Task.FromResult<ILoop>(new Data.Loop(loop1.Minterms.Union(loop2.Minterms).ToArray(), result));
        }
    }
}
