namespace MyFramework.Tools
{
    public class BTDInverter : BTDecorator
    {
        public override TickStatus Tick()
        {
            TickStatus status = child.Tick();
            if (status == TickStatus.Successed)
            {
                return TickStatus.Failed;
            }
            else if (status == TickStatus.Failed)
            {
                return TickStatus.Successed;
            }

            return TickStatus.Running;
        }

        public BTDInverter(BTNodeBase child)
        {
            this.child = child;
        }
    }
}