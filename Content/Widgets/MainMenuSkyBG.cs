using PAS.Engine;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAS.Content.Widgets
{
    internal class MainMenuSkyBG : Actor
    {
        public MainMenuSkyBG(): base() 
        {
            sprite = new SFML.Graphics.Sprite(AssetLoader.GetInstance().GetTexture("sky_bg"));
        }

        private float defaultXPos;

        const float SPEED = 100;

        public override void Start()
        {
            defaultXPos = actorLocation.X;

            base.Start();
        }

        public override void Tick()
        {
            float deltaTime = Game.GetInstance().DeltaTime;

            if (actorLocation.X < defaultXPos + sprite.Texture.Size.X)
                SetLocation(actorLocation + new SFML.System.Vector2f(1.0f, 0.0f) * SPEED * deltaTime);
            else
                SetLocation(new Vector2f(defaultXPos, actorLocation.Y));

            base.Tick();
        }
    }
}
