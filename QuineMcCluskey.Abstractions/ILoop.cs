using Common;

namespace QuineMcCluskey.Abstractions
{
    /// <summary>Represents a group of minterms that belong together.</summary>
    public interface ILoop
    {
        /// <summary>Contains the representation of this loop inside the Quine-McCluskey table (eg. 0X01X).</summary>
        LogicState[] Data { get; }

        /// <summary>Minterms contained in this loop.</summary>
        int[] Minterms { get; }
    }
}
