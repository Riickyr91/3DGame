
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void Save<T>(string path, T data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void Load<T>(string path, out T data)
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            data = (T) formatter.Deserialize(stream);
            stream.Close();

        } else
        {
            Debug.LogError("Save file not found in " + path);
            data = default(T);
        }
    }
}
