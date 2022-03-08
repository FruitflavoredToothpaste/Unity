using System;
using System.Collections.Generic;

namespace MyFramework.Tools
{
    /// <summary>
    /// 计时器使用步骤：
    /// 1.new()一个计时器
    /// 2.每帧调用Update()方法
    /// 3.Register()注册要延时调用的方法
    /// 4.Clear()清除所有计时器
    /// </summary>
    public sealed class GameTimer
    {
        private List<TimerData> timers;

        public GameTimer()
        {
            timers = new List<TimerData>();
        }

        public void Update(float deltaTime)
        {
            for (int i = 0; i < timers.Count; i++)
            {
                if (!timers[i].Update(deltaTime))
                {
                    timers.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// 注册计时器
        /// </summary>
        /// <param name="delayTime">延时时间</param>
        /// <param name="action">延时执行的方法</param>
        public void Register(float delayTime, Action action)
        {
            timers.Add(new TimerData(delayTime, action));
        }

        /// <summary>
        /// 清除所有计时器
        /// </summary>
        public void Clear()
        {
            timers.Clear();
        }
    }
}