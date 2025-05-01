using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DungeonExplorer
{
    public class Testing
    {
        private string _testResultsFile = "test_results.txt";

        public void RunTests()
        {
            try
            {
                // Start the test results output
                WriteToFile("----- TESTING STARTED -----\n");

                Test_Item_Pick_Up();
                Test_Connected_Rooms();
                Test_Item_Remove();
                Test_Player_Use_Map(); // Adding test for using the map

                WriteToFile("\n----- ALL TESTS PASSED -----\n");
                Console.WriteLine("----- ALL TESTS PASSED -----");
            }
            catch (Exception ex)
            {
                // Write the failure message to the file
                WriteToFile($"TEST FAILED: {ex.Message}\n");
                Console.WriteLine($"TEST FAILED: {ex.Message}");
            }
        }

        private void Test_Item_Pick_Up()
        {
            // Create the GameMap object
            var gameMap = new GameMap();

            // Create a test room and player
            var room = new Room("Test Room", "Testing item pickup", new List<Item>(), new List<string>());
            var player = new Player("Test Player", 100, room, gameMap); // Pass gameMap to Player

            // Create an item to pick up
            var item = new Item("Test Torch", "A simple test torch", ItemType.Misc, 5);

            // Add the item to the room
            room.Items.Add(item);

            // Pick up the item
            player.PickUpItem(item);

            // Test that the item was added to the player's inventory
            if (!player.Inventory.Contains(item))
            {
                throw new Exception("Item was not added to player's inventory.");
            }

            // Log test success
            WriteToFile("Test_Item_Pick_Up: Passed\n");
        }

        private void Test_Connected_Rooms()
        {
            var map = new GameMap();
            var room1 = new Room("Room 1", "First room", new List<Item>(), new List<string>());
            var room2 = new Room("Room 2", "Second room", new List<Item>(), new List<string>());

            map.AddRoom(room1);
            map.AddRoom(room2);
            map.ConnectRooms("Room 1", "Room 2");

            var connected = map.GetConnectedRooms("Room 1");

            if (!connected.Contains("Room 2"))
            {
                throw new Exception("Room 1 is not connected to Room 2.");
            }

            // Log test success
            WriteToFile("Test_Connected_Rooms: Passed\n");
        }

        private void Test_Item_Remove()
        {
            var item = new Item("Goblet", "A gold goblet", ItemType.Misc, 10);
            var room = new Room("Library", "Dusty bookshelves", new List<Item> { item }, new List<string>());

            room.RemoveItem("Goblet");

            if (room.Items.Contains(item))
            {
                throw new Exception("Item was not removed from the room.");
            }

            // Log test success
            WriteToFile("Test_Item_Remove: Passed\n");
        }

        private void Test_Player_Use_Map()
        {
            // Create the game map
            var gameMap = new GameMap();

            // Create rooms and add them to the game map
            var entrance = new Room("Dungeon Entrance", "You stand before a massive stone doorway, half-buried in tangled vines and ancient moss. The air is thick with the scent of damp earth and decay.", new List<Item> { new Map("Dungeon Map", "An old, weathered map showing the dungeon layout.") }, new List<string>());
            gameMap.AddRoom(entrance);

            // Create and connect test rooms
            var nextRoom = new Room("Next Room", "This is the next room with monsters and traps.", new List<Item>(), new List<string>());
            gameMap.AddRoom(nextRoom);
            gameMap.ConnectRooms("Dungeon Entrance", "Next Room");

            // Create the player and pass the game map
            var player = new Player("Hero", 100, entrance, gameMap);

            // Player picks up the map item
            var mapItem = entrance.Items.FirstOrDefault(i => i is Map);
            if (mapItem != null)
            {
                player.PickUpItem(mapItem);
            }

            // Player uses the map item
            player.UseItem("Dungeon Map");

            // Log test success
            WriteToFile("Test_Player_Use_Map: Passed\n");

            Console.WriteLine("Player used the map successfully.");
        }


        private void WriteToFile(string message)
        {
            // Write the message to the file
            try
            {
                File.AppendAllText(_testResultsFile, message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to file: {ex.Message}");
            }
        }
    }
}
