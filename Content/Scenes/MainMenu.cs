﻿using PAS.Content.Widgets;
using PAS.Content.Widgets.Combat;
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

        public Actor logo;
        public MainMenu() : base() {
            LoadTextures();
        }

        public override void Start()
        {
            Vector2u windowSize = Game.GetInstance().GetWindow().Size/10;

            AssetLoader texload = AssetLoader.GetInstance();

            AddActorOfClass<MainMenuSkyBG>(new SFML.System.Vector2f(0.0f, 0.0f));

            AddActorOfClass<MainMenuBackground>(new SFML.System.Vector2f(0.0f, 0.0f));

            logo =  AddActorOfClass<MainMenuLogoWidget>(new SFML.System.Vector2f(
                192 / 2 - texload.GetTexture("logo").Size.X / 2,
                -texload.GetTexture("logo").Size.Y
            ));

            AddActorOfClass<PlayButton>(new SFML.System.Vector2f(windowSize.X/2 - texload.GetTexture("button").Size.X/2,70f));
            AddActorOfClass<QuitButton>(new SFML.System.Vector2f(windowSize.X / 2 - texload.GetTexture("button").Size.X / 2, 87f));

            base.Start();
        }

        public override void Tick(float deltaTime)
        {
            if(PASEventHandler.GetInstance().TryCatchEventOfType<PlayerAttackEvent>() != null)
                sceneActors.Clear();

            base.Tick(deltaTime);
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
