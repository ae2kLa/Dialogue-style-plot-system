using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace plot_command_executor_fgui
{
    public class Delay : ICommand
    {
        public float time;
        private float startTime;

        public void Execute()
        {
            startTime = Time.time;
        }

        public void OnUpdate()
        {

        }

        public bool IsFinished()
        {
            return Time.time > startTime + time;
        }
    }

}

