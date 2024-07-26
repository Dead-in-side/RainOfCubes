using System.Linq;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class GameStatistics: MonoBehaviour
{
    [SerializeField] private BombSpawner _bombSpawner;
    [SerializeField] private CubeSpawner _cubeSpawner;

    private TextMeshProUGUI _text;
    private int _counterOfActiveObjects;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        Cube[] cubeActive = FindObjectsOfType<Cube>().Where(cube=>cube.gameObject.activeInHierarchy).ToArray();
        Bomb[] bombActive = FindObjectsOfType<Bomb>().Where(bomb => bomb.gameObject.activeInHierarchy).ToArray();

        _counterOfActiveObjects = cubeActive.Length + bombActive.Length;

        ChangeText();
    }

    private void ChangeText()
    {
        _text.text = $"Колличество бомб: {_bombSpawner.SpawnCounter}, количество кубов - {_cubeSpawner.SpawnCounter}, активных объектов на сцене: {_counterOfActiveObjects}.";
    }
}
