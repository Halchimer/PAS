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

        bool isPowAttackActive = false;

        public Tank() : base()
        {
            Name = "TANK";

            BaseHealth = 5;
            Power = 1;
            AbilityCooldown = 2;

        }

        public override void Ability()
        {
            if (cooldown >= AbilityCooldown)
            {
                Damage(1, this);
                Power++;
                isPowAttackActive = true;

            }
        }

        public override void Attack(Character target)
        {
            base.Attack(target);
            if(isPowAttackActive)
            {
                Power--;
                isPowAttackActive= false;
            }
        }

    }
}
