using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MyFramework.Tools
{
    public sealed class GameobjectPool : Singleton<GameobjectPool>
    {
        public Dictionary<string, Stack<GameObject>> pool;

        private GameobjectPool()
        {
            pool = new Dictionary<string, Stack<GameObject>>();
        }

        public GameObject Get(GameObject gameObject, Action<GameObject> resetAction = null,
            Action<GameObject> initAction = null)
        {
            GameObject obj = null;
            if (pool.ContainsKey(gameObject.name) && pool[gameObject.name].Count > 0)
            {
                obj = pool[gameObject.name].Pop();
                if (!Equals(resetAction, null))
                {
                    resetAction(obj);
                }
            }
            else
            {
                obj = Object.Instantiate(gameObject);
                obj.AddComponent<DelayRecycle>();
                if (!Equals(initAction, null))
                {
                    initAction(obj);
                }
            }

            return obj;
        }

        public void Recycle(float delayTime, GameObject gameObject)
        {
            gameObject.GetComponent<DelayRecycle>().Recycle(delayTime);
        }

        public void Clear()
        {
            pool.Clear();
            instance = null;
        }
    }
}