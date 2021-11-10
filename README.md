# Unity

简介：这是我第一次在GitHub上传的第一个代码文件，这个框架是我在进行游戏开发过程中总结的，目前包括UI管理器，UI事件管理器，事件管理器，状态机，对象池，以及个人写的行为树(缺少blackboard的行为树)，还有计时器，下面我会介绍这个框架有什么功能，此外有更好的意见或者设计思路都可以和我交流，我很乐于交流代码。

一、Game作为游戏的入口
1.在unity中只需要将Game.cs拖到物体上就行，所有的管理器都会通过Game进行Init(),Update()等方法
2.如果需要使用管理器，需要通过Game.Instance调用Game内实例化的管理器对象
3.需要扩展管理器可以在Game中自行创建和操作

二、UI管理器
1.UI管理器实行的是预制体和窗体逻辑的分离
2.UI管理器采用栈存储打开的UI

三、UI事件
1.通过实现Unity.EventSystems的IPointerClickHandler等鼠标接口继承MonoBehaviour的脚本添加至需要UI事件的物体
2.事件可以自行在UIEvent中扩展

四、行为树
1.行为树实现模式采用的和常规的有些不同，在设计行为树的时候，我在考虑行为树该如何去使用，个人认为无论是前提条件、符合节点、修饰节点、行为节点都是节点，使用时可以自由new()节点添加子节点，这样的实现方式很自由，但是约束不足，需要使用者考虑清楚整个行为树的逻辑
2.根节点通过在Update()中的Tick(),将Tick()传递至每一片子叶,如同心跳一般，所以前提节点的Tick()是一个判断，复合节点的Tick()是子节点的遍历，修饰节点也是对唯一子节点的Tick()进行修饰，行为节点则是一种行为
