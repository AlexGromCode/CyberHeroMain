using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class PlayerMain3 : MonoBehaviour
{
    [SerializeField] protected Joystick _joystick;
    [SerializeField] protected SpriteRenderer _spriteRenderer;
    [SerializeField] protected Rigidbody2D _rigidbody;
    [SerializeField] protected Transform _lowerLegsPoint;
    [SerializeField] protected Material _diffuseMaterial;
    [SerializeField] protected SpriteRenderer[] _backGround;
    [SerializeField] protected AudioSource _audioSource;
    [SerializeField] protected DialogueBossFinalFight _dialogue;
    [SerializeField] protected CameraShake _cameraShake;
    [SerializeField] protected Light _playerLight;

    public event Action OnTakeDamaged;
    public static int Hp;
    private bool isChecked = true;

    public void Awake()
    {
        _rigidbody ??= GetComponent<Rigidbody2D>();
        _spriteRenderer ??= GetComponentInChildren<SpriteRenderer>();
        _playerLight ??= GetComponent<Light>();

        Transform[] children = GetComponentsInChildren<Transform>();

        foreach (Transform child in children)
        {
            if (child.name == "LowerLegsPoint")
            {
                _lowerLegsPoint = child;
                break;
            }
        }

        Application.targetFrameRate = 120;
        isChecked = true;
        GM.IsPlayingRoomBossFinalFight = false;
        Hp = 10;

        _rigidbody.freezeRotation = true;
        _playerLight.enabled = false;
    }
    public void Update() => CheckHp(ref isChecked);
    private void CheckHp(ref bool isChecked)
    {
        if(Hp <= 3)
        {
            _audioSource.pitch = 2;
            _cameraShake.Shake(true);
            ChangeMaterial();
            _playerLight.enabled = true;
        }
        if (Hp <= -1 && isChecked)
        {
            GM.IsPlayingRoomBossFinalFight = false;
            _cameraShake.Shake(false);
            _dialogue.ShowLineExtra(indexExtrsLine: 0, scene: 6, indexImage: 0, indexVoice: 0);
            isChecked = false;
        }
    }
    private void ChangeMaterial()
    {
        for (int i = 0; i < _backGround.Length; i++)
            _backGround[i].material = _diffuseMaterial;
        _spriteRenderer.material = _diffuseMaterial;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BulletBoss"))
            OnTakeDamaged?.Invoke();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "BulletBoss")
            OnTakeDamaged?.Invoke();
    }
}
