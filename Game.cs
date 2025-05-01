using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using DungeonExplorer;

namespace DungeonExplorer
{
    /// <summary>
    /// Represents the main game logic, which includes player interaction.
    /// </summary>
    public class Game
    {
        private Player _newPlayer;
        private GameMap _map; 
        private Room _currentRoom;

        public Game()
        {
            _map = new GameMap();  // Initialises the game map

            var gameMap = new GameMap();

            // Create rooms and items and define their values
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

            
            var tunnel = new Room("Underground Tunnel",
                "The tunnel is damp and narrow. You hear scurrying noises in the darkness.",
                new List<Item>
                {
                    new Item("Lantern", "A sturdy lantern that helps light up dark places", ItemType.Misc, 10),
                    new Potion("Potion", "A healing potion", 30, 15)
                },
                new List<string> { "Goblin" });

            var altar = new Room("Ancient Altar",
                "A dark altar stands at the center of this cold, gloomy room, with strange markings on the walls.",
                new List<Item>
                {
                    new Weapon("Magic Staff", "A staff imbued with magical power", 15, 40),
                    new Item("Mystic Gem", "A gem with a faint glow", ItemType.Misc, 20)
                },
                new List<string> { "Skeleton" });

            var vault = new Room("Secret Vault",
                "A hidden vault filled with treasure chests. Something doesn't seem quite right...",
                new List<Item>
                {
                    new Item("Golden Key", "A golden key, perhaps it opens something special", ItemType.Key, 30),
                    new Potion("Super Potion", "A powerful healing potion", 100, 50)
                },
                new List<string> { "Dragon" });

            var crypt = new Room("Crypt",
                "A haunted crypt, full of dark shadows and the sounds of whispering ghosts.",
                new List<Item>
                {
                    new Item("Silver Sword", "A sword with ancient runes etched on its blade", ItemType.Weapon, 25),
                    new Potion("Minor Healing Potion", "A small potion that heals 20 HP", 20, 5)
                },
                new List<string> { "Zombie", "Vampire" });

            var tower = new Room("Wizard's Tower",
                "A towering structure filled with books, potions, and magical artifacts. You can feel the magic in the air.",
                new List<Item>
                {
                    new Weapon("Fire Staff", "A staff that controls fire, dealing high damage", 30, 60),
                    new Item("Spellbook", "A book containing powerful spells", ItemType.Misc, 35)
                },
                new List<string> { "Sorcerer" });

            // Add rooms to the game map
            _map.AddRoom(entrance);
            _map.AddRoom(corridor);
            _map.AddRoom(chamber);
            _map.AddRoom(tunnel);
            _map.AddRoom(altar);
            _map.AddRoom(vault);
            _map.AddRoom(crypt);
            _map.AddRoom(tower);

            //Connect rooms together
            _map.ConnectRooms("Dungeon Entrance", "Dark Corridor");
            _map.ConnectRooms("Dark Corridor", "Treasure Chamber");
            _map.ConnectRooms("Dark Corridor", "Underground Tunnel");
            _map.ConnectRooms("Underground Tunnel", "Ancient Altar");
            _map.ConnectRooms("Ancient Altar", "Secret Vault");
            _map.ConnectRooms("Secret Vault", "Crypt");
            _map.ConnectRooms("Crypt", "Wizard's Tower");

            // Set initial room and player
            _currentRoom = entrance;
            _newPlayer = new Player("Hero", 100, _currentRoom, gameMap);

            
            Console.WriteLine("\n====================\n  DUNGEON EXPLORER\n====================\n");
            Console.WriteLine("You stand at the entrance of a dark, mysterious dungeon...\n");

            Start();
        }

        /// <summary>
        /// Displays the main menu options to the player.
        /// </summary>
        private void DisplayMenu()
        {
            Console.WriteLine("{ 1 } Explore the room");
            Console.WriteLine("{ 2 } Move to another room");
            Console.WriteLine("{ 3 } View Statistics and Inventory");
            Console.WriteLine("{ 4 } Exit Game");
            Console.WriteLine("{ 5 } Testing Diagnostic");
        }

        /// <summary>
        /// Runs all tests used for debugging purposes
        /// </summary>
        private void RunAllTests()
        {
            var tests = new Testing();
            tests.RunTests();
        }

        /// <summary>
        /// Displays the player's statistics and inventory.
        /// </summary>
        private void StatMenu()
        {
            Console.Clear();
            _newPlayer.Stats.DisplayStatistics();
            Console.WriteLine($"Name: {_newPlayer.Name}");
            Console.WriteLine($"Health: {_newPlayer.Health}");

            Console.WriteLine("\nInventory:");
            var sortedItems = _newPlayer.Inventory
                .OrderByDescending(i => i.Value)
                .Select(i => $"{i.Name} - {i.Description} ({i.Value} gold)");

            foreach (var item in sortedItems)
            {
                Console.WriteLine($"- {item}");
            }

            Console.WriteLine("\nWeapons Only:");
            var weapons = _newPlayer.Inventory
                .Where(i => i.ItemType == ItemType.Weapon)
                .Select(i => $"{i.Name} - {i.Description}");

            foreach (var weapon in weapons)
            {
                Console.WriteLine($"- {weapon}");
            }

            Console.WriteLine("\nMiscellaneous Only:\n");
            var Misc = _newPlayer.Inventory
                .Where(i => i.ItemType == ItemType.Misc)
                .Select(i => $"{i.Name} - {i.Description}");

            foreach (var misc in Misc)
            {
                Console.WriteLine($"- {misc}");
            }
        }

