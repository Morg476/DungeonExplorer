using System.Collections.Generic;
using System;
using System.Diagnostics;
using DungeonExplorer;
using System.Linq;

/// <summary>
/// Represents a player in the game, including the attributes, inventory and the player movements/actions
/// </summary>
public class Player
{
    /// <summary>
    /// Gets or sets the name of the player
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Gets or sets the health of the player
    /// </summary>
    public int Health { get; set; }
    /// <summary>
    /// Gets or sets the current room the player is in.
    /// </summary>
    public Room CurrentRoom { get; set; }
    /// <summary>
    /// Gets the players inventory which contains all of the collected items whilst the game is running.
    /// </summary>
    public List<Item> Inventory { get; private set; }

    public int Attack { get; set; } = 10;
    public int Defense { get; set; } = 5;

    /// <summary>
    /// Initializes a new instance of the player class with the specified attributes within the parameters.
    /// </summary>
    /// <param name="name">The name of the player</param>
    /// <param name="health">The health of the player</param>
    /// <param name="currentRoom">The room where the player begins</param>
    public Player(string name, int health, Room startingRoom)
    {
        Debug.Assert(!string.IsNullOrWhiteSpace(name), "Player name cannot be null or empty.");
        Debug.Assert(health > 0, "Player health must be greater than zero.");
        Debug.Assert(startingRoom != null, "Current room cannot be null.");

        Name = name;
        Health = health;
        CurrentRoom = startingRoom;
        Inventory = new List<Item>();
    }

    /// <summary>
    /// Allows the player to pick up an item and will add it to inventory contents
    /// </summary>
    /// <param name="item">The item that is within the room they are in</param>
    public void PickUpItem(Item item)
    {
        Debug.Assert(item != null, "Item cannot be null.");
        Console.WriteLine($"You picked up the {item.Name}!\n");
        Inventory.Add(item);
    }

    /// <summary>
    /// Displays the contents of the player's inventory
    /// </summary>
    public string InventoryContents
    {
        get
        {
            if (Inventory.Count > 0)
            {
                return string.Join(", ", Inventory.Select(i => i.Name));
            }
            else
            {
                return "Your inventory is empty.";
            }
        }
    }

    /// <summary>
    /// Method to process damage taken by the player
    /// </summary>
    public void DamageTaken(int damage)
    {
        int actualDamage = Math.Max(damage - Defense, 0);
        Health -= actualDamage;
        Health = Math.Max(Health, 0);
        Console.WriteLine($"{Name} takes {actualDamage} damage! Remaining HP: {Health}");
    }

    /// <summary>
    /// Allows the player to attack a monster
    /// </summary>
    public void AttackMonster(Monster monster)
    {
        int damageDealt = Math.Max(Attack - monster.Defense, 0);
        monster.DamageTaken(damageDealt);  // Assuming the monster has a DamageTaken method
        Console.WriteLine($"{Name} attacks {monster.Name} for {damageDealt} damage!");
    }
}
