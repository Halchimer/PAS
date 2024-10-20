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
            Name = "BEBER";
            AbilityDescription = "Resurrection Ability : Survive from your death next round with 1 heart and 1 pow. Cooldown : 4 rounds.";

            BaseHealth = 3;
            Power = 2;
            AbilityCooldown = 4;

        }

        protected override void Ability(Character target = null)
        {
            revive = true;
        }

        public override void OnRecieveDamage(int amount, Character instigator)
        {
            if (health <= 0 && revive == true)
            {
                health = 1;
                _healthBar.SetHeartCount(health);
                Power++;
                revive = false;
            }
        }
    }
}
