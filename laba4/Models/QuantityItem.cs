using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba4
{
    public class QuantityItem : Item
    {
        public double Quality;

        public QuantityItem(string name, double weight, double price)
            :base(name, weight, price)
        {
            this.Quality = price / weight;
        }

        override public string ToString()
        {
            return "Item [name=" + Name + ", weight=" + Weight + ", price=" + Price + ", quality=" + Quality + " ]";
        }

        override public int GetHashCode()
        {
            return Name.GetHashCode() + Weight.GetHashCode() + Price.GetHashCode() + Quality.GetHashCode();
        }

        override public bool Equals(object obj)
        {
                if (!(obj is QuantityItem)) return false;
                if (obj == this) return true;

                QuantityItem castedObject = (QuantityItem)obj;

                return castedObject.Name.Equals(Name) &&
                    castedObject.Weight == Weight &&
                    castedObject.Price == Price &&
                    castedObject.Quality == Quality;
        }
    }
}
