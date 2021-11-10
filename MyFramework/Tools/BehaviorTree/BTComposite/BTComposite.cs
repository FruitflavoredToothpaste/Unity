using System.Collections.Generic;

namespace MyFramework.Tools
{
    public abstract class BTComposite : BTNodeBase
    {
        protected List<BTNodeBase> children;

        public override void Reset()
        {
            for (int i = 0; i < children.Count; i++)
            {
                children[i].Reset();
            }
        }

        protected BTComposite()
        {
            children = new List<BTNodeBase>();
        }
    }
}