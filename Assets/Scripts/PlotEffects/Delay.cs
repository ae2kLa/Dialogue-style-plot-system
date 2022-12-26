using System;

namespace plot
{
    public class Delay: EffectBase
    {
        public float time { get; set; }

        protected override void Execute()
        {
            base.Execute();

            EventReceiver.Instance.Delay(time);
        }
    }

}

