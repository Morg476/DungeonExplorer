using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class Inventory
    {
        // List of items in the inventory
        public List<Item> Items { get; private set; }

        // Constructor initializes the list
        public Inventory()
        {
            Items = new List<Item>();
        }

        // Add item to inventory
        public void AddItem(Item item)
        {
                Items.Add(item);
        }

        // Remove item from inventory
        public void RemoveItem(Item item)
        {
            Items.Remove(item);
            
        }

        // Check if an item is in the inventory
        public bool HasItem(Item item)
        {
            return Items.Contains(item);
        }

        // Display inventory contents
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

        // Get inventory as a string (for easier display in other classes)
        public string GetInventoryContents()
        {
            return Items.Any() ? string.Join(", ", Items) : "Your inventory is empty.";
        }
    }
}
