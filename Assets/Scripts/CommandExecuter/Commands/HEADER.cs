using FairyGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

            UIPackage.AddPackage("Assets/UI/Package1");
            GComponent com = (GComponent)UIPackage.CreateObject("Package1", "HEADER");
            com.SetSize(PlotUISettings.Instance.pixelSize.x, PlotUISettings.Instance.pixelSize.y);
            PlotUISettings.Instance.dialogueRoot.AddChild(com);
            com.GetChild("title").asTextField.text = title;

            Debug.Log("HEADER done");
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
