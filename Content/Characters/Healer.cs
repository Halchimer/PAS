using PAS.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAS.Content.Characters
{
    internal class Healer : Engine.Character
    {
        public Healer() : base()
        {
            BaseHealth = 4;
            Power = 1;
            AbilityCooldown = 2;
        }

        public override void Ability()
        {
            if (cooldown >= AbilityCooldown)
            {
                health += 2;
                if (health > BaseHealth)
                    health = BaseHealth;
            }
            
        }

    }
}
