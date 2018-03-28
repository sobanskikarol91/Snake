using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public Clip highlighted;
    public Clip clicked;
    AudioSource _audioSource;
    Button _button;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
         _button = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetClip(highlighted);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SetClip(clicked);
    }

    void SetClip(Clip clip)
    {
        _audioSource.clip = clip.clip;
        _audioSource.outputAudioMixerGroup = clip.group;
        _audioSource.Play();
    }
}
