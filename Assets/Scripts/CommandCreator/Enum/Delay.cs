using System;

namespace plot_command_creator
{
    [Serializable]
    public class Delay : CommandBase
    {
        public float time;
    }
}