using PAS.Content.Scenes;
using PAS.Engine;
using SFML.Graphics;
using EventArgs = System.EventArgs;

namespace PAS.Content.Widgets;

internal class CreditsButton : Engine.Button
{
    public CreditsButton() : base()
    {
        sprite = new CustomText("CREDITS", AssetLoader.GetInstance().GetFont("mini"));
        sprite.Color = Color.White;
        
    }

    public override void OnClick(EventArgs eventArgs)
    {
        Game.GetInstance().SetScene(new CreditScene());
        base.OnClick(eventArgs);
    }
}