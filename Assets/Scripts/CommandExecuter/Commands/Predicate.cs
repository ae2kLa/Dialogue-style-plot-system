using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace plot_command_executor_fgui
{
    public class Predicate : ICommand
    {
        int value;

        public Predicate(int value)
        {

        }

        public void Execute()
        {
            throw new System.NotImplementedException();
        }

        public void OnUpdate()
        {
            throw new System.NotImplementedException();
        }

    }

}