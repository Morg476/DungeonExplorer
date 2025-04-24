using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class Monster
    {
        public string Name { get; private set; }
        public int HP { get; private set; }
        public int Attack { get; private set; }
        public int Defense { get; private set; }
        public int Experience { get; private set; }
        public string Behaviour { get; private set; }
        public List<string> MobLoot { get; private set; }

        public Monster(string name, int hp, int attack, int defense, int experience, string behaviour, List<string> mobloot)
        {
            Name = name;
            HP = hp;
            Attack = attack;
            Defense = defense;
            Experience = experience;
            Behaviour = behaviour;
            
            if(MobLoot != null)
            {
                MobLoot = mobloot;
            }
            else
            {
                MobLoot = new List<string>();
            }

        }

        public bool MobAlive()
        {
            return HP > 0;
        }

        public int DamageTaken(int damage)
        {
            int actualDamage = Math.Max(damage - Defense, 0);
            HP -= actualDamage;
            HP = Math.Max(HP, 0);
            return actualDamage;
        }

        public int AttackPlayer(Player player)
        {
            int damageDealt = Math.Max(Attack - player.Defense, 0);
            player.DamageTaken(damageDealt);
            return damageDealt;
        }
        public List<string> DropLoot()
        {
            return MobLoot;
        }
        public override string ToString()
        {
            return $"{Name} (HP: {HP})";
        }
    }
}
