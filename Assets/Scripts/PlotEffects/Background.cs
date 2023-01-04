using System.Collections;
using UnityEngine;

namespace plot
{
    public class Background: EffectBase
    {
        public string image { get; set; }


        protected override void Execute()
        {
            EventReceiver.Instance.Background(image);
        }
    }

}

