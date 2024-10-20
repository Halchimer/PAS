using PAS.Content.Widgets.ClassSelection;
using PAS.Content.Characters;
using PAS.Engine;
using PAS.Content.Widgets;
using SFML.System;
using SFML.Window;
using EventArgs = System.EventArgs;

namespace PAS.Content.Scenes
{
    internal class ClassSelectionScene : Engine.Scene
    {
        private ClassSelector classSelector;
        private Button leftSelectButton;
        private Button rightSelectButton;
        private Button startButton;
        public ClassSelectionScene(Scene prevScene = null) : base(prevScene) {

            classSelector = AddActorOfClass<ClassSelector>(new Vector2f(0f,0f));
            
            classSelector.AddPlayableCharacter<Damager>();
            classSelector.AddPlayableCharacter<Healer>();
            classSelector.AddPlayableCharacter<Tank>();
            classSelector.AddPlayableCharacter<WukongBebere>();
            
            
            leftSelectButton = AddActorOfClass<ClassScrollButtonLeft>(new Vector2f(Game.GetInstance().GetWindow().Size.X/20-38, Game.GetInstance().GetWindow().Size.Y/20 - 16));
            rightSelectButton = AddActorOfClass<ClassScrollButtonRight>(new Vector2f(Game.GetInstance().GetWindow().Size.X / 20 +22, Game.GetInstance().GetWindow().Size.Y / 20 - 16));
            leftSelectButton.Clicked += LeftSelect;
            rightSelectButton.Clicked += RightSelect;

            AddActorOfClass<CancelButton>(new Vector2f(0, 0));
            startButton = AddActorOfClass<StartButton>(new Vector2f(95f, 0));
            startButton.Clicked += Confirm;
        }

        public void LeftSelect(object sender, EventArgs e)
        {
            classSelector.Scroll(-1);
        }

        public void RightSelect(object sender, EventArgs e)
        {
            classSelector.Scroll(1);
        }

        public void Confirm(object sender, EventArgs e)
        {
            classSelector.ConfirmCharacter();
        }
    }
}
