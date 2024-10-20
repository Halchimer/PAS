using PAS.Engine;
using SFML.System;
using SFML.Window;

namespace PAS.Content.Widgets
{
    internal class MainMenuLogoWidget : Actor
    {
        Vector2f defaultLocation;
        
        //Animator<Sprite,string> logoAnimator;

        public MainMenuLogoWidget() : base() 
        { 
            sprite = new SFML.Graphics.Sprite(AssetLoader.GetInstance().GetTexture("logo"));
            //logoAnimator = new Animator<Sprite, string>();
            //FadeAnimation<Sprite> fadeAnim = new FadeAnimation<Sprite>(1f, 0f);
            //logoAnimator.AddAnimation("fadeLogo", fadeAnim, Time.FromSeconds(1.0f));
        }
        public override void Start()
        {
            defaultLocation = new Vector2f(
                192 / 2 - sprite.GetLocalBounds().Width / 2,
                108.0f / 15 - 5
            );

            MoveToLocationOverTime(defaultLocation, 5);
            
            //logoAnimator.PlayAnimation("fadeLogo");
            
            base.Start();
        }

        public override void Tick()
        {
            
            base.Tick();
        }

        public override void Draw()
        {
            //logoAnimator.Update(Time.FromSeconds(Game.GetInstance().DeltaTime));
            //logoAnimator.Animate(sprite);
            base.Draw();
        }
    }
}
