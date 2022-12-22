using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

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

            foreach (string line in lines)
            {
                //UnityEngine.Debug.Log(line);
            }

            ParserByLine();
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
            UnityEngine.Debug.Log(res[i].name);
            res[i].parameter = GetParam(lines[i] , res[i].name);
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


    public List<Dictionary<string, object>> GetParam(string _line , string head)
    {
        Regex regex = new Regex(@"(?<=\()([^)]+)(?=\))");
        Match match = regex.Match(_line);

        if (match.Success)
        {
            string pattern = @"([^,]+=[^,]+)";
            MatchCollection matches = Regex.Matches(match.Value, pattern);

            foreach (Match m in matches)
            {
                UnityEngine.Debug.Log(m.Groups[1].Value.Trim());
                List<Dictionary<string, object>> res = new List<Dictionary<string, object>>();
                
            }
        }

        return null;
    }

}

public struct MyCommand
{
    public string name;
    public List<Dictionary<string, object>> parameter;
};