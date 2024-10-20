using PAS.Engine;
using SFML.Graphics;
using SFML.System;

namespace PAS.Content.Widgets.Combat
{
    internal class AttackButton : Engine.Button
    {
        PlayerActionSendEventArgs buttonEventArgs;
        public AttackButton() : base()
        { 
            sprite = new SFML.Graphics.Sprite(AssetLoader.GetInstance().GetTexture("button"));

            AddText("ATTACK", AssetLoader.GetInstance().GetFont("main"), new Vector2f(12f, 4f));
            _text.Color = new Color(50, 50, 50);
            
            buttonEventArgs = new PlayerActionSendEventArgs();
            buttonEventArgs.Action = CharacterActions.Attack;
        }

        public override void OnClick(System.EventArgs eventArgs)
        {
            base.OnClick(buttonEventArgs);
        }
    }
}
