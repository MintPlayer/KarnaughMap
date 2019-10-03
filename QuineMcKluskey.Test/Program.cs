using QuineMcCluskey.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuineMcCluskey.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();
            var list = new List<int>();
            for (int i = 0; i < 64; i++)
            {
                var num = random.Next(64);
                if (!list.Contains(num)) list.Add(num);
            }

            while (true)
            {
                QuineMcCluskeySolver.QMC_Solve(list, new[] { 9, 14 });

                Console.ReadKey();
            }
        }
    }
}
