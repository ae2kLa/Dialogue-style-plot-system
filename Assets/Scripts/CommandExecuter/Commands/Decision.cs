using FairyGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace plot_command_executor_fgui
{
    public class Decision : ICommand
    {
        public List<string> list;

        public void Execute()
        {
            if (list == null) return;

            GButton[] buttons = new GButton[list.Count];

            GList gList = new GList();
            gList.SetSize(800, 600);
            gList.SetPosition(PlotUISettings.Instance.pixelSize.x / 2 - 400, PlotUISettings.Instance.pixelSize.y / 2 - 300, 0f);
            gList.columnGap = 100;
            PlotUISettings.Instance.dialogueRoot.AddChild(gList);

            for (int i = 0; i < list.Count; i++)
            {
                //´´½¨GButton
                //buttons[i] = new GButton();
                buttons[i] = UIPackage.CreateObject("Package1", "DecisionButton").asButton;
                gList.AddChild(buttons[i]);
            }

            //yield return new WaitForButtonClick(buttons);

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Dispose();
            }

            gList.Dispose();
            Debug.Log("Decision Done!");
        }

        public void OnUpdate()
        {
            throw new System.NotImplementedException();
        }

        public bool IsFinished()
        {
            throw new System.NotImplementedException();
        }

    }

}

