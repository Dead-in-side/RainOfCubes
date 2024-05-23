using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidDetector : MonoBehaviour
{
    [SerializeField] private Pool _pool;

    private void OnCollisionEnter(Collision collision)
    {
        Cube cube = collision.collider.GetComponent<Cube>();

        if (cube != null && !cube.IsCollide)
        {
            cube.Collide();

            cube.ChangeColor();

            _pool.TakeCube(cube);
        }
    }
}
