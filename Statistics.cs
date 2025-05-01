using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    /// <summary>
    /// Class to store and track player statistics during the game.
    /// </summary>
    public class Statistics
    {
        // Private fields to track various statistics
        public int BattlesWon { get; private set; }   
        public int BattlesLost { get; private set; }  
        public int HealthLost { get; private set; }   
        public int RoomsExplored { get; private set; }  
        public int ItemsFound { get; private set; }  
        public int ItemsUsed { get; private set; }

        /// <summary>
        /// Records the result of a battle.
        /// </summary>
        /// <param name="won">Indicates whether the player won the battle.</param>
        public void RecordBattle(bool won)
        {
            if (won)
            {
                BattlesWon++;  // Increment battles won if the player won
            }
            else
            {
                BattlesLost++;  // Increment battles lost if the player lost
            }
        }

        /// <summary>
        /// Records the amount of health lost by the player.
        /// </summary>
        /// <param name="amount">The amount of health lost.</param>
        public void RecordHealthLoss(int amount)
        {
            if (amount > 0)
                HealthLost += amount;  // Adds the health lost to the total health lost
        }

        /// <summary>
        /// Records an exploration of a new room.
        /// </summary>
        public void RecordRoomExploration()
        {
            RoomsExplored++;  // Increment rooms explored
        }

        /// <summary>
        /// Records the discovery of a new item.
        /// </summary>
        public void RecordItemFound()
        {
            ItemsFound++;  // Increment the count of items found by the player
        }

        /// <summary>
        /// Records the usage of an item by the player.
        /// </summary>
        public void RecordItemUsed()
        {
            ItemsUsed++;  // Increment the count of items used by the player
        }

        /// <summary>
        /// Displays the player's statistics in a readable format.
        /// </summary>
        public void DisplayStatistics()
        {
            Console.WriteLine("----- Player Statistics -----");
            Console.WriteLine($"Battles Won: {BattlesWon}");
            Console.WriteLine($"Battles Lost: {BattlesLost}"); 
            Console.WriteLine($"Total Health Lost: {HealthLost}"); 
            Console.WriteLine($"Rooms Explored: {RoomsExplored}"); 
            Console.WriteLine($"Items Found: {ItemsFound}");  
            Console.WriteLine($"Items Used: {ItemsUsed}");  
        }
    }
}
