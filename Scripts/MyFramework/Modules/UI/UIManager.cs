using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MyFramework.Modules
{
    /// <summary>
    /// 1.UIType和UI脚本和UI预制体必须同名；2.UI预制体必须放在Resources/U文件夹下；3.UI子父结构必须按Stack形式设计
    /// </summary>
    public sealed class UIManager : ModuleBase
    {
        //场景幕布
        private Transform canvas;

        //当前已经打开的UI集合
        private Dictionary<UIType, PanelBase> curPanels;

        //当前打开UI集合的顺序
        private Stack<PanelBase> panelOrder;

        public override void Init()
        {
            base.Init();
            curPanels = new Dictionary<UIType, PanelBase>();
            panelOrder = new Stack<PanelBase>();
            canvas = GameObject.Find("Canvas").transform;
        }

        #region 公有方法

        public void OpenPanel(UIType type)
        {
            if (!curPanels.ContainsKey(type))
            {
                PanelBase uiScript = null;
                //没有打开则初始化(已经打开的panel作为父级，在此框架中不允许子级UI打开父级UI，必须先关闭子级才能打开父级，按照stack结构)
                try
                {
                    GameObject ui = Object.Instantiate(Resources.Load("UI/" + type) as GameObject, canvas);
                    uiScript = Activator.CreateInstance(Type.GetType(type.ToString())) as PanelBase;
                    uiScript.Init(ui);
                    uiScript.CanvasOrder = GetMaxCanvasOrderFromDic() + 1;
                    uiScript.Open();
                }
                catch (Exception e)
                {
                    throw new Exception("UI初始化失败:" + e.Message);
                }

                if (panelOrder.Count > 0)
                {
                    //当前最上层panel暂停
                    panelOrder.Peek().Pause();
                }

                curPanels.Add(type, uiScript);
                panelOrder.Push(uiScript);
            }
            else
            {
                throw new Exception("子级UI不允许打开父级UI！:");
            }
        }

        /// <summary>
        /// 关闭最上层panel
        /// </summary>
        public void ClosePanel()
        {
            if (panelOrder.Count > 0)
            {
                PanelBase panelBase = null;
                panelBase = panelOrder.Pop();
                panelBase.Close();
                panelOrder.Peek().Resume();

                List<UIType> keys = curPanels.Keys.ToList();
                for (int i = 0; i < curPanels.Count; i++)
                {
                    if (ReferenceEquals(curPanels[keys[i]], panelBase))
                    {
                        curPanels.Remove(keys[i]);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 获取已经打开的UI的Script,可能为空
        /// </summary>
        /// <param name="type">UIType</param>
        /// <typeparam name="T">继承PanelBase</typeparam>
        /// <returns></returns>
        public T GetPanel<T>(UIType type) where T : PanelBase
        {
            PanelBase panelBase = null;
            curPanels.TryGetValue(type, out panelBase);
            return panelBase as T;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 获取最大的sortOrder
        /// </summary>
        /// <returns></returns>
        private int GetMaxCanvasOrderFromDic()
        {
            int maxOrder;
            if (curPanels.Count == 0)
            {
                maxOrder = -1;
            }
            else
            {
                List<UIType> keys = curPanels.Keys.ToList();
                maxOrder = curPanels[keys[0]].CanvasOrder;
                for (int i = 1; i < curPanels.Count; i++)
                {
                    if (curPanels[keys[i]].CanvasOrder > maxOrder)
                    {
                        maxOrder = curPanels[keys[i]].CanvasOrder;
                    }
                }
            }

            return maxOrder;
        }


        #endregion
    }
}