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
            BaseHealth = 3;
            Power = 2;
        }

        public override void Ability()
        {
            reflectDamages = true;
            base.Ability();
        }

        public override void OnRecieveDamage(int amount, Character instigator)
        {
            instigator.Damage(amount, this);
            reflectDamages = false;
        }
    }
}
