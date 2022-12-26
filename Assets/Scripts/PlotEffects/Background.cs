namespace plot
{
    public class Background: EffectBase
    {
        public string image { get; set; }
        public float fadetime { get; set; }
        public string screenadapt { get; set; }


        protected override void Execute()
        {
            base.Execute();

            EventReceiver.Instance.Background(image, fadetime, screenadapt);
        }
    }

}

