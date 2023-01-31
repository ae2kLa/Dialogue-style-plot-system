using plot_command;
using plot_command_creator;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace plot_command_executor
{
    public class CommandSender : MonoSingleton<CommandSender>
    {
        public CommandConfig commandConfig;
        private Queue<CommandBase> commandQueue { get; set; }
        private CommandBase currentCommand = null;
        private bool isExecuted = false;

        protected void Awake()
        {
            PlotEventContainer.Instance.plotBegin.AddListener(() => {
                if (commandQueue != null || currentCommand != null) return;

                commandQueue = this.GetCommandsQueue(this.commandConfig);
                currentCommand = commandQueue.Dequeue();
                isExecuted = false;
            });

            PlotEventContainer.Instance.plotEnd.AddListener(() => {
                commandQueue = null;
                currentCommand = null;
                isExecuted = false;
            });
        }

        protected override void OnStart()
        {

        }

        private void Update()
        {
            if (commandQueue == null || currentCommand == null) return;

            if (!isExecuted)
            {
                currentCommand.Execute();
                isExecuted = true;
            }

            currentCommand.OnUpdate();

            if (currentCommand.IsFinished())
            {
                if (commandQueue.Count == 0)
                {
                    currentCommand = null;
                }
                else
                {
                    currentCommand = commandQueue.Dequeue();
                }

                isExecuted = false;
            }
        }

        public Queue<CommandBase> GetCommandsQueue(CommandConfig config)
        {
            Queue<CommandBase> commandsQueue = new Queue<CommandBase>(config.commandList);
            return commandsQueue;
        }

        public int GetCommandsCount()
        {
            return commandQueue.Count;
        }

        public CommandBase PeekCommand()
        {
            return commandQueue.Peek();
        }

        public CommandBase DequeueCommand()
        {
            return commandQueue.Dequeue();
        }
    }

}
