using PAS.Engine;
using SFML.Graphics;

namespace PAS.Content.Widgets.Combat
{
    internal class VictoryTextWidget : Actor
    {
        public VictoryTextWidget() : base()
        {
            sprite = new Sprite(AssetLoader.GetInstance().GetTexture("victory"));
        }
    }
}