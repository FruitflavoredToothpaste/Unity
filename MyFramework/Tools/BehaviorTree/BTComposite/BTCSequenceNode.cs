namespace MyFramework.Tools
{
    public class BTCSequenceNode : BTComposite
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
                        Reset();
                        return TickStatus.Failed;
                        break;

                    case TickStatus.Successed:
                        //成功则继续并更新索引
                        curChildIndex++;
                        break;

                    case TickStatus.Running:
                        return TickStatus.Running;
                        break;
                }
            }

            //子节点全部执行成功后reset
            Reset();
            return TickStatus.Successed;
        }

        public override void Reset()
        {
            base.Reset();
            curChildIndex = -1;
        }

        public BTCSequenceNode(params BTNodeBase[] BTNodes)
        {
            children.AddRange(BTNodes);
        }
    }
}