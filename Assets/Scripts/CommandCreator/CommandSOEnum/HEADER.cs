using System;

namespace plot_command_creator
{
    [Serializable]
    public class HEADER : CommandBase_SO
    {
        public string title;
        public bool is_skippable = true;
        public string fit_mode;
    }
}