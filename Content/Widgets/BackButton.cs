using PAS.Engine;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAS.Content.Widgets
{
    internal class BackButton : Engine.Button
    {
        public BackButton() 
        {
            sprite = new SFML.Graphics.Sprite(AssetLoader.GetInstance().GetTexture("button"));
            AddText("BACK", AssetLoader.GetInstance().GetFont("main"), 9, new SFML.System.Vector2f(18f, 0f));
            _text.Color = new Color(50, 50, 50);
        }
        public override void OnClick(RenderWindow window)
        {

            Game.GetInstance().SetScene(parentScene.previousScene);
            base.OnClick(window);
        }
    }
}
