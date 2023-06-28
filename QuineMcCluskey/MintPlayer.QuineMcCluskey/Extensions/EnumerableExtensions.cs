namespace MintPlayer.QuineMcCluskey.Extensions;

internal static class EnumerableExtensions
{
    public static IEnumerable<Tuple<T, T>> Pairwise<T>(this IEnumerable<T> input)
    {
        return input.Zip(input.Skip(1), (a, b) => Tuple.Create(a, b));
    }
}
