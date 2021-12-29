using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public GameObject Target;
    public AudioClip AttackSound;
    public AudioClip DieSound;
    public string EnemyName;

    public int MaxHp;
    public static int Hp;
    public int Attack;
    float enemyAttackGage;

    void Start()
    {
        Hp = MaxHp;

        enemyAttackGage = UnityEngine.Random.Range(0.0f, 1.0f);
    }

    private void FixedUpdate()
    {
        if (BattleManager.IsDuringMotion) return;

        enemyAttackGage += 0.0025f;
        if(enemyAttackGage >= 1.0f)
        {

            var attack = UnityEngine.Random.Range(Attack - (Attack / 10), Attack + (Attack / 10));
            BattleManager.TurnOrders.Add(gameObject, attack);
            enemyAttackGage = 0;
        }
    }

    public void AttackMotion()
    {
        var animator = Target.GetComponent<Animator>();
        var audioSource = Target.GetComponent<AudioSource>();
        var particleSystem = Target.GetComponent<ParticleSystem>();
        var damageText = Target.transform.Find("Canvas/DamageText").gameObject.GetComponent<Text>();
        Target.GetComponent<PlayerManager>().GetHit(animator, audioSource, AttackSound, particleSystem, damageText);
    }

    public void GetHit(Animator animator, AudioSource audioSource, AudioClip audioClip, ParticleSystem particleSystem, Text damageText)
    {
        var damage = BattleManager.TurnOrders.First().Value;
        Hp -= damage;
        if (Hp <= 0)
        {
            animator.SetBool("isDie", true);
            particleSystem.Play();
            audioSource.PlayOneShot(audioClip);

            StartCoroutine(DelayCoroutine(1f, () =>
            {
                DamageTextAnimationDie();
            }));

            return;
        }

        animator.SetTrigger("GetHit");
        particleSystem.Play();
        audioSource.PlayOneShot(audioClip);
        BattleManager.TurnOrders.Remove(BattleManager.TurnOrders.First().Key);

        StartCoroutine(DelayCoroutine(1f, () =>
        {
            damageText.enabled = true;
            damageText.text = damage.ToString();


            StartCoroutine(DelayCoroutine(1f, () => 
            {
                BattleManager.IsDuringMotion = false;
                damageText.enabled = false;
            }));
        }));
    }

    private IEnumerator DelayCoroutine(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action?.Invoke();
    }

    private void DamageTextAnimationDie()
    {
        transform.Find("Canvas/DamageText").gameObject.GetComponent<Text>().enabled = true;
        transform.Find("Canvas/DamageText").gameObject.GetComponent<Text>().text = BattleManager.TurnOrders.First().Value.ToString();

        BattleManager.TurnOrders.Remove(BattleManager.TurnOrders.First().Key);
        BattleManager.IsDuringMotion = false;
        if(gameObject.tag == "RedDragon")
        {
            Invoke("HideEnemy", 3.0f);
            StartCoroutine(DelayCoroutine(3.0f, () =>
            {
                HideEnemy();
            }));
            return;
        }
        StartCoroutine(DelayCoroutine(1.0f, () =>
        {
            HideEnemy();
        }));
    }

    private void HideEnemy()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(DieSound);
        this.gameObject.transform.Find(EnemyName).gameObject.SetActive(false);
        transform.Find("Canvas/DamageText").gameObject.GetComponent<Text>().enabled = false;
        if (gameObject.tag == "RedDragon")
        {
            BattleManager.IsBoss = true;
        }
        BattleManager.IsVictory = true;
    }
}
