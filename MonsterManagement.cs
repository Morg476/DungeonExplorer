using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    /// <summary>
    /// Static class to manage the creation of monsters in the game.
    /// </summary>
    public static class MonsterManagement
    {
        // Dictionary that stores monster names as keys and factory methods
        private static readonly Dictionary<string, Func<Monster>> MonsterDefinitions = new Dictionary<string, Func<Monster>>
        {
           
            { "Goblin", () => new Monster("Goblin", 30, 5, 2, 20, "Aggressive", new List<string> { "Potion" }, Monster.Difficulty.Easy) },
            { "Skeleton", () => new Monster("Skeleton", 50, 10, 4, 50, "Wandering", new List<string> { "Gold Coin" }, Monster.Difficulty.Medium) },
            { "Vampire", () => new Monster("Vampire", 70, 15, 5, 100, "Hostile", new List<string> { "Elixir", "Gold Coin" }, Monster.Difficulty.Hard) },
            { "Dragon", () => new Monster("Dragon", 120, 25, 10, 250, "Boss", new List<string> { "Dragon Scale", "Gold Chest" }, Monster.Difficulty.Boss) }
        };

        /// <summary>
        /// Factory method to create a monster based on the monster's name.
        /// </summary>
        /// <param name="name">The name of the monster to create.</param>
        /// <returns>A new instance of the specified monster or a default monster if the name is not recognised.</returns>
        public static Monster CreateMonster(string name)
        {
            Func<Monster> factory;

            // Check if a factory method for the specified monster name exists in the dictionary.
            if (MonsterDefinitions.TryGetValue(name, out factory))
            {
                // Call the factory method to create the monster.
                return factory();
            }

            // If the monster name is not found in the dictionary, return neutral
            return new Monster(name, 40, 8, 3, 35, "Neutral", new List<string>(), Monster.Difficulty.Medium);
        }
    }
}
