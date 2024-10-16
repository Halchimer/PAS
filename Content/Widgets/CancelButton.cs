using PAS.Engine;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAS.Content.Widgets
{
    internal class CancelButton : Engine.Button
    {
        public CancelButton() : base() 
        {
            sprite = new SFML.Graphics.Sprite(AssetLoader.GetInstance().GetTexture("button"));
            AddText("CANCEL", AssetLoader.GetInstance().GetFont("main"), 9, new SFML.System.Vector2f(12f,0));
            _text.FillColor = new Color(5,5,5);
        }

        public override void OnClick(RenderWindow window)
        {
            if (parentScene!=null && parentScene.previousScene != null)
                Game.GetInstance().SetScene(parentScene.previousScene);
            base.OnClick(window);
        }
    }
}
