using PAS.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAS.Content.Characters
{
    internal class Damager : Engine.Character
    {
        bool reflectDamages = false;

        public Damager() : base()
        {
            BaseHealth = 3000;
            Power = 2000;
            AbilityCooldown = 3;

        }

        public override void Ability(Character target = null)
        {
            if (cooldown >= AbilityCooldown)
            {
                reflectDamages = true;
            }

        }

        public override void OnRecieveDamage(int amount, Character instigator)
        {
            if (reflectDamages)
            {
                instigator.Damage(amount, this);
                reflectDamages = false;
            }
        }
    }
}
