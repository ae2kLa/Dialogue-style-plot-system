using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;

namespace plot_utils
{
    public static class TextParser
    {
        private static string GetName(string _line)
        {
            Regex regex = new Regex(@"(?<=\[)\w+");
            Match match = regex.Match(_line);

            if (match.Success)
                return match.Value;

            return null;
        }


        #region"Old GetParam Function"

        //public List<Dictionary<string, object>> GetParam(string _line , string head)
        //{
        //    Regex regex = new Regex(@"(?<=\()([^)]+)(?=\))");
        //    Match match = regex.Match(_line);

        //    if (match.Success)
        //    {
        //        string pattern = @"([^,]+=[^,]+)";
        //        MatchCollection matches = Regex.Matches(match.Value, pattern);

        //        foreach (Match m in matches)
        //        {
        //            UnityEngine.Debug.Log(m.Groups[1].Value.Trim());
        //            List<Dictionary<string, object>> res = new List<Dictionary<string, object>>();

        //        }
        //    }

        //    return null;
        //}
        #endregion

        #region"New GetParam Function"
        private static string GetParam(string _line)
        {
            Regex regex = new Regex(@"(?<=\()([^)]+)(?=\))");
            Match match = regex.Match(_line);

            if (match.Success)
                return match.Value;

            return null;
        }
        #endregion

        private static Dictionary<string, object> ParseParameters(string param)
        {
            if (string.IsNullOrEmpty(param)) return new Dictionary<string, object>();

            Dictionary<string, object> parameters = new Dictionary<string, object>();

            MatchCollection matches = Regex.Matches(param, @"(\w+)\s*=\s*((?:""[^""]*""|True|False|\d+(?:.\d+)?)+)");

            foreach (Match match in matches)
            {
                string key = match.Groups[1].Value;
                string value = match.Groups[2].Value;

                //UnityEngine.Debug.Log(key);
                //UnityEngine.Debug.Log(value);

                // 去除两端的双引号
                if (value.StartsWith("\"") && value.EndsWith("\""))
                {
                    value = value.Substring(1, value.Length - 2);
                }

                parameters.Add(key, value);
            }
            return parameters;
        }


        public static MyCommand[] ParserByLine(string _filePath)
        {
            string[] lines = File.ReadAllLines(_filePath);
            MyCommand[] res = new MyCommand[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                res[i].name = GetName(lines[i]);
                res[i].parameter = GetParam(lines[i]);

                //UnityEngine.Debug.Log(res[i].name);
                //UnityEngine.Debug.Log(res[i].parameter);
            }

            return res;
        }

        #region "解析参数，并为command对象的每个字段赋值"
        public static void AssignCommandParams(object command, Type commandType, string parameter)
        {
            Dictionary<string, object> parameters = TextParser.ParseParameters(parameter);
            FieldInfo[] fields = commandType.GetFields();

            for (int j = 0; j < fields.Length; j++)
            {
                if (parameters.ContainsKey(fields[j].Name))
                {
                    var value = Convert.ChangeType(parameters[fields[j].Name], fields[j].FieldType);
                    fields[j].SetValue(command, value);
                }
                else if (fields[j].FieldType.IsGenericType)
                {
                    //解析IList
                    if (typeof(IList).IsAssignableFrom(fields[j].FieldType))
                    {
                        //试图适配所有基本类型但有困难
                        //Type elemnetType = Type.GetType(parameters["elementType"] as string);
                        //Type listType = typeof(List<>);
                        //Type[] typeArgs = { elemnetType };
                        //Type genericListType = listType.MakeGenericType(typeArgs);
                        //object list = Activator.CreateInstance(genericListType);

                        //if (parameters.ContainsKey("elementType"))
                        //{
                        //    fields[j].SetValue(command, list);
                        //    continue;
                        //}

                        Type elemnetType = typeof(String);
                        Type listType = typeof(List<>);
                        Type[] typeArgs = { elemnetType };
                        Type genericListType = listType.MakeGenericType(typeArgs);
                        object list = Activator.CreateInstance(genericListType);

                        MethodInfo addMethod = genericListType.GetMethod("Add");

                        for (int k = 1; k < parameters.Count; k++)
                        {
                            object[] objs = new object[1] { parameters["element" + k] };
                            addMethod.Invoke(list, objs);
                            continue;
                        }

                        fields[j].SetValue(command, list);
                    }
                    //解析字典
                }
                else
                {
                    Debug.LogError("The parameters " + "does not have " + commandType + " 's field: " + fields[j].Name);
                }
            }
        }
        #endregion
    }

    public struct MyCommand
    {
        public string name;
        public string parameter;
    };

}