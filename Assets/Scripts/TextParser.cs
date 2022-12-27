using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;


namespace plot
{

    public class TextParser
    {
        string[] lines;

        public TextParser(){ throw new ArgumentException("No valid file path!"); }

        public TextParser(string _filePath)
        {
            try
            {
                lines = File.ReadAllLines(_filePath);

                ExecuteAllCommands();
            }
            catch (IOException ex)
            {
                UnityEngine.Debug.Log("Error opening file: " + ex.Message);
            }
        }

        public string GetName(string _line)
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
        public string GetParam(string _line, string head)
        {
            Regex regex = new Regex(@"(?<=\()([^)]+)(?=\))");
            Match match = regex.Match(_line);

            if (match.Success)
                return match.Value;

            return null;
        }
        #endregion


        private Dictionary<string, object> ParseParameters(string param)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            MatchCollection matches = Regex.Matches(param, @"(\w+)\s*=\s*((?:""[^""]*""|true|false|\d+(?:.\d+)?)+)");

            foreach (Match match in matches)
            {
                string key = match.Groups[1].Value;
                string value = match.Groups[2].Value;

                UnityEngine.Debug.Log(key);
                UnityEngine.Debug.Log(value);

                // 去除两端的双引号
                if (value.StartsWith("\"") && value.EndsWith("\""))
                {
                    value = value.Substring(1, value.Length - 2);
                }

                parameters.Add(key, value);
            }
            return parameters;
        }

        public MyCommand[] ParserByLine()
        {
            MyCommand[] res = new MyCommand[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                res[i].name = GetName(lines[i]);
                res[i].parameter = GetParam(lines[i], res[i].name);

                UnityEngine.Debug.Log(res[i].name);
                UnityEngine.Debug.Log(res[i].parameter);
            }

            return res;
        }

        private void ExecuteAllCommands()
        {
            MyCommand[] mc = ParserByLine();

            for (int i = 0; i < mc.Length; i++)
            {
                Type type = Type.GetType("plot." + mc[i].name);
                object obj = Activator.CreateInstance(type);
                Dictionary<string, object> parameters = ParseParameters(mc[i].parameter);

                foreach (var p in parameters)
                {
                    PropertyInfo property = type.GetProperty(p.Key);
                    property.SetValue(obj, Convert.ChangeType(p.Value, property.PropertyType));
                }

                MethodInfo m = type.GetMethod("Execute", BindingFlags.NonPublic | BindingFlags.Instance);
                m.Invoke(obj, null);
            }
        }

    }

    public struct MyCommand
    {
        public string name;
        public string parameter;
    };

}