using FairyGUI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace plot_command_executor
{
    public class PlotUISettings : Singleton<PlotUISettings>
    {
        public PlotUISettings()
        {
            dialogueRoot = CommandSender.Instance.GetComponent<UIPanel>().ui;
            dialogueRoot.MakeFullScreen();

            PlotEventContainer.Instance.plotEnd.AddListener(()=>
            {
                ResetUI();
            });
        }

        public GComponent dialogueRoot;
        public SkipWindow skipWindow = null;
        public float typingEffectTimeDevision = 0.02f;
        public Vector2 pixelSize = new Vector2(1920, 1080);

        public List<int> playerDecisions = new List<int>();
        public string fguiPackagePath = "Assets/UI/Plot";
        public string fguiPackageName = "Plot";


        public void ResetUI()
        {
            //还原角色显示
            GComponent com = PlotUISettings.Instance.dialogueRoot.GetChild("character").asCom;
            GList gList = com.GetChild("list").asList;
            gList.numItems = 0;

            //还原文字显示
            GTextField n0_gtf = PlotUISettings.Instance.dialogueRoot.GetChild("name").asTextField;
            n0_gtf.text = "";
            GTextField n1_gtf = PlotUISettings.Instance.dialogueRoot.GetChild("text").asTextField;
            n1_gtf.text = "";
        }
    }

}
