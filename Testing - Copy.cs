using DungeonExplorer;

namespace DungeonExplorer
{
    public class Testing
    {
        public void RunTests()
        {
            Test_Item_Pick_Up();
            Test_Connected_Rooms();
            Test_Item_Remove();
            Console.WriteLine("----- TESTING PASS -----");
        }

        private void Test_Item_Pick_Up()
        {
            var _room = new Room("TESTING ROOM", "A TEST ROOM", new List<Item>(), new List<string>());
            var _player = new Player("TESTING PLAYER", 100, _room);
            var _item = new Item("TEST TORCH", "DESCRIPTION", ItemType.Misc, 5);

            _player.PickUpItem(_item);

            if (!_player.Inventory.Contains(_item))
            {
                throw new InvalidOperationException("ITEM IN PLAYERS INVENTORY");
            }
        }

        private void Test_Connected_Rooms()
        {
            var map = new GameMap();
            var _room1 = new Room("ROOM1", "FIRST TEST ROOM", new List<Item>(), new List<string>());
            var _room2 = new Room("ROOM2", "SECOND TEST ROOM", new List<Item>(), new List<string>());

            map.AddRoom(_room1);
            map.AddRoom(_room2);
            map.ConnectRooms("ROOM1", "ROOM2");

            var connected = map.GetConnectedRooms("ROOM1");

            if (!connected.Contains("ROOM2"))
            {
                throw new InvalidOperationException("ROOM1 SHOULD CONNECT TO ROOM2");
            }
        }

        private void Test_Item_Remove()
        {
            var _item = new Item("GOBLET", "GOLD GOBLET", ItemType.Misc, 10);
            var _room = new Room("LIBRARY", "OLD RIDDEN SHELVES", new List<Item> { _item }, new List<string>());

            _room.RemoveItem("GOBLET");

            if (_room.Items.Contains(_item))
            {
                throw new InvalidOperationException("ITEM SHOULD BE REMOVED FROM ROOM");
            }
        }
    }
}
