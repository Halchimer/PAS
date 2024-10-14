using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAS.Engine
{
    internal class Scene
    {
        private Game parentGameInstance;

        protected List<Actor> sceneActors;

        public Scene() {
            parentGameInstance = Game.GetInstance();

            sceneActors = new List<Actor>();
        }

        public Actor AddActorOfClass<T>(Vector2f location) where T : Actor, new() 
        {
            sceneActors.Add(new T());
            sceneActors.Last().Init(location, this);

            return sceneActors.Last();
        }

        public virtual void Start() {}
        public virtual void Tick(float deltaTime) { }
        public virtual void End() { }


        public void Render()
        {
            foreach(var actor in sceneActors)
            {
                actor.Draw();
            }
        }
        public void RunActorTicks() 
        {
            List<Actor> actors = new List<Actor>(sceneActors);
            foreach (var actor in actors)
            {
                actor.Tick();
            }
        }

        public void RunActorStart() {
            foreach (var actor in sceneActors)
            {
                actor.Start();
            }
        }
        public Game GetGameInstance() { return parentGameInstance; }
    }
}
