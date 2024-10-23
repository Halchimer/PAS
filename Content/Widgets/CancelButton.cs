using PAS.Engine;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Window;

namespace PAS.Content.Widgets
{
    /// <summary>
    /// Represents a cancel button used in the game UI.
    /// Inherits from the Engine.Button class and is responsible for
    /// navigating back to the previous scene when clicked.
    /// </summary>
    internal class CancelButton : Engine.Button
    {
        /// <summary>
        /// Initializes a new instance of the CancelButton class.
        /// </summary>
        public CancelButton() : base() 
        {
            sprite = new Sprite(AssetLoader.GetInstance().GetTexture("button"));
            AddText("CANCEL", AssetLoader.GetInstance().GetFont("main"), new Vector2f(12f,4));
            _text.Color = new Color(50, 50, 50);
        }

        /// <summary>
        /// Handles the click event for the Cancel button.
        /// If the parent scene exists and has a previous scene,
        /// it sets the game to that previous scene.
        /// </summary>
        /// <param name="eventArgs">Provides data for the event.</param>
        public override void OnClick(EventArgs eventArgs)
        {
            if (parentScene!=null && parentScene.previousScene != null)
                Game.GetInstance().SetScene(parentScene.previousScene);
            base.OnClick(eventArgs);
        }
    }
}
