namespace KarnaughMap
{
    internal static class GrayCodeConverter
    {
        public static int Decimal2Gray(int n)
        {
            return n ^ (n >> 1);
        }
    }
}
