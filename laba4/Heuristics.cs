using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba4
{
    public class KnapsackHeuristics
    {
        Random random;
        public KnapsackHeuristics()
        {
            random = new Random();
        }
        public Knapsack GenerateRandomKnapsack(List<Item> fullItemList)
        {
            Knapsack newKnapsack = new Knapsack();
            List<double> ItemsQuality = new List<double>();
            List<int> shuffleList = new List<int>();

            for (int i = 0; i < fullItemList.Count; i++)
            {
                shuffleList.Add(i);
            }

            var rnd = new Random();
            shuffleList = shuffleList.OrderBy(n => rnd.Next()).ToList();

            foreach (int x in shuffleList)
            {
                Item itemToPut = fullItemList.ElementAt(x);
                if (newKnapsack.HasItem(itemToPut)) continue;
                newKnapsack.AddItem(fullItemList.ElementAt(x));
            }

            return newKnapsack;
        }

        public List<Knapsack> GenerateMultipleRandomKnapsacks(List<Item> fullItemList, int numOfParcels)
        {

            List<Knapsack> result = new List<Knapsack>();

            while (result.Count!= numOfParcels)
            {
                Knapsack newParcel = GenerateRandomKnapsack(fullItemList);
                if (result.Contains(newParcel))
                {
                    break;
                }
                result.Add(newParcel);
            }
            return result;
        }

        public List<Knapsack> GenerateRandomNeighbours(List<Item> fullItemList, Knapsack Knapsack, int numOfKnapsacks)
        {
            //rucksacks to return
            List<Knapsack> Knapsacks = new List<Knapsack>(); 
            int KnapsackListLength = Knapsack.itemList.Count;

            for (int j = 0; j < numOfKnapsacks; j++)
            {  // the amount of neighbours to generate

                Knapsack newKnapsack = new Knapsack(); 

                // return the close laying items
                if (KnapsackListLength > 6)
                {
                    for (int i = 0; i < 6; i++)
                    { 
                        Item itemToPut = Knapsack.itemList.ElementAt(new Random().Next(KnapsackListLength));
                        if (newKnapsack.HasItem(itemToPut))
                        {
                            --i;
                            continue;
                        }
                        newKnapsack.AddItem(itemToPut); // write elements to new list
                    }
                }

                List<int> shuffleList = new List<int>();
                for (int i = 0; i < fullItemList.Count; i++)
                {
                    shuffleList.Add(i);   
                }
                shuffleList = shuffleList.OrderBy(n => random.Next()).ToList();
                foreach(int x in shuffleList)
                {
                    Item itemToPut = fullItemList[x];
                    if (newKnapsack.HasItem(itemToPut)) continue;
                    newKnapsack.AddItem(fullItemList[x]);     
                    //add random items to the list
                }

                // ПРОВЕРКА, ЧТО ПАКЕТЫ НЕ ДУБЛИРУЮТСЯ - УБИЙСТВА ВОКРУГ УМНЫХ
                // ДЕЛАЙТЕ ЭТО ПОТОМУ ЧТО ЭТО ЯВНО РАСШИРЯЕТ ИТЕРАЦИИ

                /*     for (Knapsack p : Knapsacks) {   
                         if (p.equals(newKnapsack)) {   
                             j--;
                             continue outer;
                         }
                     }
                 */
                Knapsacks.Add(newKnapsack);
            }
            return Knapsacks;
        }
    }
}
