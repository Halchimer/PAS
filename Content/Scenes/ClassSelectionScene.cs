using PAS.Content.Widgets.ClassSelection;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAS.Content.Scenes
{
    internal class ClassSelectionScene : Engine.Scene
    {
        public ClassSelectionScene() : base() {
            AddActorOfClass<SelectionMenuBG>(new Vector2f(0f,0f));
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
