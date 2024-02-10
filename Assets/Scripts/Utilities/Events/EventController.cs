using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController
{
    public Action baseEvent;

    public void AddEventListener(Action listener) => baseEvent += listener; 

    public void InvokeEvent() => baseEvent?.Invoke();

    public void RemoveEventListener(Action listener) => baseEvent -= listener;
}

public class EventController<T>
{
    public Action<T> baseEvent;

    public void AddEventListener(Action<T> listener) => baseEvent += listener;

    public void InvokeEvent(T type) => baseEvent?.Invoke(type);

    public void RemoveEventListener(Action<T> listener) => baseEvent -= listener;
}