using System;
using UnityEngine;
using plot_command_executor;

namespace plot_command
{
    [Serializable]
    public abstract class CommandBase
    {
        [HideInInspector]
        [SerializeField]
        protected string class_name = "asd";

        public CommandBase()
        {
            class_name = this.GetType().Name;
        }

        public abstract void Execute();

        public abstract void OnUpdate();

        public abstract bool IsFinished();
    }

}