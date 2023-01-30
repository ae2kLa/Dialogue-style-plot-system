using FairyGUI;
using plot_command_executor;
using System;
using UnityEngine;

namespace plot_command
{
    [Serializable]
    public class Dialogue : CommandBase
    {
        [field: SerializeField]
        public string talker_name;
        [field: SerializeField]
        public string talker_text;


        private float startTime;
        private int textLength;
        private TypingEffect typingEffect;
        private bool isFinished = false;

        public override void Execute()
        {
            isFinished = false;

            GTextField n0_gtf = PlotUISettings.Instance.dialogueRoot.GetChild("name").asTextField;
            n0_gtf.text = talker_name;
            GTextField n1_gtf = PlotUISettings.Instance.dialogueRoot.GetChild("text").asTextField;
            n1_gtf.text = talker_text;

            typingEffect = new TypingEffect(n1_gtf);
            typingEffect.Start();
            typingEffect.PrintAll(PlotUISettings.Instance.typingEffectTimeDevision);

            startTime = Time.time;
            textLength = talker_text.Length;
        }

        public override void OnUpdate()
        {
            GObject mouseTargetObj = GRoot.inst.touchTarget;
            GObject frame = PlotUISettings.Instance.dialogueRoot.GetChild("frame");
            Debug.Log(mouseTargetObj == frame && PlotUISettings.Instance.dialogueRoot.IsAncestorOf(mouseTargetObj));
            if (Time.time < startTime + textLength * PlotUISettings.Instance.typingEffectTimeDevision)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (mouseTargetObj == frame && PlotUISettings.Instance.dialogueRoot.IsAncestorOf(mouseTargetObj))
                    {
                        typingEffect.Cancel();
                        startTime = Time.time - textLength * PlotUISettings.Instance.typingEffectTimeDevision;
                    }
                }
            }
            else if (Input.GetMouseButtonDown(0))
            {
                if (mouseTargetObj == frame && PlotUISettings.Instance.dialogueRoot.IsAncestorOf(mouseTargetObj))
                {
                    typingEffect.Cancel();
                    startTime = Time.time - textLength * PlotUISettings.Instance.typingEffectTimeDevision;
                    isFinished = true;
                    Debug.Log("Dialogue Done!");
                }
            }
        }

        public override bool IsFinished()
        {
            return isFinished;
        }
    }
}