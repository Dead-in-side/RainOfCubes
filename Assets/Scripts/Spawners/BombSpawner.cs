using System.Collections;
using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private PoolCubes _cubesPool;

    private Vector3 _position;

    private void Start()
    {
        _position = transform.position;

        StartCoroutine(SpawnObject());
    }

    private void OnEnable()
    {
        _cubesPool.CubeDisabled += ChangeSpawnPosition;
    }

    private void OnDisable()
    {
        _cubesPool.CubeDisabled -= ChangeSpawnPosition;
    }

    private void ChangeSpawnPosition(Vector3 position)=> _position = position;

    public override IEnumerator SpawnObject()
    {
        bool isWork = true;
        while (isWork)
        {
            if (_position != transform.position)
            {
                Bomb spawnedObject = _pool.GetObject();
                SpawnCounter++;
                spawnedObject.Init(_position);
                _position = transform.position;
                _pool.TakeObject(spawnedObject);
            }

            yield return null;
        }
    }
}
