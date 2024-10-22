using PAS.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace PAS.Content.Characters
{
    internal class WukongBebere : Engine.Character
    {
        bool  revive = false;

        public WukongBebere() : base()
        {
            sprite = new Sprite(AssetLoader.GetInstance().GetTexture("beber"));
            Name = "BEBER";
            AbilityDescription = "Resurrection Ability : \nSurvive from your death next round \nwith 1 heart and 1 pow. \nCooldown : 4 rounds.";

            BaseHealth = 3;
            Power = 1;
            AbilityCooldown = 4;

        }

        public override void Init(Vector2f location, Scene scene = null)
        {
            AddAnimation("idle", 0, 8, 32, 0.8f);
            AddAnimation("attack", 8, 8, 32, 0.8f);
            AddAnimation("ability", 16, 15, 32, 1.5f);
            AddAnimation("parry", 31, 4, 32, 0.4f);
            AddAnimation("hit", 35, 3, 32, 0.3f);
            base.Init(location, scene);
        }

        public override void Start()
        {
            PlayAnimation("idle", true);
            base.Start();
        }

        protected override void Ability(Character target = null)
        {
            PlayAnimation("ability");
            revive = true;
        }

        public override void OnRecieveDamage(int amount, Character instigator)
        {
            PlayAnimation("hit");
            if (health <= 0 && revive == true)
            {
                health = 1;
                _healthBar.SetHeartCount(health);
                Power++;
                revive = false;
            }
        }

        public override void OnParry(int amount, Character instigator)
        {
            PlayAnimation("parry");
            base.OnParry(amount, instigator);
        }

        public override void Attack(Character target)
        {
            PlayAnimation("attack");
            base.Attack(target);
        }

        public override void OnRoundComplete(CharacterActions action = CharacterActions.None)
        {
            if(action != CharacterActions.Ability)
                revive = false;
            base.OnRoundComplete(action);
        }
    }
}
