using PAS.Engine;
using SFML.Graphics;
using SFML.System;

namespace PAS.Content.Characters
{   
    internal class Healer : Engine.Character
    {
        /// <summary>
        /// Represents a Healer character in the game.
        /// </summary>
        public Healer() : base()
        {
            sprite = new Sprite(AssetLoader.GetInstance().GetTexture("healer"));
            Name = "HEALER";
            AbilityDescription = "Healing Ability : \nRestores 2 hearts. \nCooldown : 2 rounds.";
            
            BaseHealth = 4;
            Power = 1;
            AbilityCooldown = 2;
 
        }

        /// <summary>
        /// Initializes the Healer character with specific animations and sets its initial location and scene.
        /// </summary>
        /// <param name="location">The initial location of the healer character.</param>
        /// <param name="scene">The scene in which the healer character will be placed. This parameter is optional and can be null.</param>
        public override void Init(Vector2f location, Scene scene = null)
        {
            AddAnimation("idle",0, 15, 32, 1.5f);
            AddAnimation("attack",15, 4, 32, 0.4f);
            AddAnimation("spell",19, 8, 32, 0.8f);
            AddAnimation("parry",27, 5, 32, 0.5f);
            AddAnimation("hit",32, 4, 32, 0.4f);
            base.Init(location, scene);
        }

        public override void Start()
        {
            PlayAnimation("idle", true, false);
            base.Start();
        }

        protected override void Ability(Character target = null)
        {
            PlayAnimation("spell");
            health += 2;
            if (health > BaseHealth) 
                health = BaseHealth;
            _healthBar.SetHeartCount(health);
            
        }

        public override void Attack(Character target)
        {
            PlayAnimation("attack", false);
            base.Attack(target);
        }

        public override void OnRecieveDamage(int amount, Character instigator)
        {
            PlayAnimation("hit");
            base.OnRecieveDamage(amount, instigator);
        }

        /// <summary>
        /// Called when the character successfully performs a parry.
        /// </summary>
        /// <param name="amount">The amount of damage that was parried.</param>
        /// <param name="instigator">The character who initiated the attack.</param>
        public override void OnParry(int amount, Character instigator)
        {
            PlayAnimation("parry");
            base.OnParry(amount, instigator);
        }
    }
}
