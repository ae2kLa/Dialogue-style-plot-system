using FairyGUI;
using plot_utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace plot_command_executor
{
    public class CommandSender : MonoSingleton<CommandSender>
    {
        public string filePath = "Assets/Scripts/CommandCreator/Text/PlotCommandConfig.txt";
        public string commandNameSpace = "plot_command_executor_fgui";
        private Queue<ICommand> commandQueue { get; set; }
        private bool isExecuted = false;

        protected void Awake()
        {
            commandQueue = this.GetCommandsQueue(filePath, commandNameSpace);
            Debug.Log("CommandSender Instance Awake Done");
        }

        protected override void OnStart()
        {

        }

        private void Update()
        {
            if (commandQueue.Count == 0) return;

            ICommand command = commandQueue.Peek();
            if (!isExecuted)
            {
                command.Execute();
                isExecuted = true;
            }
            command.OnUpdate();
            if (command.IsFinished())
            {
                commandQueue.Dequeue();
                isExecuted = false;
            }
        }

        public Queue<ICommand> GetCommandsQueue(string filePath, string commandNameSpace)
        {
            MyCommand[] mc = TextParser.ParserByLine(filePath);
            Queue<ICommand> commandsQueue = new Queue<ICommand>();

            for (int i = 0; i < mc.Length; i++)
            {        
                Type commandType = Type.GetType(commandNameSpace + "." + mc[i].name);
                object command = Activator.CreateInstance(commandType);
                TextParser.AssignCommandParams(command, commandType, mc[i].parameter);
                commandsQueue.Enqueue(command as ICommand);
            }

            return commandsQueue;
        }

        public ICommand PeekCommand()
        {
            return commandQueue.Peek();
        }

        public ICommand DequeueCommand()
        {
            return commandQueue.Dequeue();
        }
    }

}
