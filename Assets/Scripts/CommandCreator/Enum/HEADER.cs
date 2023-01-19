using System;
using System.Collections.Generic;

namespace plot_command_creator
{
    [Serializable]
    public class HEADER : CommandBase
    {
        public string title;
        public bool is_skippable = true;
        public string fit_mode;
    }
}