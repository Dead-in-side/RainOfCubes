using System.Collections;
using UnityEngine;

public class PoolBombs : Pool<Bomb>
{
    protected override IEnumerator WaitDesableObject(Bomb spawnedObject)
    {
        float delay = Random.Range(MinDelay, MaxDelay);

        spawnedObject.StartCountdown(delay);

        yield return new WaitForSecondsRealtime(delay);

        spawnedObject.gameObject.SetActive(false);
        CounterOfActiveObject--;

        _ObjectQueue.Enqueue(spawnedObject);
    }
}
