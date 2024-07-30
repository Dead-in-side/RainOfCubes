using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class GameStatistics : MonoBehaviour
{
    [SerializeField] private BombSpawner _bombSpawner;
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private PoolBombs _poolBombs;
    [SerializeField] private PoolCubes _poolCubes;

    private TextMeshProUGUI _text;
    private int _counterOfActiveObjects;
    private int _sumOfCounters;
    private int _tempSumOfCounters;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _tempSumOfCounters = 0;
    }

    private void Update()
    {
        _counterOfActiveObjects = _poolBombs.CounterOfActiveObject + _poolCubes.CounterOfActiveObject;

        _sumOfCounters = _counterOfActiveObjects + _bombSpawner.SpawnCounter + _cubeSpawner.SpawnCounter;

        if (_tempSumOfCounters != _sumOfCounters)
        {
            ChangeText();

            _tempSumOfCounters = _sumOfCounters;
        }
    }

    private void ChangeText()
    {
        _text.text = $"Колличество бомб: {_bombSpawner.SpawnCounter}, количество кубов: {_cubeSpawner.SpawnCounter}, активных объектов на сцене: {_counterOfActiveObjects}.";
    }
}