        /// <summary>
        /// Starts the game loop.
        /// </summary>
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
                    case 5:
                        RunAllTests();
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }
        }

        /// <summary>
        /// Allows the player to explore the current room, fight monsters, and pick up items.
        /// </summary>
        private void Explore()
        {
            Console.WriteLine($"\nPlayer: {_newPlayer.Name} \nHealth: {_newPlayer.Health}");
            Console.WriteLine($"Room: {_currentRoom.GetDescription()}");

            // Randomly select monsters if they exist in the current room
            if (_currentRoom.HasMonsters())
            {
                List<string> monstersInRoom = _currentRoom.Monsters;
                Random rand = new Random();
                string monsterName = monstersInRoom[rand.Next(monstersInRoom.Count)];

                // Creates the monster and displays it
                Monster monster = MonsterManagement.CreateMonster(monsterName);
                Console.WriteLine($"\nA {monster.DifficultyLevel} monster approaches: {monster.Name}!");

                //Loop that controls the combat aspect within a room
                while (_newPlayer.IsAlive() && monster.IsAlive())
                {
                    Console.WriteLine($"{_newPlayer.Name} attacks {monster.Name}!");
                    _newPlayer.AttackMonster(monster);

                    if (!monster.IsAlive())
                    {
                        Console.WriteLine($"{monster.Name} has been defeated!");
                        Console.WriteLine($"You gained {monster.Experience} XP!");
                        _currentRoom.RemoveMonster(monster.Name);
                        break;
                    }

                    Console.WriteLine($"{monster.Name} attacks {_newPlayer.Name}!");
                    _newPlayer.TakeDamage(monster.Attack);

                    if (!_newPlayer.IsAlive())
                    {
                        Console.WriteLine($"{_newPlayer.Name} has been defeated! Game Over.");
                        EndGame();
                        return;
                    }

                    //Asks the user if they would like to heal during battle
                    string healChoice;
                    do
                    {
                        Console.WriteLine("Do you want to use a healing item? (Y/N): ");
                        healChoice = Console.ReadLine().Trim().ToUpper();
                    } while (healChoice != "Y" && healChoice != "N");

                    if (healChoice == "Y")
                    {
                        var healingItem = _newPlayer.Inventory.FirstOrDefault(i => i is Potion);
                        if (healingItem != null)
                        {
                            Console.WriteLine($"You used a {healingItem.Name}!");
                            _newPlayer.UseItem(healingItem.Name);
                        }
                        else
                        {
                            Console.WriteLine("You have no healing items left.");
                        }
                    }
                }
            }

            // Let the player pick up items in the room if any exist
            if (_currentRoom.HasItems())
            {
                Console.WriteLine("\nItems: " + string.Join(", ", _currentRoom.Items.Select(i => i.Name)));
                string itemPickUp;
                do
                {
                    Console.Write("Pick up an item? (Y/N): ");
                    itemPickUp = Console.ReadLine().Trim().ToUpper();
                } while (itemPickUp != "Y" && itemPickUp != "N");

                if (itemPickUp == "Y")
                {
                    string itemName;
                    do
                    {
                        Console.Write("Which item? :: ");
                        itemName = Console.ReadLine().Trim();
                    } while (!_currentRoom.Items.Any(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase)));

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

            // Check and prompt player to use an item from inventory
            if (_newPlayer.Inventory.Count > 0)
            {
                string useItemChoice;
                do
                {
                    Console.Write("\nDo you want to use an item from your inventory? (Y/N): ");
                    useItemChoice = Console.ReadLine().Trim().ToUpper();
                } while (useItemChoice != "Y" && useItemChoice != "N");

                if (useItemChoice == "Y")
                {
                    string itemNameToUse;
                    do
                    {
                        Console.Write("Enter the name of the item to use: ");
                        itemNameToUse = Console.ReadLine().Trim();
                    } while (!_newPlayer.Inventory.Any(i => i.Name.Equals(itemNameToUse, StringComparison.OrdinalIgnoreCase)));

                    _newPlayer.UseItem(itemNameToUse);
                }
            }
            else
            {
                Console.WriteLine("Your inventory is empty, so you cannot use any items.");
            }
        }

        /// <summary>
        /// Ends the game and displays a game over 
        /// </summary>
        private void EndGame()
        {
            Console.WriteLine("GAME OVER!");
            Console.WriteLine("Thank you for playing!");
            Environment.Exit(0);  //Ends the game
        }

        /// <summary>
        /// Allows the player to move to another room within the dungeon
        /// </summary>
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
}
