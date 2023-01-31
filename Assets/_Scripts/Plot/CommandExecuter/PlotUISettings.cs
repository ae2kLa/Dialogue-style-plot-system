using FairyGUI;
using System.Collections.Generic;
using UnityEngine;

namespace plot_command_executor
{
    public struct DialogueContent
    {
        public string talker_name;
        public string talker_text;
    }

    public class PlotUISettings : Singleton<PlotUISettings>
    {
        public PlotUISettings()
        {
            dialogueRoot = CommandSender.Instance.GetComponent<UIPanel>().ui;
            dialogueRoot.MakeFullScreen();

            PlotEventContainer.Instance.plotBegin.AddListener(() =>
            {
                SetUI();
            });

            PlotEventContainer.Instance.plotEnd.AddListener(()=>
            {
                ResetUI();
            });
        }

        public GComponent dialogueRoot;
        public SkipWindow skipWindow = null;
        public float typingEffectTimeDevision = 0.02f;
        public Vector2 pixelSize = new Vector2(1920, 1080);

        public List<DialogueContent> dialogueContents = new List<DialogueContent>();
        public List<int> playerDecisions = new List<int>();
        public string fguiPackagePath = "Assets/UI/Plot";
        public string fguiPackageName = "Plot";

        public void SetUI()
        {
            //初始化跳过按钮
            GButton skipButton = PlotUISettings.Instance.dialogueRoot.GetChild("skip_button").asButton;
            skipButton.onClick.Set(() =>
            {
                if (PlotUISettings.Instance.skipWindow == null)
                    PlotUISettings.Instance.skipWindow = new SkipWindow();
                PlotUISettings.Instance.skipWindow.Show();
            });

            //初始化显示历史记录按钮
            GButton showHistoryButton = PlotUISettings.Instance.dialogueRoot.GetChild("show_history_button").asButton;
            showHistoryButton.onClick.Set(() =>
            {
                UIPackage.AddPackage(PlotUISettings.Instance.fguiPackagePath);
                GComponent com = (GComponent)UIPackage.CreateObject(PlotUISettings.Instance.fguiPackageName, "ConversationHistory");
                PlotUISettings.Instance.dialogueRoot.AddChild(com);
                com.SetSize(PlotUISettings.Instance.pixelSize.x, PlotUISettings.Instance.pixelSize.y);
                com.Center();

                GList gList = com.GetChild("list").asList;
                gList.Center();
                gList.SetVirtual();
                gList.itemRenderer = (int index, GObject obj) =>
                {
                    var com = obj.asCom;
                    com.GetChild("name").asTextField.text = PlotUISettings.Instance.dialogueContents[index].talker_name;
                    com.GetChild("text").asTextField.text = PlotUISettings.Instance.dialogueContents[index].talker_text;
                };
                gList.numItems = PlotUISettings.Instance.dialogueContents.Count;
                gList.ScrollToView(gList.numItems - 1);
                //Debug.Log(PlotUISettings.Instance.dialogueContents.Count);

                GButton gButton = com.GetChild("close_button").asButton;
                gButton.onClick.Set(() => {
                    com.Dispose();
                });
                gButton.GetChild("text").asTextField.text = "X";
            });
        }

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

            //还原历史记录
            PlotUISettings.Instance.dialogueContents.Clear();

            //清除选项按钮（若有）
            foreach (var child in PlotUISettings.Instance.dialogueRoot.GetChildren())
            {
                if(child.name == "DecisionButtonList")
                {
                    child.Dispose();
                    break;
                }
            }
        }
    }

}
