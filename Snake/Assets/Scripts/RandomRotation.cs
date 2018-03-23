using UnityEngine;
using System.Collections;

public class RandomRotation : MonoBehaviour
{
    void Update()
    {
        GetComponent<RectTransform>().rotation = Random.rotation;
    }
}
