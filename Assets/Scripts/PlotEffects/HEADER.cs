namespace plot
{
    public class HEADER : EffectBase
    {
        public string title { get; set; }
        public bool is_skippable { get; set; }
        public string fit_mode { get; set; }

        protected override void Execute()
        {
            base.Execute();

            //UnityEngine.Debug.Log(title + " " + is_skippable + " " + fit_mode);

            //if(is_skippable == true)
            //    UnityEngine.Debug.Log("bool convert success!");
            EventReceiver.Instance.HEADER(title, is_skippable, fit_mode);
        }

    }

}


