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
    /// Represents the character WukongBebere in the game with unique capabilities and behaviors.
    ///
    /// Ability : WukongBebere, when using his ability, is able to cheat death during the next round, with increased power but 1 heart.
    /// Obviously this ability has a big cooldown of 4 rounds.
    /// 
    /// </summary>
    internal class WukongBebere : Engine.Character
    {
        /// <summary>
        /// A flag indicating whether the character has the ability to revive after being defeated.
        /// If set to true, the character will revive with 1 health and 1 power after being defeated
        /// in the next round. This ability is reset after each round unless activated again.
        /// </summary>
        bool  revive = false;

        /// <summary>
        /// Represents the character WukongBebere within the game.
        /// This character has a unique resurrection ability allowing it to survive
        /// fatal damage and return with minimal health and power.
        /// </summary>
        public WukongBebere() : base()
        {
            sprite = new Sprite(AssetLoader.GetInstance().GetTexture("beber"));
            Name = "BEBER";
            AbilityDescription = "Resurrection Ability : \nSurvive from your death next round \nwith 1 heart and 1 pow. \nCooldown : 4 rounds.";

            BaseHealth = 3;
            Power = 1;
            AbilityCooldown = 4;

        }

        /// <summary>
        /// Initializes the character at the specified location within a given scene.
        /// Adds various character animations such as idle, attack, ability, parry, and hit.
        /// </summary>
        /// <param name="location">The location at which to initialize the character.</param>
        /// <param name="scene">The scene where the character is added; can be null.</param>
        public override void Init(Vector2f location, Scene scene = null)
        {
            AddAnimation("idle", 0, 8, 32, 0.8f);
            AddAnimation("attack", 8, 8, 32, 0.8f);
            AddAnimation("ability", 16, 15, 32, 1.5f);
            AddAnimation("parry", 31, 4, 32, 0.4f);
            AddAnimation("hit", 35, 3, 32, 0.3f);
            base.Init(location, scene);
        }

        /// <summary>
        /// Initiates the character by playing the default "idle" animation and calling the base Start method.
        /// </summary>
        public override void Start()
        {
            PlayAnimation("idle", true);
            base.Start();
        }

        /// <summary>
        /// Executes the ability of the character, which involves playing the associated animation and setting the revive flag to true.
        /// </summary>
        /// <param name="target">The target character, if any, towards which the ability is directed.</param>
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

        /// <summary>
        /// Invoked when the character successfully parries an attack.
        /// Plays the parry animation and triggers the base class parry logic.
        /// </summary>
        /// <param name="amount">The amount of damage that was parried.</param>
        /// <param name="instigator">The character who initiated the attack.</param>
        public override void OnParry(int amount, Character instigator)
        {
            PlayAnimation("parry");
            base.OnParry(amount, instigator);
        }

        /// <summary>
        /// Executes an attack on the specified target character. Plays the "attack" animation and
        /// triggers the base class's Attack method.
        /// </summary>
        /// <param name="target">The character to be attacked.</param>
        public override void Attack(Character target)
        {
            PlayAnimation("attack");
            base.Attack(target);
        }

        /// <summary>
        /// Called when a round is complete and triggers necessary updates or actions for the character.
        /// </summary>
        /// <param name="action">The action that the character performed during the round.</param>
        public override void OnRoundComplete(CharacterActions action = CharacterActions.None)
        {
            if(action != CharacterActions.Ability)
                revive = false;
            base.OnRoundComplete(action);
        }
    }
}
