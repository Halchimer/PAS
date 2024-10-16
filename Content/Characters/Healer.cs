using PAS.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace PAS.Content.Characters
{
    internal class Healer : Engine.Character
    {
        public Healer() : base()
        {
            sprite = new Sprite(AssetLoader.GetInstance().GetTexture("healer"));
            Name = "HEALER";

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
