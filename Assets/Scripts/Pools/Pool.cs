using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _recreatedObject;

    protected Queue<T> _objectQueue = new();
    private Coroutine _coroutine;

    public float MinDelay { get; private set; } = 2;
    public float MaxDelay { get; private set; } = 5;
    
    public T GetObject()
    {
        T spawnedObject;

        if (_objectQueue.Count > 0)
        {
            spawnedObject = _objectQueue.Dequeue();
            return spawnedObject;
        }
        else
        {
            spawnedObject = CreateObject();
            return spawnedObject;
        }
    }

    public void TakeObject(T spawnedObject)
    {
        _coroutine = StartCoroutine(WaitDesableObject(spawnedObject));
    }

    public virtual IEnumerator WaitDesableObject(T spawnedObject)
    {
        yield return new WaitForSecondsRealtime(Random.Range(MinDelay, MaxDelay));

        spawnedObject.gameObject.SetActive(false);

        _objectQueue.Enqueue(spawnedObject);
    }

    private T CreateObject() => Instantiate(_recreatedObject);
}
