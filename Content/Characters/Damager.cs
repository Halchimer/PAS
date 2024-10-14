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
        public Damager() : base() 
        {
            BaseHealth = 3;
            Power = 2;
        }

        public override void Ability()
        {
            base.Ability();
        }
    }
}
