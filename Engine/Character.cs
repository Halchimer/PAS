using SFML.Graphics;
using NetEXT.Animation;
using PAS.Content.VisualEffects;
using PAS.Content.Widgets.Combat;
using SFML.System;

namespace PAS.Engine
{
    /// <summary>
    /// Represents the character's action during the game.
    /// </summary>
    public enum CharacterActions
    {
        None,
        Attack,
        Parry,
        Ability
    }

    /// <summary>
    /// Represents a character in the game, inheriting from the Actor class.
    /// </summary>
    internal class Character : Actor
    {
        /// <summary>
        /// Event triggered when a character dies.
        /// </summary>
        public event EventHandler DieEvent;
        
        protected Animator<Sprite, string> _animator;

        /// <summary>
        /// Represents the health bar of the character, used to visually display the health status of the character.
        /// This field is used to update the health bar when the character's health changes.
        /// </summary>
        protected HealthBar _healthBar;
        public string Name { get; protected set; }
        
        public string AbilityDescription { get; protected set; }


        /// <summary>
        /// Gets or sets the base health for the character. This property defines
        /// the initial amount of health the character has when it is created.
        /// </summary>
        public int BaseHealth { get; protected set; }

        /// <summary>
        /// Gets or sets the power level of the character.
        /// This value determines the strength of the character's attacks.
        /// </summary>
        public int Power { get; protected set; }
        public int AbilityCooldown { get; protected set; }
        
        protected int health;
        public int cooldown;

        /// <summary>
        /// Indicates whether the character is currently in a parrying state.
        /// </summary>
        public bool isParrying;
        
        public Character() : base()
        {
            _animator = new Animator<Sprite, string>();
        }

        /// <summary>
        /// Initializes the character at a given location and optionally within a specified scene.
        /// </summary>
        /// <param name="location">The location at which to initialize the character.</param>
        /// <param name="scene">The scene in which to initialize the character, default is null.</param>
        public override void Init(Vector2f location, Scene scene = null)
        {
            health = BaseHealth;
            cooldown = AbilityCooldown;
            base.Init(location, scene);
        }

        /// <summary>
        /// Binds a HealthBar to the character, allowing the health of the character to be represented visually.
        /// </summary>
        /// <param name="healthBar">The HealthBar instance to bind to the character.</param>
        public virtual void BindHealthBar(HealthBar healthBar)
        {
            _healthBar = healthBar;
        }

        /// <summary>
        /// Starts the character by initializing the health bar with the base health
        /// and invoking the base class's Start method.
        /// </summary>
        public override void Start()
        {
            _healthBar.SetHeartCount(BaseHealth);
            base.Start();
        }

        /// <summary>
        /// Adds a new animation to the character's animator.
        /// </summary>
        /// <param name="animName">The name of the animation.</param>
        /// <param name="startFrame">The starting frame of the animation sequence.</param>
        /// <param name="duration">The number of frames in the animation sequence.</param>
        /// <param name="frameWidth">The width of each frame in the animation.</param>
        /// <param name="animDuration">The total duration of the animation in seconds.</param>
        public void AddAnimation(string animName, int startFrame, int duration, int frameWidth, float animDuration)
        {
            FrameAnimation<Sprite> animation = new FrameAnimation<Sprite>();
            
            for(int i = startFrame; i < startFrame+duration; i++)
                animation.AddFrame(1f, new IntRect(i*frameWidth, 0, frameWidth, sprite.TextureRect.Height));
            
            _animator.AddAnimation(animName, animation, Time.FromSeconds(animDuration));
            
        }

        /// <summary>
        /// Plays a specified animation on the character.
        /// </summary>
        /// <param name="animName">The name of the animation to play.</param>
        /// <param name="loopAnimation">Determines if the animation should loop when it reaches the end.</param>
        /// <param name="stopOthers">Indicates whether other animations should be stopped before playing the new animation.</param>
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

        /// <summary>
        /// Executes the character's unique ability.
        /// This method should be overridden by derived classes to provide specific ability behaviors.
        /// </summary>
        /// <param name="target">The target of the ability. This parameter is optional and can be null depending on the ability.</param>
        protected virtual void Ability(Character target = null) {}

        /// <summary>
        /// Executes an attack on the specified target character, inflicting damage based on the attacker's Power.
        /// </summary>
        /// <param name="target">The character to be attacked.</param>
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

        /// <summary>
        /// Executed when a character successfully parries an attack. This method is triggered to manage the specific behavior or animation associated with a parry action.
        /// </summary>
        /// <param name="amount">The amount of damage that was parried.</param>
        /// <param name="instigator">The character who initiated the attack that was parried.</param>
        public virtual void OnParry(int amount, Character instigator) {}

        /// <summary>
        /// Method called when a round in the combat sequence is completed.
        /// Performs necessary clean-ups or state changes depending on the action taken.
        /// </summary>
        /// <param name="action">The action that was taken during the round. Defaults to CharacterActions.None if no action was specified.</param>
        public virtual void OnRoundComplete(CharacterActions action = CharacterActions.None)
        {
            if(action != CharacterActions.Parry)
                isParrying = false;
        }

        /// <summary>
        /// Draws the character by updating and animating the sprite using the animator,
        /// and calling the base class Draw method to handle any additional drawing logic.
        /// </summary>
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
