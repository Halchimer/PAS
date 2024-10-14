using PAS.Engine;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAS.Content.Widgets.ClassSelection
{
    internal class ClassFrame<T> : Engine.Actor where T : Character, new()
    {
        Sprite characterSprite;
        Text characterNameText;
        public ClassFrame() : base() 
        { 
            sprite = new Sprite(AssetLoader.GetInstance().GetTexture("class_frame"));

            T temp_character = new T();
            characterSprite = temp_character.GetSprite();
            characterNameText = new Text(temp_character.Name, AssetLoader.GetInstance().GetFont("main"));
        }
    }
}
