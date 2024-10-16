using PAS.Engine;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace PAS.Content.Widgets.ClassSelection
{
    internal class ClassSelector : Engine.Actor
    {
        List<ClassFrame> classFrames = new List<ClassFrame>();
        
        private int _index;
        private int _playableClassNumber;
    
        
        const int FRAMES_MARGIN = 116;
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
                        24f
                    )
                )
            );
            
            _playableClassNumber++;
        }

        public override void Tick()
        {
            base.Tick();
            Stack<Engine.Event> eventStack = PASEventHandler.GetInstance().PollEvents();
            while (eventStack.Count > 0)
            {
                Engine.Event e = eventStack.Pop();

                if (e.GetType() == typeof(ClassScrollRightEvent))
                {
                    if (_index < _playableClassNumber-1)
                    {
                        _index++;
                        foreach (ClassFrame frame in classFrames)
                        {
                            frame.MoveToLocationOverTime(
                                frame.defaultCharacterPosition - _index * new Vector2f(FRAMES_MARGIN, 0f),
                                1f
                            );
                        }
                    }
                }

                if (e.GetType() == typeof(ClassScrollLeftEvent))
                {
                    if (_index > 0)
                    {
                        _index--;
                        foreach (ClassFrame frame in classFrames)
                        {
                            frame.MoveToLocationOverTime(
                                frame.defaultCharacterPosition - _index * new Vector2f(FRAMES_MARGIN, 0f),
                                1f
                            );
                        }
                    }
                }

                if (e.GetType() == typeof(ConfirmClassEvent))
                {
                    classFrames[_index].StartWithCharacter();
                }
            }
            
        }
    }
}