using System.Collections;
using UnityEngine;

public class PoolBombs : Pool<Bomb>
{
    public override IEnumerator WaitDesableObject(Bomb spawnedObject)
    {
        float delay = Random.Range(MinDelay, MaxDelay);

        spawnedObject.StartCountdown(delay);

        yield return new WaitForSecondsRealtime(delay);

        spawnedObject.gameObject.SetActive(false);

        _objectQueue.Enqueue(spawnedObject);
    }
}
