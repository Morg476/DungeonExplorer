using System;
using System.Collections.Generic;
using System.Net;

public class Game
{
    private Player _newPlayer;
    private Dictionary <string, Room> _rooms;
    private Room _currentRoom;

    public Game()
    {
        // Initialise multiple rooms and their attributes
        _rooms = new Dictionary<string, Room>
        {
            //Dungeon room which containd two items and a monster that can be defeated
            { "Dungeon Entrance", new Room(
                "You stand at the entrance of an eerie dungeon. The wind howls behind you.",
                new List<string> { "Torch", "Map" },
                new List<string> { "Goblin" }) },
            //Corridor room which contains two items and two monsters that can be defeated
            { "Dark Corridor", new Room(
                "The walls narrow as you step into a dark corridor. Shadows flicker wildly.",
                new List<string> { "Potion", "Dagger" },
                new List<string> { "Skeleton", "Spider" }) },
            //Chamber room which contains two items and one monster that can be defeated
            { "Treasure Chamber", new Room(
                "Glittering treasures lie in piles, but a growl warns you of danger.",
                new List<string> { "Gold Coin", "Ancient Scroll" },
                new List<string> { "Dragon" }) }
        };

        //Sets the starting room of the game as the Dungeon Entrance
        _currentRoom = _rooms["Dungeon Entrance"];

        //Initlaises the player, named Hero and sets health within the currentRoom
        _newPlayer = new Player("Hero", 100, _currentRoom);

        Console.WriteLine("\n====================\n" +
            "  DUNGEON EXPLORER        \n" +
            "====================\n");
        Start();
    }
    //Simply displays the menu options at the start of debugging
    private void DisplayMenu()
    {
        Console.WriteLine("{ 1 } Explore the room");
        Console.WriteLine("{ 2 } Move to another room");
        Console.WriteLine("{ 3 } Exit Game");
    }
    //Method which controls the game as it starts
    public void Start()
    {
        bool playing = true;

        while (playing)
        {
            try
            {
                //Calls menu and asks user for input on their choice.
                DisplayMenu();
                Console.Write("Enter Choice: ");

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
                        Explore();
                        break;
                    case 2:
                        ChangeRoom();
                        break;
                    case 3:
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
    //Method which contains the attributes of the player and prints them out.
    private void Explore()
    {

        Console.WriteLine($"Player: {_newPlayer.Name} \nHealth: {_newPlayer.Health}");
        Console.WriteLine($"Inventory: {_newPlayer.InventoryContents}");
        //Uses GetDescription() to fetch the descriptions of rooms more efficiently for each room entered.
        Console.WriteLine($"\n{_currentRoom.GetDescription()}");

        //Selection to determine whether the room contains monsters, if so prompt the user with the choice to battle.
        if (_currentRoom.HasMonsters())
        {
            bool loopExplore = true;
            while (loopExplore)
            {
                Console.WriteLine("There are Monsters lurking here somewhere... " + string.Join(", ", _currentRoom.Monsters));
                Console.WriteLine("Would you like to battle? (Y/N)");
                string fightChoice = Console.ReadLine().Trim().ToUpper();
                //If yes, then call the Fight() method.
                try
                {
                    //Selection to decide the user fight choice
                    switch (fightChoice)
                    {
                        //If yes, then call the fight function, and break out of the while loop.
                        case "Y":
                            Fight();
                            loopExplore = false;
                            break;
                        case "N":
                            //Then break out of the while loop, and continue onto next section of the program. 
                            loopExplore = false;
                            continue;
                        case "":
                            //If user enters null value, recalls the Explore funtion to gather an appropriate decision.
                            Console.Clear();
                            Console.WriteLine("Please make a choice, Y/N\n\n");
                            Explore();
                            loopExplore = true;
                            break;
                    }

                }
                //Any exceptions occur, they will be outputted with the correlating error code. 
                catch (Exception ex)
                {
                    Console.Write($"An error has occured: {ex}");
                }
            }

        }

        //Selection to determine if the room has any items in it.
        if (_currentRoom.HasItems())
        {
            Console.WriteLine("\nItems you see: " + string.Join(", ", _currentRoom.Items));
            Console.WriteLine("Would you like to pick up an item? (Y/N)");
            string itemPickUp = Console.ReadLine().Trim().ToUpper();
            //If so then allow the user to choose which item they want, then add that to the inventory of the player.
            if (itemPickUp == "Y")
            {
                Console.Write("Which item would you like to pick up?: ");
                string item = Console.ReadLine().Trim().ToLower();

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
    //Fight method which engages the player in a fight.obs
    private void Fight()
    {
        string monster = _currentRoom.Monsters[0];//Chooses rthe first monster in the room, if more than one present. 
        Console.WriteLine($"You fight the {monster}!");

        //Simple fighting mechanic which allows the user to always win (for now), removing 10HP each time.
        _newPlayer.Health -= 10;
        _currentRoom.RemoveMonster(monster);

        Console.WriteLine($"You defeated the {monster}, but lost 10 HP! Current HP: {_newPlayer.Health}");
    }

    //Allow the user to decid which room they wish to enter.
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
