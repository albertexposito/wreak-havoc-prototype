using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class BinarySerializationUtil : ISerializationUtil
{

    private BinaryFormatter _formatter;

    public BinarySerializationUtil()
    {
        _formatter = new BinaryFormatter();
    }

    public bool SaveFile(string fullPath, object saveData)
    {
        try
        { 
            FileStream file = File.Open(fullPath, FileMode.OpenOrCreate);
            _formatter.Serialize(file, saveData);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool LoadFile<T>(string fullPath, out T loadedData)
    {
        loadedData = default;
        bool result = false;

        if (!File.Exists(fullPath))
            throw new FileNotFoundException($"File in path: {fullPath} does not exist!");

        FileStream file = File.Open(fullPath, FileMode.Open);

        try
        {
            loadedData = (T)_formatter.Deserialize(file);
            result = true;
        }
        catch (SerializationException serEx)
        {
            // TODO show error message!
        }
        catch (InvalidCastException invCastEx)
        {
            // TODO show error message!
        }

        return result;
    }

    /// 

    //public static bool Save(string saveName, object saveData)
    //{
    //    BinaryFormatter formatter = GetBinaryFormatter();

    //    if (!Directory.Exists(Application.persistentDataPath + "/saves"))
    //    {
    //        Directory.CreateDirectory(Application.persistentDataPath + "/saves");
    //    }

    //    string path = $"{Application.persistentDataPath}/saves/{saveName}.save";

    //    FileStream file = File.Create(path);

    //    formatter.Serialize(file, saveData);

    //    file.Close();

    //    return true;
    //}



    //public static object Load(string path)
    //{
    //    if (!File.Exists(path))
    //    {
    //        return null;
    //    }

    //    BinaryFormatter formatter = GetBinaryFormatter();

    //    FileStream file = File.Open(path, FileMode.Open);

    //    try
    //    {
    //        object save = formatter.Deserialize(file);
    //        file.Close();
    //        return save;
    //    }
    //    catch (SerializationException exception)
    //    {
    //        Debug.LogError($"Failed to load file at {path}, Message: {exception.Message}");
    //        file.Close();
    //        return null;
    //    }

    //}

    //public static BinaryFormatter GetBinaryFormatter()
    //{
    //    BinaryFormatter formatter = new BinaryFormatter();

    //    return formatter;
    //}
}
