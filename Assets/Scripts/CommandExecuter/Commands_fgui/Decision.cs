using FairyGUI;
using plot_command_executor;
using System.Collections.Generic;
using UnityEngine;

namespace plot_command_executor_fgui
{
    public class Decision : ICommand
    {
        public List<string> list;

        private GComponent com;
        private GList gList;
        private bool buttonClicked = false;
        public void Execute()
        {
            if (list == null) return;

            UIPackage.AddPackage("Assets/UI/Package1");
            com = (GComponent)UIPackage.CreateObject("Package1", "ButtonList");
            PlotUISettings.Instance.dialogueRoot.AddChild(com);
            com.Center();
            gList = com.GetChild("list").asList;
            gList.Center();
            gList.itemRenderer = RenderListItem;
            gList.numItems = list.Count; 
        }

        private void RenderListItem(int index, GObject obj)
        {
            GButton button = obj.asButton;
            button.title = index.ToString();
            button.GetChild("text").asTextField.text = list[index];
            button.onClick.Set((EventContext context) =>
            {
                //不断出队，直至队头为 Predicate value == i 的下一个命令
                while (CommandSender.Instance.GetCommandsCount() != 0)
                {
                    ICommand command = CommandSender.Instance.DequeueCommand();
                    Predicate p = command as Predicate;
                    if (p != null && p.value == index + 1) break;
                }

                for (int i = 0; i < gList.numChildren; i++)
                    gList.GetChildAt(i).touchable = false;

                Destory(null);
            });
        }

        public void OnUpdate()
        {

        }

        private void Destory(object state)
        {
            for (int i = 0; i < gList.numChildren; i++)
                gList.GetChildAt(i).Dispose();

            gList.Dispose();
            com.Dispose();
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

