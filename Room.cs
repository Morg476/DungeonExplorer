using DungeonExplorer;
using System.Collections.Generic;
using System;
using System.Net.Http.Headers;
using System.Linq;

public class Room
{
    // Properties to define the characteristics of a room
    public string Name { get; set; }  // The name of the room
    public string Description { get; set; }  // The description of the room
    public List<Item> Items { get; set; }  // The list of items present in the room
    public List<string> Monsters { get; set; }  // The list of monsters present in the room
    public List<string> ConnectedRooms { get; private set; } = new List<string>();  // List of rooms connected to this room

    // Constructor to initalise a new room
    public Room(string name, string description, List<Item> items, List<string> monsters)
    {
        Name = name;  // Set the name of the room
        Description = description;  // Set the description of the room
        Items = items ?? new List<Item>();  // initalise the items list, default to an empty list if null
        Monsters = monsters ?? new List<string>();  // initalise the monsters list, default to an empty list if null
    }

    /// <summary>
    /// Adds a connection to another room if it doesn't already exist.
    /// </summary>
    /// <param name="roomName">The name of the room to connect to.</param>
    public void AddConnection(string roomName)
    {
        if (!ConnectedRooms.Contains(roomName))  // Only add if the room isn't already connected
        {
            ConnectedRooms.Add(roomName);  // Add the room to the list of connected rooms
        }
    }

    /// <summary>
    /// Returns the description of the room.
    /// </summary>
    /// <returns>The description of the room.</returns>
    public string GetDescription() => Description;

    /// <summary>
    /// Checks if there are any items in the room.
    /// </summary>
    /// <returns>True if there are items in the room, false otherwise.</returns>
    public bool HasItems() => Items.Any();

    /// <summary>
    /// Checks if there are any monsters in the room.
    /// </summary>
    /// <returns>True if there are monsters in the room, false otherwise.</returns>
    public bool HasMonsters() => Monsters.Any();

    /// <summary>
    /// Removes an item from the room
    /// </summary>
    /// <param name="itemName">The name of the item to remove.</param>
    public void RemoveItem(string itemName)
    {
        // Find the item by its name and remove it from the list
        var item = Items.FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
        if (item != null)  // If the item was found
        {
            Items.Remove(item);  // Remove the item from the list
        }
    }

    /// <summary>
    /// Removes a monster from the room based on the monster's name.
    /// </summary>
    /// <param name="monsterName">The name of the monster to remove.</param>
    public void RemoveMonster(string monsterName)
    {
        Monsters.Remove(monsterName);  // Remove the monster from the list by its name
    }
}
