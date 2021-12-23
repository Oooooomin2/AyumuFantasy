using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public GameObject Target;
    public AudioClip AttackSound;
    public Slider PlayerSlider;

    int maxHp = 100;
    public static int hp = 100;
    int maxMp = 36;
    public static int mp = 36;
    public static int attack = 80;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ActiveCollider()
    {
        var animator = Target.GetComponent<Animator>();
        var audioSource = Target.GetComponent<EnemyManager>().AudioSource;
        var particleSystem = Target.GetComponent<EnemyManager>().GetComponent<ParticleSystem>();
        var damageText = Target.GetComponent<EnemyManager>().transform.Find("Canvas/DamageText").gameObject.GetComponent<Text>();
        Target.GetComponent<EnemyManager>().GetHit(animator, audioSource, AttackSound, particleSystem, damageText);
    }

    public void GetHit(
        Animator animator,
        AudioSource audioSource,
        AudioClip audioClip,
        ParticleSystem particleSystem,
        Text damageText)
    {
        var damage = BattleManager.TurnOrders.First().Value.Attack;
        hp -= damage;
        if (hp <= 0)
        {
            animator.SetBool("isDie", true);
            //particleSystem.Play();
            audioSource.PlayOneShot(audioClip);
            Invoke("DamageTextAnimationDie", 1.0f);
            BattleManager.hasEndBattle = true;
            BattleManager.isVictory = true;
            return;
        }
        animator.SetTrigger("GetHit");
        //particleSystem.Play();
        //audioSource.PlayOneShot(audioClip);
        StartCoroutine(DelayCoroutine(1f, () =>
        {
            damageText.enabled = true;
            damageText.text = BattleManager.TurnOrders.First().Value.Attack.ToString();
            BattleManager.TurnOrders.Remove(BattleManager.TurnOrders.First().Key);
            BattleManager.isMoveToEnemy = true;
            StartCoroutine(DelayCoroutine(1f, () =>
            {
                damageText.enabled = false;
            }));
        }));
    }

    private IEnumerator DelayCoroutine(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action?.Invoke();
    }
}
