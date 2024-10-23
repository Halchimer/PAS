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
    /// <summary>
    /// Represents a Tank character in the game.
    ///
    /// Ability : Deals 1 more heart of damage during the next round at the expense of 1 heart.
    /// Cooldown : 2 Rounds.
    /// </summary>
    internal class Tank : Engine.Character
    {
        /// <summary>
        /// Indicates whether the power attack ability is currently active for the Tank character.
        /// When true, the Tank's power has been temporarily increased by sacrificing health,
        /// and this state will persist until the end of the round.
        /// </summary>
        bool isPowAttackActive = false;

        /// <summary>
        /// Represents a Tank character in the game, inheriting from the base Character class.
        /// The Tank character has a unique ability called "Pyromania" that sacrifices health to increase power temporarily.
        /// </summary>
        public Tank() : base()
        {
            sprite = new Sprite(AssetLoader.GetInstance().GetTexture("tank"));
            Name = "TANK";
            AbilityDescription = "Pyromania Ability : \nSacrifice 1 heart and increase power \nby 1 for the next round. \nCooldown : 2 rounds.";
            
            BaseHealth = 5;
            Power = 1;
            AbilityCooldown = 2;

        }

        /// <summary>
        /// Initializes the Tank character at a specified location and optionally assigns it to a scene.
        /// </summary>
        /// <param name="location">The initial location of the Tank character.</param>
        /// <param name="scene">The scene to which the Tank character belongs. This parameter is optional and defaults to null.</param>
        public override void Init(Vector2f location, Scene scene = null)
        {
            AddAnimation("idle", 0, 8, 32, 0.8f);
            AddAnimation("attack", 8, 7, 32, 0.7f);
            AddAnimation("parry", 15, 8, 32, 0.8f);
            AddAnimation("ability", 26, 11, 32, 1.1f);
            AddAnimation("hit", 23, 3, 32, 0.3f);
            
            base.Init(location, scene);
        }

        /// This method is responsible for starting the character's initial state.
        /// Typically includes playing the opening animation sequence and any other
        /// necessary initialization tasks.
        /// Overrides the base Start method to perform animations specific to the Tank character class.
        /// /
        public override void Start()
        {
            PlayAnimation("idle", true);
            base.Start();
        }

        /// <summary>
        /// Executes the ability action for the Tank character.
        /// Plays the ability animation, applies damage, increases power,
        /// and activates the power attack status.
        /// </summary>
        /// <param name="target">The character that is the target of the ability.
        /// If null, the ability will be performed without a specific target.</param>
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

        /// <summary>
        /// Called when the character receives damage. Plays the "hit" animation and then calls the base method to handle damage logic.
        /// </summary>
        /// <param name="amount">The amount of damage received.</param>
        /// <param name="instigator">The character that caused the damage.</param>
        public override void OnRecieveDamage(int amount, Character instigator)
        {
            PlayAnimation("hit");
            base.OnRecieveDamage(amount, instigator);
        }

        /// <summary>
        /// Invoked when the character successfully parries an attack, triggering the associated parry animation.
        /// </summary>
        /// <param name="amount">The amount of damage that was parried.</param>
        /// <param name="instigator">The character who initiated the attack that was parried.</param>
        public override void OnParry(int amount, Character instigator)
        {
            PlayAnimation("parry");
            base.OnParry(amount, instigator);
        }

        /// <summary>
        /// Called when a round is completed. It handles the end of the round logic for the tank character, including deactivating power attack if it was active.
        /// </summary>
        /// <param name="action">The action that the character took during the round.</param>
        public override void OnRoundComplete(CharacterActions action = CharacterActions.None)
        {
            base.OnRoundComplete(action);
            if (!isPowAttackActive || action == CharacterActions.None || action == CharacterActions.Ability)
                return;
            Power--;
            isPowAttackActive = false;
        }
    }
}
