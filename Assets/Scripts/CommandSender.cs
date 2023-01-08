using plot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor.Timeline.Actions;
using UnityEngine;

namespace plot
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


        #region old function
        //IEnumerator ExecuteAllCommands()
        //{
        //    MyCommand[] mc = textParser.ParserByLine();

        //    for (int i = 0; i < mc.Length; i++)
        //    {
        //        Type type = Type.GetType("plot." + mc[i].name);
        //        object obj = Activator.CreateInstance(type);
        //        Dictionary<string, object> parameters = textParser.ParseParameters(mc[i].parameter);

        //        foreach (var p in parameters)
        //        {
        //            PropertyInfo property = type.GetProperty(p.Key);
        //            property.SetValue(obj, Convert.ChangeType(p.Value, property.PropertyType));
        //        }

        //        MethodInfo m = type.GetMethod("Execute", BindingFlags.NonPublic | BindingFlags.Instance);
        //        m.Invoke(obj, null);
        //        yield return new WaitForSeconds(1);
        //    }
        //}
        #endregion

        #region new function
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

                //生成形参
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

                yield return StartCoroutine((IEnumerator)method.Invoke(CommandReceiver.Instance, invokeParams));
            }
        }
        #endregion

        public void FindPredicate(ref int i , MyCommand[] mc ,int targetChoice)
        {
            for(i++ ; i < mc.Length ; i++)
            {
                if (mc[i].name.Equals("Predicate"))
                {
                    Dictionary<string, object> ps = textParser.ParseParameters(mc[i].parameter);
                    int predicateNum = (int)Convert.ChangeType(ps["value"], Type.GetType("int"));
                    if (predicateNum == targetChoice)
                        return;
                }
            }
        }

    }


}
