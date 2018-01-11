using UnityEngine;
using System.Collections;

namespace DispatcherMachineSystem
{
    public class DispatcherSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static T instance;
        private static bool instantiated = false;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    if (instantiated)
                        return null;
                    instance = (T) FindObjectOfType(typeof (T));

                    if (instance == null)
                    {
                        GameObject go = new GameObject();
                        go.name = typeof (T).ToString();
                        DontDestroyOnLoad(go);
                        instance = go.AddComponent<T>();
                        instantiated = true;
                    }
                }

                //Destroy instance if the singleton persists into the editor
                if (Application.isEditor && !Application.isPlaying)
                {
                    Destroy(instance);
                    instance = null;
                }

                return instance;
            }
        }
    }
}
