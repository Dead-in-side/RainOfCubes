using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]

public class Cube : MonoBehaviour
{
    private const string ColorShader = "_Color";

    private Renderer _renderer;
    private Color _color;
    public bool IsCollide { get; private set; } = false;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _color = _renderer.material.color;
    }

    public void Collide() => IsCollide = true;

    public void ReturnToPool()=> IsCollide = false;

    public void ChangeColor()
    {
        Color color = Random.ColorHSV();

        _renderer.material.SetColor(ColorShader, color);
    }

    public void ReturnColorToDefault() => _renderer.material.SetColor(ColorShader, _color);
}
