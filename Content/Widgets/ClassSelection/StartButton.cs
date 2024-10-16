using PAS.Engine;
using SFML.Graphics;

namespace PAS.Content.Widgets.ClassSelection
{
    internal class ConfirmClassEvent : Engine.Event {}
    
    internal class StartButton : Engine.Button
    {
        public StartButton() : base()
        {
            sprite = new SFML.Graphics.Sprite(AssetLoader.GetInstance().GetTexture("button"));
            AddText("CONFIRM", AssetLoader.GetInstance().GetFont("main"), 9, new SFML.System.Vector2f(9f,0));
            _text.FillColor = new Color(5,5,5);
        }

        public override void OnClick(RenderWindow window)
        {
            PASEventHandler.GetInstance().TriggerEvent(new ConfirmClassEvent());
            base.OnClick(window);
        }
    }
}