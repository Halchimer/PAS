using PAS.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAS.Content.Characters
{
    internal class WukongBebere : Engine.Character
    {
        bool revive = false;

        public WukongBebere() : base()
        {
            BaseHealth = 3000;
            Power = 2000;
            AbilityCooldown = 4;

        }

        public override void Ability(Character target = null)
        {
            if (cooldown >= AbilityCooldown)
                revive = true;
        }

        public override void OnRecieveDamage(int amount, Character instigator)
        {
            if (health <= 0 && revive == true)
            {
                health = 1000;
                Power++;
                revive = false;
            }
        }
    }
}
