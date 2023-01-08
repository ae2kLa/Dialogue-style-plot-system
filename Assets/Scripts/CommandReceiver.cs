using FairyGUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;


namespace plot
{
    public class CommandReceiver : MonoSingleton<CommandReceiver>
    {
        private GComponent dialogueRoot;
        public List<int> decisions = new List<int>();
        private float typingEffectTimeDevision = 0.01f;

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

        public IEnumerator Dialogue(string name = "", string text = "")
        {
            GTextField gtf = dialogueRoot.GetChild("n0").asTextField;
            gtf.text = name;
            gtf = dialogueRoot.GetChild("n1").asTextField;
            gtf.text = text;
            TypingEffect typingEffect = new TypingEffect(gtf);
            typingEffect.Start();
            typingEffect.PrintAll(typingEffectTimeDevision);
            yield return new WaitForMouseButtonDown(1);
            Debug.Log("Dialogue Done!");
        }

        public IEnumerator Delay(float time)
        {
            yield return new WaitForSeconds(time);
            Debug.Log("Delay Done!");
        }


        public IEnumerator Decision(string options)
        {
            string[] parts = Regex.Split(options, ";");
            GButton[] buttons = new GButton[parts.Length];
            for(int i = 0 ; i < parts.Length; i++)
            {
                //创建GButton

            }
            yield return new WaitForButtonClick(buttons);
            Debug.Log("Decision Done!");
        }

        public IEnumerator Predicate(int value)
        {
            yield return new WaitForMouseButtonDown(1);
            Debug.Log("Predicate Done!");
        }

        public IEnumerator CameraShake(double duration, double xstrength, double ystrength, int vibrato, int randomness, bool fadeout)
        {
            yield return null;
            Debug.Log("CameraShake Done!");
        }




    }

}
