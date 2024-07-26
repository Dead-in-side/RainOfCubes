using System.Collections;
using UnityEngine;

public class Spawner<T> : MonoBehaviour where T: MonoBehaviour, IRecreated
{
    [SerializeField] protected Pool<T> _pool;
    [SerializeField][Range(0,5)] private float _spawnDelay = 5;

    private Coroutine _coroutine;

    public int SpawnCounter { get; protected set; }

    private void Start()
    {
        _coroutine = StartCoroutine(SpawnObject());
        SpawnCounter = 0;
    }

    public virtual IEnumerator SpawnObject()
    {
        bool isWork = true;
        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(_spawnDelay);

        while (isWork)
        {
            T spawnedObject = _pool.GetObject();
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
