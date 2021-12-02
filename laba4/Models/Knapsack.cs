using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba4
{
	public class Knapsack : IComparable 
	{
		public static double MAX_WEIGHT;
	
		public double CurrentWeight = 0;
		
		public double CurrentQuality  = 0;
		
		public ICollection<Item> itemList;

		public Knapsack()
		{
			itemList = new List<Item>();
		}

		public Knapsack(ICollection<Item> itemList)
		{
			this.itemList = itemList;
		}

		public bool HasItem(Item item)
		{
			return itemList.Contains(item);
		}

		/**
		 * Adds an item to the Knapsack if it is possible.
		 * If the item is already in the Knapsack, method will throw an exception, but will continue to run.
		 * The weight of the item should be less or equal than remaining weight in the Knapsack.
		 */
		public bool AddItem(Item item)
		{

			if (itemList.Contains(item)) throw new Exception();

			if (CurrentWeight + item.Weight > MAX_WEIGHT) return false;

			itemList.Add(item);
			CurrentWeight += item.Weight;
			CurrentQuality  += item.Price;
			return true;
		}

		public bool RemoveItem(Item item)
		{
			if (itemList.Remove(item))
			{
				CurrentWeight -= item.Weight;
				CurrentQuality  -= item.Price;
				return true;
			}
			return false;
		}

		public void RemoveItems()
		{              //17.122
			CurrentWeight = 0;
			CurrentQuality  = 0;
			itemList.Clear();
		}

		override public string ToString()
		{
			var result = "Knapsack [MAX_WEIGHT=" + MAX_WEIGHT + ", CurrentWeight=" + CurrentWeight + ", CurrentQuality =" + CurrentQuality
					+ ", itemList=";
			foreach(var item in itemList)
            {
				result += item.ToString()+"\n";
            }
			return result += " ]";
		}
		override public bool Equals(object o)
		{
			if (this == o) return true;
			if (o == null || this.GetType() != o.GetType()) return false;

			Knapsack Knapsack = (Knapsack)o;

			if (Knapsack.GetHashCode() != GetHashCode()) return false;

			HashSet<Item> hs = new HashSet<Item>(Knapsack.itemList);
			HashSet<Item> hs2 = new HashSet<Item>(itemList);
			return hs.Equals(hs2);
		}

		override public int GetHashCode()
		{
			return itemList != null ? itemList.GetHashCode() : 0;
		}
#nullable enable
		public int CompareTo(object? o)
		{
			if (o is not Knapsack)
				throw new ArgumentException("Passed non-knapsack object to CompareTo method of Knapsack");
			return (int)(CurrentQuality  - ((Knapsack)o).CurrentQuality);
		}
#nullable disable
	}
    public class KnapsackComparer : IComparer<Knapsack>
    {
        public int Compare(Knapsack x, Knapsack y) =>
			(int)(x.CurrentQuality - y.CurrentQuality);
    }
	public class ReverseKnapsackComparer : IComparer<Knapsack>
	{
		public int Compare(Knapsack x, Knapsack y) =>
			(int)(y.CurrentQuality - x.CurrentQuality);
	}
}
