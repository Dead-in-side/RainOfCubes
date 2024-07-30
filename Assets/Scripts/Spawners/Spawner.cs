using System.Collections;
using UnityEngine;

public class Spawner<T> : MonoBehaviour where T: MonoBehaviour, IRecreated
{
    [SerializeField] protected Pool<T> Pool;
    [SerializeField] private float _spawnDelay = 5;

    public int SpawnCounter { get; protected set; }

    private void Start()
    {
        StartCoroutine(SpawnObject());
        SpawnCounter = 0;
    }

    public virtual IEnumerator SpawnObject()
    {
        bool isWork = true;
        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(_spawnDelay);

        while (isWork)
        {
            T spawnedObject = Pool.GetObject();
            SpawnCounter++;
            spawnedObject.Init(GetPosition());
            yield return delay;
        }
    }

    private Vector3 GetPosition()
    {
        float position = 20;
        return new Vector3(Random.Range(-position, position), transform.position.y, Random.Range(-position, position));
    }
}
