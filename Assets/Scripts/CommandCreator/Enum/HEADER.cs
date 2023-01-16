using System;

namespace plot
{

    [Serializable]
    public class HEADER : CommandBase
    {
        public string title;
        public bool is_skippable;
        public string fit_mode;
    }

}