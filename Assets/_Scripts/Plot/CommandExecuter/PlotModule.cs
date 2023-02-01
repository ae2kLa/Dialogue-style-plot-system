using plot_command_creator;
using plot_command_executor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace plot_module
{
    public class PlotModule : Singleton<PlotModule>
    {
        /// <summary>
        /// 每次要进剧情就调这个
        /// </summary>
        public UnityEvent plotBegin = new UnityEvent();

        /// <summary>
        /// 每次剧情结束，补间动画放完就自动调这个，如果有必要，你可以对它注册一些方法
        /// </summary>
        public UnityEvent plotEnd = new UnityEvent();

        /// <summary>
        /// 初始化的方法，需传入UI的预制体，它应该在CommandExecuter文件夹下。全局只需初始化一次即可，界面会持久存在
        /// </summary>
        public void PlotInit(GameObject ui_prefab)
        {
            if(ui_prefab == null)
            {
                Debug.Log("请检查UI预制体是否绑定到了PlotEventContainer脚本上。");
                return;
            }

            GameObject.Instantiate(ui_prefab);
            PlotUISettings.Instance.dialogueRoot.visible = false;
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
