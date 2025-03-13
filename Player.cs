using System.Collections.Generic;
using System;
using System.Diagnostics;

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
        Debug.Assert(!string.IsNullOrWhiteSpace(name), "Player name cannot be null or empty.");
        Debug.Assert(health > 0, "Player health must be greater than zero.");
        Debug.Assert(currentRoom != null, "Current room cannot be null.");

        Name = name;
        Health = health;
        CurrentRoom = currentRoom;
    }

    /// <summary>
    /// Allows the player to pick up and item and will add it to inventory contents
    /// </summary>
    /// <param name="item">the item that is within the room they are in</param>
    public void PickUpItem(string item)
    {
        Debug.Assert(!string.IsNullOrWhiteSpace(item), "Item name cannot be null or empty.");
        Console.WriteLine($"You picked up the {item}!\n");
        Inventory.Add(item);
    }

    public string InventoryContents
    {
        get
        {
            if (Inventory.Count > 0)
            {
                return string.Join(", ", Inventory);
            }
            else
            {
                return "Your inventory is empty.";
            }
        }
    }
}
