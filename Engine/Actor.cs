using SFML.Graphics;
using SFML.System;


namespace PAS.Engine
{
    /// <summary>
    /// Represents the data structure used for managing movement of an actor.
    /// </summary>
    internal struct _MovementDataStruct
    {
        /// <summary>
        /// The target location to which the actor is moving.
        /// </summary>
        public Vector2f target;
        
        public float time;

        /// <summary>
        /// Indicates if the sprite should align to pixel boundaries to achieve
        /// a sharp and clean visual appearance. This is particularly useful when
        /// working with pixel art or low-resolution sprites.
        /// </summary>
        public bool spritePixelSnap;

        /// <summary>
        /// Represents the time that has elapsed during an ongoing movement operation for an Actor.
        /// This value is used to track the progress of the movement over the specified duration.
        /// </summary>
        public float elapsedTime;
    }

    /// <summary>
    /// Represents an actor in the game engine, which can be moved, scaled, and drawn.
    /// </summary>
    internal class Actor
    {
        /// <summary>
        /// Represents the scene to which this actor currently belongs.
        /// It is used for interactions between the actor and the scene, such as adding
        /// or removing actors, or accessing scene-level data and operations.
        /// </summary>
        protected Scene parentScene;

        /// <summary>
        /// Represents the visual representation of an actor in the form of a sprite.
        /// The sprite can be used for rendering the actor on the screen,
        /// setting the position, scale, and other visual properties.
        /// </summary>
        protected Sprite sprite;

        /// <summary>
        /// Stores the current location of the actor in the game world.
        /// </summary>
        protected Vector2f actorLocation;

        /// <summary>
        /// Represents the scale of the actor in the form of a 2D vector,
        /// determining how the actor is scaled along the X and Y axes.
        /// </summary>
        protected Vector2f actorScale;

        /// <summary>
        /// Represents the movement data required to move an Actor to a specified location over time.
        /// </summary>
        /// <remarks>
        /// This structure includes information about the target location,
        /// the duration of the movement, whether to snap the sprite to pixel boundaries,
        /// and the elapsed time since the movement started.
        /// </remarks>
        private _MovementDataStruct _movementStruct;

        /// <summary>
        /// Indicates whether the actor is currently moving to a target location over time.
        /// </summary>
        protected bool moving;

        /// <summary>
        /// Represents an actor in the game, which can be a drawable entity, character, or visual effect.
        /// </summary>
        public Actor() {
            actorScale = new Vector2f(1f, 1f);
        }

        /// <summary>
        /// Initializes the actor with a specified location and an optional parent scene.
        /// </summary>
        /// <param name="location">The initial location of the actor.</param>
        /// <param name="scene">The parent scene where the actor resides. Default is null.</param>
        public virtual void Init(Vector2f location, Scene scene = null) {
            SetLocation(location);

            if(scene != null)
                parentScene = scene;
        }

        /// <summary>
        /// Sets the location of the actor.
        /// </summary>
        /// <param name="location">The new location of the actor.</param>
        /// <param name="snapSprite">Indicates whether the sprite should be snapped to the nearest pixel. Defaults to true.</param>
        public virtual void SetLocation(Vector2f location, bool snapSprite = true)
        {
            Vector2f flooredLocation = new Vector2f((float)Math.Floor(location.X), (float)Math.Floor(location.Y));

            if(sprite!= null)
                sprite.Position = snapSprite?flooredLocation:location;

            actorLocation = location;
        }

        /// <summary>
        /// Scales the actor and its sprite to the specified size.
        /// </summary>
        /// <param name="scale">The new scale vector to apply to the actor and its sprite.</param>
        public virtual void SetScale(Vector2f scale)
        {
            if (sprite != null)
                sprite.Scale = scale;
            actorScale = scale;
        }

        /// <summary>
        /// Sets the scale of the actor's sprite based on the specified scale factor.
        /// </summary>
        /// <param name="scale">The scale factor to be applied uniformly to both dimensions of the actor.</param>
        public virtual void SetScale(float scale)
        {
            SetScale(new Vector2f(scale, scale));
        }

        /// <summary>
        /// Initiates a movement action for the actor to move to a specified location over a given period.
        /// </summary>
        /// <param name="location">The target location to move the actor to.</param>
        /// <param name="time">The time duration in which to complete the movement.</param>
        /// <param name="spritePixelSnap">Indicates whether to snap the sprite to pixel boundaries during movement.</param>
        public virtual void MoveToLocationOverTime(Vector2f location, float time, bool spritePixelSnap = true)
        {
            moving = true;
            _movementStruct = new _MovementDataStruct();
            _movementStruct.target = location;
            _movementStruct.time = time;
            _movementStruct.elapsedTime = 0;
            _movementStruct.spritePixelSnap = spritePixelSnap;
        }

        /// <summary>
        /// Retrieves the current location of the actor in the scene.
        /// </summary>
        /// <returns>The location of the actor as a Vector2f.</returns>
        public Vector2f GetLocation() { return actorLocation; }

        /// <summary>
        /// Retrieves the sprite associated with the actor.
        /// </summary>
        /// <returns>The sprite of the actor.</returns>
        public Sprite GetSprite() { return sprite; }

        /// <summary>
        /// Retrieves the parent scene of the actor.
        /// </summary>
        /// <returns>The Scene object that is the parent of this actor.</returns>
        public Scene GetScene() { return parentScene; }

        /// <summary>
        /// Represents the initialization logic for the Actor class. This method is intended to be overridden by derived classes.
        /// </summary>
        /// <remarks>
        /// The Start method is typically used to include any start-up logic that needs to be executed when an Actor is first initialized.
        /// It can operate on the actorLocation and other properties to set up the Actor's initial state or actions.
        /// </remarks>
        public virtual void Start() { }

        /// <summary>
        /// Updates the state of the actor. This method is called on each frame update.
        /// If the actor is moving, it increments the elapsed movement time and calculates
        /// the new location based on the elapsed time and movement duration.
        /// </summary>
        public virtual void Tick() 
        {
            if(moving)
            {
                _movementStruct.elapsedTime += Game.GetInstance().DeltaTime;
                if (_movementStruct.elapsedTime > _movementStruct.time)
                {
                    _movementStruct.elapsedTime = _movementStruct.time;
                    moving = false;
                }
                SetLocation(
                    actorLocation + (_movementStruct.target-actorLocation)*(_movementStruct.elapsedTime/_movementStruct.time),
                    _movementStruct.spritePixelSnap
                );
            }
        }

        /// Renders the actor onto the game window.
        /// If the actor has a sprite, it draws the sprite using the game window's drawing method.
        /// This method may be overridden in derived classes to render additional components.
        public virtual void Draw()
        {
            if (sprite != null)
                Game.GetInstance().GetWindow().Draw(sprite);
        }

        /// <summary>
        /// Destroys the Actor by disposing its sprite and removing it from the parent scene.
        /// </summary>
        public virtual void Destroy()
        {
            if (parentScene == null)
                return;
            
            sprite.Dispose();
            parentScene.DeleteActor(this);
        }
    }
}
