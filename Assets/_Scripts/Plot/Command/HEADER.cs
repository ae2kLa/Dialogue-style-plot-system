using FairyGUI;
using plot_command_executor;
using plot_utils;
using System;
using UnityEngine;

namespace plot_command
{
    [Serializable]
    public class HEADER : CommandBase
    {
        [field: SerializeField]
        public string title;
        [field: SerializeField]
        public bool is_skippable = true;
        [field: SerializeField]
        public string fit_mode;

        public override void Execute()
        {
            PlotUISettings.Instance.dialogueRoot.SetSize(PlotUISettings.Instance.pixelSize.x, PlotUISettings.Instance.pixelSize.y);

            //先隐藏其他组件
            foreach (var child in PlotUISettings.Instance.dialogueRoot.GetChildren())
            {
                child.visible = false;
            }

            //以后可能需要注册装载器
            //UIObjectFactory.SetLoaderExtension(typeof(MyGLoader));

            //创建HEADER并赋值
            UIPackage.AddPackage(PlotUISettings.Instance.fguiPackagePath);
            GComponent com = (GComponent)UIPackage.CreateObject(PlotUISettings.Instance.fguiPackageName, "HEADER");
            PlotUISettings.Instance.dialogueRoot.AddChild(com);
            com.SetSize(PlotUISettings.Instance.pixelSize.x, PlotUISettings.Instance.pixelSize.y);
            com.Center();
            com.GetChild("title").asTextField.text = title;

            //显示组件
            PlotUISettings.Instance.dialogueRoot.visible = true;

            //初始化按钮
            GButton skipButton = PlotUISettings.Instance.dialogueRoot.GetChild("skip_button").asButton;

            if (!is_skippable)
                skipButton.Dispose();
            else
            {
                skipButton.onClick.Set(() =>
                {
                    if (PlotUISettings.Instance.skipWindow == null)
                        PlotUISettings.Instance.skipWindow = new SkipWindow();
                    PlotUISettings.Instance.skipWindow.Show();
                });
            }

            //动效关键帧触发回调
            Transition trans = com.GetTransition("enter_plot");
            trans.SetHook("full_black", () =>
            {
                foreach (var child in PlotUISettings.Instance.dialogueRoot.GetChildren())
                {
                    child.visible = true;
                }
            });

            trans.SetHook("done", () =>
            {
                com.Dispose();
            });
        }

        public override void OnUpdate()
        {

        }

        public override bool IsFinished()
        {
            return true;
        }
    }
}