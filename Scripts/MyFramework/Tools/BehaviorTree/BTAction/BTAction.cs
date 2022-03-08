namespace MyFramework.Tools
{
    public abstract class BTAction : BTNodeBase
    {
        protected bool isInit;

        public sealed override TickStatus Tick()
        {
            if (!isInit)
            {
                OnEnter();
            }

            return OnTick();
        }

        protected abstract TickStatus OnTick();

        protected virtual void OnEnter()
        {
            isInit = true;
        }

        public override void Reset()
        {
            base.Reset();
            isInit = false;
        }
    }
}