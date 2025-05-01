using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    // Interface that defines the behavior of an object that can take damage
    public interface IDamageable
    {
        /// <summary>
        /// Method to apply damage to an object that implements this interface.
        /// </summary>
        /// <param name="damageAmount">The amount of damage to apply to the object.</param>
        void TakeDamage(int damageAmount);

        /// <summary>
        /// Method to check if the object is still alive.
        /// </summary>
        /// <returns>Returns true if the object is alive, false if it is dead.</returns>
        bool IsAlive();
    }
}
