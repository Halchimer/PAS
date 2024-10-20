using PAS.Engine;
using SFML.Graphics;
using PAS.Content.Scenes;
using SFML.System;
using SFML.Window;

namespace PAS.Content.Widgets.ClassSelection
{
    internal abstract class ClassFrame : Engine.Actor
    {
        public Vector2f defaultCharacterPosition;

        public virtual void ConfirmCharacter() { }
    }
    internal class ClassFrame<T> : ClassFrame where T : Character, new() 
    {
        Sprite characterSprite;
        CustomText characterNameText;

        public ClassFrame() : base() 
        { 
            sprite = new Sprite(AssetLoader.GetInstance().GetTexture("class_frame"));
            
            T temp_character = new T();
            characterSprite = temp_character.GetSprite();
            if(characterSprite != null)
                characterSprite.TextureRect = new IntRect(0, 0, 32, 32);
            characterNameText = new CustomText(temp_character.Name, AssetLoader.GetInstance().GetFont("mini"));
            characterNameText.Color = Color.White;
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

        public override void ConfirmCharacter()
        {
            Game.GetInstance().SetScene(new CombatScene<T>(parentScene));
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
                characterNameText.Position = (snapSprite ? flooredLocation : location) + new Vector2f(
                    sprite.Texture.Size.X/2 - characterNameText.Texture.Size.X/2,
                    57
                );

        }

    }
}
