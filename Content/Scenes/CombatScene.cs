using PAS.Content.Characters;
using PAS.Content.VisualEffects;
using PAS.Content.Widgets;
using PAS.Content.Widgets.Combat;
using PAS.Engine;
using SFML.Graphics;
using SFML.System;
using Event = PAS.Engine.Event;
using EventArgs = System.EventArgs;

namespace PAS.Content.Scenes
{
    internal class CombatScene<T> : Engine.Scene where T : Character, new() // T is the type of the character chosen by the player.
    {
        public bool IsPlayerTurn = true;
        
        private Character _player;
        private Character _enemy;

        private Button _attackButton;
        private Button _parryButton;
        private Button _abilityButton;

        private Actor _turnIndicator;
        
        private bool _isPaused = false;
        public CombatScene(Scene prevScene = null) : base(prevScene)
        {
            Vector2u windowSize = Game.GetInstance().GetWindow().Size / 10; // Gets the window size in render pixels (10x bigger than real pixels)

            AssetLoader texload = AssetLoader.GetInstance(); // Get an instance of the asset loader
            
            AddActorOfClass<MainMenuSkyBG>(new Vector2f(0.0f, 0f)); // Adds the sky background to the scene
            AddActorOfClass<CloudBG>(new Vector2f(0.0f, -20f));     // Adds the secondary cloud layer to the scene
            
            AddActorOfClass<CombatSceneBackground>(new Vector2f(0f, 0f)); // Adds the ground to the scene
            
            _player = AddActorOfClass<T>(new Vector2f(48f, 30f)); // Adds the player using the chosen class
            _player.DieEvent += PlayerDeath; // Binds the PlayerDeath event to the DieEvent
  
            int choice = Game.GetInstance().Rand.Next(1, 5); // Generate a random number to choose the enemy's class
            Vector2f enemyPos = new Vector2f(144f, 30f); // Instanciate a variable storing the enemy's position on the screen
            switch (choice)
            {
                case 1: 
                    _enemy = AddActorOfClass<Damager>(enemyPos);
                    break;
                case 2:
                    _enemy = AddActorOfClass<Healer>(enemyPos);
                    break;
                case 3:
                    _enemy = AddActorOfClass<Tank>(enemyPos);
                    break;
                case 4:
                    _enemy = AddActorOfClass<WukongBebere>(enemyPos);
                    break;
                default :
                    _enemy = AddActorOfClass<Damager>(enemyPos);
                    break;
            }
            _enemy.SetScale(new Vector2f(-1f, 1f)); // Sets a negative scale on X to inverse the enemy's sprite.
            _enemy.DieEvent += EnemyDeath; // Binds the EnemyDeath function to the DieEvent of _enemy

            _attackButton = AddActorOfClass<AttackButton>(new Vector2f(2f, 92f)); // Adds an attack button to the scene
            _attackButton.Clicked += PlayerAction;                                // Bind player action to this button's event "Clicked" (same for other actions)
            _parryButton = AddActorOfClass<ParryButton>(new Vector2f(66f, 92f));
            _parryButton.Clicked += PlayerAction;
            _abilityButton = AddActorOfClass<AbilityButton>(new Vector2f(131f, 92f));
            _abilityButton.Clicked += PlayerAction;
            
            HealthBar playerHealthBar = AddActorOfClass<HealthBar>(new Vector2f(2f, 2f));
            playerHealthBar.SetHeartCount(_player.BaseHealth);
            HealthBar enemyHealthBar = AddActorOfClass<HealthBar>(new Vector2f(190f, 2f));
            enemyHealthBar.SetHeartCount(_enemy.BaseHealth);
            enemyHealthBar.invertHealthBar = true; // Reverse the direction of the enemy's health bar so that it empties from the left.
            _player.BindHealthBar(playerHealthBar);
            _enemy.BindHealthBar(enemyHealthBar);
            
            _turnIndicator = AddActorOfClass<TurnIndicator>(_player.GetLocation() + new Vector2f(10, -16)); // Adds to the scene a cursor to indicate who's turn it is.
        }

