using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SnakeAnimation : MonoBehaviour
{
    private const string eatingAnim = "PlayEating";
    private const string destroyAnim = "PlayDeath";
    private float eatingAnimationDelay = 0.1f;
    private float destroyAnimationDelay = 0.15f;



    public void PlayEating(List<SnakeTile> tilesList)
    {
        PlayDelayedAnimation(tilesList, eatingAnim, eatingAnimationDelay);
    }

    public void PlayDeath(List<SnakeTile> tilesList)
    {
        PlayDelayedAnimation(tilesList, destroyAnim, destroyAnimationDelay);
    }

    void PlayDelayedAnimation(List<SnakeTile> tilesList, string name, float delay)
    {
        for (int nr = 0, i = tilesList.Count - 1; i >= 0; i--, nr++)
            tilesList[i].Invoke(name, delay * nr);
    }
}
