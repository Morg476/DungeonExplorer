using System.Collections.Generic;

public class Room
{
    //Get set methods to initialise variables.
    public string Description { get; private set; }
    public List<string> Items { get; private set; }
    public List<string> Monsters { get; private set; }

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
            itemText = $"Items: {string.Join(", ", Items)}";
        }
        else
        {
            itemText = "No items here.";
        }

        string monsterText;
        if (HasMonsters())
        {
            monsterText = $"Monsters: {string.Join(", ", Monsters)}";
        }
        else
        {
            monsterText = "No monsters here.";
        }

        return $"{Description}\n\n{itemText}\n{monsterText}";
    }

}