        private float _tickEllapsedTimeCounter = 0f;
        public override void Tick()
        {
            // Tick function executes the logic when it's enemy's turn.
            
            if (_isPaused || IsPlayerTurn) // If it's the player's turn or if the game is paused return and don't execute enemy's logic
                return;
            
            _turnIndicator.MoveToLocationOverTime(_enemy.GetLocation() + new Vector2f(-19, -16), 0.1f); // Moves cursor to enemy's location
            _tickEllapsedTimeCounter += Game.GetInstance().DeltaTime; // Counts ellapsed time since player has finished playing.
                                                                      // this helps adding delay on enemy's action without stoping the whole game

            if (_tickEllapsedTimeCounter < 1.5f) // if the counter still hasn't reached the wanted delay, return.
                return;
            
            _tickEllapsedTimeCounter = 0; // When the enemmy finally plays, reset the counter
            
            CharacterActions choice = CharacterActions.None; // Initialize enemy's action (CharacterAction enum) to None
            
            Array values = Enum.GetValues(typeof(CharacterActions));  // Get possible actions
            while(choice == CharacterActions.None) // While the action is none, choose a random action. (prevents it to chose None)
                choice = (CharacterActions)values.GetValue(Game.GetInstance().Rand.Next(values.Length));
            
            if (!_enemy.DoAction(choice, _player)) //Try to execute the action. If it fails then return. (for exemple when ability cooldown isn't finished)
                return;

            IsPlayerTurn = true; // Sets is player turn to true to allow the player to play next round.

            if (choice != CharacterActions.Ability) // If the chosen action isn't the ability, then increase the cooldown counter
                _enemy.cooldown++;
            
            _enemy.OnRoundComplete(choice);
            _player.OnRoundComplete();
            
            _turnIndicator.MoveToLocationOverTime(_player.GetLocation() + new Vector2f(10, -16), 1); // Move the cursor to the player, indicating it's his turn
        }
        
        /// <summary>
        /// This function is called when the player press an action button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"> Contains the action the player wants to do, of type PlayerActionSendEventArgs </param>
        public void PlayerAction(object sender, EventArgs args)
        {
            if (_isPaused || !IsPlayerTurn) // if it's not player turn or if the game is paused then return, preventing the player to play.
                return;

            if (!_player.DoAction(((PlayerActionSendEventArgs)args).Action, _enemy)) // tries to execute the action, if it fails return and let the player chose again.
                return;
            
            IsPlayerTurn = false; // Allow the enemy to play next round
            if (((PlayerActionSendEventArgs)args).Action != CharacterActions.Ability) // If the action chosen isn't ability, then increase cooldown counter.
                _player.cooldown++;
            
            _enemy.OnRoundComplete();
            _player.OnRoundComplete(((PlayerActionSendEventArgs)args).Action);
        }

        public void PlayerDeath(object sender, EventArgs args) // Function executed when the player dies, bound to the character event "DieEvent"
        {
            
            AddActorOfClass<DeathTextWidget>(new Vector2f(
                Game.GetInstance().GetWindow().Size.X/20 - AssetLoader.GetInstance().GetTexture("youdied").Size.X/2, 
                15f
            ));
            EndGame(_enemy);
            
        }

        public void EnemyDeath(object sender, EventArgs args) // Function executed when the enemy dies, bound to the character event "DieEvent"
        {
            
            AddActorOfClass<VictoryTextWidget>(new Vector2f(
                Game.GetInstance().GetWindow().Size.X/20 - AssetLoader.GetInstance().GetTexture("victory").Size.X/2, 
                15f
            ));
            EndGame(_player);
        }

        private void EndGame(Character winner) // End Game function that does stuff common to victory or defeat.
        {
            _isPaused = true;
            DeleteActor(_attackButton);
            DeleteActor(_parryButton);
            DeleteActor(_abilityButton);
            DeleteActor(_turnIndicator);
            if(winner == _player)
                AddActorOfClass<ConfettiParticleEffect>(new Vector2f(0, 0));
            AddActorOfClass<MainMenuButton>(new Vector2f(
                Game.GetInstance().GetWindow().Size.X/20 - AssetLoader.GetInstance().GetTexture("button").Size.X/2,
                75f
            ));
        }
        
    }
}
