using PAS.Engine;
using SFML.Graphics;
using SFML.System;

namespace PAS.Content.Characters
{
    internal class Damager : Engine.Character
    {
        bool reflectDamages = false;

        public Damager() : base()
        {
            sprite = new Sprite(AssetLoader.GetInstance().GetTexture("damager"));
            Name = "DAMAGER";
            AbilityDescription = "Reflect Ability : \nDuring next round the Damager reflects \nany damage taken, but still takes them. \nCooldown : 3 rounds.";

            BaseHealth = 3;
            Power = 2;
            AbilityCooldown = 3;

        }

        /// <summary>
        /// Initializes the Damager character with the given location and scene.
        /// Configures initial animations such as idle, attack, parry, hit, and ability
        /// before calling the base class Init method.
        /// </summary>
        /// <param name="location">The position where the character will be initialized.</param>
        /// <param name="scene">Optional scene to associate with the character during initialization.</param>
        public override void Init(Vector2f location, Scene scene = null)
        {
            AddAnimation("idle", 0, 7, 32, 0.7f);
            AddAnimation("attack", 7, 9, 32, 0.9f);
            AddAnimation("parry", 16, 9, 32, 0.9f);
            AddAnimation("hit", 25, 3, 32, 0.3f);
            AddAnimation("ability", 28, 14, 32, 1.4f);
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
            reflectDamages = true;
            base.Ability(target);
        }

        public override void Attack(Character target)
        {
            PlayAnimation("attack");
            base.Attack(target);
        }

        public override void OnRecieveDamage(int amount, Character instigator)
        {
            PlayAnimation("hit");
            if (reflectDamages)
            {
                reflectDamages = false;
                PlayAnimation("attack");
                instigator.Damage(amount, this);
            }
        }

        public override void OnParry(int amount, Character instigator)
        {
            PlayAnimation("parry");
            base.OnParry(amount, instigator);
        }

        public override void OnRoundComplete(CharacterActions action = CharacterActions.None)
        {
            base.OnRoundComplete(action);
            if (action == CharacterActions.Ability)
                return;
            reflectDamages = false;
        }
    }
}
