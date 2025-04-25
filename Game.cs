using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DungeonExplorer;

/// <summary>
/// Represents the main game logic, which includes player interaction 
/// </summary>

public class Game
{
    private Player _newPlayer;
    private GameMap _map;
    private Room _currentRoom;

    public Game()
    {
        _map = new GameMap();  // Initialize the GameMap object here

        // Create rooms
        var entrance = new Room("Dungeon Entrance",
            "You stand before a massive stone doorway, half-buried in tangled vines and ancient moss. The air is thick with the scent of damp earth and decay.",
            new List<Item>
            {
                new Item("Torch", "A basic torch that lights up dark paths", ItemType.Misc, 5),
                new Item("Map", "A faded dungeon map with barely legible markings", ItemType.Misc, 3)
            },
            new List<string> { "Goblin" });

        var corridor = new Room("Dark Corridor",
            "The air grows colder as you step into the narrow corridor...",
            new List<Item>
            {
                new Potion("Potion", "A red healing potion", 50, 10),
                new Item("Dagger", "A rusty but sharp dagger", ItemType.Weapon, 8)
            },
            new List<string> { "Skeleton", "Spider" });

        var chamber = new Room("Treasure Chamber",
            "Gold and jewels glisten in the dim light...",
            new List<Item>
            {
                new Item("Gold Coin", "A shiny gold coin", ItemType.Misc, 2),
                new Item("Ancient Scroll", "An old scroll", ItemType.Misc, 20)
            },
            new List<string> { "Dragon" });

        // Add rooms to the map
        _map.AddRoom(entrance);
        _map.AddRoom(corridor);
        _map.AddRoom(chamber);

        // Connect rooms
        _map.ConnectRooms("Dungeon Entrance", "Dark Corridor");
        _map.ConnectRooms("Dark Corridor", "Treasure Chamber");

        // Start the game with the first room
        _currentRoom = entrance;
        _newPlayer = new Player("Hero", 100, _currentRoom);

        Console.WriteLine("\n====================\n  DUNGEON EXPLORER\n====================\n");
        Console.WriteLine("You stand at the entrance of a dark, mysterious dungeon...\n");
        Start();
    }

    private void DisplayMenu()
    {
        Console.WriteLine("{ 1 } Explore the room");
        Console.WriteLine("{ 2 } Move to another room");
        Console.WriteLine("{ 3 } View Statistics and Inventory");
        Console.WriteLine("{ 4 } Exit Game");
    }

    private void StatMenu()
    {
        Console.Clear();
        Console.WriteLine("===== STATISTICS =====");
        Console.WriteLine($"Name: {_newPlayer.Name}");
        Console.WriteLine($"Health: {_newPlayer.Health}");
        Console.WriteLine("Inventory:");
        foreach (var item in _newPlayer.Inventory)
        {
            Console.WriteLine($"- {item.Name}: {item.Description}");
        }
    }

    public void Start()
    {
        bool playing = true;

        while (playing)
        {
            DisplayMenu();
            Console.Write("\n::  ");

            if (!int.TryParse(Console.ReadLine(), out int userChoice))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                continue;
            }

            switch (userChoice)
            {
                case 1:
                    Console.Clear();
                    Explore();
                    break;
                case 2:
                    ChangeRoom();
                    break;
                case 3:
                    StatMenu();
                    break;
                case 4:
                    Console.WriteLine("Your adventure continues another day...");
                    playing = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    break;
            }
        }
    }

    private void Explore()
    {
        Console.WriteLine($"\nPlayer: {_newPlayer.Name} \nHealth: {_newPlayer.Health}");
        Console.WriteLine($"Room: {_currentRoom.GetDescription()}");

        if (_currentRoom.HasMonsters())
        {
            Console.WriteLine("\nMonsters: " + string.Join(", ", _currentRoom.Monsters));
            Console.Write("Do you want to fight? (Y/N): ");
            string fightChoice = Console.ReadLine().Trim().ToUpper();

            if (fightChoice == "Y")
            {
                Fight();
            }
        }

        if (_currentRoom.HasItems())
        {
            Console.WriteLine("\nItems: " + string.Join(", ", _currentRoom.Items.Select(i => i.Name)));
            Console.Write("Pick up an item? (Y/N): ");
            string itemPickUp = Console.ReadLine().Trim().ToUpper();

            if (itemPickUp == "Y")
            {
                Console.Write("Which item? :: ");
                string itemName = Console.ReadLine().Trim();

                var item = _currentRoom.Items.FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
                if (item != null)
                {
                    _newPlayer.PickUpItem(item);
                    _currentRoom.RemoveItem(itemName);
                    Console.WriteLine($"{itemName} added to inventory.");
                }
                else
                {
                    Console.WriteLine("That item is not in this room.");
                }
            }
        }
    }

    private void Fight()
    {
        string monster = _currentRoom.Monsters[0];
        Console.WriteLine($"\nYou fought the {monster}!");

        _newPlayer.Health -= 10;
        _currentRoom.RemoveMonster(monster);

        Console.WriteLine($"You defeated the {monster}, but lost 10 HP! Current HP: {_newPlayer.Health}");
    }

    private void ChangeRoom()
    {
        Console.WriteLine("\nAvailable rooms:");
        var connected = _map.GetConnectedRooms(_currentRoom.Name);

        foreach (var roomName in connected)
        {
            Console.WriteLine($"- {roomName}");
        }

        Console.Write("\nEnter the room name you want to move to: ");
        string chosenRoom = Console.ReadLine().Trim();

        if (connected.Contains(chosenRoom))
        {
            _currentRoom = _map.GetRoom(chosenRoom);
            _newPlayer.CurrentRoom = _currentRoom;
            Console.WriteLine($"\nYou have entered: {chosenRoom}");
        }
        else
        {
            Console.WriteLine("That room does not exist.");
        }
    }
}

