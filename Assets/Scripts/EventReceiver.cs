using FairyGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace plot
{
    public class EventReceiver : MonoSingleton<EventReceiver>
    {
        private GComponent dialogueRoot;

        protected override void OnStart()
        {
            base.OnStart();

            dialogueRoot = GetComponent<UIPanel>().ui;

            Debug.Log("EventReciver Instance Done");
        }

        public void HEADER(string title, bool is_skippable, string fit_mode)
        {
            Debug.Log("HEADER Done!");
        }

        public void Background(string image, float fadetime, string screenadapt)
        {
            Debug.Log("Background Done!");
        }

        public void Dialogue(string name , string text)
        {
            GTextField gtf = dialogueRoot.GetChild("n0").asTextField;
            gtf.text = name;
            gtf = dialogueRoot.GetChild("n1").asTextField;
            gtf.text = text;
            Debug.Log("Dialogue Done!");
        }

        public void Delay(float delaytime)
        {
            Debug.Log("Delay Done!");
        }

        public void CameraShake(double Duration, double XStrength, double YStrength, int Vibrato, int Randomness, bool Fadeout)
        {
            Debug.Log("CameraShake Done!");
        }



    }

}
