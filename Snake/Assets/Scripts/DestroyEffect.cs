using UnityEngine;
using System.Collections;

public class DestroyEffect : MonoBehaviour
{
    public GameObject effectPrefab;

    public void Effect()
    {
        RectTransform spawnHolder = GameManager.instance.spawnHolder;
        GameObject go = Instantiate(effectPrefab);
        go.transform.SetParent(spawnHolder);
        go.transform.localScale = Vector2.one;
        RectTransform goRT = go.GetComponent<RectTransform>();

        goRT.localPosition = GetComponent<RectTransform>().localPosition;
        Destroy(gameObject);
    }
}
