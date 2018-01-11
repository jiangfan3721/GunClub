using System;
using System.Collections;
using JetBrains.Annotations;

namespace DispatcherMachineSystem
{
    //内部接口
    public interface IHandlers
    {
        void Add(string id, DispatcherHandler handler, int priority);

        void Remove(string id, DispatcherHandler handler);

        void Remove(string id);

        void Clear();
    }

    //内部接口
    public interface IEvents
    {
        void Enqueue(Event e);

        bool TryDequeue();

        Event Dequeue();

        void Clear();
    }

    //事件分发接口，被分发的事件暂存于updateEvent队列，将于Update函数中被调用
    public interface IDispatcherUpdateEvent
    {
        //用于分发事件事件函数（id：事件名，args：数据，可以为空）
        void DispatcherUpdate(string id, ArrayList args);

        //用于强制执行事件，一般不需要手动调用
        void DispatcherUpdateExecute();
    }

    //事件分发接口，被分发的事件暂存于LaterUpdateEvent队列，将于LaterUpdate函数中被调用
    public interface IDispatcherLaterUpdateEvent
    {
        //用于分发事件事件函数（id：事件名，args：数据，可以为空）
        void DispatcherLaterUpdate(string id, ArrayList args);

        //用于强制执行事件，一般不需要手动调用
        void DispatcherLaterUpdateExecute();
    }

    //事件分发接口，会立刻调用相应的Handler
    public interface IDispatcherImmediatelyEvent
    {
        //用于分发事件事件函数（id：事件名，args：数据，可以为空）
        void DispatcherImmediately(string id, ArrayList args);
    }

    //事件分发接口，被分发的事件暂存于WaitForEndOfFrame队列，将于每帧结束前，使用协同程序调用
    public interface IDispatcherWaitForEndOfFrameEvent
    {
        //用于分发事件事件函数（id：事件名，args：数据，可以为空）
        void DispatcherWaitForEndOfFrame(string id, ArrayList args);

        //用于强制执行事件，一般不需要手动调用
        void DispatcherWaitForEndOfFrameExecute();
    }

    public interface IDispatcherHandler
    {
        //用于添加事件的回调函数（id：事件名。handler：回调函数（必须满足DispatcherHandler(string id, ArrayList args)原型）
        //type：回调的类型，不同的回调在不同的队列。priority：回调的优先级，只会影响在本队列内的优先级）
        void AddHandler(string id, DispatcherHandler handler, HandlerType type, int priority);

        //用于删除事件回调（必须id和handler都相同才删除）
        void RemoveHandler(string id, DispatcherHandler handler, HandlerType type);

        //用于删除事件回调（某一id下的handler全部删除）
        void RemoveHandlerById(string id, HandlerType type);

        //清空某一类型的Handler队列
        void ClearHandler(HandlerType type);

        //清空所有类型的Handler队列
        void ClearAllHandler();
    }
}