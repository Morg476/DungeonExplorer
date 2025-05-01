using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    /// <summary>
    /// Represents a monster in the dungeon.
    /// Inherits from the Creature class, with additional properties for behavior, loot, experience, and difficulty.
    /// </summary>
    public class Monster : Creature
    {
        /// <summary>
        /// Defines the difficulty levels of the monster.
        /// </summary>
        public enum Difficulty
        {
            Easy,  
            Medium, 
            Hard,   
            Boss    
        }

        /// <summary>
        /// Gets the experience points the player gains by defeating this monster.
        /// </summary>
        public int Experience { get; private set; }

        /// <summary>
        /// Gets the behavior of the monster
        /// </summary>
        public string Behaviour { get; private set; }

        /// <summary>
        /// Gets the list of loot items that the monster can drop upon being defeated.
        /// </summary>
        public List<string> MobLoot { get; private set; }

        /// <summary>
        /// Gets the difficulty level of the monster (Easy, Medium, Hard, or Boss).
        /// </summary>
        public Difficulty DifficultyLevel { get; private set; }

        /// <summary>
        /// Initalises instance of the <see cref="Monster"/> class with specified values.
        /// </summary>
        /// <param name="name">The name of the monster.</param>
        /// <param name="hp">The health points of the monster.</param>
        /// <param name="attack">The attack power of the monster.</param>
        /// <param name="defense">The defense value of the monster.</param>
        /// <param name="experience">The experience points awarded for defeating this monster.</param>
        /// <param name="behaviour">The behavior of the monster </param>
        /// <param name="mobloot">A list of items the monster can drop as loot.</param>
        /// <param name="difficulty">The difficulty level of the monster.</param>
        public Monster(string name, int hp, int attack, int defense, int experience, string behaviour, List<string> mobloot, Difficulty difficulty)
            : base(name, hp, attack, defense)
        {
            Experience = experience;
            Behaviour = behaviour;
            MobLoot = mobloot ?? new List<string>();
            DifficultyLevel = difficulty;
        }

        /// <summary>
        /// Checks whether the monster is alive.
        /// </summary>
        /// <returns>True if the monster is alive otherwise, false.</returns>
        public bool MobAlive()
        {
            return IsAlive();
        }

        /// <summary>
        /// Calculates and applies damage taken by the monster.
        /// </summary>
        /// <param name="damage">The incoming damage.</param>
        /// <returns>The actual damage taken after defense calculation.</returns>
        public override int DamageTaken(int damage)
        {
            int actualDamage = Math.Max(damage - Defense, 0);
            HP -= actualDamage;
            HP = Math.Max(HP, 0);
            return actualDamage;
        }

        /// <summary>
        /// Allows the monster to attack the player.
        /// </summary>
        /// <param name="player">The player being attacked.</param>
        /// <returns>The amount of damage dealt to the player.</returns>
        public int AttackPlayer(Player player)
        {
            int damageDealt = Math.Max(Attack - player.Defense, 0);
            player.DamageTaken(damageDealt);
            return damageDealt;
        }

        /// <summary>
        /// Allows the monster to attack another creature i.e player
        /// </summary>
        /// <param name="target">The target creature to attack.</param>
        /// <returns>The amount of damage dealt to the target.</returns>
        public override int AttackTarget(Creature target)
        {
            int damageDealt = Math.Max(Attack - target.Defense, 0);
            target.DamageTaken(damageDealt);
            return damageDealt;
        }

        /// <summary>
        /// Drops the loot associated with the monster.
        /// </summary>
        /// <returns>A list of items that the monster drops.</returns>
        public List<string> DropLoot()
        {
            return MobLoot;
        }

        /// <summary>
        /// Returns a string representation of the monster.
        /// </summary>
        /// <returns>A string that represents the monster </returns>
        public override string ToString()
        {
            return $"{Name} (HP: {HP})";
        }
    }
}
