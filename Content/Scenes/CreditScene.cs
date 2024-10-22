using PAS.Content.Widgets;
using PAS.Engine;
using SFML.Graphics;
using SFML.System;

namespace PAS.Content.Scenes;

internal class CreditScene : Scene
{
    public CreditScene() : base()
    {
        AssetLoader texload = AssetLoader.GetInstance();
        AddActorOfClass<MainMenuSkyBG>(new Vector2f(0.0f, 0.0f));
        AddActorOfClass<CloudBG>(new Vector2f(0.0f, -20f));
        AddActorOfClass<MainMenuBackground>(new Vector2f(0.0f, 0.0f));
        
        AddActorOfClass<MainMenuLogoWidget>(new Vector2f(
            192 / 2 - texload.GetTexture("logo").Size.X / 2,
            -texload.GetTexture("logo").Size.Y
        ));

        AddActorOfClass<MainMenuButton>(new Vector2f(
            192/2 - texload.GetTexture("button").Size.X / 2,
            95f
        ));

        TextActor text = AddActorOfClass<TextActor>(new Vector2f(
            0,
            0
        ));
        text.SetText(new CustomText("  Elgrind : Developper\n      Tom : Developper\n    Yvann : Developper,Musics\nHalchimer : Developper,Pixel Artist\n", texload.GetFont("mini")));
        text.GetSprite().Color = Color.White;
        
        text.SetLocation(new Vector2f(9, 70));
    }
}