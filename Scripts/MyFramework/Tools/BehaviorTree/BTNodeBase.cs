using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFramework.Tools
{
    public abstract class BTNodeBase
    {
        public abstract TickStatus Tick();

        public virtual void Reset()
        {
        }
    }

    public enum TickStatus
    {
        Failed,
        Successed,
        Running
    }
}