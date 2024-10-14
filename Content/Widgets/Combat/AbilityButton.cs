using PAS.Engine;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAS.Content.Widgets.Combat
{
    internal class PlayerAbilityEvent : Engine.Event
    {
        public PlayerAbilityEvent() : base() { }
    }
    internal class AbilityButton : Engine.Button
    {
        public AbilityButton() : base()
        { 
            sprite = new SFML.Graphics.Sprite(AssetLoader.GetInstance().GetTexture("button"));

            AddText("ATTACK", AssetLoader.GetInstance().GetFont("upheavtt"), 9, new SFML.System.Vector2f(12f, 0f));
        }

        public override void OnClick(RenderWindow window)
        {
            PASEventHandler.GetInstance().TriggerEvent(new PlayerAbilityEvent());

            base.OnClick(window);
        }
    }
}
