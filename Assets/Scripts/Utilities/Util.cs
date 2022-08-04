using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

public class Util
{
    public static string GetPathToFolder(string folder)
    {
        string path = "Assets";
        var info = new DirectoryInfo(path);

        DirectoryInfo[] directories = info.GetDirectories("*", SearchOption.AllDirectories);

        foreach (DirectoryInfo d in directories)
        {
            if (d.Name.Equals(folder))
            {
                string filePath = d.FullName;
                int length = filePath.Length - info.FullName.Length + path.Length;
                filePath = filePath.Substring(d.FullName.Length - length, length);
                return filePath;
            }
        }
        return "No File Found!";
    }

    public static string getName(FileInfo fileInfo)
    {
        return fileInfo.Name.Split('.')[0];
    }

    public static JsonData LoadCfgAsJsonData(string path)
    {
        if (!File.Exists(path))
        {
            Debug.Log("File does not exist.");
            throw new ArgumentNullException("File does not exist.");
        }
        //fix chinese code problem
        string JsonStr = File.ReadAllText(path);
        Regex reg = new Regex(@"(?i)\\[uU]([0-9a-f]{4})");
        JsonStr = reg.Replace(JsonStr, delegate (Match m) { return ((char)Convert.ToInt32(m.Groups[1].Value, 16)).ToString(); });
        return JsonMapper.ToObject(JsonStr);
    }
}
