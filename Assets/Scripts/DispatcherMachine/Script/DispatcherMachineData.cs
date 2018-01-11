using System;
using System.Collections;
using System.Collections.Generic;

namespace DispatcherMachineSystem
{
    public delegate void DispatcherHandler(string id, ArrayList args);

    public class Handler
    {
        public string Id { get; private set; }

        public DispatcherHandler DispatcherHandler { get; private set; }

        public int Priority { get; private set; }

        public Handler(string id, DispatcherHandler handler, int priority)
        {
            Id = id;
            DispatcherHandler = handler;
            Priority = priority;
        }
    }

    public class Handlers : IHandlers
    {
        protected Dictionary<string, List<Handler>> HandlerMap { get; private set; }

        public void Add(string id, DispatcherHandler handler, int priority)
        {
            //相同优先级先来先处理
            Handler h = new Handler(id, handler, priority);
            if (HandlerMap.ContainsKey(id))
            {
                //优先级比有序队列的最后一个元素都低，直接插入链表最后
                if (h.Priority >= HandlerMap[id][HandlerMap[id].Count - 1].Priority)
                {
                    HandlerMap[id].Add(h);
                    //HandlerMap[id].Sort();
                }
                else
                {
                    for (int i = 0; i < HandlerMap[id].Count; i++)
                    {
                        if (priority < HandlerMap[id][i].Priority)
                        {
                            HandlerMap[id].Insert(i, h);
                            break;
                        }
                    }
                }
            }
            else
            {
                List<Handler> handlers = new List<Handler>();
                handlers.Add(h);
                HandlerMap.Add(id, handlers);
            }
        }

        public void Remove(string id, DispatcherHandler handler)
        {
            if (HandlerMap.ContainsKey(id))
            {
                for (int i = 0; i < HandlerMap[id].Count; i++)
                {
                    if (handler == HandlerMap[id][i].DispatcherHandler)
                    {
                        HandlerMap[id].RemoveAt(i);
                    }
                }

                if (HandlerMap[id].Count == 0)
                {
                    HandlerMap.Remove(id);
                }
            }
        }

        public void Remove(string id)
        {
            if (HandlerMap.ContainsKey(id))
            {
                HandlerMap.Remove(id);
            }
        }

        public void Clear()
        {
            HandlerMap.Clear();
        }

        public void Execute(string id, ArrayList data)
        {
            if (HandlerMap.ContainsKey(id))
            {
                //缓存当前列表，防止新加入的回调被立刻执行。
                List<Handler> broadcastList = new List<Handler>(HandlerMap[id]);

                for (int i = 0; i < broadcastList.Count; i++)
                {
                    broadcastList[i].DispatcherHandler(id, data);
                }
            }
        }

        public Handlers()
        {
            HandlerMap = new Dictionary<string, List<Handler>>();
        }
    }

    public class Event
    {
        public string Id { get; private set; }

        public ArrayList Data { get; private set; }

        public Event(string id, ArrayList data)
        {
            Id = id;
            Data = data;
        }
    }

    public class Events : IEvents
    {
        protected Queue<Event> EventQueue { get; private set; }

        public void Enqueue(Event e)
        {
            EventQueue.Enqueue(e);
        }

        public bool TryDequeue()
        {
            return EventQueue.Count > 0;
        }

        public Event Dequeue()
        {
            return EventQueue.Dequeue();
        }

        public void Clear()
        {
            EventQueue.Clear();
        }

        public Events()
        {
            EventQueue = new Queue<Event>();
        }
    }
}
