using FairyGUI;
using plot_command_executor;
using System;
using UnityEditor;
using UnityEngine;

namespace plot_command
{
    [Serializable]
    public class Background : CommandBase
    {
        [field: SerializeField]
        private Sprite image;

        public override void Execute()
        {
            //Debug.Log(AssetDatabase.GetAssetPath(image));
            GLoader loader = PlotUISettings.Instance.dialogueRoot.GetChild("background").asLoader;
            loader.texture = new NTexture(image.texture);
            //Debug.Log("Background Done!");
        }

        public override void OnUpdate()
        {

        }

        public override bool IsFinished()
        {
            return true;
        }
    }
}