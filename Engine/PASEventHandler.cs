using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// DEPRECATED

// Custom (and bad) event system i made before i learnt about C# events & delegates.
namespace PAS.Engine
{
    internal class Event
    {
        public Event() { }
    }

    internal class PASEventHandler
    {
        Stack<Event> events;

        private PASEventHandler() 
        {
            events = new Stack<Engine.Event>();
        }

        static PASEventHandler _instance;
        public static PASEventHandler GetInstance() { 
            if(_instance==null)
                _instance = new PASEventHandler();
            return _instance;
        }

        public void TriggerEvent(Engine.Event ev)
        {
            events.Push(ev);
        }
        public Stack<Event> PollEvents()
        {
            return events;
        }
        public T TryCatchEventOfType<T>() where T : Engine.Event 
        {
            foreach(Event ev in events)
            {
                try
                {
                    return (T)ev;
                }
                catch { }
            }
            return null;
        }
    }
}
