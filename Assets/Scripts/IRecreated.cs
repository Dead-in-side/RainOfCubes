using System;
using UnityEngine;

public interface IRecreated
{
    public event Action<GameObject> LifetimeIsEnd;

    public void Init(Vector3 position);
}
