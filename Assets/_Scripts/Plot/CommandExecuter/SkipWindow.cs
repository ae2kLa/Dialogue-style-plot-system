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
            this.contentPane = UIPackage.CreateObject("Package1", "SkipWindow").asCom;
            this.Center();
        }

        override protected void OnShown()
        {

        }

        public void SetConfirm()
        {
            GButton gButton = this.contentPane.GetChild("frame").asCom.GetChild("confirmButton").asButton;
            gButton.onClick.Set(() =>{ PlotUISettings.Instance.dialogueRoot.Dispose(); });
        }
    }

}
