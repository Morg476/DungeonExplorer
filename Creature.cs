using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    // Abstract base class for all creatures
    public abstract class Creature
    {
        // Properties representing the creature's name, health points, attack, and defense
        public string Name { get; protected set; }
        public int HP { get; protected set; }
        public int Attack { get; protected set; }
        public int Defense { get; protected set; }

        // Constructor to initialise the creature's properties
        public Creature(string name, int hp, int attack, int defense)
        {
            Name = name;
            HP = hp;
            Attack = attack;
            Defense = defense;
        }

        // Checks if the creature is still alive (
        public bool IsAlive()
        {
            return HP > 0;
        }

        // Calculates and returns the damage taken after considering defense
        public virtual int DamageTaken(int damage)
        {
            int actualDamage = Math.Max(damage - Defense, 0); // Ensure no negative damage
            HP -= actualDamage; // Subtract damage from HP
            HP = Math.Max(HP, 0); // Ensure HP doesn't go below 0
            return actualDamage;
        }

        // Abstract method to be implemented by derived classes to define how the creature attacks another target
        public abstract int AttackTarget(Creature target);

        // Returns a string representation of the creature
        public override string ToString()
        {
            return $"{Name} (HP: {HP})";
        }
    }
}
