using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class GameMap
    {
        private Dictionary<string, Room> _rooms;
        private Dictionary<string, List<string>> _connections;

        public GameMap()
        {
            _rooms = new Dictionary<string, Room>();
            _connections = new Dictionary<string, List<string>>();
        }

        public void AddRoom(Room room)
        {
            if (!_rooms.ContainsKey(room.Name))
            {
                _rooms[room.Name] = room;
                _connections[room.Name] = new List<string>();
            }
        }

        public void ConnectRooms(string roomA, string roomB)
        {
            if (_rooms.ContainsKey(roomA) && _rooms.ContainsKey(roomB))
            {
                _connections[roomA].Add(roomB);
                _connections[roomB].Add(roomA);
            }
        }

        public List<string> GetConnectedRooms(string roomName)
        {
            return _connections.ContainsKey(roomName) ? _connections[roomName] : new List<string>();
        }

        public Room GetRoom(string name)
        {
            return _rooms.TryGetValue(name, out var room) ? room : null;
        }

        public List<Room> GetRoomsWithMonsters()
        {
            return _rooms.Values
                .Where(r => r.Monsters != null && r.Monsters.Any())
                .ToList();
        }

        public List<Item> GetAllItemsInDungeon()
        {
            return _rooms.Values
                .SelectMany(r => r.Items)
                .ToList();
        }
    }
}
