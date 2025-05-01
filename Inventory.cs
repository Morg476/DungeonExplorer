using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    // Represents the player's inventory that holds all items
    public class Inventory
    {
        public List<Item> Items { get; private set; } // List to store all items in the inventory

        // Constructor to initialize an empty inventory
        public Inventory()
        {
            Items = new List<Item>();
        }

        // Adds an item to the inventory
        public void AddItem(Item item)
        {
            Items.Add(item);
        }

        // Removes an item from the inventory
        public void RemoveItem(Item item)
        {
            Items.Remove(item);
        }

        // Checks if a specific item is in the inventory
        public bool HasItem(Item item)
        {
            return Items.Contains(item);
        }

        // Displays all items in the inventory
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
                    Console.WriteLine($"- {item}"); // Print each item in the inventory
                }
            }
        }

        // Returns a string containing all inventory items, or a message if it's empty
        public string GetInventoryContents()
        {
            return Items.Any() ? string.Join(", ", Items) : "Your inventory is empty.";
        }

        //Displays items sorted by their gold value (highest value first)
        public void DisplaySortedByValue()
        {
            var sortedItems = Items.OrderByDescending(i => i.Value); // Sort by value in descending order
            Console.WriteLine("\nInventory (sorted by value):");
            foreach (var item in sortedItems)
            {
                Console.WriteLine($"- {item.Name} - {item.Description} ({item.Value} gold)"); // Display each item with its value
            }
        }

        //Displays only weapons from the inventory
        public void DisplayWeapons()
        {
            var weapons = Items.Where(i => i.ItemType == ItemType.Weapon).Cast<Weapon>(); // Filter weapons
            Console.WriteLine("\nWeapons Only:");
            if (weapons.Any()) // Check if there are any weapons
            {
                foreach (var weapon in weapons)
                {
                    Console.WriteLine($"- {weapon.Name} (+{weapon.AttackBonus} Attack)"); // Display each weapon with its attack bonus
                }
            }
            else
            {
                Console.WriteLine("You have no weapons."); // If no weapons exist, inform the player
            }
        }

        //Displays only potions from the inventory
        public void DisplayPotions()
        {
            var potions = Items.Where(i => i.ItemType == ItemType.Potion).Cast<Potion>(); // Filter potions
            Console.WriteLine("\nPotions:");
            if (potions.Any()) // Check if there are any potions
            {
                foreach (var potion in potions)
                {
                    Console.WriteLine($"- {potion.Name} (+{potion.HealingAmount} HP)"); // Display each potion with its healing amount
                }
            }
            else
            {
                Console.WriteLine("You have no potions."); // If no potions exist, inform the player
            }
        }

        //Displays only keys from the inventory
        public void DisplayKeys()
        {
            var keys = Items.Where(i => i.ItemType == ItemType.Key).Cast<Key>(); // Filter keys
            Console.WriteLine("\nKeys:");
            if (keys.Any()) // Check if there are any keys
            {
                foreach (var key in keys)
                {
                    Console.WriteLine($"- {key.Name} (Opens: {key.KeyForRoom})"); // Display each key and the room it unlocks
                }
            }
            else
            {
                Console.WriteLine("You have no keys."); // If no keys exist, inform the player
            }
        }

        //Displays miscellaneous items from the inventory
        public void DisplayMiscItems()
        {
            var miscItems = Items.Where(i => i.ItemType == ItemType.Misc); // Filter miscellaneous items
            Console.WriteLine("\nMiscellaneous Items:");
            if (miscItems.Any()) // Check if there are any miscellaneous items
            {
                foreach (var item in miscItems)
                {
                    Console.WriteLine($"- {item.Name} - {item.Description}"); // Display each miscellaneous item with its description
                }
            }
            else
            {
                Console.WriteLine("You have no miscellaneous items."); // If no miscellaneous items exist, inform the player
            }
        }
    }
}
