using FairyGUI;

namespace plot_command_executor_fgui
{
    public class HEADER : ICommand
    {
        public string title;
        public bool is_skippable = true;
        public string fit_mode;

        public void Execute()
        {
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

        public void OnUpdate()
        {

        }

        public bool IsFinished()
        {
            return true;
        }

    }

}
