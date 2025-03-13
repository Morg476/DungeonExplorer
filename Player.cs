using System.Collections.Generic;
using System;

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
    public List<string> Inventory { get; set; } = new List<string>();

    /// <summary>
    /// Intialises a new instance of the player class with the specified attributes within the paramaters. 
    /// </summary>
    /// <param name="name">The name of the player</param>
    /// <param name="health">The health of the player</param>
    /// <param name="currentRoom">The room where the player begins</param>
    public Player(string name, int health, Room currentRoom)
    {
        Name = name;
        Health = health;
        CurrentRoom = currentRoom;
    }

    public void PickUpItem(string item)
    {
        Console.WriteLine($"You picked up the {item}!\n");
        Inventory.Add(item);
    }

    public string InventoryContents => Inventory.Count > 0 ? string.Join(", ", Inventory) : "Your inventory is empty.";
}
