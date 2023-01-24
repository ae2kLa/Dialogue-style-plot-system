using FairyGUI;
using FairyGUI.Utils;
using plot_command_executor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEditor.Progress;

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

            UIPackage.AddPackage("Assets/UI/Package1");
            GComponent com = (GComponent)UIPackage.CreateObject("Package1", "ButtonList");
            PlotUISettings.Instance.dialogueRoot.AddChild(com);
            gList = com.GetChild("n0").asList;
            com.Center();
            gList.Center();

            gList.itemRenderer = RenderListItem;
            gList.numItems = list.Count;
            gList.onClickItem.Add(DetectButtonClicked);

            buttons = new GButton[list.Count];
            for (int i = 0; i < list.Count; i++)
                buttons[i] = gList.GetChildAt(i).asButton;
        }

        private void RenderListItem(int index, GObject obj)
        {
            GButton button = obj.asButton;
            button.GetChild("text").asTextField.text = list[index];
            button.onClick.Set(DetectButtonClicked);
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
            Debug.Log("Decision Done!");
        }

        public bool IsFinished()
        {
            //Debug.Log(buttonClicked);
            return buttonClicked;
        }

    }

}

