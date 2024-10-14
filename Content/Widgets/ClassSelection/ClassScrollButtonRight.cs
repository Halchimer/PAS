﻿using PAS.Engine;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAS.Content.Widgets.ClassSelection
{
    internal class ClassScrollRightEvent : Engine.Event {}

    internal class ClassScrollButtonRight : Engine.Button
    {
        public ClassScrollButtonRight() : base()
        {
            sprite = new SFML.Graphics.Sprite(AssetLoader.GetInstance().GetTexture("selector_button"));
        }

        public override void OnClick(RenderWindow window)
        {
            PASEventHandler.GetInstance().TriggerEvent(new ClassScrollRightEvent());
            base.OnClick(window);
        }
    }
}
