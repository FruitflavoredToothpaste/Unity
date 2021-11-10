using MyFramework.Modules;
using UnityEngine;

namespace MyFramework
{
    public class Game : MonoBehaviour
    {
        public static Game Instance;

        #region Modules

        public UIManager uIManager;

        #endregion
    
        void Awake()
        {
            Instance = this;
            InitModules();
        }

        void Start()
        {
            StartModules();
        }

        void Update()
        {
            UpdateModules();
        }

        /// <summary>
        /// 初始化各模块
        /// </summary>
        private void InitModules()
        {
            uIManager = new UIManager();
            uIManager.Init();
        }
    
        /// <summary>
        /// 启动各模块功能
        /// </summary>
        private void StartModules()
        {
            uIManager.Start();
        }
    
        /// <summary>
        /// Update各模块
        /// </summary>
        private void UpdateModules()
        {
            uIManager.Update(Time.deltaTime);
        }
    }
}