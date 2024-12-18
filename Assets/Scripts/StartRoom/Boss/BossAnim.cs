using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnim : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public void BossRunAnim() => _animator.SetFloat("Speed", 1);
}
