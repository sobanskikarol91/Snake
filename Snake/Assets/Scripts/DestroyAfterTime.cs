using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour
{
    public float time = 2f;

    void Start()
    {
        Destroy(gameObject, time);
    }
}
