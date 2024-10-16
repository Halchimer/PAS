using PAS.Engine;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAS.Content.Widgets
{
    internal class ConfirmCharacterEvent : Engine.Event { }
    internal class StartButton : Engine.Button
    {
        public StartButton() 
        {
            sprite = new SFML.Graphics.Sprite(AssetLoader.GetInstance().GetTexture("button"));
            AddText("CONFIRM", AssetLoader.GetInstance().GetFont("main"), 9, new SFML.System.Vector2f(12f, 0f));
            _text.Color = new Color(50, 50, 50);
        }
        public override void OnClick(RenderWindow window)
        {

            PASEventHandler.GetInstance().TriggerEvent(new ConfirmCharacterEvent());
            base.OnClick(window);
        }
    }
}
