using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using static SFML.Window.Mouse;
using static System.Net.Mime.MediaTypeNames;
using SFML.Window;
using SFML.System;

namespace PAS.Engine
{
    /*
        Game class is a singleton that handles the window and the game scenes.
        A Singleton is a class that allows only one instance of itself.
    */
    internal sealed class Game
    {
        private RenderWindow window; // SFML Window
        private Clock clock;
        private Clock deltaTimeClock;

        private Scene currentScene = null;  // Current active scene
        private Game() {} // The constructor is private, removing the ability to create multiple game instances during execution.

        private static Game _instance; // Stores the only game instance

        public float DeltaTime { get; private set; }

        public static Game GetInstance() // Returns the only instance of the class, and if there is no, instanciate it
        {
            if (_instance == null)
                _instance = new Game();
            return _instance;
        }

        public ref RenderWindow GetWindow() { return ref window; } // Returns the SFML window
        public ref Scene GetScene() { return ref currentScene; } // Returns the currently running scene.

        public float GetTime() // Get ellapsed time since scene started
        {
            return clock.ElapsedTime.AsSeconds();
        }
        public void SetScene(Scene scene) // Changes the scene 
        {
            if (scene != null) // If a scene is active, execute its end script before changing scene.
                scene.End();

            currentScene = scene;

            clock.Restart();
            currentScene.Start();
        }

        public void InitWindow(uint width, uint height, string name, bool fullscreen) // Initializes SFML window
        {
            window = new RenderWindow(
                new SFML.Window.VideoMode(width, height),
                name, 
                fullscreen?SFML.Window.Styles.Fullscreen:SFML.Window.Styles.Default
            );
            window.SetFramerateLimit(60);

            clock = new Clock();
            deltaTimeClock = new Clock();

            View pixelPerfectView = new View(new Vector2f(96, 54),new Vector2f(192, 108));

            window.SetView(pixelPerfectView);

            window.Closed += new EventHandler(OnClose);
        }
        public void StartGame() // Executes scene actors startup function and initialize main loop
        {
            currentScene.RunActorStart();
            MainLoop();
        }
        
        private void MainLoop() // Main game loop that polls event, executes scene update func and renders the screen.
        {
            while(window.IsOpen)
            {
                deltaTimeClock.Restart();

                window.DispatchEvents();

                currentScene.Tick(DeltaTime);
                currentScene.RunActorTicks();

                window.Clear();
                currentScene.Render();
                window.Display();
                
                DeltaTime = deltaTimeClock.ElapsedTime.AsSeconds();
            }
        }
        public void OnClose(object sender, EventArgs e)
        {
            Quit();
        }
        public void Quit()
        {
            currentScene.End();

            window.Close();
        }
    }
}
