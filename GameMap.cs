using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    /// <summary>
    /// Represents the map of the dungeon, managing rooms and their connections to one another
    /// </summary>
    public class GameMap
    {
        private Dictionary<string, Room> _rooms;  // Dictionary to store rooms by name
        private Dictionary<string, List<string>> _connections;  // Dictionary to store connections between rooms

        public GameMap()
        {
            _rooms = new Dictionary<string, Room>();  // Initalise the rooms dictionary
            _connections = new Dictionary<string, List<string>>();  // Initalise the connections dictionary
        }

        /// <summary>
        /// Adds a room to the dungeon map.
        /// </summary>
        /// <param name="room">The room to be added to the map.</param>
        public void AddRoom(Room room)
        {
            if (!_rooms.ContainsKey(room.Name))  //Makes sure the room is not already present
            {
                _rooms[room.Name] = room;  // Add the room to the rooms dictionary
                _connections[room.Name] = new List<string>();  // Initalise the list of connections for the room
            }
        }

        /// <summary>
        /// Connects two rooms, allowing the player to move between them.
        /// </summary>
        /// <param name="roomA">The name of the first room.</param>
        /// <param name="roomB">The name of the second room.</param>
        public void ConnectRooms(string roomA, string roomB)
        {
            if (_rooms.ContainsKey(roomA) && _rooms.ContainsKey(roomB))  // Ensure both rooms exist
            {
                _connections[roomA].Add(roomB);  // Add roomB as a connection to roomA
                _connections[roomB].Add(roomA);  // Add roomA as a connection to roomB
            }
        }

        /// <summary>
        /// Gets a list of names of rooms connected to the specified room.
        /// </summary>
        /// <param name="roomName">The name of the room.</param>
        /// <returns>A list of connected room names.</returns>
        public List<string> GetConnectedRooms(string roomName)
        {
            return _connections.ContainsKey(roomName) ? _connections[roomName] : new List<string>();  // Return the connected rooms or an empty list if none
        }

        /// <summary>
        /// Gets the Room object
        /// </summary>
        /// <param name="name">The name of the room.</param>
        /// <returns>The room object, or null if the room is not found.</returns>
        public Room GetRoom(string name)
        {
            return _rooms.TryGetValue(name, out var room) ? room : null;
        }

        /// <summary>
        /// Displays the list of rooms connected to the current room and their details.
        /// </summary>
        /// <param name="currentRoomName">The name of the current room the player is in.</param>
        public void DisplayConnectedRooms(string currentRoomName)
        {
            if (!_rooms.ContainsKey(currentRoomName))  // If the room does not exist
            {
                Console.WriteLine("You are in an unknown location.");
                return;
            }

            var connected = GetConnectedRooms(currentRoomName);  // Get the list of connected rooms

            if (connected.Count == 0)  // If there are no connected rooms
            {
                Console.WriteLine("There are no known paths from here.");
                return;
            }

            Console.WriteLine("\n== Dungeon Map ==");
            Console.WriteLine($"From {currentRoomName}, you can go to:");

            foreach (var roomName in connected)  // Loop through each connected room
            {
                var room = GetRoom(roomName);  // Retrieve the room object
                Console.WriteLine($"- {room.Name}: {room.Description}");

                // If the room has monsters, display them to the user
                if (room.Monsters != null && room.Monsters.Any())
                {
                    Console.WriteLine($"  Monsters: {string.Join(", ", room.Monsters)}");
                }

                // If the room has items, display them to the user
                if (room.Items != null && room.Items.Any())
                {
                    Console.WriteLine($"  Items: {string.Join(", ", room.Items.Select(i => i.Name))}");
                }
            }
        }
    }
}
