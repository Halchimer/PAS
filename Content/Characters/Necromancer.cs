using PAS.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAS.Content.Characters
{
    internal class Necromancer : Engine.Character
    {
        bool invocation = false;
        Random random = new Random(42);
        int isSkeletonTouch = 0;
        public Necromancer() : base()
        {
            BaseHealth = 2500;
            Power = 1500;
            AbilityCooldown = 3;

        }

        public override void Ability(Character target = null)
        {
            if (cooldown >= AbilityCooldown)
            {
                Damage(1000, this);
                Power = 500;
                for (int i = 0; i < 10; i++)
                {
                    isSkeletonTouch = new random(1, 100);
                    if (isSkeletonTouch >= 66)
                        Attack(target);
                    else if (isSkeletonTouch <= 33)
                        health += 200;

                }
                Power = 1500;
            }
        }
    }
}
