using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PAS.Engine
{
    internal struct _MovementDataStruct
    {
        public Vector2f target;
        public float time;
        public bool spritePixelSnap;

        public float ellapsedTime;
    }

    internal class Actor
    {
        protected Scene parentScene;

        protected Sprite sprite;
        protected Vector2f actorLocation;
        protected Vector2f actorScale;

        private _MovementDataStruct _movementStruct;
        private bool moving;
        public Actor() {
            actorScale = new Vector2f(1f, 1f);
        }

        public virtual void Init(Vector2f location, Scene scene = null) {
            SetLocation(location);

            if(scene != null)
                parentScene = scene;
        }

        public virtual void SetLocation(Vector2f location, bool snapSprite = true)
        {
            Vector2f flooredLocation = new Vector2f((float)Math.Floor(location.X), (float)Math.Floor(location.Y));

            if(sprite!= null)
                sprite.Position = snapSprite?flooredLocation:location;

            actorLocation = location;
        }
        public virtual void SetScale(Vector2f scale)
        {
            if (sprite != null)
                sprite.Scale = scale;
            actorScale = scale;
        }
        public virtual void SetScale(float scale)
        {
            SetScale(new Vector2f(scale, scale));
        }

        public virtual void MoveToLocationOverTime(Vector2f location, float time, bool spritePixelSnap = true)
        {
            moving = true;
            _movementStruct = new _MovementDataStruct();
            _movementStruct.target = location;
            _movementStruct.time = time;
            _movementStruct.ellapsedTime = 0;
            _movementStruct.spritePixelSnap = spritePixelSnap;
        }

        public Vector2f GetLocation() { return actorLocation; }

        public Sprite GetSprite() { return sprite; }

        public Scene GetScene() { return parentScene; }

        public virtual void Start() { }
        public virtual void Tick() 
        {
            if(moving)
            {
                _movementStruct.ellapsedTime += Game.GetInstance().DeltaTime;
                if (_movementStruct.ellapsedTime > _movementStruct.time)
                {
                    _movementStruct.ellapsedTime = _movementStruct.time;
                    moving = false;
                }
                SetLocation(
                    actorLocation + (_movementStruct.target-actorLocation)*(_movementStruct.ellapsedTime/_movementStruct.time),
                    _movementStruct.spritePixelSnap
                );
            }
        }
        public virtual void Draw()
        {
            if (sprite != null)
                Game.GetInstance().GetWindow().Draw(sprite);
        }
    }
}
