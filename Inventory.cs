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
        public List<string> Items { get; private set; }

        // Constructor initializes the list
        public Inventory()
        {
            Items = new List<string>();
        }

        // Add item to inventory
        public void AddItem(string item)
        {
            if (!string.IsNullOrEmpty(item))
            {
                Items.Add(item);
                Console.WriteLine($"{item} added to inventory.");
            }
            else
            {
                Console.WriteLine("Invalid item name.");
            }
        }

        // Remove item from inventory
        public bool RemoveItem(string item)
        {
            if (Items.Contains(item))
            {
                Items.Remove(item);
                Console.WriteLine($"{item} removed from inventory.");
                return true;
            }
            else
            {
                Console.WriteLine($"{item} not found in inventory.");
                return false;
            }
        }

        // Check if an item is in the inventory
        public bool HasItem(string item)
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
