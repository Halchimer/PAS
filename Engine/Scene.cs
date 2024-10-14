﻿using SFML.Graphics;
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

        List<Actor> sceneActors;

        public Scene(Game gameInstance) {
            parentGameInstance = gameInstance;

            sceneActors = new List<Actor>();
        }

        public void AddActorOfClass<T>(Vector2f location) where T : Actor, new() 
        {
            sceneActors.Add(new T());
            sceneActors.Last().Init(location, this);
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
