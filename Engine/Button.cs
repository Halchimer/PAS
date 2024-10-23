using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace PAS.Engine
{
    /// <summary>
    /// Represents a clickable button within the UI framework. The Button class inherits from the Actor class and provides
    /// additional functionality such as handling click events, adding text, and managing hover states.
    /// </summary>
    internal class Button : Actor
    {
        /// <summary>
        /// An event that is triggered when the button is clicked.
        /// </summary>
        public event EventHandler Clicked;

        /// <summary>
        /// Represents the text associated with the Button.
        /// This text is displayed on the button and can be customized using a font and position offset.
        /// </summary>
        protected CustomText _text;

        /// <summary>
        /// The offset position of the text relative to the button's position.
        /// Determines where the text will be drawn on the button.
        /// </summary>
        private Vector2f _text_offset = new Vector2f(0.0f,0.0f);

        /// <summary>
        /// Indicates whether the button has been clicked.
        /// This flag helps ensure the button click is registered only once per mouse press.
        /// </summary>
        private static bool _hasClicked = false;

        /// <summary>
        /// Indicates whether the button is currently being hovered over by the mouse cursor.
        /// </summary>
        /// <remarks>
        /// When the mouse cursor enters the button's area, this flag is set to true.
        /// When the mouse cursor exits the button's area, this flag is set to false.
        /// </remarks>
        private bool _hasHovered = false;

        /// <summary>
        /// Represents a UI button actor that can handle various user interactions such as clicks and hovering.
        /// </summary>
        public Button() : base() {
        }

        /// <summary>
        /// Adds text to the button with specified font and offset.
        /// </summary>
        /// <param name="text">The text to display on the button.</param>
        /// <param name="font">The custom font used for displaying the text.</param>
        /// <param name="textOffset">The offset to position the text relative to the button's location.</param>
        public void AddText(string text, CustomFont font , Vector2f textOffset)
        {
            _text = new CustomText(text, font);
            _text.Position = actorLocation + textOffset;
            _text_offset = textOffset;
        }

        /// <summary>
        /// Calculates and returns the mouse position relative to the button's location and scale.
        /// </summary>
        /// <returns>A Vector2i representing the mouse position relative to the button's location and scale.</returns>
        public Vector2i getRelativeMousePos()
        {
            Vector2i mousePos = Mouse.GetPosition(parentScene.GetGameInstance().GetWindow())/10;
            return new Vector2i(
                (int)(mousePos.X / actorScale.X - actorLocation.X),
                (int)(mousePos.Y / actorScale.Y - actorLocation.Y)
            );
        }

        /// <summary>
        /// Checks if the button was clicked or hovered over based on the current mouse position and mouse button state.
        /// If the mouse is over the button, it triggers hover or click events.
        /// </summary>
        /// <param name="window">The active window to check the mouse position relative to.</param>
        public void OnClickCheckCall(RenderWindow window)
        {
            Vector2i mousePos = getRelativeMousePos();
            if (sprite.TextureRect.Contains(mousePos.X, mousePos.Y))
            {
                if(!_hasHovered)
                {
                    Hover();
                    _hasHovered = true;
                }

                if (Mouse.IsButtonPressed(Mouse.Button.Left))
                {
                    if (!_hasClicked)
                        OnClick(System.EventArgs.Empty);

                    _hasClicked = true;
                }
                else
                    _hasClicked = false;
            }
            else
            {
                if(_hasHovered)
                {
                    Unhover();
                    _hasHovered = false;
                }
            }
        }

        /// <summary>
        /// Updates the state of the Button object every frame.
        /// This method checks for click events and invokes the base class's Tick method.
        /// </summary>
        public override void Tick()
        {
            OnClickCheckCall(Game.GetInstance().GetWindow());
                
            base.Tick();
        }

        /// Called when the button is clicked.
        /// This method triggers the click event for this button, invoking all registered
        /// event handlers. It can be overridden in derived classes to provide custom
        /// behavior when the button is clicked.
        /// <param name="eventArgs">
        /// An instance of <see cref="System.EventArgs"/> that contains the event data.
        /// </param>
        public virtual void OnClick(System.EventArgs eventArgs)
        {
            Clicked?.Invoke(this, eventArgs);
        }

        /// <summary>
        /// The Hover method is triggered when the mouse cursor hovers over the button.
        /// This method can be overridden in derived classes to implement specific hover behavior.
        /// </summary>
        public virtual void Hover() { }

        /// <summary>
        /// Reverse the hover state of the button to its original state.
        /// This method is called when the mouse cursor exits the button's clickable area.
        /// </summary>
        public virtual void Unhover() { }

        /// <summary>
        /// Draws the button on the screen. If the button has associated text, it draws the text as well.
        /// This method overrides the Draw method in the Actor class, ensuring that any button-specific
        /// drawing operations are performed appropriately.
        /// </summary>
        public override void Draw()
        {
            base.Draw();
            if(_text != null)
                Game.GetInstance().GetWindow().Draw(_text);
        }

        /// Retrieves the current text associated with the button.
        /// This method returns the CustomText object, which contains
        /// the text string and its associated properties.
        /// <return>The CustomText object representing the button's text.</return>
        public CustomText GetText()
        {
            return _text;
        }

        /// <summary>
        /// Sets the location of the button.
        /// </summary>
        /// <param name="location">The new location to set.</param>
        /// <param name="snapSprite">Specifies whether the sprite should be snapped to the grid.</param>
        public override void SetLocation(Vector2f location, bool snapSprite = true)
        {   
            if(_text != null)
                _text.Position = location + _text_offset;
            base.SetLocation(location);
        }

        /// <summary>
        /// Sets the scale of the Button and adjusts the position and scale of any attached text.
        /// </summary>
        /// <param name="scale">The new scale to apply to the Button and its text.</param>
        public override void SetScale(Vector2f scale)
        {
            if (_text != null)
            {
                _text.Scale = scale;
                _text.Position = actorLocation + new Vector2f(_text_offset.X*scale.X, _text_offset.Y * scale.Y);
            }
            base.SetScale(scale);
        }

    }
    
}
