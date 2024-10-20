using PAS.Engine;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Window;
using EventArgs = PAS.Engine.EventArgs;

namespace PAS.Content.Widgets
{
    internal class CancelButton : Engine.Button
    {
        public CancelButton() : base() 
        {
            sprite = new Sprite(AssetLoader.GetInstance().GetTexture("button"));
            AddText("CANCEL", AssetLoader.GetInstance().GetFont("main"), new Vector2f(12f,4));
            _text.Color = new Color(50, 50, 50);
        }

        public override void OnClick(System.EventArgs eventArgs)
        {
            if (parentScene!=null && parentScene.previousScene != null)
                Game.GetInstance().SetScene(parentScene.previousScene);
            base.OnClick(eventArgs);
        }
    }
}
