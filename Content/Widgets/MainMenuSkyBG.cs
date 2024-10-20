using PAS.Engine;
using SFML.Graphics;

namespace PAS.Content.Widgets
{
    internal class MainMenuSkyBG : Actor
    {
        public MainMenuSkyBG(): base()
        {

            sprite = new SFML.Graphics.Sprite(AssetLoader.GetInstance().GetTexture("sky_bg"));
            
            sprite.Texture.Repeated = true;
        }

        protected float SPEED = 0.5f;

        private float _skyOffset = 0;

        public override void Tick()
        {
            float deltaTime = Game.GetInstance().DeltaTime;
            _skyOffset-=SPEED * deltaTime;
            sprite.TextureRect = new IntRect((int)Math.Floor(_skyOffset), 0, 192, 108);

            base.Tick();
        }
    }
}
