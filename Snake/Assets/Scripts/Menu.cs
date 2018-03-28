using UnityEngine;

public class Menu : MonoBehaviour
{
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    protected virtual void OnEnable()
    {
        anim.SetTrigger("show");
    }
}
