using UnityEngine;
using System.Collections;

public class BoardTileAnimator : MonoBehaviour
{
    public MinMax timer = new MinMax(1, 3);
    public MinMax offset = new MinMax(1, 2);
    public MinMax speed = new MinMax(10, 12);
    public bool PlayingAnimationIsDone { get; private set; }
    private Animator anim;


    void Start()
    {
        anim = GetComponent<Animator>();

        if (!GameManager.instance.instantStart)
            StartCoroutine("IEDelayShow");
    }

    IEnumerator IEDelayShow()
    {
        float timeToStart = Random.Range(timer.min, timer.max);

        yield return new WaitForSeconds(timeToStart);

        float offsetY = Random.Range(offset.min, offset.max);
        float randomSpeed = Random.Range(speed.min, speed.max);
        RectTransform currentPositionRT = GetComponent<RectTransform>();
        Vector3 origin = currentPositionRT.localPosition;

        Vector3 destiny = origin - new Vector3(0, offsetY, 0);

        anim.SetTrigger("start");
        yield return StartCoroutine(LerpMove(destiny, currentPositionRT, randomSpeed));
        yield return StartCoroutine(LerpMove(origin, currentPositionRT, randomSpeed));
        anim.SetTrigger("decrease");
        PlayingAnimationIsDone = true;
    }


    // TODO: random rotation
    IEnumerator LerpMove(Vector3 destiny, RectTransform currentPos, float speed)
    {
        float distance = Mathf.Abs((destiny - currentPos.localPosition).magnitude);

        while (Mathf.Abs((destiny - currentPos.localPosition).magnitude) > .005f * distance)
        {
            currentPos.localPosition = Vector3.Lerp(currentPos.localPosition, destiny, Time.deltaTime * speed);
            yield return null;
        }

        currentPos.localPosition = destiny;
    }
}
