using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public static class PathManager 
{
    public static List<string> filenames = new List<string>();

    public static void AddPath(string filename)
    {
        filenames.Add(filename);
    }

    public static string GetPath(string filename)
    {
        string path;
        for(int i = 0; i < filenames.Count; i++)
        {
            path = Application.persistentDataPath + "/" + filenames[i] + ".dat";
            if (filenames[i].Equals(filename) && File.Exists(path))
            {
                return path;
            }
        }

        return null;
    }

}
