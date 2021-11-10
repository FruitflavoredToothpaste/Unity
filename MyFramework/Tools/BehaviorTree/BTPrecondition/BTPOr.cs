namespace MyFramework.Tools
{
    public class BTPOr : BTPrecondition
    {
        private BTPrecondition left;
        private BTPrecondition right;

        public override bool Evaluate()
        {
            return left.Evaluate() || right.Evaluate();
        }

        public BTPOr(BTPrecondition left, BTPrecondition right)
        {
            this.left = left;
            this.right = right;
        }
    }
}