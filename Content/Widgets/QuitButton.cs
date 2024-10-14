using PAS.Engine;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAS.Content.Widgets
{
    internal class QuitButton : Engine.Button
    {
        public QuitButton() 
        {
            sprite = new SFML.Graphics.Sprite(AssetLoader.GetInstance().GetTexture("button"));
            AddText("QUIT", AssetLoader.GetInstance().GetFont("main"), 9, new SFML.System.Vector2f(18f, 0f));
        }
        public override void OnClick(RenderWindow window)
        {
            
            Game.GetInstance().Quit();
            base.OnClick(window);
        }
    }
}
