# Dialogue-style-plot-system

这是一个能够在Unity中运行的剧情系统。

该系统旨在提高对于游戏剧情的开发效率。



## 各个类的职责

`CommandConfig`：继承自`ScriptableObject`。它用于存储一系列命令对象。

`CommandBase`：一个抽象类。它作为所有命令的基类，声明了三种抽象方法：

1. `Execute` ：在该命令对象出队后立刻被调用，且只调用一次。
2. `OnUpdate` ：在`Execute` 后每帧被调用，直到返回`true`；
3. `IsFinished`：返回值类型为`bool`，在该命令对象执行完毕时返回`true`，未执行完则返回`false`。

`CommandSender`：一个单例，包含一个命令队列。它将从外部传入的`CommandConfig`中获取一系列命令对象并入队，按队列顺序执行各个命令。

`PlotUISettings`：一个单例，负责在进入和退出剧情时，对某些参数进行设置和清理；同时对外暴露界面的像素大小、打字效果的速度等可供用户调整的参数。

`PlotEventContainer`：一个单例，用于存放各个`Event`，并对外提供方法来进行系统的初始化。



以下是各个命令类，均继承自`CommandBase`。

|     类名     |              可自定义的字段              |      该命令何时结束      |           注释           |
| :----------: | :--------------------------------------: | :----------------------: | :----------------------: |
|   `HEADER`   |        章节名、该段剧情是否可跳过        |           立刻           | 负责进入剧情时的补间动画 |
| `Background` |                 背景图片                 |           立刻           |            无            |
|   `Delay`    |              需要等待的时间              |     等待自定义时间后     |            无            |
|  `Dialogue`  |         说话者的名字、说话的内容         | 打字效果结束且用户点击后 |            无            |
| `Character`  |               说话者的图片               |           立刻           |   最多支持显示两个立绘   |
|  `Decision`  |           用户可选择的对话选项           |  用户选择一个对话选项后  |   最多支持五个剧情选项   |
| `Predicate`  | 标志位，作为用户所选项对应的命令段的首位 |           立刻           |   最多支持五个剧情选项   |
|    `END`     |        标志位，作为整个剧情的出口        |           永不           | 负责退出剧情时的补间动画 |



## 如何使用



### 注意事项



### 开始和结束时的回调

该系统提供了两个`UnityEvent`回调：

```
plot_command_executor.PlotEventContainer.Instance.plotBegin;
plot_command_executor.PlotEventContainer.Instance.plotEnd;
```



未完待续























