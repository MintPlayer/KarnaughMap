using MintPlayer.GrayConverter.Abstractions;

namespace MintPlayer.GrayConverter;

internal class GrayConverter : IGrayConverter
{
    public int Decimal2Gray(int n)
    {
        return n ^ (n >> 1);
    }

    public int Gray2Decimal(int n)
    {
        var mask = n >> 1;
        while (mask != 0)
        {
            n ^= mask;
            mask >>= 1;
        }
        return n;
    }
}