using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Rigidbody))]
public class Bomb : MonoBehaviour, IRecreated
{
    private const string ColorShader = "_Color";

    private Renderer _renderer;
    private Rigidbody _rigidbody;
    private Color _startColor;
    private float _explosionForce = 400;
    private float _explosionRadius = 10;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
        _startColor = _renderer.material.color;
    }

    public void Init(Vector3 position)
    {
        _renderer.material.SetColor(ColorShader, _startColor);

        transform.position = position;

        gameObject.SetActive(true);
    }

    public void StartCountdown(float delay)
    {
        StartCoroutine(ExplodeCoroutine(delay));
    }

    private IEnumerator ExplodeCoroutine(float delay)
    {
        Color tempColor = _startColor;

        float time = 0;

        while (time < delay)
        {
            time += Time.deltaTime;

            tempColor.a = Mathf.MoveTowards(tempColor.a, 0, Time.deltaTime / delay);

            _renderer.material.SetColor(ColorShader, tempColor);

            yield return null;
        }

        Explode();
    }

    private void Explode()
    {
        foreach(Rigidbody rigidbody in GetExplodableObjects())
        {
            rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }
    }

    private List<Rigidbody> GetExplodableObjects()
    {
        List<Rigidbody> explodableObjects = new List<Rigidbody>();

        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.attachedRigidbody != null)
            {
                explodableObjects.Add(collider.attachedRigidbody);
            }
        }

        return explodableObjects;
    }
}

