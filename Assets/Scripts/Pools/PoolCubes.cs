using System;
using System.Collections;
using UnityEngine;
public class PoolCubes : Pool<Cube>
{
    public event Action<Vector3> CubeDisabled;

    public override IEnumerator WaitDesableObject(Cube spawnedObject)
    {
        yield return new WaitForSecondsRealtime(UnityEngine.Random.Range(MinDelay, MaxDelay));

        CubeDisabled?.Invoke(spawnedObject.transform.position);

        spawnedObject.gameObject.SetActive(false);

        _objectQueue.Enqueue(spawnedObject);
    }
}
