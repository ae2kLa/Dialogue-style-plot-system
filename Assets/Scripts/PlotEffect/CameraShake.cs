namespace plot
{
    public class CameraShake: EffectBase
    {
        public double Duration { get; set; }
        public int XStrength { get; set; }
        public int YStrength { get; set; }
        public int Vibrato { get; set; }
        public int Randomness { get; set; }
        public bool Fadeout { get; set; }
        public bool Block { get; set; }
    }

}
