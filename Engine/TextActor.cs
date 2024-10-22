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
        public TextActor() : base()
        {
        }

        public void SetText(CustomText text)
        {
            sprite = text;
        }
    }
}