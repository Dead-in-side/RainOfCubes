using UnityEngine;

[RequireComponent(typeof(Renderer))]

public class Cube : MonoBehaviour, IRecreated
{
    private const string ColorShader = "_Color";

    private Renderer _renderer;
    private Color _color;
    public bool IsCollide { get; private set; } = false;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _color = _renderer.material.color;
    }

    public void Collide() => IsCollide = true;

    public void Init(Vector3 position)
    {
        IsCollide = false;
        
        transform.position = position;

        ReturnColorToDefault();

        gameObject.SetActive(true);
    }

    public void ChangeColor()
    {
        Color color = Random.ColorHSV();

        _renderer.material.SetColor(ColorShader, color);
    }

    private void ReturnColorToDefault() => _renderer.material.SetColor(ColorShader, _color);
}
