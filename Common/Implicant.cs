namespace Common
{
    /// <summary>Represents an implicant as result of the Quine-McCluskey algorithm.</summary>
    public class Implicant
    {
        public Implicant(LogicState[] data, int[] minterms)
        {
            Data = data;
            Minterms = minterms;
        }

        /// <summary>Contains the representation of this loop inside the Quine-McCluskey table (eg. 0X01X).</summary>
        public LogicState[] Data { get; }

        /// <summary>Minterms contained in this loop.</summary>
        public int[] Minterms { get; }

    }
}
