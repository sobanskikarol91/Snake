using UnityEngine;
using System.Collections.Generic;

// Contains snake animation settings and methods.
public class SnakeAnimation : MonoBehaviour
{
    private const string eatingAnim = "PlayEating";
    private const string destroyAnim = "PlayDeath";
    private float eatingAnimationDelay = 0.1f;
    private float destroyAnimationDelay = 0.15f;
    private float sumDestroyAnimationDelay = 2f;


    public void PlayEating(List<SnakeTile> tilesList)
    {
        PlayDelayedAnimation(tilesList, eatingAnim, eatingAnimationDelay);
    }

    public void PlayDeath(List<SnakeTile> tilesList, int snakeSize)
    {
        float time = Mathf.Min(destroyAnimationDelay, sumDestroyAnimationDelay / snakeSize);
        PlayDelayedAnimation(tilesList, destroyAnim, time);
    }

    void PlayDelayedAnimation(List<SnakeTile> tilesList, string name, float delay)
    {
        for (int nr = 0, i = tilesList.Count - 1; i >= 0; i--, nr++)
            tilesList[i].Invoke(name, delay * nr);
    }
}
