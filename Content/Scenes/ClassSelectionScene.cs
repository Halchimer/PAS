using PAS.Content.Widgets.ClassSelection;
using PAS.Content.Characters;
using PAS.Engine;
using PAS.Content.Widgets;
using SFML.System;
using EventArgs = System.EventArgs;

namespace PAS.Content.Scenes
{
    /// <summary>
    /// Represents the scene where players can select their character class.
    /// </summary>
    internal class ClassSelectionScene : Engine.Scene
    {
        /// <summary>
        /// Handles the selection of different character classes for the player.
        /// Manages and displays the playable character options and allows navigation
        /// through the available choices.
        /// </summary>
        private ClassSelector classSelector;

        /// <summary>
        /// Represents the button used to scroll left in the class selection scene.
        /// Handles user interactions to navigate through different character classes.
        /// </summary>
        private Button leftSelectButton;

        /// <summary>
        /// Represents the right selection button used to navigate through available classes in the ClassSelectionScene.
        /// This button is an instance of the <see cref="ClassScrollButtonRight"/> class and is responsible for
        /// handling the action when the user clicks to scroll through the character classes to the right.
        /// </summary>
        private Button rightSelectButton;

        /// <summary>
        /// Represents the button used to start the game in the Class Selection Scene.
        /// </summary>
        private Button startButton;

        /// <summary>
        /// Instance of TextActor used to display the ability description of the currently selected class.
        /// </summary>
        private TextActor _abilityDescriptionText;

        /// Represents the scene where the player can select their character class.
        /// Inherits from the Engine.Scene base class.
        public ClassSelectionScene(Scene prevScene = null) : base(prevScene)
        {

            AddActorOfClass<MainMenuBackground>(new Vector2f(0, -108));

            classSelector = AddActorOfClass<ClassSelector>(new Vector2f(0f,0f));
            
            classSelector.AddPlayableCharacter<Damager>();
            classSelector.AddPlayableCharacter<Healer>();
            classSelector.AddPlayableCharacter<Tank>();
            classSelector.AddPlayableCharacter<WukongBebere>();
            
            
            leftSelectButton = AddActorOfClass<ClassScrollButtonLeft>(new Vector2f(Game.GetInstance().GetWindow().Size.X/20-38, Game.GetInstance().GetWindow().Size.Y/20 - 16));
            rightSelectButton = AddActorOfClass<ClassScrollButtonRight>(new Vector2f(Game.GetInstance().GetWindow().Size.X / 20 +22, Game.GetInstance().GetWindow().Size.Y / 20 - 16));
            leftSelectButton.Clicked += LeftSelect;
            rightSelectButton.Clicked += RightSelect;

            AddActorOfClass<CancelButton>(new Vector2f(1, 1));
            startButton = AddActorOfClass<StartButton>(new Vector2f(132f, 1));
            startButton.Clicked += Confirm;
            
            _abilityDescriptionText = AddActorOfClass<TextActor>(new Vector2f(0,0));
        }

        /// <summary>
        /// Initializes the ClassSelectionScene by setting the ability description text and location
        /// of the initially selected class frame. Calls the Start method of the base class.
        /// </summary>
        public override void Start()
        {
            _abilityDescriptionText.SetText(classSelector.classFrames[0].abilityDescription);
            _abilityDescriptionText.SetLocation(new Vector2f(
                1,
                85f
            ));
            base.Start();
        }

        /// <summary>
        /// Handles the event when the left selection button is clicked.
        /// Scrolls the class selector to the left and updates the ability description text accordingly.
        /// </summary>
        /// <param name="sender">The source of the click event.</param>
        /// <param name="e">The event arguments associated with the click event.</param>
        public void LeftSelect(object sender, EventArgs e)
        {
            classSelector.Scroll(-1);
            _abilityDescriptionText.SetText(classSelector.classFrames[classSelector.GetIndex()].abilityDescription);
            _abilityDescriptionText.SetLocation(new Vector2f(
                1,
                85f
            ));
        }

        /// <summary>
        /// Handles the selection of the right character class.
        /// Scrolls the class selector to the next class and updates the ability description text accordingly.
        /// </summary>
        /// <param name="sender">The source of the event, typically the button that was clicked.</param>
        /// <param name="e">Event arguments associated with the click event.</param>
        public void RightSelect(object sender, EventArgs e)
        {
            classSelector.Scroll(1);
            _abilityDescriptionText.SetText(classSelector.classFrames[classSelector.GetIndex()].abilityDescription);
            _abilityDescriptionText.SetLocation(new Vector2f(
                1,
                85f
            ));
        }

        /// Confirms the selection of a character in the ClassSelector.
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        public void Confirm(object sender, EventArgs e)
        {
            classSelector.ConfirmCharacter();
        }
    }
}
