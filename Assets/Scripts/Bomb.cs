using System;
using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Rigidbody))]
public class Bomb : MonoBehaviour, IRecreated
{
    private const string ColorShader = "_Color";

    private Renderer _renderer;
    private Color _startColor;
    private float _explosionForce = 400;
    private float _explosionRadius = 10;
    private float _minDelay = 2;
    private float _maxDelay = 5;


    public event Action<GameObject> LifetimeIsEnd;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _startColor = _renderer.material.color;
    }

    public void Init(Vector3 position)
    {
        _renderer.material.SetColor(ColorShader, _startColor);

        transform.position = position;

        gameObject.SetActive(true);

        StartCoroutine(ExplodeCoroutine(UnityEngine.Random.Range(_minDelay, _maxDelay)));
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

        LifetimeIsEnd?.Invoke(gameObject);
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

