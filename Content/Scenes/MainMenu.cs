using PAS.Content.VisualEffects;
using PAS.Content.Widgets;
using PAS.Content.Widgets.Combat;
using PAS.Engine;
using SFML.System;

namespace PAS.Content.Scenes
{
    internal class MainMenu : Scene
    {

        public Actor logo;

        public MainMenu() : base()
        {
            Vector2u windowSize = Game.GetInstance().GetWindow().Size/10;

            AssetLoader texload = AssetLoader.GetInstance();

            AddActorOfClass<MainMenuSkyBG>(new Vector2f(0.0f, 0.0f));
            AddActorOfClass<CloudBG>(new Vector2f(0.0f, -20f));

            AddActorOfClass<MainMenuBackground>(new Vector2f(0.0f, 0.0f));

            logo =  AddActorOfClass<MainMenuLogoWidget>(new Vector2f(
                192 / 2 - texload.GetTexture("logo").Size.X / 2,
                -texload.GetTexture("logo").Size.Y
            ));

            AddActorOfClass<PlayButton>(new Vector2f(windowSize.X/2 - texload.GetTexture("button").Size.X/2,70f));
            AddActorOfClass<QuitButton>(new Vector2f(windowSize.X / 2 - texload.GetTexture("button").Size.X / 2, 87f));
            
        }

        
        
    }
}
