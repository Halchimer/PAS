using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Audio;
using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace PAS.Engine
{
    internal class Button : Actor
    {
        protected Text _text;
        private Vector2f _text_offset = new Vector2f(0.0f,0.0f);

        private bool hasClicked = false;
        public Button() : base() {}

        public void AddText(string text, Font font,uint character_size , Vector2f textOffset)
        {
            _text = new Text(text, font);
            _text.Position = actorLocation + textOffset;
            _text.CharacterSize = character_size;
            _text_offset = textOffset;
        }

        public Vector2f getRelativeMousePos()
        {
            Vector2i mousePos = Mouse.GetPosition(parentScene.GetGameInstance().GetWindow())/10;
            return new Vector2f(
                mousePos.X/actorScale.X - actorLocation.X,
                mousePos.Y / actorScale.Y - actorLocation.Y
            );
        }
        public void OnClickCheckCall(RenderWindow window)
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                if (sprite.TextureRect.Contains(getRelativeMousePos()))
                {
                    if(!hasClicked)
                        OnClick(window);

                    hasClicked = true;
                }

            }
            else
                hasClicked = false;
        }

        public override void Tick()
        {
            OnClickCheckCall(parentScene.GetGameInstance().GetWindow());
                
            base.Tick();
        }

        public virtual void OnClick(RenderWindow window) { }

        public override void Draw()
        {
            base.Draw();
            if(_text != null)
                parentScene.GetGameInstance().GetWindow().Draw(_text);
        }

        public override void SetLocation(Vector2f location)
        {   
            if(_text != null)
                _text.Position = location + _text_offset;
            base.SetLocation(location);
        }

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
