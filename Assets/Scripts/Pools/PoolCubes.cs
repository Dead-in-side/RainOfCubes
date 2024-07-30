using System;
using UnityEngine;
public class PoolCubes : Pool<Cube>
{
    public event Action<Vector3> CubeDisabled;

    public override void TakeObject(GameObject gameObject)
    {
        Cube spawnedObject = gameObject.GetComponent<Cube>();

        CubeDisabled?.Invoke(spawnedObject.transform.position);

        spawnedObject.LifetimeIsEnd -= TakeObject;

        spawnedObject.gameObject.SetActive(false);
        CounterOfActiveObject--;

        ObjectQueue.Enqueue(spawnedObject);

        Debug.Log(CounterOfActiveObject);
    }
}
