using FairyGUI;
using plot_command_executor;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace plot_command
{
    [Serializable]
    public class Decision : CommandBase
    {
        [field: SerializeField]
        public List<string> list;

        private GComponent com;
        private GList gList;
        private bool buttonClicked = false;
        public override void Execute()
        {
            buttonClicked = false;

            if (list == null) return;

            UIPackage.AddPackage(PlotUISettings.Instance.fguiPackagePath);
            com = (GComponent)UIPackage.CreateObject(PlotUISettings.Instance.fguiPackageName, "ButtonList");
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
                    CommandBase command = CommandSender.Instance.DequeueCommand();
                    Predicate p = command as Predicate;
                    if (p != null && p.value == (ValueRange)Enum.ToObject(typeof(ValueRange) , index)) break;
                }

                for (int i = 0; i < gList.numChildren; i++)
                    gList.GetChildAt(i).touchable = false;

                Destory(null);
            });
        }

        public override void OnUpdate()
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

        public override bool IsFinished()
        {
            return buttonClicked;
        }
    }
}