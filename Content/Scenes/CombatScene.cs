using PAS.Content.Characters;
using PAS.Content.Widgets;
using PAS.Content.Widgets.Combat;
using PAS.Engine;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAS.Content.Scenes

{
    internal class CombatScene : Engine.Scene
    {

        Actor IACharacter = new Actor();
        Actor playerCharacter = new Actor();

        


        //pas sur de ça


        public CombatScene() : base()
        {
            Vector2u windowSize = Game.GetInstance().GetWindow().Size / 10;

            AssetLoader texload = AssetLoader.GetInstance();

            AddActorOfClass<MainMenuSkyBG>(new SFML.System.Vector2f(texload.GetTexture("mainmenu_sky_bg").Size.X, 0.0f));
            AddActorOfClass<MainMenuSkyBG>(new SFML.System.Vector2f(0.0f, 0f));

            AddActorOfClass<MainMenuBackground>(new SFML.System.Vector2f(0.0f, 25f));

            Random randChoice = new Random();

            //coords : min 192, max 108

            int choice;
            choice = randChoice.Next(1, 4);

            Console.WriteLine("Choix 1 : " + choice);

            switch (choice)
            {
                case 1:
                    {
                        //Actor playerCharacter = AddActorOfClass<Damager>(new SFML.System.Vector2f(24f, 36f));
                        playerCharacter = AddActorOfClass<Damager>(new SFML.System.Vector2f(24f, 36f));
                    }
                    break;
                case 2:
                    {
                        //Actor playerCharacter = AddActorOfClass<Healer>(new SFML.System.Vector2f(24f, 36f));
                        playerCharacter = AddActorOfClass<Healer>(new SFML.System.Vector2f(24f, 36f));
                    }
                    break;
                case 3:
                    {
                        //Actor playerCharacter = AddActorOfClass<Tank>(new SFML.System.Vector2f(24f, 36f));s
                        playerCharacter = AddActorOfClass<Tank>(new SFML.System.Vector2f(24f, 36f));
                    }
                    break;
            }
            //Console.WriteLine("pos char player = " + playerCharacter.GetLocation);
            choice = randChoice.Next(1, 4);

            Console.WriteLine("Choix 2 : " + choice);

            switch (choice)
            {
                case 1:
                    {
                        IACharacter = AddActorOfClass<Damager>(new SFML.System.Vector2f(168f, 36f));
                    }
                    break;
                case 2:
                    {
                        IACharacter = AddActorOfClass<Healer>(new SFML.System.Vector2f(168f, 36f));
                    }
                    break;
                case 3:
                    {
                        IACharacter = AddActorOfClass<Tank>(new SFML.System.Vector2f(168f, 36f));
                    }
                    break;
            }
            IACharacter.SetScale(new Vector2f(-1f, 1f));
            //Actor IACharacter = AddActorOfClass<Healer>(new SFML.System.Vector2f(168f, 36f));
            //Actor IACharacter = AddActorOfClass<Tank>(new SFML.System.Vector2f(168f, 36f));
            //IACharacter.SetScale(new Vector2f(-1f, 1f));
        }

        public override void Start()
        {
            base.Start();
        }

        public override void Tick(float deltaTime)
        {
            // Code here 
         

            base.Tick(deltaTime);

            

            //Console.WriteLine(IACharacter.GetLocation());
            //Console.WriteLine(playerCharacter.GetLocation());

            if (PASEventHandler.GetInstance().TryCatchEventOfType<PlayerAttackEvent> != null)
            {
                //ATTAQUER

            }
            /*else if (PASEventHandler.GetInstance().TryCatchEventOfType<PlayerDefenseEvent> != null)
            {
                //DEFENSE
            }
            else if (PASEventHandler.GetInstance().TryCatchEventOfType<PlayerSpecialAttackEvent> != null)
            {
                //ATTAQUE SPECIALE
            }*/

        }
    }
}
