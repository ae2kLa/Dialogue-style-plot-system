using FairyGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace plot_command_executor_fgui
{
    public class Background : ICommand
    {
        public string image;

        public void Execute()
        {
            GLoader loader = PlotUISettings.Instance.dialogueRoot.GetChild("n4").asLoader;
            loader.url = image;
            Debug.Log("Background Done!");
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
