using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace plot
{

    public class TextParser
    {
        string[] lines;

        public TextParser(){ UnityEngine.Debug.LogError("No valid file path!"); }

        public TextParser(string _filePath)
        {
            try
            {
                lines = File.ReadAllLines(_filePath);
            }
            catch (IOException ex)
            {
                UnityEngine.Debug.Log("Error opening file: " + ex.Message);
            }
        }

        private string GetName(string _line)
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
        private string GetParam(string _line)
        {
            Regex regex = new Regex(@"(?<=\()([^)]+)(?=\))");
            Match match = regex.Match(_line);

            if (match.Success)
                return match.Value;

            return null;
        }
        #endregion

        public Dictionary<string, object> ParseParameters(string param)
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


        public MyCommand[] ParserByLine()
        {
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
    }

    public struct MyCommand
    {
        public string name;
        public string parameter;
    };

}