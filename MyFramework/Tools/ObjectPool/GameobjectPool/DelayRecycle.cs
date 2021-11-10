using System.Collections.Generic;
using UnityEngine;

namespace MyFramework.Tools
{
    public sealed class DelayRecycle : MonoBehaviour
    {
        private GameTimer gm;

        void Start()
        {
            gm = new GameTimer();
        }


        void Update()
        {
            gm.Update(Time.deltaTime);
        }

        public void Recycle(float delayTime)
        {
            gm.Register(delayTime, delegate
            {
                if (!GameobjectPool.Instance.pool.ContainsKey(gameObject.name))
                {
                    GameobjectPool.Instance.pool.Add(gameObject.name, new Stack<GameObject>());
                }

                GameobjectPool.Instance.pool[gameObject.name].Push(gameObject);
            });
        }
    }
}