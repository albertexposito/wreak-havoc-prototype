using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using UnityEngine;

public class JsonSerializationUtil : ISerializationUtil
{
    public bool SaveFile(string fullPath, object saveData)
    {
        try
        {
            string jsonString = JsonUtility.ToJson(saveData);
            File.WriteAllText($"{fullPath}", jsonString);

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

        string fullPathWithFormat = $"{fullPath}";

        if (!File.Exists(fullPathWithFormat))
        {
            //throw new FileNotFoundException($"File in path: {fullPath} does not exist!");
            Debug.LogError($"File in path: {fullPath} does not exist!");
            return false;
        }

        try
        {
            string jsonString = File.ReadAllText(fullPathWithFormat);

            loadedData = JsonUtility.FromJson<T>(jsonString);
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

}
