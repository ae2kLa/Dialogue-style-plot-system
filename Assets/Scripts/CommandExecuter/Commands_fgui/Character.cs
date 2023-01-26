using FairyGUI;
using plot_command_executor;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

namespace plot_command_executor_fgui
{
    public class Character : ICommand
    {
        public string name1;
        public string name2;
        public int focus;

        int cnt;
        private string[] images;

        public void Execute()
        {
            cnt = (string.IsNullOrEmpty(name1) ? 0 : 1) + (string.IsNullOrEmpty(name2) ? 0 : 1);

            if (cnt == 0) return;

            images = new string[cnt];
            images[0] = (string.IsNullOrEmpty(name1) ? name2 : name1);
            if (cnt == 2) images[1] = name2;

            GComponent com = PlotUISettings.Instance.dialogueRoot.GetChild("character").asCom;
            GList gList = com.GetChild("list").asList;
            gList.itemRenderer = RenderListItem;
            gList.numItems = cnt;
        }

        private void RenderListItem(int index, GObject obj)
        {
            GComponent com = obj.asCom;
            GLoader gLoader = com.GetChild("image").asLoader;
            gLoader.url = images[index];

            if(cnt == 2 && focus == index + 1 || cnt == 1)
            {
                Transition trans = com.GetTransition("focus");
                trans.Play();
            }
            else
            {
                Transition trans = com.GetTransition("leave_focus");
                trans.Play();
            }
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
