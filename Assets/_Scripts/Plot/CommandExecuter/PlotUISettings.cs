using FairyGUI;
using System.Collections.Generic;
using UnityEngine;

namespace plot_command_executor
{
    public class PlotUISettings : Singleton<PlotUISettings>
    {
        public PlotUISettings()
        {
            dialogueRoot = CommandSender.Instance.GetComponent<UIPanel>().ui;
            dialogueRoot.MakeFullScreen();
        }

        public GComponent dialogueRoot;
        public SkipWindow skipWindow = null;
        public float typingEffectTimeDevision = 0.02f;
        public Vector2 pixelSize = new Vector2(1920, 1080);

        public List<int> playerDecisions = new List<int>();


        public void EndPlot()
        {

        }

    }

}