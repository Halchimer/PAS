using PAS.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAS.Content.Scenes
{
    internal class CombatScene<C> : Engine.Scene where C:Character, new()
    {
        public CombatScene(Scene prevScene = null) : base(prevScene) 
        { 
            AddActorOfClass<C>(new SFML.System.Vector2f(0, 0));
        }

        public override void Start()
        {
            // Code here

            base.Start();
        }

        public override void Tick(float deltaTime)
        {
            // Code here

            base.Tick(deltaTime);
        }
    }
}
