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
    internal class Tank : Engine.Character
    {

        bool isPowAttackActive = false;

        public Tank() : base()
        {
            sprite = new Sprite(AssetLoader.GetInstance().GetTexture("tank"));
            Name = "TANK";
            AbilityDescription = "Pyromania Ability : Sacrifice 1 heart and increase power by 1 for the next round. Cooldown : 2 rounds.";
            
            BaseHealth = 5;
            Power = 1;
            AbilityCooldown = 2;

        }

        public override void Init(Vector2f location, Scene scene = null)
        {
            AddAnimation("idle", 0, 8, 32, 0.8f);
            AddAnimation("attack", 8, 7, 32, 0.7f);
            AddAnimation("parry", 15, 8, 32, 0.8f);
            AddAnimation("ability", 26, 11, 32, 1.1f);
            AddAnimation("hit", 23, 3, 32, 0.3f);
            
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
                Damage(1, this);
                Power++;
                isPowAttackActive = true;
        }

        public override void Attack(Character target)
        {
            PlayAnimation("attack");
            base.Attack(target);
        }

        public override void OnRecieveDamage(int amount, Character instigator)
        {
            PlayAnimation("hit");
            base.OnRecieveDamage(amount, instigator);
        }

        public override void OnParry(int amount, Character instigator)
        {
            PlayAnimation("parry");
            base.OnParry(amount, instigator);
        }

        public override void OnRoundComplete(CharacterActions action = CharacterActions.None)
        {
            base.OnRoundComplete(action);
            if (!isPowAttackActive || action == CharacterActions.Ability)
                return;
            Power--;
            isPowAttackActive = false;
        }
    }
}
