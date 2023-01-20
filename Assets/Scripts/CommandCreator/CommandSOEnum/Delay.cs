using System;

namespace plot_command_creator
{
    [Serializable]
    public class Delay : CommandBase_SO
    {
        public float time;
    }
}