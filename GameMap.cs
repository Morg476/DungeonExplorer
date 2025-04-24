using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class GameMap
    {
        // List of rooms in the game
        public List<Room> Rooms { get; private set; }

        // Constructor initializes the list of rooms
        public GameMap()
        {
            Rooms = new List<Room>();
        }

        // Add a room to the map
        public void AddRoom(Room room)
        {
            if (room != null && !Rooms.Contains(room))
            {
                Rooms.Add(room);
                Console.WriteLine($"Room '{room.Name}' added to the map.");
            }
        }

        // Connect two rooms by linking them (you can specify directions like north, south, east, west, etc.)
        public void ConnectRooms(Room room1, Room room2, string direction)
        {
            if (room1 != null && room2 != null)
            {
                room1.AddConnection(direction, room2);
                Console.WriteLine($"Rooms connected: '{room1.Name}' <-> '{room2.Name}' via {direction}.");
            }
        }

        // Find a room by name
        public Room GetRoomByName(string roomName)
        {
            return Rooms.FirstOrDefault(r => r.Name.Equals(roomName, StringComparison.OrdinalIgnoreCase));
        }
    }

    public class Room
    {
        // Name of the room (e.g., "Dungeon Entrance")
        public string Name { get; set; }
        // A dictionary to store connected rooms by direction (e.g., North, South)
        public Dictionary<string, Room> ConnectedRooms { get; private set; }
        // Other room properties like description, items, etc. could go here

        public Room(string name)
        {
            Name = name;
            ConnectedRooms = new Dictionary<string, Room>();
        }

        // Add a connection to another room in a given direction (e.g., "North", "East")
        public void AddConnection(string direction, Room connectedRoom)
        {
            if (!ConnectedRooms.ContainsKey(direction))
            {
                ConnectedRooms[direction] = connectedRoom;
            }
            else
            {
                Console.WriteLine($"Room already connected in the {direction} direction.");
            }
        }

        // Get the room connected in a given direction
        public Room GetConnectedRoom(string direction)
        {
            return ConnectedRooms.ContainsKey(direction) ? ConnectedRooms[direction] : null;
        }

        public override string ToString()
        {
            return $"{Name} (Connections: {string.Join(", ", ConnectedRooms.Keys)})";
        }
    }
}
