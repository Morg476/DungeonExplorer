using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    
    public class Item
    {
        
        public string Name { get; private set; }     
        public string Description { get; private set; }
        public ItemType ItemType { get; private set; }    
        public int Value { get; private set; }         

        
        public Item(string name, string description, ItemType type, int value)
        {
            Name = name;
            Description = description;
            ItemType = type;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Name} - {Description} (Value: {Value} Gold)";
        }

        public virtual void Use(Player player)
        {
            Console.WriteLine($"You use {Name}. It has no effect.");
        }
    }


    public enum ItemType
    {
        Weapon,
        Potion,
        Key,
        Armor,
        Misc
    }

    public class Potion : Item
    {
        public int HealingAmount { get; set; }

        public Potion(string name, string description, int healingAmount, int value)
            : base(name, description, ItemType.Potion, value)
        {
            HealingAmount = healingAmount;
        }

        public override void Use(Player player)
        {
            player.Health += HealingAmount;
            Console.WriteLine($"{player.Name} drinks {Name} and heals {HealingAmount} HP!");
        }
    }

    public class Weapon : Item
    {
        public int AttackBonus { get; private set; }

        public Weapon(string name, string description, int attackBonus, int value)
            : base(name, description, ItemType.Weapon, value)
        {
            AttackBonus = attackBonus;
        }

        public override void Use(Player player)
        {
            player.Attack += AttackBonus;
            Console.WriteLine($"{player.Name} equips {Name} and gains {AttackBonus} attack!");
        }
    }

    public class Key : Item
    {
        public string KeyForRoom { get; private set; }

        // Corrected constructor (ItemType.Key)
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
