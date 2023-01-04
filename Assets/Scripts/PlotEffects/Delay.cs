using System.Collections;
using UnityEngine;

namespace plot
{
    public class Delay: EffectBase
    {
        public float time { get; set; }

        protected override void Execute()
        {
            EventReceiver.Instance.Delay(time);
        }
    }

}

