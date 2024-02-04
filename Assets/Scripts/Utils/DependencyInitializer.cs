using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;


public class DependencyInitializer
{

    private Dictionary<Type, object> _dependencyCollection;

    public DependencyInitializer()
    {
        _dependencyCollection = new Dictionary<Type, object>();
    }

    public bool DoesDependencyExist(Type dependencyType) => _dependencyCollection.ContainsKey(dependencyType);
    

    public T GetDependencyByType<T>()
    {
        Type type = typeof(T);

        if (!_dependencyCollection.ContainsKey(type))
            throw new DependencyNotCreatedException($"[InterfaceAdaptersHolder -> GetInterfaceAdapterByType] - Object with type {type.Name} does not exist.");
        
        return (T)_dependencyCollection[type];
    }

    public bool CreateNewDependency(Type dependencyType, object dependencyObject)
    {
        if (_dependencyCollection.ContainsKey(dependencyType))
        {
            Debug.LogError($"[DependencyInitializer -> CreateNewDependency] -> Cannot create dependency of type {dependencyType}, it already exists");
            return false;
        }

        _dependencyCollection.Add(dependencyType, dependencyObject);

        return true;
    }

}

[Serializable]
internal class DependencyNotCreatedException : Exception
{
    public DependencyNotCreatedException()
    {
    }

    public DependencyNotCreatedException(string message) : base(message)
    {
    }

    public DependencyNotCreatedException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected DependencyNotCreatedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}