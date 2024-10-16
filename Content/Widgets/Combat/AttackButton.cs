using PAS.Engine;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAS.Content.Widgets.Combat
{
    internal class PlayerAttackEvent : Engine.Event
    {
        public PlayerAttackEvent() : base() { }
    }
    internal class AttackButton : Engine.Button
    {
        public AttackButton() : base()
        { 
            sprite = new SFML.Graphics.Sprite(AssetLoader.GetInstance().GetTexture("button"));

            AddText("ATTACK", AssetLoader.GetInstance().GetFont("upheavtt"), 9, new SFML.System.Vector2f(12f, 0f));
        }

        public override void OnClick(RenderWindow window)
        {
            PASEventHandler.GetInstance().TriggerEvent(new PlayerAttackEvent());

            base.OnClick(window);
        }
    }
}
