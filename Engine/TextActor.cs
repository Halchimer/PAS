using SFML.Graphics;
using SFML.System;

namespace PAS.Engine
{
    /// <summary>
    /// Represents an actor in a scene that can display text.
    /// </summary>
    internal class TextActor: Actor
    {
        /// Represents a text actor in the game, which utilizes a custom font and renders text as a sprite.
        public TextActor(string text, CustomFont font) : base()
        {
            sprite = new CustomText(text, font);
        }

        /// <summary>
        /// The TextActor class represents a text drawable actor in the game engine.
        /// It extends the base Actor class and allows setting and updating the displayed text.
        /// </summary>
        public TextActor() : base()
        {
        }

        /// <summary>
        /// Sets the text for the TextActor object.
        /// </summary>
        /// <param name="text">The text to be displayed by the TextActor.</param>
        public void SetText(CustomText text)
        {
            sprite = text;
        }
    }
}