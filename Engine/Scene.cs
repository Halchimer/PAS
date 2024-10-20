using SFML.System;

namespace PAS.Engine
{
    internal class Scene
    {
        private Game parentGameInstance;
        public Scene previousScene;

        protected List<Actor> sceneActors;

        public Scene(Scene prevScene = null) {
            parentGameInstance = Game.GetInstance();

            sceneActors = new List<Actor>();

            previousScene = prevScene;
        }

        public T AddActorOfClass<T>(Vector2f location, bool runStartFn = false) where T : Actor, new() 
        {
            sceneActors.Add(new T());
            sceneActors.Last().Init(location, this);
            
            if(runStartFn)
                sceneActors.Last().Start();
            
            return (T)sceneActors.Last();
        }

        public void DeleteActor(Actor actor)
        {
            sceneActors.Remove(actor);
        }
        
        public virtual void Start() {}
        public virtual void Tick() { }
        public virtual void End() { }


        public void Render()
        {
            List<Actor> actorsToRender = new List<Actor>(sceneActors);
            foreach(var actor in actorsToRender)
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
