using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Rendering.FilterWindow;

public class BaseObjectPool : MonoBehaviour
{

    
    private BasePoolableObject _poolableObjectPrefab;

    [Min(1)][SerializeField] private int _initialAmount;
    
    private Queue<BasePoolableObject> _pool;

    private bool _initialized;

    private void Awake()
    {
        _pool = new Queue<BasePoolableObject>(_initialAmount);
    }

    public void InitializePool(GameObject prefab)
    {
        if (_initialized)
        {
            Debug.LogError($"[BaseObjectPool] - Pool from gameObject: '{gameObject.name}' is already initialized");
            return;
        }

        _poolableObjectPrefab = prefab.GetComponent<BasePoolableObject>();

        if(_poolableObjectPrefab == null)
        {
            Debug.LogError($"[BaseObjectPool] - prefab object: '{prefab.name}' does not contain a poolableObject component");
            return;
        }

        for (int i = 0; i < _initialAmount; i++)
            _pool.Enqueue(GenerateNewPoolableElement());

        _initialized = true;
    }

    public BasePoolableObject GetAvailableObject()
    {
        BasePoolableObject poolObject = null;

        if (_pool.Count > 0)
            poolObject = _pool.Peek();
        

        if (poolObject != null && poolObject.IsAvailable())
            return _pool.Dequeue();
        else
            return GenerateNewPoolableElement();
    }

    private BasePoolableObject GenerateNewPoolableElement()
    {
        BasePoolableObject newPoolableObject = Instantiate(_poolableObjectPrefab, new Vector3(1000f, 1000f, 1000f), Quaternion.identity);
        newPoolableObject.Initialize(this);
        newPoolableObject.gameObject.SetActive(false);

        return newPoolableObject;
    }
    
    public void DespawnObject(BasePoolableObject poolableObject)
    {
        _pool.Enqueue(poolableObject);
    }

}
