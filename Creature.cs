using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public abstract class Creature
    {
        public string Name { get; protected set; }
        public int HP { get; protected set; }
        public int Attack { get; protected set; }
        public int Defense { get; protected set; }

        public Creature(string name, int hp, int attack, int defense)
        {
            Name = name;
            HP = hp;
            Attack = attack;
            Defense = defense;
        }

        public bool IsAlive()
        {
            return HP > 0;
        }

        public virtual int DamageTaken(int damage)
        {
            int actualDamage = Math.Max(damage - Defense, 0);
            HP -= actualDamage;
            HP = Math.Max(HP, 0);
            return actualDamage;
        }

        public abstract int AttackTarget(Creature target);

        public override string ToString()
        {
            return $"{Name} (HP: {HP})";
        }
    }
}
