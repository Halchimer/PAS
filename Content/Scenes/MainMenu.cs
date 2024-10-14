using PAS.Content.Widgets;
using PAS.Engine;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAS.Content.Scenes
{
    internal class MainMenu : Scene
    {
        public MainMenu(Game gameInstance) : base(gameInstance) {
            LoadTextures();
        }

        public override void Start()
        {
            Vector2u windowSize = Game.GetInstance().GetWindow().Size/10;

            AssetLoader texload = AssetLoader.GetInstance();

            AddActorOfClass<MainMenuSkyBG>(new SFML.System.Vector2f(-texload.GetTexture("sky_bg").Size.X, 0.0f));
            AddActorOfClass<MainMenuSkyBG>(new SFML.System.Vector2f(0.0f, 0.0f));

            AddActorOfClass<MainMenuBackground>(new SFML.System.Vector2f(0.0f, 0.0f));

            AddActorOfClass<MainMenuLogoWidget>(new SFML.System.Vector2f(0.0f, 0.0f));

            AddActorOfClass<PlayButton>(new SFML.System.Vector2f(windowSize.X/2 - texload.GetTexture("menu_button").Size.X/2, (3* windowSize.Y) /4));

            base.Start();
        }

        public AssetLoader LoadTextures()
        {
            AssetLoader texload = AssetLoader.GetInstance();

            texload.LoadTexture("logo.png", "logo");
            texload.LoadTexture("menu_bg.png", "menu_bg");
            texload.LoadTexture("mainmenu_sky_bg.png", "sky_bg");
            texload.LoadTexture("main_menu_button.png", "menu_button");

            return texload;
        }
    }
}
