using PAS.Engine;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventArgs = PAS.Engine.EventArgs;

namespace PAS.Content.Widgets.ClassSelection
{
    internal class ClassScrollRightEvent : Engine.Event {}

    internal class ClassScrollButtonRight : Engine.Button
    {
        public ClassScrollButtonRight() : base()
        {
            sprite = new SFML.Graphics.Sprite(AssetLoader.GetInstance().GetTexture("selector_button"));
            sprite.TextureRect = new IntRect(0, 0, 16, 16);
        }

        public override void OnClick(System.EventArgs eventArgs)
        {
            PASEventHandler.GetInstance().TriggerEvent(new ClassScrollRightEvent());
            base.OnClick(eventArgs);
        }

       
    }
}
