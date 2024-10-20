using PAS.Engine;
using SFML.Graphics;

namespace PAS.Content.Widgets.Combat
{
    internal class DeathTextWidget : Actor
    {
        public DeathTextWidget() : base()
        {
            sprite = new Sprite(AssetLoader.GetInstance().GetTexture("youdied"));
        }
    }
}