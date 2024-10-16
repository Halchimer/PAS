using PAS.Content.Widgets.ClassSelection;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAS.Content.Characters;
using PAS.Engine;
using PAS.Content.Widgets;

namespace PAS.Content.Scenes
{
    internal class ClassSelectionScene : Engine.Scene
    {
        public ClassSelectionScene(Scene previousScene = null) : base(previousScene) {

            var classSelector = AddActorOfClass<ClassSelector>(new Vector2f(0f,0f));
            
            classSelector.AddPlayableCharacter<Damager>();
            classSelector.AddPlayableCharacter<Healer>();
            classSelector.AddPlayableCharacter<Tank>();
            classSelector.AddPlayableCharacter<WukongBebere>();
            
            
            AddActorOfClass<ClassScrollButtonLeft>(new Vector2f(Game.GetInstance().GetWindow().Size.X/20-38, Game.GetInstance().GetWindow().Size.Y/20 - 8));
            AddActorOfClass<ClassScrollButtonRight>(new Vector2f(Game.GetInstance().GetWindow().Size.X / 20 +22, Game.GetInstance().GetWindow().Size.Y / 20 - 8));

            AddActorOfClass<CancelButton>(new Vector2f(0,0));
            AddActorOfClass<StartButton>(new Vector2f(Game.GetInstance().GetWindow().Size.X / 20 - 8, 0));
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
