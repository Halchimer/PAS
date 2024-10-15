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
            BaseHealth = 5000;
            Power = 1000;
            AbilityCooldown = 2;

        }

        public override void Ability(Character target = null)
        {
            if (cooldown >= AbilityCooldown)
            {
                Damage(1000, this);
                Power += 1000;
                Attack(target);
                Power -= 1000;
            }
        }



    }
}
