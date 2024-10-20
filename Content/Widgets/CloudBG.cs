using PAS.Engine;
using SFML.Graphics;

namespace PAS.Content.Widgets;

internal class CloudBG : MainMenuSkyBG
{
    public CloudBG() : base()
    {
        sprite.Texture = AssetLoader.GetInstance().GetTexture("cloud_layer");
        sprite.Texture.Repeated = true;
        SPEED = 3;
    }
}