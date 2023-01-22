using FairyGUI;
using plot_command_executor;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace plot_command_executor_fgui
{
    public class Decision : ICommand
    {
        public List<string> list;
        private GList gList;
        private GButton[] buttons;
        private System.Threading.Timer timer;
        private bool buttonClicked = false;

        public void Execute()
        {
            if (list == null) return;

            gList = new GList();
            gList.SetSize(800, 600);
            gList.SetPosition(PlotUISettings.Instance.pixelSize.x / 2 - 400, PlotUISettings.Instance.pixelSize.y / 2 - 300, 0f);
            gList.columnGap = 100;
            PlotUISettings.Instance.dialogueRoot.AddChild(gList);

            buttons = new GButton[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                //创建GButton
                //buttons[i] = new GButton();
                buttons[i] = UIPackage.CreateObject("Package1", "DecisionButton").asButton;
                buttons[i].onClick.Add(DetectButtonClicked);
                gList.AddChild(buttons[i]);
            }

            Debug.Log("Decision Done!");
        }

        public void OnUpdate()
        {

        }

        private void DetectButtonClicked()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i].selected == true)
                {
                    //不断出队，直至队头为 Predicate value == i 的下一个命令
                    while (CommandSender.Instance.PeekCommand() != null)
                    {
                        ICommand command = CommandSender.Instance.DequeueCommand();
                        Predicate p = command as Predicate;
                        if (p != null && p.value == i+1) break;
                    }
                    buttons[i].enabled = false;
                }
                else
                {
                    buttons[i].touchable = false;
                }
            }

            //TODO:补间动画
            //timer = new Timer(Destory, null, 2000, Timeout.Infinite);
            Destory(null);
        }

        private void Destory(object state)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Dispose();
            }

            gList.Dispose();
            buttonClicked = true;
        }


        public bool IsFinished()
        {
            Debug.Log(buttonClicked);
            return buttonClicked;
        }

    }

}

