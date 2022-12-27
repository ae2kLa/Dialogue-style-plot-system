using System;

namespace plot
{
    public class CameraShake: EffectBase
    {
        public double duration { get; set; }
        public double xstrength { get; set; }
        public double ystrength { get; set; }
        public int vibrato { get; set; }
        public int randomness { get; set; }
        public bool fadeout { get; set; }

        protected override void Execute()
        {
            base.Execute();

            EventReceiver.Instance.CameraShake(duration, xstrength, ystrength, vibrato, randomness, fadeout);
        }
    }

}
