using PAS.Engine;
using SFML.System;
using SFML.Window;

namespace PAS.Content.Widgets.ClassSelection
{
    internal class ClassSelector : Engine.Actor
    {
        List<ClassFrame> classFrames = new List<ClassFrame>();
        
        private int _index;
        private int _playableClassNumber;
    
        
        const int FRAMES_MARGIN = 96;
        public ClassSelector() : base() 
        {}

        public void AddPlayableCharacter<T>() where T : Character, new()
        {
            ClassFrame<T> tempFrame = new ClassFrame<T>();
            uint frameWidth = AssetLoader.GetInstance().GetTexture("class_frame").Size.X;
            
            classFrames.Add(
                parentScene.AddActorOfClass<ClassFrame<T>>(
                    new Vector2f(
                        _playableClassNumber * FRAMES_MARGIN + Game.GetInstance().GetWindow().Size.X/20 - AssetLoader.GetInstance().GetTexture("class_frame").Size.X/2,
                        16f
                    )
                )
            );
            
            _playableClassNumber++;
        }

        public void Scroll(int amount)
        {
            if (_index + amount < 0 || _index + amount >= classFrames.Count)
                return;
            _index += amount;
            UpdateFrames();
        }

        public void ConfirmCharacter()
        {
            classFrames[_index].ConfirmCharacter();
        }
        public void UpdateFrames()
        {
            foreach (ClassFrame frame in classFrames)
            {
                frame.MoveToLocationOverTime(
                    frame.defaultCharacterPosition - _index * new Vector2f(FRAMES_MARGIN, 0f),
                    1f
                );
            }
        }
    }
}