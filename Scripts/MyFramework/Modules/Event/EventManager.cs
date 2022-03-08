using System;
using System.Collections.Generic;
using Object = System.Object;

namespace MyFramework.Modules
{
    public class EventManager : ModuleBase
    {
        private Dictionary<int, Action<Object>> eventsDic;

        public override void Init()
        {
            base.Init();
            eventsDic = new Dictionary<int, Action<object>>();
        }

        #region 创建事件

        /// <summary>
        /// 已创建的不会覆盖
        /// </summary>
        /// <param name="eventCode"></param>
        /// <returns>创建成功返回true</returns>
        public bool CreateEvent(int eventCode)
        {
            if (!eventsDic.ContainsKey(eventCode))
            {
                Action<Object> eventHandler = null;
                eventsDic.Add(eventCode, eventHandler);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 创建或者订阅
        /// </summary>
        /// <param name="eventCode"></param>
        /// <param name="eventHandler"></param>
        /// <returns>0：创建/1：订阅</returns>
        public int CreateOrSubscribeEvent(int eventCode, Action<Object> eventHandler)
        {
            if (!eventsDic.ContainsKey(eventCode))
            {
                eventsDic.Add(eventCode, eventHandler);
                return 0;
            }
            else
            {
                eventsDic[eventCode] += eventHandler;
                return 1;
            }
        }

        #endregion

        #region 订阅事件

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventCode"></param>
        /// <param name="eventHandler"></param>
        /// <returns>订阅成功返回true</returns>
        public bool SubscribeEvent(int eventCode, Action<Object> eventHandler)
        {
            if (eventsDic.ContainsKey(eventCode))
            {
                eventsDic[eventCode] += eventHandler;
                return true;
            }

            return false;
        }

        #endregion

        #region 移除事件

        public bool RemoveEvent(int eventCode)
        {
            if (eventsDic.ContainsKey(eventCode))
            {
                eventsDic.Remove(eventCode);
                return true;
            }

            return false;
        }

        #endregion

        #region 取消订阅

        public bool UnsubscribeEvent(int eventCode, Action<Object> eventHandler)
        {
            if (eventsDic.ContainsKey(eventCode))
            {
                eventsDic[eventCode] -= eventHandler;
                return true;
            }

            return false;
        }

        #endregion

        #region 分发事件

        /// <summary>
        /// 分发事件
        /// </summary>
        /// <param name="eventCode"></param>
        /// <param name="value">事件传参</param>
        /// <returns>没有事件或者没有订阅时返回FALSE</returns>
        public bool PostEvent(int eventCode, Object value)
        {
            if (eventsDic.ContainsKey(eventCode))
            {
                Action<Object> tempHandler = eventsDic[eventCode];
                Delegate[] handlers = tempHandler.GetInvocationList();
                if (handlers.Length == 0)
                {
                    return false;
                }

                tempHandler(value);
                return true;
            }

            return false;
        }

        #endregion
    }
}