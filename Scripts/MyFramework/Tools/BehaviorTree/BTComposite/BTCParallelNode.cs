using System.Collections.Generic;

namespace MyFramework.Tools
{
    public class BTCParallelNode : BTComposite
    {
        private List<TickStatus> childrenStatus;

        public override TickStatus Tick()
        {
            for (int i = 0; i < children.Count; i++)
            {
                if (childrenStatus[i] == TickStatus.Running)
                {
                    TickStatus status = children[i].Tick();

                    if (status == TickStatus.Failed)
                    {
                        Reset();
                        return TickStatus.Failed;
                    }

                    childrenStatus[i] = status;
                }
            }

            if (childrenStatus.Contains(TickStatus.Running))
            {
                return TickStatus.Running;
            }
            else
            {
                return TickStatus.Successed;
            }
        }

        public override void Reset()
        {
            base.Reset();
            ResetArray();
        }

        public BTCParallelNode(params BTNodeBase[] BTNodes)
        {
            children.AddRange(BTNodes);
            childrenStatus = new List<TickStatus>();
            ResetArray();
        }

        private void ResetArray()
        {
            for (int i = 0; i < childrenStatus.Count; i++)
            {
                childrenStatus[i] = TickStatus.Running;
            }
        }
    }
}