using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace plot_command_executor
{
    public class PlotEventContainer : Singleton<PlotEventContainer>
    {
        /// <summary>
        /// 每次要进剧情就调这个
        /// </summary>
        public UnityEvent plotBegin = new UnityEvent();

        /// <summary>
        /// 每次剧情结束，补间动画放完就自动调这个，如果有必要，你可以利用它来注册一些方法
        /// </summary>
        public UnityEvent plotEnd = new UnityEvent();

        /// <summary>
        /// 初始化的方法，需传入UI的预制体，应该在CommandExecuter文件夹下。全局只需调用一次即可
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
    }

}
