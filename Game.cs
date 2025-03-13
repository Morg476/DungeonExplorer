using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Represents the main game logic, which includes player interaction 
/// </summary>
public class Game
{
    private Player _newPlayer;
    private Dictionary <string, Room> _rooms;
    private Room _currentRoom;

    /// <summary>
    /// Initialises a new instance of the game class.
    /// Sets up rooms throughout them game, intialises the player and then starts the game
    /// </summary>
    public Game()
    {
        Debug.Assert(_rooms == null, "_rooms should be uninitialised before setup.");
        // Initialise multiple rooms and their attributes
        _rooms = new Dictionary<string, Room>
        {
            //Dungeon room which containd two items and a monster that can be defeated
            { "Dungeon Entrance", new Room(

                "You stand before a massive stone doorway, half-buried in tangled vines and ancient moss. The air is thick with the scent of damp earth and decay. " +
                "Faint carvings, long eroded by time, whisper of forgotten civilizations and untold dangers. " +
                "A chilling wind seeps from the dark abyss beyond, carrying distant echoes—whispers, perhaps… or the breathing of something unseen.",

                new List<string> { "Torch", "Map" },
                new List<string> { "Goblin" }) },
            //Corridor room which contains two items and two monsters that can be defeated
            { "Dark Corridor", new Room(
                "The air grows colder as you step into the narrow corridor. The stone walls, slick with moisture, seem to press in around you. " +
                "Flickering torchlight from the entrance barely reaches this far, leaving most of the passage swallowed in darkness. " +
                "The faint sound of dripping water echoes through the hall, each drop unsettlingly loud in the eerie silence.\r\n\r\n" +
                "Somewhere in the distance, a faint scratching noise stirs—something moving, just beyond sight. The floor beneath your feet is uneven, worn down by centuries of footsteps… or something else.",
                new List<string> { "Potion", "Dagger" },
                new List<string> { "Skeleton", "Spider" }) },
            //Chamber room which contains two items and one monster that can be defeated
            { "Treasure Chamber", new Room(
                "Gold and jewels glisten in the dim light, spilling from shattered chests and crumbling urns. " +
                "An ornate pedestal at the far end holds a relic pulsing with energy—its power undeniable.\r\n\r\n" +
                "But the silence is unnerving. Scattered bones and rusted weapons hint at a deadly past. The air is thick with something unseen… waiting.",
                new List<string> { "Gold Coin", "Ancient Scroll" },
                new List<string> { "Dragon" }) }
        };

        Debug.Assert(_rooms.Count > 0, "Rooms should be intialised with at least one room.");

        //Sets the starting room of the game as the Dungeon Entrance
        _currentRoom = _rooms["Dungeon Entrance"];
        Debug.Assert(_currentRoom != null, "_currentRoom should not be null after intilaisation.");

        //Initlaises the player, named Hero and sets health within the currentRoom
        _newPlayer = new Player("Hero", 100, _currentRoom);
        Debug.Assert(_newPlayer != null, "Player should be properly intialised.");

        Console.WriteLine("\n====================\n" +
            "  DUNGEON EXPLORER        \n" +
            "====================\n");

        Console.WriteLine("You stand at the entrance of a dark, mysterious dungeon, the scent of damp stone and ancient secrets filling the air." +
            " Legends speak of hidden treasures, deadly traps, and creatures lurking in the shadows. Armed with only your wits—and whatever weapons you can find—you must navigate the labyrinthine halls, uncovering secrets, battling monsters, and surviving the unknown.\r\n\r\n" +
            "Will you emerge victorious, your pockets lined with gold and glory? Or will the dungeon claim yet another lost soul?\n");
        Start();
    }
    /// <summary>
    /// Displays the main menu options to the player
    /// </summary>
    private void DisplayMenu()
    {
        Console.WriteLine("{ 1 } Explore the room");
        Console.WriteLine("{ 2 } Move to another room");
        Console.WriteLine("{ 3 } View Statistics and Inventory");
        Console.WriteLine("{ 4 } Exit Game");
    }
    /// <summary>
    /// Displays the statistics menu when that option is chosen
    /// Displays, name, health and inventory contents
    /// </summary>
    private void StatMenu()
    {
        Console.Clear();
        Console.WriteLine("===== STATISTICS =====");
        Console.WriteLine($"Name: {_newPlayer.Name}");
        Console.WriteLine($"Inventory: {_newPlayer.InventoryContents}");
        Console.WriteLine($"Health: {_newPlayer.Health}");
    }
    /// <summary>
    /// Starts the game loop, allowing the game to flow and the player to progress through the dungeon.
    /// </summary>
    public void Start()
    {
        bool playing = true;

        while (playing)
        {
            DisplayMenu();
            try
            {
                //Calls menu and asks user for input on their choice.
                Console.Write("\n::  ");

                if (!int.TryParse(Console.ReadLine(), out int userChoice))
                {
                    //Error catching for users choice
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }
                //Switch case to control the outcome of the program based on the inputs of the user
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
                        Console.Write("\n");
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
            catch (Exception ex)
            {
                //Prints error if one occurs
                Console.WriteLine($"An error has occurred: {ex.Message}");
            }
        }
    }
    /// <summary>
    /// Allows the player to explore the current room, search for items and battle enemies
    /// </summary>
    private void Explore()
    {
        Debug.Assert(_currentRoom != null, "_currentRoom should never be null");
        Console.WriteLine($"Player: {_newPlayer.Name} \nHealth: {_newPlayer.Health}");
        Console.WriteLine($"Inventory: {_newPlayer.InventoryContents}");
        //Uses GetDescription() to fetch the descriptions of rooms more efficiently for each room entered.
        Console.WriteLine($"\n{_currentRoom.GetDescription()}");

        //Selection to determine whether the room contains monsters, if so prompt the user with the choice to battle.
        if (_currentRoom.HasMonsters())
        {
            Console.WriteLine("\nMonsters lurking here: " + string.Join(", ", _currentRoom.Monsters));
            Console.WriteLine("Do you want to fight? (Y/N)");
            string fightChoice = Console.ReadLine().Trim().ToUpper();
            //If yes, then call the Fight() method.
            if (fightChoice == "Y")
            {
                Fight();
            }

        }

        //Selection to determine if the room has any items in it.
        if (_currentRoom.HasItems())
        {
            Console.WriteLine("\nItems you see: " + string.Join(", ", _currentRoom.Items));
            Console.WriteLine("Would you like to pick up an item? (Y/N)");
            Console.Write(":: ");
            string itemPickUp = Console.ReadLine().Trim().ToUpper();
            //If so then allow the user to choose which item they want, then add that to the inventory of the player.
            if (itemPickUp == "Y")
            {
                Console.Write("Which item would you like to pick up?");
                Console.Write("\n:: ");
                string item = Console.ReadLine().Trim();
                Debug.Assert(!string.IsNullOrEmpty(item), "Item name should not be empty.");

                if (_currentRoom.Items.Contains(item))
                {
                    _newPlayer.PickUpItem(item);
                    _currentRoom.RemoveItem(item);
                }
                else
                {
                    Console.WriteLine("That item is not here.");
                }
            }
        }
    }
    /// <summary>
    /// Initiates a fight with the first monster within the room the player is in
    /// </summary>
    private void Fight()
    {
        string monster = _currentRoom.Monsters[0];//Chooses rthe first monster in the room, if more than one present. 
        Console.WriteLine($"\nYou fought the {monster}!");

        //Simple fighting mechanic which allows the user to always win (for now), removing 10HP each time.
        _newPlayer.Health -= 10;
        _currentRoom.RemoveMonster(monster);

        Console.WriteLine($"You defeated the {monster}, but lost 10 HP!\nCurrent HP: {_newPlayer.Health}");
    }

    /// <summary>
    /// Allows the user to move rooms if there is one available from where they are.
    /// </summary>
    private void ChangeRoom()
    {
        //Prints avauilable rooms
        Console.WriteLine("\nAvailable rooms:");
        foreach (var room in _rooms.Keys)
        {
            Console.WriteLine($"- {room}");
        }

        //Asks user to enter the name of the room (little specific but will change later on for ease of error handling)
        Console.Write("\nEnter the name of the room you want to enter: ");
        string chosenRoom = Console.ReadLine().Trim();
        Debug.Assert(!string.IsNullOrEmpty(chosenRoom), "Room name should not be empty.");


        if (_rooms.ContainsKey(chosenRoom))
        {
            _currentRoom = _rooms[chosenRoom];
            _newPlayer.CurrentRoom = _currentRoom;
            Console.WriteLine($"\nYou have entered: {chosenRoom}\n");
        }
        else
        {
            Console.WriteLine("That room does not exist.");
        }
    }
}
