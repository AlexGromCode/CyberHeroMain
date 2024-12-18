using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    [SerializeField] private GameObject _boss;
    [SerializeField] private GameObject wall;

    [SerializeField] protected SpriteRenderer _spriteRenderer;
    [SerializeField] protected PlayerAnimation _playerAnim;
    protected Vector3 _moveVector;

    private bool isTrue = true;
    protected bool initialized = false;

    public static int CountKeys = 0;

    public void Awake()
    {
        Application.targetFrameRate = 120;
        GM.IsPlayingRoomStart = true;
        _playerAnim = GetComponentInChildren<PlayerAnimation>();
    }
    private void Update()
    {
        if (CountKeys >= 4 && isTrue)
        {        
            _boss.SetActive(true);
            wall.SetActive(false);
            isTrue = false;
        }
    }
}
