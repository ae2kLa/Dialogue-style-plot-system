using plot_command_executor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace plot_command_executor
{
    public class CommandSender : MonoSingleton<CommandSender>
    {
        private TextParser textParser;
        public string filePath = "Assets/Text/level01.txt";

        [HideInInspector]
        public int i = 0;
        [HideInInspector]
        public MyCommand[] mc;

        private void Awake()
        {
            try
            {
                textParser = new TextParser(filePath);
            }
            catch (IOException ex)
            {
                UnityEngine.Debug.LogError("Error opening file: " + ex.Message);
            }
        }

        private void Start()
        {
            StartCoroutine(ExecuteAllCommands());
        }


        IEnumerator ExecuteAllCommands()
        {
            mc = textParser.ParserByLine();
            Type type = Type.GetType("plot.CommandReceiver");

            for (i = 0; i < mc.Length; i++)
            {

                MethodInfo method = type.GetMethod(mc[i].name, BindingFlags.Public | BindingFlags.Instance);
                ParameterInfo[] paramInfo = method.GetParameters();
                object[] invokeParams = new object[paramInfo.Length];

                Dictionary<string, object> parameters = textParser.ParseParameters(mc[i].parameter);

                #region 生成形参
                for (int j = 0; j < paramInfo.Length; j++)
                {
                    if (parameters.ContainsKey(paramInfo[j].Name))
                    {
                        invokeParams[j] = Convert.ChangeType(parameters[paramInfo[j].Name], paramInfo[j].ParameterType);
                    }
                    else if (paramInfo[j].HasDefaultValue)
                    {
                        invokeParams[j] = paramInfo[j].DefaultValue;
                    }
                    else
                    {
                        UnityEngine.Debug.LogError("Function has no default parameter!");
                    }
                }
                #endregion

                yield return StartCoroutine((IEnumerator)method.Invoke(CommandReceiver.Instance, invokeParams));
            }
        }


        public void FindPredicate(int targetChoice)
        {
            for(i++ ; i < mc.Length ; i++)
            {
                if (mc[i].name.Equals("Predicate"))
                {
                    if (mc[i].parameter == null) return;

                    Dictionary<string, object> ps = textParser.ParseParameters(mc[i].parameter);
                    int predicateNum = (int)Convert.ChangeType(ps["value"], typeof(int));
                    if (predicateNum == targetChoice) return;
                }
            }
        }

    }


}
