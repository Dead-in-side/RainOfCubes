using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _maxPosition;
    [SerializeField] private Pool _pool;
    [SerializeField][Range(0,5)] private float _spawnDelay = 5;

    private float _positionY;
    private Coroutine _coroutine;

    private void Start()
    {
        _positionY = transform.position.y;

        _coroutine = StartCoroutine(SpawnCubes());
    }

    private IEnumerator SpawnCubes()
    {
        bool isWork = true;
        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(_spawnDelay);

        while (isWork)
        {
            Cube cube = _pool.GetCube();
            yield return delay;
        }
    }
}
