using PAS.Content.Scenes;
using PAS.Engine;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAS.Content.Widgets
{
    internal class TestEvent : Engine.Event
    {

    }

    internal class PlayButton : Engine.Button
    {
        public PlayButton() : base() 
        {
            sprite = new SFML.Graphics.Sprite(AssetLoader.GetInstance().GetTexture("button"));
            AddText("PLAY", AssetLoader.GetInstance().GetFont("main"), 9, new SFML.System.Vector2f(18.0f, 0f));
            _text.Color = new Color(50, 50, 50);
        }

        public override void Tick()
        {
            base.Tick();
        }

        public override void OnClick(RenderWindow window)
        {
            Game.GetInstance().SetScene(new Scenes.ClassSelectionScene());

            base.OnClick(window);
        }
    }
}
