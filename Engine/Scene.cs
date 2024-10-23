using SFML.System;

namespace PAS.Engine
{
    /// <summary>
    /// The Scene class represents a graphical scene in the game, responsible for managing a collection of actors,
    /// handling scene transitions, and rendering actors.
    /// </summary>
    internal class Scene
    {
        /// <summary>
        /// Reference to the current instance of the Game class.
        /// This is used to interact with the overarching game logic
        /// and systems such as window management, scene control, and timing utilities.
        /// </summary>
        private Game parentGameInstance;

        /// <summary>
        /// Stores a reference to the previous scene.
        /// This is useful for navigation purposes, such as returning to the previous scene.
        /// </summary>
        public Scene previousScene;

        /// <summary>
        /// A list of all actors currently active in the scene.
        /// </summary>
        protected List<Actor> sceneActors;

        /// Represents a scene in the game engine, responsible for managing actors and
        /// their behaviors within the scene lifecycle.
        public Scene(Scene prevScene = null) {
            parentGameInstance = Game.GetInstance();

            sceneActors = new List<Actor>();

            previousScene = prevScene;
        }

        /// <summary>
        /// Adds a new actor of the specified class type to the scene.
        /// </summary>
        /// <typeparam name="T">The type of actor to be added, which must inherit from the Actor class and have a parameterless constructor.</typeparam>
        /// <param name="location">The initial location for the new actor in the scene.</param>
        /// <param name="runStartFn">Determines whether the Start() method of the new actor should be invoked immediately after its creation.</param>
        /// <returns>The newly created actor of type T.</returns>
        public T AddActorOfClass<T>(Vector2f location, bool runStartFn = false) where T : Actor, new() 
        {
            sceneActors.Add(new T());
            sceneActors.Last().Init(location, this);
            
            if(runStartFn)
                sceneActors.Last().Start();
            
            return (T)sceneActors.Last();
        }

        /// <summary>
        /// Removes the specified actor from the scene.
        /// </summary>
        /// <param name="actor">The actor to be removed from the scene.</param>
        public void DeleteActor(Actor actor)
        {
            sceneActors.Remove(actor);
        }

        /// Initializes the scene and prepares it for gameplay.
        /// This method is intended to be overridden in derived classes to set up the scene's initial state.
        /// Typically, it will be called when the scene is first started or when it is restarted.
        /// Users should define their own initialization logic within the overridden method.
        /// /
        public virtual void Start() {}

        /// Method to update the state of the scene.
        /// Tick is called once per frame to update the logic and handle actions
        /// required by the scene. It allows the scene to process interactions,
        /// animations, and any other per-frame behavior.
        /// This method should be overridden by subclasses to implement specific logic.
        /// /
        public virtual void Tick() { }

        /// <summary>
        /// The method to be called when the scene is ending.
        /// </summary>
        public virtual void End() { }


        /// <summary>
        /// Renders all actors in the current scene by invoking their Draw method.
        /// </summary>
        /// <remarks>
        /// This method iterates over a list of actors in the scene and calls the Draw method on each actor, ensuring
        /// that all actors are rendered in the order they are present in the list.
        /// </remarks>
        public void Render()
        {
            List<Actor> actorsToRender = new List<Actor>(sceneActors);
            foreach(var actor in actorsToRender)
            {
                actor.Draw();
            }
        }

        /// <summary>
        /// Executes the Tick method for each actor in the scene.
        /// </summary>
        /// <remarks>
        /// This method iterates through the list of actors in the current scene
        /// and invokes their Tick method. This can be used to update the state of
        /// all actors for each frame or game loop iteration.
        /// </remarks>
        public void RunActorTicks() 
        {
            List<Actor> actors = new List<Actor>(sceneActors);
            foreach (var actor in actors)
            {
                actor.Tick();
            }
        }

        /// <summary>
        /// Initiates the Start method for all actors within the scene.
        /// </summary>
        public void RunActorStart() {
            foreach (var actor in sceneActors)
            {
                actor.Start();
            }
        }

        /// <summary>
        /// Retrieves the game instance associated with the current scene.
        /// </summary>
        /// <returns>The current game instance.</returns>
        public Game GetGameInstance() { return parentGameInstance; }
        
        
    }
}
