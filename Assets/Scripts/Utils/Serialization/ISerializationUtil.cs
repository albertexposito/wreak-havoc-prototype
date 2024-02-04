using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISerializationUtil
{

    bool SaveFile(string fullPath, object saveData);
    bool LoadFile<T>(string fullPath, out T loadedData);


}
