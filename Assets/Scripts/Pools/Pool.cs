using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _recreatedObject;

    protected Queue<T> _ObjectQueue = new();

    public float MinDelay { get; private set; } = 2;
    public float MaxDelay { get; private set; } = 5;

    public int CounterOfActiveObject { get; protected set; } = 0;

    public T GetObject()
    {
        T spawnedObject;

        if (_ObjectQueue.Count > 0)
        {
            spawnedObject = _ObjectQueue.Dequeue();
            CounterOfActiveObject++;

            return spawnedObject;
        }
        else
        {
            spawnedObject = CreateObject();
            CounterOfActiveObject++;

            return spawnedObject;
        }


    }

    public void TakeObject(T spawnedObject)
    {
        StartCoroutine(WaitDesableObject(spawnedObject));
    }

    protected virtual IEnumerator WaitDesableObject(T spawnedObject)
    {
        yield return new WaitForSecondsRealtime(Random.Range(MinDelay, MaxDelay));

        spawnedObject.gameObject.SetActive(false);
        CounterOfActiveObject--;

        _ObjectQueue.Enqueue(spawnedObject);
    }

    private T CreateObject() => Instantiate(_recreatedObject);
}
