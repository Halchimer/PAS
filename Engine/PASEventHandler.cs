﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAS.Engine
{
    internal class Event
    {
        public Event() { }
    }

    internal class PASEventHandler
    {
        List<Event> events;

        private PASEventHandler() { }

        static PASEventHandler _instance;
        public static PASEventHandler GetInstance() { 
            if(_instance==null)
                _instance = new PASEventHandler();
            return _instance; 
        }

        public void TriggerEvent(Event ev)
        {
            events.Add(ev);
        }
        public Event[] PollEvents()
        {
            return events.ToArray();
        }
        public T TryCatchEventOfType<T>() where T : Event 
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
