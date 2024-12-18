using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMain : MonoBehaviour
{
    [Space(10)]
    [SerializeField] private DialogueBossFinalFight _dialogue;
    [SerializeField] private BossAnimFG _bossAnim;
    
    public event Action OnTakeDamaged;
    
    public static int Hp;
    private bool _isChecked = true;

    private void Awake() => Hp = 12;
    public void Update() => CheckHp(ref _isChecked);
    private void CheckHp(ref bool isChecked)
    { 
        if(Hp <= 0 && isChecked)
        {
            _bossAnim.BossFallAnim();
            GM.IsPlayingRoomBossFinalFight = false;
            _dialogue.ShowLineExtra(indexExtrsLine: 1, scene: 7, indexImage: 1, indexVoice: 1);
            isChecked = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            OnTakeDamaged?.Invoke();
            Debug.Log(Hp);
        }
    }
}
