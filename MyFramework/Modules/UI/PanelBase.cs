using UnityEngine;
using UnityEngine.UI;

namespace MyFramework.Modules
{
    /// <summary>
    /// UI基类（生命周期：初始化/打开/暂停/恢复/关闭）
    /// </summary>
    public class PanelBase
    {
        #region 字段属性

        protected Transform self;

        protected int canvasOrder;

        public int CanvasOrder
        {
            get { return canvasOrder; }
            set
            {
                canvasOrder = value;
                canvas.sortingOrder = canvasOrder;
            }
        }

        protected Canvas canvas;

        protected GraphicRaycaster graphicRaycaster;

        #endregion

        #region 方法

        #region 私有方法

        protected T Get<T>() where T : Component
        {
            return self.GetComponent<T>();
        }

        #region 寻找子物件

        protected Transform Find(string subPath)
        {
            return self.Find(subPath);
        }

        protected T Find<T>(string subPath) where T : Component
        {
            return self.Find(subPath).GetComponent<T>();
        }

        #endregion

        #region 添加UI事件

        protected UIEvent RegisterUIEvent(string subPath)
        {
            return RegisterUIEvent(Find(subPath).gameObject);
        }

        protected UIEvent RegisterUIEvent(GameObject subGameObject)
        {
            if (Equals(subGameObject.GetComponent<UIEvent>(), null))
            {
                subGameObject.AddComponent<UIEvent>();
            }

            return subGameObject.GetComponent<UIEvent>();
        }

        protected UIEvent RegisterUIEvent(Transform subTransform)
        {
            return RegisterUIEvent(subTransform.gameObject);
        }

        #endregion

        #endregion

        #region 公有方法

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="self">UI的GameObject</param>
        public virtual void Init(GameObject self)
        {
            this.self = self.transform;

            if (Equals(Get<Canvas>(), null))
            {
                self.AddComponent<Canvas>();
            }

            if (Equals(Get<GraphicRaycaster>(), null))
            {
                self.AddComponent<GraphicRaycaster>();
            }

            canvas = Get<Canvas>();
            graphicRaycaster = Get<GraphicRaycaster>();
            canvas.overrideSorting = true;
        }

        /// <summary>
        /// 打开
        /// </summary>
        public virtual void Open()
        {
            self.gameObject.SetActive(true);
        }

        /// <summary>
        /// 暂停
        /// </summary>
        public virtual void Pause()
        {
            SetInteraction(false);
        }

        /// <summary>
        /// 恢复
        /// </summary>
        public virtual void Resume()
        {
            SetInteraction(true);
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public virtual void Close()
        {
            Object.Destroy(self.gameObject);
        }


        /// <summary>
        /// 设置界面是否能交互
        /// </summary>
        /// <param name="interactable"></param>
        public void SetInteraction(bool interactable)
        {
            graphicRaycaster.enabled = interactable;
        }

        #endregion

        #endregion
    }
}