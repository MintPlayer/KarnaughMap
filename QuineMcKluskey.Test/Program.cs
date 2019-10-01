using System;

namespace QuineMcCluskey.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();
            while (true)
            {
                QuineMcCluskey.QMC_Solve(new[] { 0, 4, 8, 10, 11, 12, 15 }, new[] { 9, 14 });
                Console.ReadKey();
            }
        }
    }
}
