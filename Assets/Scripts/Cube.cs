using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof (Rigidbody))]
public class Cube : MonoBehaviour, IRecreated
{
    private const string ColorShader = "_Color";

    private Renderer _renderer;
    private Color _color;
    private float _minDelay = 2;
    private float _maxDelay = 5;

    public bool IsCollide { get; private set; } = false;

    public event Action<GameObject> LifetimeIsEnd;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _color = _renderer.material.color;
    }

    public void Collide()
    {
        IsCollide = true;

        ChangeColor();

        StartCoroutine(WaitForEndLifetime());
    }

    public void Init(Vector3 position)
    {
        IsCollide = false;
        
        transform.position = position;

        ReturnColorToDefault();

        gameObject.SetActive(true);
    }

    public void ChangeColor()
    {
        Color color =UnityEngine.Random.ColorHSV();

        _renderer.material.SetColor(ColorShader, color);
    }

    private void ReturnColorToDefault() => _renderer.material.SetColor(ColorShader, _color);

    private IEnumerator WaitForEndLifetime()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(_minDelay, _maxDelay));

        LifetimeIsEnd?.Invoke(gameObject);
    }
}
