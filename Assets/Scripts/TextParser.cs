using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using static UnityEditor.Progress;

public class TextParser
{
    string[] lines;

    public TextParser(){ throw new ArgumentException("No valid file path!"); }

    public TextParser(string _filePath)
    {
        _filePath = "Assets/Text/level01.txt";

        try
        {
            FileStream fs = new FileStream(_filePath, FileMode.Open, FileAccess.Read);
            lines = File.ReadAllLines(_filePath);
            fs.Close();

            //foreach (string line in lines)
            //{
            //    UnityEngine.Debug.Log(line);
            //}

            MyCommand[] mc = ParserByLine();
            Type type = Type.GetType(mc[0].name);
            object obj = Activator.CreateInstance(type);
            Dictionary<string, object> parameters = ParseParameters(mc[0].parameter);

            foreach (var p in parameters)
            {
                PropertyInfo property = type.GetProperty(p.Key);
                property.SetValue(obj, Convert.ChangeType(p.Value, property.PropertyType));
            }

            MethodInfo m = type.GetMethod("Execute");
            m.Invoke(obj , null);
        }
        catch (IOException ex)
        {
            UnityEngine.Debug.Log("Error opening file: " + ex.Message);
        }
    }

    public MyCommand[] ParserByLine()
    {
        MyCommand[] res = new MyCommand[lines.Length];
        
        for(int i = 0; i < lines.Length; i++)
        {
            res[i].name = GetName(lines[i]);
            res[i].parameter = GetParam(lines[i] , res[i].name);

            UnityEngine.Debug.Log(res[i].name);
            UnityEngine.Debug.Log(res[i].parameter);
        }

        return res;
    }

    public string GetName(string _line)
    {
        Regex regex = new Regex(@"(?<=\[)\w+");
        Match match = regex.Match(_line);

        if (match.Success)
            return match.Value;

        return null;
    }


    #region"Old Function"

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

    #region"New Function"
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

        MatchCollection matches = Regex.Matches(param, @"(\w+)\s*=\s*([^,]+)");

        foreach (Match match in matches)
        {
            string key = match.Groups[1].Value;
            string value = match.Groups[2].Value;

            // 去除两端的双引号
            if (value.StartsWith("\"") && value.EndsWith("\""))
            {
                value = value.Substring(1, value.Length - 2);
            }

            parameters.Add(key, value);
        }
        return parameters;
    }


}

public struct MyCommand
{
    public string name;
    public string parameter;
};