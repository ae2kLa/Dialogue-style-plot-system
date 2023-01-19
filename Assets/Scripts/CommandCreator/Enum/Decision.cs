using System;
using System.Collections.Generic;
using UnityEngine;

namespace plot_command_creator
{
    [Serializable]
    public class Decision : CommandBase
    {
        public List<string> list;
    }
}