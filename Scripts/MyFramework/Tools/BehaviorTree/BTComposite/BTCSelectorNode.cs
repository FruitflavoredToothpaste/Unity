namespace MyFramework.Tools
{
    public class BTCSelectorNode : BTComposite
    {
        //执行到的子节点索引
        private int curChildIndex = -1;

        public override TickStatus Tick()
        {
            if (children.Count == 0)
            {
                return TickStatus.Failed;
            }

            if (curChildIndex < 0)
            {
                curChildIndex = 0;
            }

            TickStatus status;
            for (int i = curChildIndex; i < children.Count; i++)
            {
                status = children[i].Tick();
                switch (status)
                {
                    case TickStatus.Failed:
                        curChildIndex++;
                        break;
                
                    case TickStatus.Successed:
                        Reset();
                        return TickStatus.Successed;
                        break;
                
                    case TickStatus.Running:
                        return TickStatus.Running;
                        break;
                }
            }
            Reset();
            return TickStatus.Failed;
        }

        public override void Reset()
        {
            base.Reset();
            curChildIndex = -1;
        }

        public BTCSelectorNode(params BTNodeBase[] BTNodes)
        {
            children.AddRange(BTNodes);
        }
    }
}