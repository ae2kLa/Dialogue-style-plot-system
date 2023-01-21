using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace plot_command_executor_fgui
{
    public class HEADER : ICommand
    {
        public string title;
        public bool is_skippable = true;
        public string fit_mode;

        public void Execute()
        {
            Debug.Log("HEADER done");
        }

        public void OnUpdate()
        {

        }

        public bool IsFinished()
        {
            return true;
        }

    }

}
