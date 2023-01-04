
using System.Collections;
using UnityEngine;

namespace plot
{ 
    public class Dialogue : EffectBase
    {
        public string name { get; set; }
        public string text { get; set; }

        protected override void Execute()
        {
            EventReceiver.Instance.Dialogue(name , text);
        }
    }

}
