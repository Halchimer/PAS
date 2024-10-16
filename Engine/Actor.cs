using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PAS.Engine
{
    internal class Actor
    {
        protected Scene parentScene;

        protected Sprite sprite;
        protected Vector2f actorLocation;
        protected Vector2f actorScale;

        public Actor() { }

        public virtual void Init(Vector2f location, Scene scene = null) {
            sprite.Position = location;
            actorLocation = location;

            if(scene != null)
                parentScene = scene;
        }

        public virtual void SetLocation(Vector2f location)
        {
            Vector2f flooredLocation = new Vector2f((float)Math.Floor(location.X), (float)Math.Floor(location.Y));

            sprite.Position = flooredLocation;
            actorLocation = flooredLocation;
        }
        public virtual void SetScale(Vector2f scale)
        {
            sprite.Scale = scale;
            actorScale = scale;
        }
        public virtual void SetScale(float scale)
        {
            SetScale(new Vector2f(scale, scale));
        }
        public Vector2f GetLocation() { return actorLocation; }

        public Scene GetScene() { return parentScene; }

        public virtual void Start() { }
        public virtual void Tick() { }
        public virtual void Draw()
        {
            if (parentScene != null)
                parentScene.GetGameInstance().GetWindow().Draw(sprite);
        }

        public virtual void Destroy()
        {
            if (parentScene == null)
                return;
            
            sprite.Dispose();
            parentScene.DeleteActor(this);
        }
    }
}
