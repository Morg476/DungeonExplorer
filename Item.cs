using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    // Represents an item in the game
    public class Item
    {
        public string Name { get; private set; }      // Name of the item
        public string Description { get; private set; } // Description of the item
        public ItemType ItemType { get; private set; }    // Type of the item (Weapon, Potion, etc.)
        public int Value { get; private set; }          // Value of the item in gold

        // Constructor to initalise the item
        public Item(string name, string description, ItemType type, int value)
        {
            Name = name;
            Description = description;
            ItemType = type;
            Value = value;
        }

        // ToString override to display the item information
        public override string ToString()
        {
            return $"{Name} - {Description} (Value: {Value} Gold)";
        }

        // Use method for items
        public virtual void Use(Player player)
        {
            Console.WriteLine($"You use {Name}. It has no effect.");
        }
    }

    // Enum to define different types of items
    public enum ItemType
    {
        Weapon,
        Potion,
        Key,
        Armor,
        Misc
    }

    // Map item class inherits from Item
    public class Map : Item
    {
        public Map(string name, string description)
            : base(name, description, ItemType.Misc, 0)  // Map has no value in gold
        {
        }

        public override void Use(Player player)
        {
            // Display the map usage and the connected rooms
            Console.WriteLine($"{player.Name} uses {Name}. The map reveals the dungeon layout!");

            // Display the connected rooms to simulate map's usefulness
            if (player.CurrentRoom != null)
            {
                Console.WriteLine("Rooms connected to your current location:");
                foreach (var room in player.CurrentRoom.ConnectedRooms)
                {
                    Console.WriteLine($"- {room}");
                }
            }
        }
    }




    // Potion class inherits from Item
    public class Potion : Item
    {
        public int HealingAmount { get; set; } // Amount of health restored by the potion

        // Constructor for Potion
        public Potion(string name, string description, int healingAmount, int value)
            : base(name, description, ItemType.Potion, value)
        {
            HealingAmount = healingAmount;
        }

        // Override Use method to heal the player when the potion is used
        public override void Use(Player player)
        {
            player.Health += HealingAmount;
            Console.WriteLine($"{player.Name} drinks {Name} and heals {HealingAmount} HP!");
        }
    }

    // Weapon class inherits from Item
    public class Weapon : Item
    {
        public int AttackBonus { get; private set; } // Bonus to player's attack from the weapon

        // Constructor for Weapon
        public Weapon(string name, string description, int attackBonus, int value)
            : base(name, description, ItemType.Weapon, value)
        {
            AttackBonus = attackBonus;
        }

        // Override Use method to equip the weapon and increase player's attack
        public override void Use(Player player)
        {
            player.Attack += AttackBonus;
            Console.WriteLine($"{player.Name} equips {Name} and gains {AttackBonus} attack!");
        }
    }

    // Key class inherits from Item
    public class Key : Item
    {
        public string KeyForRoom { get; private set; } // The room the key unlocks

        // Constructor for Key
        public Key(string name, string description, string keyForRoom, int value)
            : base(name, description, ItemType.Key, value)
        {
            KeyForRoom = keyForRoom;
        }

        // Override Use method to unlock the corresponding room or door
        public override void Use(Player player)
        {
            Console.WriteLine($"{player.Name} uses {Name} to unlock the {KeyForRoom} room!");
        }
    }
}
