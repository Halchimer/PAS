using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace PAS.Engine
{
    internal class EventArgs
    {
        public EventArgs(RenderWindow window)
        {
            Window = window;
        }

        public RenderWindow Window { get; private set; }
    }

    internal class Button : Actor
    {
        public event EventHandler Clicked;
        
        protected CustomText _text;
        private Vector2f _text_offset = new Vector2f(0.0f,0.0f);

        private static bool _hasClicked = false;
        private bool _hasHovered = false;
        public Button() : base() {
        }

        public void AddText(string text, CustomFont font , Vector2f textOffset)
        {
            _text = new CustomText(text, font);
            _text.Position = actorLocation + textOffset;
            _text_offset = textOffset;
        }

        public Vector2i getRelativeMousePos()
        {
            Vector2i mousePos = Mouse.GetPosition(parentScene.GetGameInstance().GetWindow())/10;
            return new Vector2i(
                (int)(mousePos.X / actorScale.X - actorLocation.X),
                (int)(mousePos.Y / actorScale.Y - actorLocation.Y)
            );
        }
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
        
        public override void Tick()
        {
            OnClickCheckCall(Game.GetInstance().GetWindow());
                
            base.Tick();
        }

        public virtual void OnClick(System.EventArgs eventArgs)
        {
            Clicked?.Invoke(this, eventArgs);
        }

        public virtual void Hover() { }
        public virtual void Unhover() { }
        public override void Draw()
        {
            base.Draw();
            if(_text != null)
                Game.GetInstance().GetWindow().Draw(_text);
        }

        public override void SetLocation(Vector2f location, bool snapSprite = true)
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
