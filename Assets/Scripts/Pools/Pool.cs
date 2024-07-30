using System.Collections.Generic;
using UnityEngine;

public class Pool<T> : MonoBehaviour where T : MonoBehaviour, IRecreated
{
    [SerializeField] private T _recreatedObject;

    protected Queue<T> ObjectQueue = new();

    public int CounterOfActiveObject { get; protected set; } = 0;

    public T GetObject()
    {
        T spawnedObject;

        if (ObjectQueue.Count > 0)
        {
            spawnedObject = ObjectQueue.Dequeue();
        }
        else
        {
            spawnedObject = CreateObject();
        }

        spawnedObject.LifetimeIsEnd += TakeObject;
        CounterOfActiveObject++;

        return spawnedObject;
    }

    public virtual void TakeObject(GameObject gameObject)
    {
        T spawnedObject = gameObject.GetComponent<T>();

        spawnedObject.LifetimeIsEnd -= TakeObject;

        spawnedObject.gameObject.SetActive(false);
        CounterOfActiveObject--;

        ObjectQueue.Enqueue(spawnedObject);
    }

    private T CreateObject() => Instantiate(_recreatedObject);
}
