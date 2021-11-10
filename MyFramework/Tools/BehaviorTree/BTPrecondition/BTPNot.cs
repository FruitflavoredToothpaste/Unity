namespace MyFramework.Tools
{
    public class BTPNot : BTPrecondition
    {
        private BTPrecondition condition;

        public override bool Evaluate()
        {
            return !condition.Evaluate();
        }

        public BTPNot(BTPrecondition condition)
        {
            this.condition = condition;
        }
    }
}