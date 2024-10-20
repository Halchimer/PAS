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
    internal class CombatScene<T> : Engine.Scene where T : Character, new()
    {
        public bool IsPlayerTurn = true;
        
        private Character _player;
        private Character _enemy;

        private Button _attackButton;
        private Button _parryButton;
        private Button _abilityButton;
        
        private Random _randChoice = new Random();
        
        private bool _isPaused = false;
        public CombatScene(Scene prevScene = null) : base(prevScene)
        {
            Vector2u windowSize = Game.GetInstance().GetWindow().Size / 10;

            AssetLoader texload = AssetLoader.GetInstance();
            
            AddActorOfClass<MainMenuSkyBG>(new Vector2f(0.0f, 0f));
            AddActorOfClass<CloudBG>(new Vector2f(0.0f, -20f));
            
            AddActorOfClass<CombatSceneBackground>(new Vector2f(0f, 0f));
            
            _player = AddActorOfClass<T>(new Vector2f(48f, 30f));
            _player.DieEvent += PlayerDeath;
  
            int choice = _randChoice.Next(1, 5);
            Vector2f enemyPos = new Vector2f(144f, 30f);
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
            _enemy.SetScale(new Vector2f(-1f, 1f));
            _enemy.DieEvent += EnemyDeath;

            _attackButton = AddActorOfClass<AttackButton>(new Vector2f(2f, 92f));
            _attackButton.Clicked += PlayerAction;
            _parryButton = AddActorOfClass<ParryButton>(new Vector2f(66f, 92f));
            _parryButton.Clicked += PlayerAction;
            _abilityButton = AddActorOfClass<AbilityButton>(new Vector2f(131f, 92f));
            _abilityButton.Clicked += PlayerAction;
            
            HealthBar playerHealthBar = AddActorOfClass<HealthBar>(new Vector2f(2f, 2f));
            playerHealthBar.SetHeartCount(_player.BaseHealth);
            HealthBar enemyHealthBar = AddActorOfClass<HealthBar>(new Vector2f(190f, 2f));
            enemyHealthBar.SetHeartCount(_enemy.BaseHealth);
            enemyHealthBar.invertHealthBar = true;
            _player.BindHealthBar(playerHealthBar);
            _enemy.BindHealthBar(enemyHealthBar);
        }

        private float _tickEllapsedTimeCounter = 0f;
        public override void Tick()
        {
            if (_isPaused)
                return;

            _tickEllapsedTimeCounter += Game.GetInstance().DeltaTime;
            
            if (IsPlayerTurn)
                return;
            if (_tickEllapsedTimeCounter < 1)
                return;
            _tickEllapsedTimeCounter = 0;
            
            Array values = Enum.GetValues(typeof(CharacterActions)); 
            CharacterActions choice = (CharacterActions)values.GetValue(_randChoice.Next(values.Length));
            if (!_enemy.DoAction(choice, _player))
                return;

            IsPlayerTurn = true;

            if (choice != CharacterActions.Ability)
                _enemy.cooldown++;
            
            _enemy.OnRoundComplete(choice);
            _player.OnRoundComplete();
        }
        
        public void PlayerAction(object sender, EventArgs args)
        {
            if (_isPaused)
                return;
            if (!IsPlayerTurn)
                return;

            if (!_player.DoAction(((PlayerActionSendEventArgs)args).Action, _enemy))
                return;
            
            IsPlayerTurn = false;
            if (((PlayerActionSendEventArgs)args).Action != CharacterActions.Ability)
                _player.cooldown++;
            
            _enemy.OnRoundComplete();
            _player.OnRoundComplete(((PlayerActionSendEventArgs)args).Action);
        }

        public void PlayerDeath(object sender, EventArgs args)
        {
            
            AddActorOfClass<DeathTextWidget>(new Vector2f(
                Game.GetInstance().GetWindow().Size.X/20 - AssetLoader.GetInstance().GetTexture("youdied").Size.X/2, 
                15f
            ));
            EndGame(_enemy);
            
        }

        public void EnemyDeath(object sender, EventArgs args)
        {
            
            AddActorOfClass<VictoryTextWidget>(new Vector2f(
                Game.GetInstance().GetWindow().Size.X/20 - AssetLoader.GetInstance().GetTexture("victory").Size.X/2, 
                15f
            ));
            EndGame(_player);
        }

        private void EndGame(Character winner)
        {
            _isPaused = true;
            DeleteActor(_attackButton);
            DeleteActor(_parryButton);
            DeleteActor(_abilityButton);
            if(winner == _player)
                AddActorOfClass<ConfettiParticleEffect>(new Vector2f(0, 0));
            AddActorOfClass<MainMenuButton>(new Vector2f(
                Game.GetInstance().GetWindow().Size.X/20 - AssetLoader.GetInstance().GetTexture("button").Size.X/2,
                75f
            ));
        }
        
    }
}
