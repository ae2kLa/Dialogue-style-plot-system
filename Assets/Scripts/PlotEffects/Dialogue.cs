
using UnityEngine;

namespace plot
{ 
    public class Dialogue : EffectBase
    {
        public string name { get; set; }
        public string text { get; set; }

        protected override void Execute()
        {
            base.Execute();

            EventReceiver.Instance.Dialogue(name , text);
        }
    }

}
