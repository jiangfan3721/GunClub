using System;
using System.Collections;
using UnityEngine;


namespace DispatcherMachineSystem
{
    public abstract class DispatcherMachineBase
    {
        protected Handlers[] handlers;

        public virtual void ExecuteEvent(Event e) { }

        public DispatcherMachineBase()
        {
            handlers = new Handlers[(int)HandlerType.COUNT];
            for (int i = 0; i < handlers.Length; i++)
            {
                handlers[i] = new Handlers();
            }
        }
    }

    public class DispatcherMachine : DispatcherMachineBase, IDispatcherUpdateEvent, IDispatcherImmediatelyEvent, IDispatcherWaitForEndOfFrameEvent, IDispatcherHandler
    {
        protected Events updateEvents;

        protected Events waitForEndOfFrameEvents;

        public int MaxEventNumber { get; set; }

        public void DispatcherUpdate(string id, ArrayList args = null)
        {
            updateEvents.Enqueue(new Event(id, args));
        }

        public void DispatcherUpdateExecute()
        {
            for (int i = 0; i < MaxEventNumber; i++)
            {
                if (!updateEvents.TryDequeue())
                {
                    break;
                }

                ExecuteEvent(updateEvents.Dequeue());
            }
        }

        public void DispatcherImmediately(string id, ArrayList args = null)
        {
            ExecuteEvent(new Event(id, args));
        }

        public void DispatcherWaitForEndOfFrame(string id, ArrayList args = null)
        {
            waitForEndOfFrameEvents.Enqueue(new Event(id, args));
        }

        public void DispatcherWaitForEndOfFrameExecute()
        {
            for (int i = 0; i < MaxEventNumber; i++)
            {
                if (!waitForEndOfFrameEvents.TryDequeue())
                {
                    break;
                }

                ExecuteEvent(waitForEndOfFrameEvents.Dequeue());
            }
        }


        public void AddHandler(string id, DispatcherHandler handler, HandlerType type = HandlerType.SCENE_HANDLER, int priority = 0)
        {
            if ((int) type < 0 || (int) type >= handlers.Length)
            {
                return;
            }

            handlers[(int)type].Add(id, handler, priority);
        }

        public void RemoveHandler(string id, DispatcherHandler handler, HandlerType type = HandlerType.SCENE_HANDLER)
        {
            if ((int)type < 0 || (int)type >= handlers.Length)
            {
                return;
            }

            handlers[(int)type].Remove(id, handler);
        }

        public void RemoveHandlerById(string id, HandlerType type = HandlerType.SCENE_HANDLER)
        {
            if ((int)type < 0 || (int)type >= handlers.Length)
            {
                return;
            }

            handlers[(int)type].Remove(id);
        }

        public void ClearHandler(HandlerType type = HandlerType.SCENE_HANDLER)
        {
            if ((int)type < 0 || (int)type >= handlers.Length)
            {
                return;
            }

            handlers[(int)type].Clear();
        }

        public void ClearAllHandler()
        {
            for (int i = 0; i < handlers.Length; i++)
            {
                handlers[i].Clear();
            }
        }

        public override void ExecuteEvent(Event e)
        {
            for (int i = 0; i < handlers.Length; i++)
            {
                handlers[i].Execute(e.Id, e.Data);
            }
        }

        public DispatcherMachine()
        {
            updateEvents = new Events();
            waitForEndOfFrameEvents = new Events();
            MaxEventNumber = 100;
        }
    }
}
