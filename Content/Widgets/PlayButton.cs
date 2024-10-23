using PAS.Engine;
using SFML.Graphics;
using SFML.System;

namespace PAS.Content.Widgets
{
    /// <summary>
    /// Represents a test event within the PAS content widgets namespace.
    /// Inherits from the base Engine.Event class.
    /// </summary>
    internal class TestEvent : Engine.Event
    {

    }

    /// <summary>
    /// Represents a button widget that initiates a play action in the game.
    /// Derived from the Engine.Button class, this button displays the text "PLAY"
    /// and triggers a specific event upon being clicked.
    /// </summary>
    internal class PlayButton : Engine.Button
    {
        /// <summary>
        /// Represents a PlayButton which inherits from the Engine Button class.
        /// It initializes the button sprite and adds a "PLAY" text label.
        /// </summary>
        public PlayButton() : base() 
        {
            sprite = new SFML.Graphics.Sprite(AssetLoader.GetInstance().GetTexture("button"));
            AddText("PLAY", AssetLoader.GetInstance().GetFont("main"), new Vector2f(18.0f, 4f));
            _text.Color = new Color(50, 50, 50);
        }
        
    }
}
