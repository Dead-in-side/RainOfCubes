using UnityEngine;

public class CollidDetector : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Cube cube) && !cube.IsCollide)
        {
            cube.Collide();
        }
    }
}
