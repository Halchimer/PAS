using PAS.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAS.Content.Characters
{
    internal class Tank : Engine.Character
    {
        public Tank() : base()
        {
            BaseHealth = 5;
            Power = 1;
            Cooldown = 2;

        }

        public override void Ability()
        {
        if (Cooldown >= BaseCooldown)
            {
                health--;
                Power++;
                // attacker
                Power--;

            }
        }

        public override void OnRecieveDamage(int amount, Character instigator)
        {
        }
    }
}
