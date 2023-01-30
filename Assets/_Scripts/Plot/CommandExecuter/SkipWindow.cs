using FairyGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace plot_command_executor
{
    public class SkipWindow : Window
    {
        public SkipWindow()
        {
            
        }

        protected override void OnInit()
        {
            UIPackage.AddPackage(PlotUISettings.Instance.fguiPackagePath);
            this.contentPane = UIPackage.CreateObject(PlotUISettings.Instance.fguiPackageName, "SkipWindow").asCom;
            this.SetSize(PlotUISettings.Instance.pixelSize.x, PlotUISettings.Instance.pixelSize.y);
            this.Center();
            SetConfirmButton();
        }

        override protected void OnShown()
        {

        }

        public void SetConfirmButton()
        {
            GButton gButton = this.contentPane.GetChild("frame").asCom.GetChild("confirmButton").asButton;

            gButton.onClick.Set(() =>{
                UIPackage.AddPackage(PlotUISettings.Instance.fguiPackagePath);
                GComponent com = (GComponent)UIPackage.CreateObject(PlotUISettings.Instance.fguiPackageName, "END");
                PlotUISettings.Instance.dialogueRoot.AddChild(com);
                com.SetSize(PlotUISettings.Instance.pixelSize.x, PlotUISettings.Instance.pixelSize.y);
                com.Center();

                com.alpha = 0;

                var gtween = TweenManager.GetTween(com, TweenPropType.Alpha);
                if (gtween != null)
                    gtween.Kill(false);

                gtween = com.TweenFade(1, 1.5f).OnComplete(() => {

                    foreach (var child in PlotUISettings.Instance.dialogueRoot.GetChildren())
                    {
                        if (child != com)
                            child.visible = false;
                        //if(child.name == "")
                    }

                    gtween = com.TweenFade(0, 1f).OnComplete(() => {
                        gtween = null;

                        PlotUISettings.Instance.dialogueRoot.visible = false;
                        foreach (var child in PlotUISettings.Instance.dialogueRoot.GetChildren())
                        {
                            child.visible = true;
                        }

                        PlotEventContainer.Instance.plotEnd.Invoke();
                        com.Dispose();
                    });
                });

                Debug.Log("END");
                Hide();
            });
        }
    }

}
