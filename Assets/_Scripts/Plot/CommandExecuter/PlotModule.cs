using plot_command_creator;
using plot_command_executor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace plot_module
{
    public class PlotModule : Singleton<PlotModule>
    {
        /// <summary>
        /// 供外部调用进入剧情
        /// </summary>
        public UnityEvent plotBegin = new UnityEvent();

        /// <summary>
        /// 每次剧情结束，补间动画放完就自动调这个，外部可以对它注册一些回调
        /// </summary>
        public UnityEvent plotEnd = new UnityEvent();

        /// <summary>
        /// 全局初始化一次即可，界面持久存在
        /// </summary>
        public void Init()
        {
            PlotUISettings.Instance.Init();
        }

        /// <summary>
        /// 在剧情开始前，设置好你的剧情配置文件
        /// </summary>
        /// <param name="plotConfig"></param>
        public void SetPlotConfig(CommandConfig plotConfig)
        {
            CommandSender.Instance.commandConfig = plotConfig;
        }

        /// <summary>
        /// 在剧情开始前，设置好你的剧情配置文件，路径在Resources文件夹下
        /// </summary>
        /// <param name="plotConfig"></param>
        public void SetPlotConfig(string plotConfigPath)
        {
            CommandConfig commandConfig = Resources.Load<CommandConfig>(plotConfigPath);
            if (commandConfig == null)
            {
                Debug.LogError("SetPlotConfig : path invalid.");
                return;
            }
            SetPlotConfig(commandConfig);
        }

        /// <summary>
        /// 设置屏幕分辨率大小
        /// </summary>
        /// <param name="screenPixel"></param>
        public void SetPixel(Vector2 screenPixel)
        {
            PlotUISettings.Instance.pixelSize = screenPixel;
        }

        /// <summary>
        /// 打字效果的时间间隔，推荐范围为 0.01f - 0.1f
        /// </summary>
        /// <param name="typingEffectTimeDevision"></param>
        public void SetTypingTimeDevision(float t)
        {
            PlotUISettings.Instance.typingEffectTimeDevision = t;
        }

        public List<int> GetPlayerDecisions()
        {
            return PlotUISettings.Instance.playerDecisions;
        }
    }

}
