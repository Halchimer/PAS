using PAS.Engine;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace PAS.Content.Widgets.Combat
{
    internal class HealthBar : Actor
    {
        public bool invertHealthBar = false;
        private Vector2f defaultPos;
        public HealthBar() : base()
        {
            sprite = new Sprite(AssetLoader.GetInstance().GetTexture("heart"));
        }

        public override void Init(Vector2f location, Scene scene = null)
        {
            base.Init(location, scene);
            defaultPos = location;
        }
        
        public void SetHeartCount(int count)
        {
            sprite.TextureRect = new IntRect(0, 0, (int)(AssetLoader.GetInstance().GetTexture("heart").Size.X * count), (int)AssetLoader.GetInstance().GetTexture("heart").Size.Y );

            if(invertHealthBar)
                SetLocation(defaultPos - new Vector2f(AssetLoader.GetInstance().GetTexture("heart").Size.X * count, 0));
            
        }
    }
}