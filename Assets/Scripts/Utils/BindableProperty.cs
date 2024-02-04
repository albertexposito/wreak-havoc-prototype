using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BindableProperty<T>
{
    public T Value
    {
        get
        {
            return _value;
        }
        internal set
        {
            _value = value;
            _onValueChange?.Invoke(_value);
        }
    }

    private T _value;

    private event Action<T> _onValueChange;

    public void BindToProperty(Action<T> methodToBind, bool executeActionOnBinding = false)
    {
        _onValueChange += methodToBind;

        if (executeActionOnBinding)
            methodToBind.Invoke(_value);
    }

    public void UnbindFromProperty(Action<T> methodToUnbind)
    {
        _onValueChange -= methodToUnbind;
    }

    internal void ForceValueUpdate() => _onValueChange?.Invoke(_value);

}
