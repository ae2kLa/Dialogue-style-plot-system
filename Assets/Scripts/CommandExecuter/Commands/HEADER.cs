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
            throw new System.NotImplementedException();
        }

        public void OnUpdate()
        {
            if(Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.Space))
            {
                
            }
        }

    }

}
