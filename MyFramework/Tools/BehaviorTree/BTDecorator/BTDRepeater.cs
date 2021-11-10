using UnityEngine;

namespace MyFramework.Tools
{
    /// <summary>
    /// 次数指成功的次数，失败会继续
    /// </summary>
    public class BTDRepeater : BTDecorator
    {
        private int loopTime;
        private int curTime = 0;

        public override TickStatus Tick()
        {
            TickStatus status;
            while (true)
            {
                if (curTime > loopTime && loopTime != -1)
                {
                    return TickStatus.Successed;
                }

                status = child.Tick();
                if (status == TickStatus.Successed)
                {
                    curTime++;
                }

                return status;
            }
        }

        public override void Reset()
        {
            base.Reset();
            curTime = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="child"></param>
        /// <param name="loopTime">-1为无限循环,0为执行一次</param>
        public BTDRepeater(BTNodeBase child, int loopTime = -1)
        {
            this.child = child;
            this.loopTime = Mathf.Clamp(loopTime, -1, int.MaxValue);
        }
    }
}