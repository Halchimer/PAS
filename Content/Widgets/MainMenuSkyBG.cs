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
            sprite.Texture.Repeated = true;
        }

        private float defaultXPos;

        const float SPEED = 2;

        public override void Start()
        {
            defaultXPos = actorLocation.X;

            base.Start();
        }

        public override void Tick()
        {
            float deltaTime = Game.GetInstance().DeltaTime;

            sprite.TextureRect = new SFML.Graphics.IntRect((int)Math.Floor(sprite.TextureRect.Left - 15 * deltaTime), 0, 192, 108);

            base.Tick();
        }
    }
}
