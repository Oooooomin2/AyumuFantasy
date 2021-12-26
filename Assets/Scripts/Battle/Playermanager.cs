using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public GameObject Target;
    public AudioClip AttackSound;
    public Slider PlayerSlider;
    public Text PlayerHpText;

    public static int MaxHp = 100;
    public static int Hp = 100;
    public static int MaxMp = 36;
    public static int Mp = 36;
    public static int Attack = 80;

    public void ActiveCollider()
    {
        var animator = Target.GetComponent<Animator>();
        var audioSource = Target.GetComponent<EnemyManager>().GetComponent<AudioSource>();
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
        Hp -= damage;

        if (Hp <= 0)
        {
            Hp = 0;
            PlayerHpText.text = Hp.ToString();

            animator.SetBool("isDie", true);
            particleSystem.Play();
            audioSource.PlayOneShot(audioClip);
            BattleManager.IsLose = true;

            StartCoroutine(DelayCoroutine(1f, () =>
            {
                damageText.enabled = true;
                damageText.text = damage.ToString();
            }));

            return;
        }

        PlayerHpText.text = Hp.ToString();

        animator.SetTrigger("GetHit");
        particleSystem.Play();
        audioSource.PlayOneShot(audioClip);

        StartCoroutine(DelayCoroutine(1f, () =>
        {
            damageText.enabled = true;
            damageText.text = damage.ToString();

            BattleManager.TurnOrders.Remove(BattleManager.TurnOrders.First().Key);

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
}
