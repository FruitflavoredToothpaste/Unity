using System;
using System.Collections.Generic;

namespace MyFramework.Tools
{
    /*public class ObjectPool<T> where T : class, IResetable, IInitializable, new()
    {
        private Stack<T> objectStack;

        public ObjectPool(int poolSize)
        {
            objectStack = new Stack<T>(poolSize);
        }

        public T New()
        {
            if (objectStack.Count > 0)
            {
                T t = objectStack.Pop();
                t.Reset();

                return t;
            }
            else
            {
                T t = new T();
                t.FirstTimeInit();

                return t;
            }
        }

        public void Store(T t)
        {
            objectStack.Push(t);
        }
    }*/
    public sealed class ObjectPool<T> where T : class, new()
    {
        private Stack<T> objectStack;

        public Action<T> resetAction;

        public Action<T> initAction;

        public ObjectPool(int poolSize, Action<T> resetAction = null, Action<T> initAction = null)
        {
            objectStack = new Stack<T>(poolSize);
            this.resetAction = resetAction;
            this.initAction = initAction;
        }

        public ObjectPool(Action<T> resetAction = null, Action<T> initAction = null)
        {
            objectStack = new Stack<T>();
            this.resetAction = resetAction;
            this.initAction = initAction;
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <returns></returns>
        public T New()
        {
            if (objectStack.Count > 0)
            {
                T t = objectStack.Pop();
                if (!Equals(resetAction, null))
                {
                    resetAction(t);
                }

                return t;
            }
            else
            {
                T t = new T();
                if (!Equals(initAction, null))
                {
                    initAction(t);
                }

                return t;
            }
        }

        /// <summary>
        /// 存放
        /// </summary>
        /// <param name="t"></param>
        public void Store(T t)
        {
            objectStack.Push(t);
        }
    }
}