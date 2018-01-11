using UnityEngine;
using System.Collections;

namespace DispatcherMachineSystem
{
    //事件队列的类型
    public enum HandlerType
    {
        GLOBLE_HANDLER,//生命周期是整个游戏过程，始终存在的队列，不会自动清空
        SCENE_HANDLER,//生命周期是某一个场景，始终存在的队列，但场景变化时会自动清空
        COUNT,//用来记录队列数量，内部接口
    }
}
