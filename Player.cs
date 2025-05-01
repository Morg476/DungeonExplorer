using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DungeonExplorer
{
    public class Player : IDamageable
    {
        // Properties for the Player's attributes and state
        public string Name { get; set; }
        public int Health { get; set; }
        public Room CurrentRoom { get; set; }
        public List<Item> Inventory { get; private set; }
        public int Attack { get; set; } = 10;
        public int Defense { get; set; } = 5;
        private GameMap _gameMap;
        public Statistics Stats { get; private set; }

        /// <summary>
        /// Initializes a new instance of the player class.
        /// </summary>
        public Player(string name, int health, Room startingRoom, GameMap gameMap)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(name), "Player name cannot be null or empty.");
            Debug.Assert(health > 0, "Player health must be greater than zero.");
            Debug.Assert(startingRoom != null, "Current room cannot be null.");

            Name = name;
            Health = health;
            CurrentRoom = startingRoom;
            Inventory = new List<Item>();
            Stats = new Statistics();
            _gameMap = gameMap;
        }

        /// <summary>
        /// Allows the player to pick up an item.
        /// </summary>
        public void PickUpItem(Item item)
        {
            Debug.Assert(item != null, "Item cannot be null.");
            Console.WriteLine($"You picked up the {item.Name}!\n");
            Inventory.Add(item);
            Stats.RecordItemFound();
        }

        /// <summary>
        /// Displays the player's inventory, grouped by item type.
        /// </summary>
        public string InventoryContents
        {
            get
            {
                if (Inventory.Count > 0)
                {
                    var groupedItems = Inventory.GroupBy(i => i.ItemType)
                                                .OrderBy(group => group.Key)
                                                .Select(group => new
                                                {
                                                    ItemType = group.Key,
                                                    Items = group.OrderBy(i => i.Name)
                                                });

                    StringBuilder sb = new StringBuilder();

                    foreach (var group in groupedItems)
                    {
                        sb.AppendLine($"{group.ItemType}:");
                        foreach (var item in group.Items)
                        {
                            sb.AppendLine($"  - {item.Name}: {item.Description} (Value: {item.Value} Gold)");
                        }
                        sb.AppendLine();
                    }

                    return sb.ToString();
                }
                else
                {
                    return "Your inventory is empty.";
                }
            }
        }

        /// <summary>
        /// Allows the player to use an item from their inventory.
        /// </summary>
        public void UseItem(string itemName)
        {
            var item = Inventory.FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
            if (item == null)
            {
                Console.WriteLine("Item not found in inventory.");
                return;
            }

            // Use item based on its type
            switch (item.ItemType)
            {
                case ItemType.Potion:
                    UsePotion((Potion)item);
                    break;

                case ItemType.Weapon:
                    UseWeapon((Weapon)item);
                    break;

                case ItemType.Key:
                    UseKey((Key)item);
                    break;

                case ItemType.Misc:
                    UseMiscItem(item);
                    break;

                default:
                    Console.WriteLine($"{item.Name} can't be used directly.");
                    break;
            }
        }

        // Potion use: Healing logic
        private void UsePotion(Potion potion)
        {
            Health += potion.HealingAmount;
            if (Health > 100) Health = 100;
            Inventory.Remove(potion);
            Stats.RecordItemUsed();
            Console.WriteLine($"{Name} used {potion.Name} and recovered {potion.HealingAmount} HP! Current HP: {Health}");
        }

        // Weapon use: Attack bonus logic
        private void UseWeapon(Weapon weapon)
        {
            Attack += weapon.AttackBonus;
            Stats.RecordItemUsed();
            Console.WriteLine($"{Name} equips {weapon.Name} and gains {weapon.AttackBonus} attack power!");
        }

        // Key use (for now just outputs a message)
        private void UseKey(Key key)
        {
            Console.WriteLine($"You examine the {key.Name}, but it doesn't fit any lock here.");
        }

        // Misc item use (map usage logic)
        private void UseMiscItem(Item item)
        {
            if (item.Name.Equals("Map", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("You unfold the Map...");
                _gameMap?.DisplayConnectedRooms(CurrentRoom.Name);
                Stats.RecordItemUsed();
                Inventory.Remove(item);
                Console.WriteLine("The map is now removed from your inventory.");
            }
        }

        /// <summary>
        /// Takes damage from an attack, reducing health.
        /// </summary>
        public void TakeDamage(int damageAmount)
        {
            int actualDamage = Math.Max(damageAmount - Defense, 0);
            Health -= actualDamage;
            Health = Math.Max(Health, 0);
            Stats.RecordHealthLoss(actualDamage);
            Console.WriteLine($"{Name} takes {actualDamage} damage!\nRemaining HP: {Health}");
        }

        /// <summary>
        /// Handles when the player attacks a monster.
        /// </summary>
        public void AttackMonster(Monster monster)
        {
            int damageDealt = Math.Max(Attack - monster.Defense, 0);
            monster.DamageTaken(damageDealt);
            Console.WriteLine($"{Name} attacks {monster.Name} for {damageDealt} damage!");

            if (!monster.IsAlive())
            {
                Stats.RecordBattle(true);
            }
        }

        /// <summary>
        /// Checks if the player is alive.
        /// </summary>
        public bool IsAlive()
        {
            return Health > 0;
        }

        // Takes damage (alias for TakeDamage)
        public void DamageTaken(int damage) => TakeDamage(damage);
    }
}
