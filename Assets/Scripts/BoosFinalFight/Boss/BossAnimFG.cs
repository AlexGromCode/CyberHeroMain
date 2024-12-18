using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimFG : MonoBehaviour
{
    [SerializeField] private Animator _bossAnim;
    [SerializeField] private BossMain _bossMain;
    private bool _isAnimStart;

    private void OnEnable() => _bossMain.OnTakeDamaged += HitAnim;
    private void OnDisable() => _bossMain.OnTakeDamaged -= HitAnim;
    public void Awake() => _bossAnim = GetComponent<Animator>();
    private void Start() => _isAnimStart = false;
    private void Update()
    {
        if (DialogueBossFinalFight.IsDialogueEnd && !_isAnimStart)
        {
            StartCoroutine(BossChange(DialogueBossFinalFight.IsDialogueEnd));
            _isAnimStart = true;
        }
    }
    private IEnumerator BossChange(bool isEnd)
    {
        _bossAnim.SetBool("Up", true);
        yield return new WaitForSeconds(2f);
        _bossAnim.SetBool("Up", false);
        GM.IsPlayingRoomBossFinalFight = isEnd ? true : false;
    }
    public void BossFallAnim() => _bossAnim.SetBool("Fall", true);
    public void HitAnim() => _bossAnim.SetBool("Hit", true);
    public void HitAnimFalse() => _bossAnim.SetBool("Hit", false);
}
