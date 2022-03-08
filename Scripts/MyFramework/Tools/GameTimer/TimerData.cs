using System;

namespace MyFramework.Tools
{
    public sealed class TimerData
    {
        private float time;
        private System.Action action;

        public TimerData(float time, Action action)
        {
            this.time = time;
            this.action = action;
        }

        public bool Update(float deltaTime)
        {
            if (time <= 0)
            {
                action();
                return false;
            }

            time -= deltaTime;
            return true;
        }
    }
}