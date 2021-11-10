using MyFramework.Tools;

namespace MyFramework.Modules
{
	public class ModuleBase
	{
		public GameTimer gameTimer;

		/// <summary>
		/// 初始化组件和数据
		/// </summary>
		public virtual void Init()
		{
			gameTimer = new GameTimer();
		}

		/// <summary>
		/// 开始执行功能
		/// </summary>
		public virtual void Start()
		{
		
		}

		public virtual void Update(float deltaTime)
		{
			gameTimer.Update(deltaTime);
		}
	
		/// <summary>
		/// 模块退出
		/// </summary>
		public virtual void Destroy()
		{
			gameTimer.Clear();
		}
	
	}
}
