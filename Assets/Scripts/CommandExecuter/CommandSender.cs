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
        public string commandNameSpace = "plot_command_executor_fgui";
        public string filePath = "Assets/Scripts/CommandCreator/Text/PlotCommandConfig.txt";
        private Queue<ICommand> commandQueue { get; set; }
        private ICommand currentCommand = null;
        private bool isExecuted = false;

        protected void Awake()
        {
            commandQueue = this.GetCommandsQueue(filePath, commandNameSpace);
            Debug.Log("CommandSender Instance Awake Done");
        }

        protected override void OnStart()
        {
            currentCommand = commandQueue.Dequeue();
        }

        private void Update()
        {
            if (currentCommand == null) return;

            if (!isExecuted)
            {
                currentCommand.Execute();
                isExecuted = true;
            }

            currentCommand.OnUpdate();

            if (currentCommand.IsFinished())
            {
                if (commandQueue.Count == 0)
                    currentCommand = null;
                else
                    currentCommand = commandQueue.Dequeue();

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

        public int GetCommandsCount()
        {
            return commandQueue.Count;
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
