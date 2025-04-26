using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{

    public class Monster : Creature
    {
        public int Experience { get; private set; }
        public string Behaviour { get; private set; }
        public List<string> MobLoot { get; private set; }

        public Monster(string name, int hp, int attack, int defense, int experience, string behaviour, List<string> mobloot)
            : base(name, hp, attack, defense)
        {
            Experience = experience;
            Behaviour = behaviour;
            MobLoot = mobloot ?? new List<string>();
        }

        public bool MobAlive()
        {
            return IsAlive();
        }

        public override int DamageTaken(int damage)
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

        public override int AttackTarget(Creature target)
        {
            int damageDealt = Math.Max(Attack - target.Defense, 0);
            target.DamageTaken(damageDealt);
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
