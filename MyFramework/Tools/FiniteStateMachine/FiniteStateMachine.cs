using System;
using System.Collections.Generic;

namespace MyFramework.Tools
{
    /// <summary>
    /// 有限状态机
    /// </summary>
    /// <typeparam name="T1">状态枚举</typeparam>
    /// <typeparam name="T2">状态机拥有者</typeparam>
    public sealed class FiniteStateMachine<T1, T2>
        where T1 : struct
        where T2 : class
    {
        #region 字段属性

        //上一个状态（枚举，只读）
        private T1 preState;

        public T1 PreState
        {
            get { return preState; }
        }

        //当前状态（枚举，只读）
        private T1 curState;

        public T1 CurState
        {
            get { return curState; }
        }

        //所有状态映射
        private Dictionary<T1, IStateBase<T2>> states;

        //状态所有者
        private T2 owner;

        #endregion

        //构造方法初始化
        public FiniteStateMachine(T2 owner)
        {
            if (!typeof(T1).IsEnum)
            {
                throw new Exception("T1 必须是枚举类型！");
            }
            states = new Dictionary<T1, IStateBase<T2>>();
            this.owner = owner;
        }

        public void Update()
        {
            states[curState].OnUpdate(owner);
        }

        #region 添加状态

        public void AddState(T1 stateEnum, IStateBase<T2> state)
        {
            if (!states.ContainsKey(stateEnum))
            {
                states.Add(stateEnum, state);
            }
        }

        #endregion

        #region 删除状态

        public void RemoveState(T1 stateEnum)
        {
            if (states.ContainsKey(stateEnum))
            {
                states.Remove(stateEnum);
            }
        }

        #endregion

        #region 切换状态

        public void ChangeState(T1 stateEnum)
        {
            //当前状态退出
            states[curState].OnExit(owner);

            //更新上一个状态和当前状态
            preState = curState;
            curState = stateEnum;
        
            //进入新状态
            states[curState].OnEnter(owner);
        }

        #endregion
    }
}