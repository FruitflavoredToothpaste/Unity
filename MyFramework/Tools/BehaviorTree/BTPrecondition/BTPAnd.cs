namespace MyFramework.Tools
{
    public class BTPAnd : BTPrecondition
    {
        private BTPrecondition left;
        private BTPrecondition right;

        public override bool Evaluate()
        {
            return left.Evaluate() && right.Evaluate();
        }

        public BTPAnd(BTPrecondition left, BTPrecondition right)
        {
            this.left = left;
            this.right = right;
        }
    }
}