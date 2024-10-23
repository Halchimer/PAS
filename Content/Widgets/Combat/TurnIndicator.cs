using PAS.Engine;
using SFML.Graphics;

namespace PAS.Content.Widgets.Combat;

internal class TurnIndicator : Engine.Actor
{
    public TurnIndicator()
    {
        sprite = new Sprite(AssetLoader.GetInstance().GetTexture("turn_indicator"));
    }
}