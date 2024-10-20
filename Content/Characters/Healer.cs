using PAS.Engine;
using SFML.Graphics;
using SFML.System;

namespace PAS.Content.Characters
{   
    internal class Healer : Engine.Character
    {
        public Healer() : base()
        {
            sprite = new Sprite(AssetLoader.GetInstance().GetTexture("healer"));
            Name = "HEALER";
            AbilityDescription = "Healing Ability : Restores 2 hearts. Cooldown : 2 rounds.";
            
            BaseHealth = 4;
            Power = 1;
            AbilityCooldown = 2;
 
        }

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

        public override void OnParry(int amount, Character instigator)
        {
            PlayAnimation("parry");
            base.OnParry(amount, instigator);
        }
    }
}
