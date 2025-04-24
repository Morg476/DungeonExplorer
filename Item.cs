using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    // Base class for an Item in the game
    public class Item
    {
        // Basic properties of an item
        public string Name { get; private set; }      // Name of the item (e.g., "Healing Potion")
        public string Description { get; private set; } // A short description of the item
        public ItemType Type { get; private set; }     // Type of item (e.g., Potion, Weapon, Key)
        public int Value { get; private set; }          // Value of the item (could be gold, or a stat boost)

        // Constructor to create a basic item
        public Item(string name, string description, ItemType type, int value)
        {
            Name = name;
            Description = description;
            Type = type;
            Value = value;
        }

        // Override ToString() to print item details
        public override string ToString()
        {
            return $"{Name} - {Description} (Value: {Value} Gold)";
        }

        // Use the item (could be overridden by specific item types)
        public virtual void Use(Player player)
        {
            Console.WriteLine($"You use {Name}. It has no effect.");
        }
    }

    // Enum for item types (e.g., weapons, potions, keys)
    public enum ItemType
    {
        Weapon,
        Potion,
        Key,
        Armor,
        Misc
    }

    // Example subclass for a Potion (extends Item class)
    public class Potion : Item
    {
        public int HealingAmount { get; private set; }

        public Potion(string name, string description, int healingAmount, int value)
            : base(name, description, ItemType.Potion, value)
        {
            HealingAmount = healingAmount;
        }

        // Override Use() to apply healing effect
        public override void Use(Player player)
        {
            player.Health += HealingAmount;
            Console.WriteLine($"{player.Name} drinks {Name} and heals {HealingAmount} HP!");
        }
    }

    // Example subclass for a Weapon (extends Item class)
    public class Weapon : Item
    {
        public int AttackBonus { get; private set; }

        public Weapon(string name, string description, int attackBonus, int value)
            : base(name, description, ItemType.Weapon, value)
        {
            AttackBonus = attackBonus;
        }

        // Override Use() to equip weapon and boost attack
        public override void Use(Player player)
        {
            player.Attack += AttackBonus;
            Console.WriteLine($"{player.Name} equips {Name} and gains {AttackBonus} attack!");
        }
    }

    // Example subclass for a Key (extends Item class)
    public class Key : Item
    {
        public string KeyForRoom { get; private set; }

        public Key(string name, string description, string keyForRoom, int value)
            : base(name, description, ItemType.Key, value)
        {
            KeyForRoom = keyForRoom;
        }

        // Override Use() to unlock rooms or doors
        public override void Use(Player player)
        {
            Console.WriteLine($"{player.Name} uses {Name} to unlock the {KeyForRoom} room!");
        }
    }
}
