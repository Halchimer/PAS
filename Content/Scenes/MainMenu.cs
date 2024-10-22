using NetEXT.Animation;
using PAS.Content.VisualEffects;
using PAS.Content.Widgets;
using PAS.Content.Widgets.Combat;
using PAS.Engine;
using SFML.Graphics;
using SFML.System;
using EventArgs = System.EventArgs;

namespace PAS.Content.Scenes
{
    internal class MainMenu : Scene
    {

        public Actor logo;
        private Button _playButton;
        private Button _quitButton;
        private Actor _dirtBackGround;
        private Button _creditsButton;

        private Animator<Sprite, string> _animator;

        public MainMenu() : base()
        {
            _animator = new Animator<Sprite, string>();
            Vector2u windowSize = Game.GetInstance().GetWindow().Size/10;

            AssetLoader texload = AssetLoader.GetInstance();

            AddActorOfClass<MainMenuSkyBG>(new Vector2f(0.0f, 0.0f));
            AddActorOfClass<CloudBG>(new Vector2f(0.0f, -20f));

            _dirtBackGround = AddActorOfClass<MainMenuBackground>(new Vector2f(0.0f, 0.0f));

            logo =  AddActorOfClass<MainMenuLogoWidget>(new Vector2f(
                192 / 2 - texload.GetTexture("logo").Size.X / 2,
                -texload.GetTexture("logo").Size.Y
            ));

            _playButton = AddActorOfClass<PlayButton>(new Vector2f(windowSize.X/2 - texload.GetTexture("button").Size.X/2,70f));
            _playButton.Clicked += PlayClick;
            _quitButton = AddActorOfClass<QuitButton>(new Vector2f(windowSize.X / 2 - texload.GetTexture("button").Size.X / 2, 87f));
            _creditsButton = AddActorOfClass<CreditsButton>(new Vector2f(1, 103f));
            
            _animator.AddAnimation("fadeOutAnim", new FadeAnimation<Sprite>(0f, 0.5f), Time.FromSeconds(1f));
        }

        private bool shouldPlay = false;
        private float ellapsedTimeBeforePlay = 0f;
        public override void Tick()
        {
            base.Tick();
            _animator.Update(Time.FromSeconds(Game.GetInstance().DeltaTime));
            _animator.Animate(logo.GetSprite());
            _animator.Animate(_playButton.GetSprite());
            _animator.Animate(_quitButton.GetSprite());
            _animator.Animate(_playButton.GetText());
            _animator.Animate(_quitButton.GetText());
            _animator.Animate(_creditsButton.GetSprite());
            

            if (!shouldPlay)
                return;
            ellapsedTimeBeforePlay += Game.GetInstance().DeltaTime;
            if (ellapsedTimeBeforePlay < 2f)
                return;
            Game.GetInstance().SetScene(new ClassSelectionScene(new MainMenu()));
        }

        private void PlayClick(object sender, EventArgs e)
        {
            _animator.PlayAnimation("fadeOutAnim");
            _dirtBackGround.MoveToLocationOverTime(new Vector2f(0, -108), 2);
            shouldPlay = true;
        }
        
    }
}
