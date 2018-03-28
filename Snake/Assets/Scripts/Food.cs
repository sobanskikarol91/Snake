using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour 
{
    public float delay=0.3f;

    void Start()
    {
        Invoke("PlayCreateFoodSnd", delay);
    }

    void PlayCreateFoodSnd()
    {
        GetComponent<AudioSource>().Play();
    }
}
