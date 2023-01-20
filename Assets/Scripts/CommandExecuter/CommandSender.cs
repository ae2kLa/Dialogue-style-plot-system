using FairyGUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;


namespace plot_command_executor
{
    public class CommandSender : MonoSingleton<CommandReceiver>
    {
        private Queue<ICommand> commandsQueue { get; set; }


        private GComponent dialogueRoot;
        [HideInInspector]
        public List<int> decisions = new List<int>();
        private float typingEffectTimeDevision = 0.01f;
        private Vector2 pixelSize = new Vector2(1920, 1080);

        protected void Awake()
        {
            dialogueRoot = GetComponent<UIPanel>().ui;
            dialogueRoot.MakeFullScreen();
            Debug.Log("CommandSender Instance Awake Done");
        }

        protected override void OnStart()
        {
            GLoader loader = dialogueRoot.GetChild("n4").asLoader;
            //TODO:和设置屏幕分辨率相关联
            loader.SetSize(pixelSize.x, pixelSize.y);
        }

        private void Update()
        {
            if (commandsQueue.Count != 0)
                commandsQueue.Peek().OnUpdate();
        }

        #region "Old implementation"
        //public IEnumerator HEADER(string title, bool is_skippable, string fit_mode)
        //{
        //    yield return null;
        //    Debug.Log("HEADER Done!");
        //}

        //public IEnumerator Background(string image = "grey")
        //{
        //    GLoader loader = dialogueRoot.GetChild("n4").asLoader;
        //    loader.url = image;
        //    yield return null;
        //    Debug.Log("Background Done!");
        //}

        //public IEnumerator BackgroundClose(float time = 2)
        //{
        //    yield return new WaitForSeconds(time);
        //    Debug.Log("Background Close!");
        //}

        //public IEnumerator Dialogue(string name = "", string text = "")
        //{
        //    GTextField gtf = dialogueRoot.GetChild("n0").asTextField;
        //    gtf.text = name;
        //    gtf = dialogueRoot.GetChild("n1").asTextField;
        //    gtf.text = text;
        //    TypingEffect typingEffect = new TypingEffect(gtf);
        //    typingEffect.Start();
        //    typingEffect.PrintAll(typingEffectTimeDevision);
        //    yield return new WaitForMouseButtonDown(1);
        //    Debug.Log("Dialogue Done!");
        //}

        //public IEnumerator Delay(float time)
        //{
        //    yield return new WaitForSeconds(time);
        //    Debug.Log("Delay Done!");
        //}


        //public IEnumerator Decision(string options)
        //{
        //    string[] parts = Regex.Split(options, ";");
        //    GButton[] buttons = new GButton[parts.Length];

        //    GList list = new GList();
        //    list.SetSize(800, 600);
        //    list.SetPosition(pixelSize.x/2 - 400 , pixelSize.y/2 - 300 , 0f);
        //    list.columnGap = 100;
        //    dialogueRoot.AddChild(list);

        //    for (int i = 0 ; i < parts.Length; i++)
        //    {
        //        //创建GButton
        //        //buttons[i] = new GButton();
        //        buttons[i] = UIPackage.CreateObject("Package1", "DecisionButton").asButton;
        //        list.AddChild(buttons[i]);
        //    }

        //    yield return new WaitForButtonClick(buttons);

        //    for (int i = 0; i < buttons.Length; i++)
        //    {
        //        buttons[i].Dispose();
        //    }
        //    list.Dispose();
        //    Debug.Log("Decision Done!");
        //}

        //public IEnumerator Predicate(int value = -1)
        //{
        //    if (value != -1)
        //    {
        //        CommandSender.Instance.FindPredicate(-1);
        //    }

        //    yield return null;
        //    Debug.Log("Predicate Done!");
        //}
        #endregion



    }

}
