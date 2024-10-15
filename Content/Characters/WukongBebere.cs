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
        bool  revive = false;

        public WukongBebere() : base()
        {
            BaseHealth = 3;
            Power = 2;
            Cooldown = 4;

        }

        public override void Ability()
        {
            if (Cooldown >= BaseCooldown) 
                revive = true;
        }

        public override void OnRecieveDamage(int amount, Character instigator)
        {
            if (health <= 0 && revive == true)
            {
                health = 1;
                Power++;
                revive = true;
            }
        }
    }
}
