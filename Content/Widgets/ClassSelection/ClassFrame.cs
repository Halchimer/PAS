using PAS.Engine;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace PAS.Content.Widgets.ClassSelection
{
    internal abstract class ClassFrame : Engine.Actor
    {
        public Vector2f defaultCharacterPosition;
    }
    internal class ClassFrame<T> : ClassFrame where T : Character, new() 
    {
        Sprite characterSprite;
        Text characterNameText;

        public ClassFrame() : base() 
        { 
            sprite = new Sprite(AssetLoader.GetInstance().GetTexture("class_frame"));

            T temp_character = new T();
            characterSprite = temp_character.GetSprite();
            characterNameText = new Text(temp_character.Name, AssetLoader.GetInstance().GetFont("pico"));
            characterNameText.CharacterSize = 8;
        }

        public override void Init(Vector2f location, Scene scene = null)
        {
            defaultCharacterPosition = location;
            base.Init(location, scene);
        }

        public override void Draw()
        {
            base.Draw();
            if(characterSprite != null)
                Game.GetInstance().GetWindow().Draw(characterSprite);
            if(characterNameText != null)
                Game.GetInstance().GetWindow().Draw(characterNameText);
        }

        public override void SetLocation(Vector2f location, bool snapSprite = true)
        {
            Vector2f flooredLocation = new Vector2f((float)Math.Floor(location.X), (float)Math.Floor(location.Y));

            if (sprite != null)
                sprite.Position = snapSprite ? flooredLocation : location;

            actorLocation = location;

            if (characterSprite != null)
                characterSprite.Position = (snapSprite ? flooredLocation : location) + new Vector2f(4,19);
            if (characterNameText != null)
                characterNameText.Position = (snapSprite ? flooredLocation : location) + new Vector2f(4,53);

        }
    }
}
