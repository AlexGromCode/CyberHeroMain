using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private Animator _animCamera;
    [SerializeField] private Animator _animFade;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] GameObject _fade;

    public void Awake() 
    {
        _animCamera = GetComponent<Animator>();
        _fade = GameObject.FindGameObjectWithTag("Fade");
        _animFade = _fade.GetComponent<Animator>();
        _canvasGroup = _fade.GetComponent<CanvasGroup>();
    }
    public void Shake(bool isShaking)
    {
        _canvasGroup.alpha = 1;
        _animFade.SetBool("IsShaking", isShaking);
        _animCamera.SetBool("IsShaking", isShaking);
    }
}