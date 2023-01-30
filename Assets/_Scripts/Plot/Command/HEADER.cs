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
            //注册装载器
            //UIObjectFactory.SetLoaderExtension(typeof(MyGLoader));
            PlotUISettings.Instance.dialogueRoot.SetSize(PlotUISettings.Instance.pixelSize.x, PlotUISettings.Instance.pixelSize.y);

            //创建HEADER并赋值
            UIPackage.AddPackage("Assets/UI/Package1");
            GComponent com = (GComponent)UIPackage.CreateObject("Package1", "HEADER");
            PlotUISettings.Instance.dialogueRoot.AddChild(com);
            com.SetSize(PlotUISettings.Instance.pixelSize.x, PlotUISettings.Instance.pixelSize.y);
            com.Center();
            com.GetChild("title").asTextField.text = title;

            //初始化按钮
            GButton skipButton = PlotUISettings.Instance.dialogueRoot.GetChild("skip_button").asButton;

            if (!is_skippable)
                skipButton.Dispose();
            else
            {
                skipButton.onClick.Add(() =>
                {
                    if (PlotUISettings.Instance.skipWindow == null)
                        PlotUISettings.Instance.skipWindow = new SkipWindow();
                    PlotUISettings.Instance.skipWindow.Show();
                    //TODO:加入过渡
                    PlotUISettings.Instance.skipWindow.SetConfirm();

                    //退出
                });
            }

            //动效末触发回调
            Transition trans = com.GetTransition("enter_plot");
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