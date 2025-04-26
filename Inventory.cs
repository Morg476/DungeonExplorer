using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class Inventory
    {
        public List<Item> Items { get; private set; }

        public Inventory()
        {
            Items = new List<Item>();
        }

        public void AddItem(Item item)
        {
                Items.Add(item);
        }

        public void RemoveItem(Item item)
        {
            Items.Remove(item);
            
        }

        public bool HasItem(Item item)
        {
            return Items.Contains(item);
        }

        public void DisplayInventory()
        {
            if (Items.Count == 0)
            {
                Console.WriteLine("Your inventory is empty.");
            } 
            else
            {
                Console.WriteLine("Inventory:");
                foreach (var item in Items)
                {
                    Console.WriteLine($"- {item}");
                }
            }
        }

        public string GetInventoryContents()
        {
            return Items.Any() ? string.Join(", ", Items) : "Your inventory is empty.";
        }
    }
}
