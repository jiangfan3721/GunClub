using UnityEngine;
using System.Collections;

namespace DispatcherMachineSystem
{
    public class DispatcherMachineSceneCheckComponent : MonoBehaviour
    {
        private DispatcherMachine dispatcherMachine;

        public void Init(DispatcherMachine machine)
        {
            dispatcherMachine = machine;
        }

        void OnDestroy()
        {
            if (dispatcherMachine != null)
            {
                dispatcherMachine.ClearHandler(HandlerType.SCENE_HANDLER);
            }
        }

        void OnApplicationQuit()
        {
            if (this.gameObject != null)
            {
                Destroy(this);
            }
        }
    }
}
