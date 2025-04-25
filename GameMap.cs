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

        public void ConnectRooms(string room1, string room2)
        {
            if (_rooms.ContainsKey(room1) && _rooms.ContainsKey(room2))
            {
                _connections[room1].Add(room2);
                _connections[room2].Add(room1);
            }
        }

        public Room GetRoom(string name) => _rooms.ContainsKey(name) ? _rooms[name] : null;

        public List<string> GetConnectedRooms(string name) =>
            _connections.ContainsKey(name) ? _connections[name] : new List<string>();

        public IEnumerable<string> RoomNames => _rooms.Keys;
    }
}
