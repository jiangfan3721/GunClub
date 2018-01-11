using UnityEngine;
using System.Collections;
using DispatcherMachineSystem;

public class DispatcherMachineComponent : DispatcherSingleton<DispatcherMachineComponent>
{
    //事件分发器组件，通过DispatcherMachineComponent.Instance.DispatcherMachine访问，会自动生成单例和GameObject，并且可以跨域场景。
    public DispatcherMachine DispatcherMachine = new DispatcherMachine();

    //private int currentLevel;

    void Awake()
    {
        CteateDispatcherMachineSceneCheckComponent();
        StartCoroutine(UpdateWaitForEndOfFrame());
        //currentLevel = Application.loadedLevel;
    }

	void Update () {
        DispatcherMachine.DispatcherUpdateExecute();
	}

//    public override void CheckScene()
//    {
//        if (currentLevel != Application.loadedLevel)
//        {
//            DispatcherMachine.ClearHandler(HandlerType.SCENE_HANDLER);
//            currentLevel = Application.loadedLevel;
//        }
//    }

    void OnLevelWasLoaded(int level)
    {
        CteateDispatcherMachineSceneCheckComponent();
    }

    void OnApplicationQuit()
    {
        DispatcherMachine.ClearAllHandler();
        if (this.gameObject != null)
        {
            Destroy(this);
        }
    }

    IEnumerator UpdateWaitForEndOfFrame()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            DispatcherMachine.DispatcherWaitForEndOfFrameExecute();
        }
    }

    void CteateDispatcherMachineSceneCheckComponent()
    {
        if (GameObject.Find("DispatcherMachineSceneCheckComponent") == null)
        {
            GameObject obj = new GameObject("DispatcherMachineSceneCheckComponent");
            DispatcherMachineSceneCheckComponent comp = obj.AddComponent<DispatcherMachineSceneCheckComponent>();
            comp.Init(DispatcherMachine);
        }
    }
}
