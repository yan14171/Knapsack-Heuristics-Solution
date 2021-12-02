using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba4
{
	public class BeesColonyAlgorithm
	{

		public List<double> bestOfRun = new List<double>();
		public List<double> meanOfRun = new List<double>();
		public int iterWithBest = 0;

		protected KnapsackHeuristics ph;
		protected Knapsack originalParcel;
		protected List<Item> fullItemList;

		int maxIter;
		int[] beesDistribution;
		int sourcesToDump;

		public BeesColonyAlgorithm(Knapsack parcel,
			List<Item> fullItemList,
			KnapsackHeuristics ph, 
			int maxIter,
			int[] beesDistribution,
			int sourcesToDump)
		{
			this.originalParcel = parcel;
			this.fullItemList = fullItemList;
			this.ph = ph;
			this.maxIter = maxIter;
			this.beesDistribution = beesDistribution;
			this.sourcesToDump = sourcesToDump;
		}

        public Knapsack Run()
        {
            if (sourcesToDump > beesDistribution.Length) throw new Exception("MoreDumpedSourcesThanScouts");

            bestOfRun.Clear();
            meanOfRun.Clear();

            int currentIter;
            int numOfScouts = beesDistribution.Length;
            List<Knapsack> currentSources = ph.GenerateMultipleRandomKnapsacks(fullItemList, numOfScouts); // generate different rucksacks

            for (currentIter = 0; currentIter < maxIter; currentIter++)
            {
                Console.WriteLine("" + (currentIter + 1) + ": ");

                currentSources.Sort(new ReverseKnapsackComparer());

                for (int i = 0; i < numOfScouts; ++i)
                {
                    currentSources[i] = SendBees(currentSources[i], beesDistribution[i]);
                }

                currentSources.Sort(new KnapsackComparer());

                for (int i = 0; i < sourcesToDump; i++)
                {
                    currentSources[i] = ph.GenerateRandomKnapsack(fullItemList);
                }
                currentSources.Sort(new ReverseKnapsackComparer());
                foreach (var knapsack in currentSources)
                {
                    Console.WriteLine("" + knapsack.CurrentQuality + " ");
                }

                Console.WriteLine();

                bestOfRun.Add(currentSources[0].CurrentQuality);

                double sum = 0;
                long count = 0;
                foreach(Knapsack currentSource in currentSources)
                {
                    double currentQuality = currentSource.CurrentQuality;
                    sum += currentQuality;
                    count++;
                }
                meanOfRun.Add(count > 0 ? sum / (double)count : 0);
            }

            currentSources.Sort(new ReverseKnapsackComparer());
            return currentSources[0];

        }

        private Knapsack SendBees(Knapsack source, int numOfBeesToSend)
        {
            List<Knapsack> neighbours = new List<Knapsack>(ph.GenerateRandomNeighbours(fullItemList, source, numOfBeesToSend));
            bool seen = false;
            Knapsack result = null;
            foreach(Knapsack neighbour in neighbours)
            {
                if (!seen || neighbour.CompareTo(result) > 0)
                {
                    seen = true;
                    result = neighbour;
                }
            }
            Knapsack best = seen ? result : new Knapsack();
            return best.CompareTo(source) > 0 ? best : source;
        }


        public bool shouldStop()
            {
                return false;
            }
        }
}
