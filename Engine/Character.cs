using SFML.Graphics;
using NetEXT.Animation;
using PAS.Content.VisualEffects;
using PAS.Content.Widgets.Combat;
using SFML.System;

namespace PAS.Engine
{
    public enum CharacterActions
    {
        None,
        Attack,
        Parry,
        Ability
    }

    internal class Character : Actor
    {
        public event EventHandler DieEvent;
        
        protected Animator<Sprite, string> _animator;
        
        protected HealthBar _healthBar;
        public string Name { get; protected set; }
        
        public string AbilityDescription { get; protected set; }
        public int BaseHealth { get; protected set; }
        public int Power { get; protected set; }
        public int AbilityCooldown { get; protected set; }
        
        protected int health;
        public int cooldown;
        public bool isParrying;
        
        public Character() : base()
        {
            _animator = new Animator<Sprite, string>();
        }

        public override void Init(Vector2f location, Scene scene = null)
        {
            health = BaseHealth;
            cooldown = AbilityCooldown;
            base.Init(location, scene);
        }

        public virtual void BindHealthBar(HealthBar healthBar)
        {
            _healthBar = healthBar;
        }

        public override void Start()
        {
            _healthBar.SetHeartCount(BaseHealth);
            base.Start();
        }
        public void AddAnimation(string animName, int startFrame, int duration, int frameWidth, float animDuration)
        {
            FrameAnimation<Sprite> animation = new FrameAnimation<Sprite>();
            
            for(int i = startFrame; i < startFrame+duration; i++)
                animation.AddFrame(1f, new IntRect(i*frameWidth, 0, frameWidth, sprite.TextureRect.Height));
            
            _animator.AddAnimation(animName, animation, Time.FromSeconds(animDuration));
            
        }

        public void PlayAnimation(string animName, bool loopAnimation = false, bool stopOthers = false)
        {
            _animator.PlayAnimation(animName, loopAnimation, stopOthers);
        }

        public bool TryUseAbility(Character target)
        {
            if (cooldown >= AbilityCooldown)
            {
                Ability(target);
                cooldown = 0;
                return true;
            }

            return false;
        }
        
        protected virtual void Ability(Character target = null) {}

        public virtual void Attack(Character target)
        {
            if (target != null)
                target.Damage(Power, this);
        }

        public void Damage(int amount, Character instigator)
        {
            if (isParrying)
            {
                isParrying = false;
                OnParry(amount, instigator);
                
                return;
            }
            
            health -= amount;

            OnRecieveDamage(amount, instigator);

            if(health <= 0)
            {
                OnDie(System.EventArgs.Empty);
            }
            
            _healthBar.SetHeartCount(health);
        }

        public virtual void OnDie(System.EventArgs e)
        {
            DieEvent?.Invoke(this, e);
        }
        
        public virtual void OnRecieveDamage(int amount, Character instigator)
        {
            
        }

        public virtual void OnParry(int amount, Character instigator) {}

        public virtual void OnRoundComplete(CharacterActions action = CharacterActions.None)
        {
            if(action != CharacterActions.Parry)
                isParrying = false;
        }
        public override void Draw()
        {
            _animator.Update(Time.FromSeconds(Game.GetInstance().DeltaTime));
            _animator.Animate(sprite);
            base.Draw();
        }

        public bool DoAction(CharacterActions action, Character enemy)
        {
            switch (action)
            {
                case CharacterActions.Attack:
                    Attack(enemy);
                    break;
                case CharacterActions.Parry:
                    parentScene.AddActorOfClass<ParryIndicatorEffectActor>(actorLocation, true);
                    isParrying = true;
                    break;
                case CharacterActions.Ability:
                    if (!TryUseAbility(enemy))
                        return false;
                    break;
            }

            return true;
        }
    }
}
