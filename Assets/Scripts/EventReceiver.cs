using FairyGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;


namespace plot
{
    public class EventReceiver : MonoSingleton<EventReceiver>
    {
        private GComponent dialogueRoot;

        protected void Awake()
        {
            dialogueRoot = GetComponent<UIPanel>().ui;
            dialogueRoot.MakeFullScreen();
            Debug.Log("EventReciver Instance Awake Done");
        }

        protected override void OnStart()
        {
            GLoader loader = dialogueRoot.GetChild("n4").asLoader;
            //TODO:和设置屏幕分辨率相关联
            loader.SetSize(1920, 1080);
        }

        public IEnumerator HEADER(string title, bool is_skippable, string fit_mode)
        {
            yield return null;
            Debug.Log("HEADER Done!");
        }

        public IEnumerator Background(string image = "white")
        {
            GLoader loader = dialogueRoot.GetChild("n4").asLoader;
            loader.url = image;
            yield return null;
            Debug.Log("Background Done!");
        }

        public IEnumerator BackgroundClose(float time = 2)
        {
            yield return new WaitForSeconds(time);
            Debug.Log("Background Close!");
        }

        public IEnumerator Dialogue(string name = "" , string text = "")
        {
            GTextField gtf = dialogueRoot.GetChild("n0").asTextField;
            gtf.text = name;
            gtf = dialogueRoot.GetChild("n1").asTextField;
            gtf.text = text;
            yield return new WaitForMouseButtonDown(1);
            Debug.Log("Dialogue Done!");
        }

        public IEnumerator Delay(float time)
        {
            yield return new WaitForSeconds(time);
            Debug.Log("Delay Done!");
        }



        public IEnumerator Decision(string options , string values)
        {

            yield return new WaitForMouseButtonDown(1);
        }

        public IEnumerator Predicate()
        {
            yield return new WaitForMouseButtonDown(1);
        }

        public IEnumerator CameraShake(double duration, double xstrength, double ystrength, int vibrato, int randomness, bool fadeout)
        {
            yield return null;
            Debug.Log("CameraShake Done!");
        }




    }

}
