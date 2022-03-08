namespace MyFramework.Tools
{
    public abstract class BTPrecondition : BTNodeBase
    {
        public abstract bool Evaluate();

        public override TickStatus Tick()
        {
            if (Evaluate())
            {
                return TickStatus.Successed;
            }

            return TickStatus.Failed;
        }

    }
}