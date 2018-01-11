using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardSimulator : MonoBehaviour {

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyUp(KeyCode.F))
            DispatcherMachineComponent.Instance.DispatcherMachine.DispatcherImmediately(GameInputConfig.INPUT_FIRE_SINGLE);
        if (Input.GetKeyUp(KeyCode.O))
            DispatcherMachineComponent.Instance.DispatcherMachine.DispatcherImmediately(GameInputConfig.INPUT_MAGAZINE_OPEN);
        if (Input.GetKeyUp(KeyCode.C))
            DispatcherMachineComponent.Instance.DispatcherMachine.DispatcherImmediately(GameInputConfig.INPUT_MAGAZINE_CLOSE);
        if (Input.GetKeyUp(KeyCode.R))
            DispatcherMachineComponent.Instance.DispatcherMachine.DispatcherImmediately(GameInputConfig.INPUT_FILLED_BULLETS);
#endif
    }

}
