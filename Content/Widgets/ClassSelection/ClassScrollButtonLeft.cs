﻿using PAS.Engine;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace PAS.Content.Widgets.ClassSelection
{
    internal class ClassScrollLeftEvent : Engine.Event {}

    internal class ClassScrollButtonLeft : Engine.Button
    {
        public ClassScrollButtonLeft() : base() 
        {
            sprite = new SFML.Graphics.Sprite(AssetLoader.GetInstance().GetTexture("selector_button"));
            sprite.Origin = new Vector2f(0, -16);
            sprite.TextureRect = new IntRect(0, 16, 16, 16);
            
        }

        public override void OnClick(RenderWindow window)
        {
            PASEventHandler.GetInstance().TriggerEvent(new ClassScrollLeftEvent());
            base.OnClick(window);
        }

        public override void SetLocation(Vector2f location, bool snapSprite = true)
        {
            base.SetLocation(location + new Vector2f(0, -16f), snapSprite);
        }
    }
}
