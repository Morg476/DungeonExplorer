using DungeonExplorer;
using System.Collections.Generic;
using System;
using System.Net.Http.Headers;
using System.Linq;

public class Room
{
    public string Name { get; set; }
    public string Description { get; set; } 
    public List<Item> Items { get; set; } 
    public List<string> Monsters { get; set; }

    public Room(string name, string description, List<Item> items, List<string> monsters)
    {
        Name = name;
        Description = description;
        Items = items ?? new List<Item>();
        Monsters = monsters ?? new List<string>();
    }
    public string GetDescription() => Description;

    public bool HasItems() => Items.Any();

    public bool HasMonsters() => Monsters.Any();

    public void RemoveItem(string itemName)
    {
        var item = Items.FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
        if (item != null)
        {
            Items.Remove(item);
        }
    }

    public void RemoveMonster(string monster)
    {
        Monsters.Remove(monster);
    }


}

