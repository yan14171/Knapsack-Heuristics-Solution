using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba4
{
	public class Item
	{

		public string Name;
		public double Weight;
		public double Price;

		public Item(string name, double weight, double price)
		{
			this.Name = name;
			this.Weight = weight;
			this.Price = price;
		}
		
		override public bool Equals(object obj)
		{
			if (!(obj is Item)) return false;
			if (obj == this) return true;

			Item castedObject = (Item)obj;

			return castedObject.Name.Equals(Name) &&
				castedObject.Weight == Weight && 
				castedObject.Price == Price;
		}
		
		override public int GetHashCode()
		{
			return Name.GetHashCode() + Weight.GetHashCode() + Price.GetHashCode();
		}

		override public string ToString()
		{
			return "Item [name=" + Name + ", weight=" + Weight + ", price=" + Price + " ]";
		}
	}
}
