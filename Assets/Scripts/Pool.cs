using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Pool : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;

    private Queue<Cube> _cubeQueue = new();
    private float _minDelay = 2;
    private float _maxDelay = 5;
    private Coroutine _coroutine;

    public Cube GetCube()
    {
        if (_cubeQueue.Count > 0)
        {
            Cube cube = _cubeQueue.Dequeue();
            cube.transform.position = GetRandomPosition();
            cube.gameObject.SetActive(true);

            return cube;
        }
        else
        {
            return CreateCube();
        }
    }

    public void TakeCube(Cube cube)
    {
        _coroutine = StartCoroutine(WaitDesableCube(cube));
    }

    private IEnumerator WaitDesableCube(Cube cube)
    {
        yield return new WaitForSecondsRealtime(Random.Range(_minDelay, _maxDelay));

        cube.ReturnColorToDefault();

        cube.ReturnToPool();

        cube.gameObject.SetActive(false);

        _cubeQueue.Enqueue(cube);
    }

    private Cube CreateCube()
    {
        Quaternion cubeRotation = transform.rotation;
        Vector3 cubePosition = GetRandomPosition();

        return Instantiate(_cubePrefab,cubePosition,cubeRotation);
    }

    private Vector3 GetRandomPosition()
    {
        float position = 20;
        return new Vector3(Random.Range(-position, position), transform.position.y, Random.Range(-position, position));
    }
}
