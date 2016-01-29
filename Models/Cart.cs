using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmStore0906.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Fruit fruit, int quantity)
        {
            CartLine line = lineCollection
                .Where(f => f.Fruit.Id == fruit.Id)
                .FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new CartLine { Fruit = fruit, Quantity = quantity });

            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void UpdateItem(Fruit fruit, int quantity)
        {
            CartLine line = lineCollection
                .Where(p => p.Fruit.Id == fruit.Id)
                .FirstOrDefault();
            if (line != null)
            {
              
                line.Quantity = quantity;
            }
        }

        public void RemoveLine(Fruit fruit)
        {
            lineCollection.RemoveAll(l => l.Fruit.Id == fruit.Id);

        }

        public decimal ComputeTotalValue()
        {
            return (decimal)lineCollection.Sum(e=>e.Fruit.Price*e.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines { get { return lineCollection; } }
    }

    public class CartLine
    {
        public Fruit Fruit { get; set; }
        public int Quantity { get; set; }
    }
}