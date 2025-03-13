using System.Collections.Generic;

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
    public bool HasItems() => Items.Count > 0;
    public bool HasMonsters() => Monsters.Count > 0;

    //Function to remove item from the item list.
    public void RemoveItem(string item)
    {
        Items.Remove(item);
    }
    //Function to remove monster from the monster list.
    public void RemoveMonster(string monster)
    {
        Monsters.Remove(monster);
    }

    //New method to return a description of the room and any items and monsters within it. 
    public string GetDescription()
    {
        string itemText;
        if (HasItems())
        {
            itemText = $"Item(s), within this room: {string.Join(", ", Items)}";
        }
        else
        {
            itemText = "No items to scavenge for in here!";
        }

        string monsterText;
        if (HasMonsters())
        {
            monsterText = $"Monster(s), lurking in this room: {string.Join(", ", Monsters)}";
        }
        else
        {
            monsterText = "You're safe, for now... There are no monsters nearby!";
        }

        return $"{Description}\n\n{itemText}\n{monsterText}";
    }

}
