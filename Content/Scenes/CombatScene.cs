using PAS.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace PAS.Content.Scenes
{
    internal class CombatScene<T> : Engine.Scene where T : Character, new()
    {
<<<<<<< Updated upstream
        public CombatScene(Engine.Game gameInstance) : base(gameInstance) { }
=======
        public CombatScene(Scene previousScene = null) : base(previousScene)
        {
            AddActorOfClass<T>(new Vector2f(0, 0));
        }
>>>>>>> Stashed changes

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
