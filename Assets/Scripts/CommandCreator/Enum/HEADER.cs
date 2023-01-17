using System;
using System.Collections.Generic;

namespace plot
{
    [Serializable]
    public class HEADER : CommandBase
    {
        public string title;
        public bool is_skippable;
        public string fit_mode;
        public List<string> list;
    }
}