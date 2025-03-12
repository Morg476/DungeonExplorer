using System.Collections.Generic;
using System;

public class Player
{
    public string Name { get; set; }
    public int Health { get; set; }
    public Room CurrentRoom { get; set; }
    public List<string> Inventory { get; set; } = new List<string>();

    //Initialise the player attributes.
    public Player(string name, int health, Room currentRoom)
    {
        Name = name;
        Health = health;
        CurrentRoom = currentRoom;
    }

    //If the user picks up an item, passes the item through the function and adds it to the players inventory.
    public void PickUpItem(string item)
    {
        Console.WriteLine($"You picked up the {item}!");
        Inventory.Add(item);
    }

    //Function to simply output the contents of the inventory whenever called. 
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
