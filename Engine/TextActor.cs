using SFML.Graphics;
using SFML.System;

namespace PAS.Engine
{
    internal class TextActor: Actor
    {
        public TextActor(string text, CustomFont font) : base()
        {
            sprite = new CustomText(text, font);
        }
    }
}