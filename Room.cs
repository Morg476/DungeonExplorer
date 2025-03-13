using System.Collections.Generic;
using System.Diagnostics;

/// <summary>
/// Represents a room within the game, which contains items, mosnters and a room description
/// </summary>
public class Room
{
    /// <summary>
    /// Gets the description of the room
    /// </summary>
    public string Description { get; private set; }
    /// <summary>
    /// Gets the items contained within the room
    /// </summary>
    public List<string> Items { get; private set; }
    /// <summary>
    /// Gets the list of monsters withi the room
    /// </summary>
    public List<string> Monsters { get; private set; }

    /// <summary>
    /// Initialises a new instance of the room class, with a descirption, list of items and monsters
    /// </summary>
    /// <param name="description">a test description of the room</param>
    /// <param name="items">The list of items available in the room, if null and empty list is returned</param>
    /// <param name="monsters">The list of monsters in the room, if null and empty list is returned</param>
    public Room(string description, List<string> items, List<string> monsters)
    {
        Debug.Assert(!string.IsNullOrWhiteSpace(description), "Room description should not be null or empty");
        Description = description;

        //Creates a new list if items are null.
        if (items == null)
        {
            Items = new List<string>();
        }
        else
        {
            Items = items;
        }
        //Same here if monsters is null create a new list.
        if (monsters == null)
        {
            Monsters = new List<string>();
        }
        else
        {
            Monsters = monsters;
        }

    }
    //Boolean operators to check if items length is greater than 0 and monsters length is greater than 0.
    /// <summary>
    /// Determines whether the room contains any items.
    /// </summary>
    /// <returns>True, if there are items within the room, otherwise, false</returns>
    public bool HasItems() => Items.Count > 0;
    /// <summary>
    /// Determines whether there are any monsters in the room
    /// </summary>
    /// <returns>True if there are monsers present, otherwise returns false</returns>
    public bool HasMonsters() => Monsters.Count > 0;

    /// <summary>
    /// Removes an item from a room when it has been collected
    /// </summary
    /// <param name="item">The items name to be removed</param>
    public void RemoveItem(string item)
    {
        Debug.Assert(!string.IsNullOrEmpty(item), "Item name should not be null or empty.");
        Debug.Assert(Items.Contains(item), $"Item '{item}' does not exist in the room.");
        Items.Remove(item);
    }
    /// <summary>
    /// Removes a monster from a room when it has been defeated
    /// </summary>
    /// <param name="monster">The name of the monster to be removed.</param>
    public void RemoveMonster(string monster)
    {
        Debug.Assert(!string.IsNullOrEmpty(monster), "Monster name should not be null or empty.");
        Debug.Assert(Monsters.Contains(monster), $"Monster '{monster}' does not exist in the room.");
        Monsters.Remove(monster);
    }

    /// <summary>
    /// Gets a full description of the room, including its items and monsters. 
    /// </summary>
    /// <returns>room description, items and monsters within the room</returns>
    public string GetDescription()
    {
        string itemText = HasItems() ? $"Items: {string.Join(", ", Items)}" : "No items here.";
        string monsterText = HasMonsters() ? $"Monsters: {string.Join(", ", Monsters)}" : "No monsters here.";

        return $"{Description}\n\n{itemText}\n{monsterText}";
    }

}
