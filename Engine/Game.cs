using SFML.Graphics;
using SFML.System;

namespace PAS.Engine
{

    /// <summary>
    /// Game class is a singleton that manages the game window and scenes.
    /// </summary>
    internal sealed class Game
    {
        /// <summary>
        /// Represents the game window using the SFML RenderWindow.
        /// This window is responsible for all rendering operations
        /// pertaining to the game, including displaying scenes and handling events.
        /// </summary>
        private RenderWindow window; // SFML Window

        /// <summary>
        /// Represents the primary clock used to track time elapsed since the current scene started.
        /// </summary>
        /// <remarks>
        /// This clock is crucial for managing the timing aspect of scenes within the game engine. It ensures that the game scene timings
        /// are consistent by keeping track of the elapsed time since the scene was initiated. This is useful for various game mechanics
        /// that require synchronization over time, such as animations, movement, and spawning events.
        /// </remarks>
        private Clock clock;

        /// <summary>
        /// Delta time clock used to measure the elapsed time between frames.
        /// </summary>
        private Clock deltaTimeClock;

        /// <summary>
        /// The currently active scene in the game.
        /// This variable holds a reference to the scene that is being rendered and updated
        /// during the game loop. It facilitates changing scenes by assigning a new Scene object
        /// to this variable, ensuring that the game's logic and rendering are redirected to the new scene.
        /// </summary>
        private Scene currentScene = null;  // Current active scene

        /// <summary>
        /// Game class is a singleton responsible for managing the game window and scenes.
        /// </summary>
        private Game()
        {
            Rand = new Random();
            
        }

        /// <summary>
        /// Stores the single instance of the Game class, implementing the Singleton pattern.
        /// Ensures only one instance of the Game is created, preventing multiple game instances during execution.
        /// </summary>
        private static Game _instance;

        /// <summary>
        /// Provides access to a <see cref="Random"/> instance used for generating random numbers within the game.
        /// This property ensures that all random number generation within the game uses a consistent instance,
        /// guaranteeing uniformity and reducing the likelihood of unintended behavior due to multiple random number generators.
        /// </summary>
        public Random Rand { get; private set; }

        /// <summary>
        /// Gets the elapsed time in seconds since the last frame. This value is
        /// used to ensure that game logic and rendering are frame rate independent.
        /// </summary>
        public float DeltaTime { get; private set; }

        public static Game GetInstance() // Returns the only instance of the class, and if there is no, instanciate it
        {
            if (_instance == null)
                _instance = new Game();
            return _instance;
        }

        public ref RenderWindow GetWindow() { return ref window; } // Returns the SFML window

        /// <summary>
        /// Retrieves a reference to the currently active scene in the game.
        /// </summary>
        /// <returns>A reference to the current Scene object.</returns>
        public ref Scene GetScene() { return ref currentScene; } // Returns the currently running scene.

        /// <summary>
        /// Gets the elapsed time in seconds since the current scene started.
        /// </summary>
        /// <returns>
        /// The elapsed time in seconds as a float.
        /// </returns>
        public float GetTime() // Get ellapsed time since scene started
        {
            return clock.ElapsedTime.AsSeconds();
        }

        /// <summary>
        /// Changes the current active scene to a new scene.
        /// </summary>
        /// <param name="scene">The new scene to switch to. If null, the current scene remains unchanged.</param>
        public void SetScene(Scene scene) // Changes the scene 
        {
            if (scene != null) // If a scene is active, execute its end script before changing scene.
                scene.End();

            currentScene = scene; 

            clock.Restart();
            currentScene.Start();
            currentScene.RunActorStart();
        }

        /// <summary>
        /// Initializes the SFML window for the game.
        /// </summary>
        /// <param name="width">The width of the window in pixels.</param>
        /// <param name="height">The height of the window in pixels.</param>
        /// <param name="name">The title of the window.</param>
        /// <param name="fullscreen">Determines if the window should be in fullscreen mode.</param>
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
    
            //window.Closed += new EventHandler(OnClose);
        }

        /// <summary>
        /// Starts the game by initializing the main game loop.
        /// </summary>
        public void StartGame() 
        {
            MainLoop();
        }

        /// <summary>
        /// Main game loop that handles event polling, scene updates, and rendering.
        /// </summary>
        /// <remarks>
        /// This method continuously runs as long as the SFML window is open. It polls
        /// for events, updates the delta time, executes the tick functions for the
        /// current scene and actors, clears the window, renders the scene, and displays
        /// the content on the window.
        /// </remarks>
        private void MainLoop() // Main game loop that polls event, executes scene update func and renders the screen.
        {
            while(window.IsOpen)
            {
                window.DispatchEvents();
                
                DeltaTime = deltaTimeClock.Restart().AsSeconds();
                currentScene.RunActorTicks();
                currentScene.Tick();
                
                window.Clear();
                currentScene.Render();
                window.Display();
                
                
            }
        }

        /// <summary>
        /// Handles the event triggered when the window is closed, ensuring
        /// proper cleanup and termination of the game.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        public void OnClose(object sender, EventArgs e)
        {
            Quit();
        }

        /// Shuts down the current scene and closes the game window.
        /// This method should be called to safely exit the game. It will execute
        /// the `End` method of the current active scene to perform any necessary
        /// cleanup and then close the SFML window associated with the game.
        /// Ensure that any critical operations are finalized before calling this method.
        public void Quit()
        {
            currentScene.End();

            window.Close();
        }
    }
}
