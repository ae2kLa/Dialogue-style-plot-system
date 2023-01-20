using System;

namespace plot_command_creator
{
    [Serializable]
    public class Dialogue : CommandBase_SO
    {
        public string talker_name;
        public string talker_text;
    }
}