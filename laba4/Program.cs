using System;
using System.Collections.Generic;

namespace laba4
{
    class Program
    {
        // Returns the maximum value that can be put in a knapsack of capacity W
        static int knapSack(int capacity, int[] weight, int[] val, int n)
        {
            // Base Case
            if (n == 0 || capacity == 0)
                return 0;

            // If weight of the nth item is more than Knapsack capacity W, then
            // this item cannot be included in the optimal solution
            if (weight[n - 1] > capacity)
                return knapSack(capacity, weight, val, n - 1);

            // Return the maximum of two cases:
            // (1) nth item included
            // (2) not included
            else return Math.Max(
                    val[n - 1] + knapSack(capacity - weight[n - 1], weight, val, n - 1),
                    knapSack(capacity, weight, val, n - 1)
            );
        }

        public static void Main(string[] args)
        {
            FileReader FileReader = FileReader.getInstance();
            FileReader.filename = "input.txt";
            FileReader.ReadKnapsack();
            List<Item> fullItemList = FileReader.ReadItems();

            int[] beesDistribution = { 70, 50, 30, 20, 15, 10, 8, 7, 6, 5, 4, 3, 2};
            //int[] beesDistribution = { 70, 50, 30, 20, 15, 8, 7, 6, 5, 4, 3, 2, 2, 2, 2, 1, 1, 1, 1 };

            BeesColonyAlgorithm algo1 =
                new BeesColonyAlgorithm(
                parcel: new Knapsack(),
                fullItemList: fullItemList,
                maxIter: 500,
                beesDistribution: beesDistribution,
                sourcesToDump: 5,
                ph: new KnapsackHeuristics());

            Knapsack p1 = algo1.Run();

            Console.WriteLine(p1);
        }

        static void ApproximateResult()
        {
            int[] val =
            new int[] { 67, 52, 15, 14, 46, 56, 82, 62, 61, 154, 45,
                    145, 112, 27, 101, 53, 112, 175, 142, 41, 99, 143, 144,
                    33, 20, 20, 21, 8, 89, 127, 162, 160, 32, 113, 109, 38,
                    94, 77, 21, 23 };

            int[] weight =
                new int[] { 100, 46, 43, 21, 25, 81, 82, 63, 42, 16, 57, 46,
                    83, 31, 68, 92, 77, 51, 97, 45, 19, 90, 19, 29, 22, 31,
                    29, 18, 54, 36, 95, 41, 36, 51, 33, 27, 22, 52, 19, 91 };

            int W = 400;
            int n = val.Length;

            Console.WriteLine(knapSack(W, weight, val, n));
        }
    }
}

