using MyFramework.Modules;
using UnityEngine;

namespace MyFramework
{
    public class Game : MonoBehaviour
    {
        public static Game Instance;

        #region Modules

        public UIManager uIManager;
        
        public ResourcesManager resourcesManager;

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

            resourcesManager = new ResourcesManager();
            resourcesManager.Init();
        }
    
        /// <summary>
        /// 启动各模块功能
        /// </summary>
        private void StartModules()
        {
            uIManager.Start();
            resourcesManager.Start();
        }
    
        /// <summary>
        /// Update各模块
        /// </summary>
        private void UpdateModules()
        {
            uIManager.Update(Time.unscaledDeltaTime);
            resourcesManager.Update(Time.unscaledDeltaTime);
        }
    }
}