using PAS.Engine;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAS.Content.Widgets.ClassSelection
{
    internal class ClassScrollLeftEvent : Engine.Event {}

    internal class ClassScrollButtonLeft : Engine.Button
    {
        public ClassScrollButtonLeft() : base() 
        {
            sprite = new SFML.Graphics.Sprite(AssetLoader.GetInstance().GetTexture("selector_button"));
            sprite.Scale = new SFML.System.Vector2f(-1, 1);
        }

        public override void OnClick(RenderWindow window)
        {
            PASEventHandler.GetInstance().TriggerEvent(new ClassScrollLeftEvent());
            base.OnClick(window);
        }
    }
}
