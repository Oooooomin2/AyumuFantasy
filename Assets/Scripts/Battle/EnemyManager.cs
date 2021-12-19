using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip AttackSound;
    public AudioClip DieSound;

    Animator animator;
    public Text DamageText;
    public int MaxHp = 200;
    public static int hp = 200;

    void Start()
    {
        hp = MaxHp;
        animator = GetComponent<Animator>();
        DamageText.enabled = false;
        EnemyTriggerOff();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        var damage = BattleManager.TurnOrders.First().Value.Attack;
        hp -= damage;
        if(hp <= 0)
        {
            animator.SetBool("isDie", true);
            GetComponent<ParticleSystem>().Play();
            AudioSource.PlayOneShot(AttackSound);
            Invoke("DamageTextAnimationDie", 1.0f);
            BattleManager.hasEndBattle = true;
            BattleManager.isVictory = true;
            return;
        }
        animator.SetTrigger("GetHit");
        GetComponent<ParticleSystem>().Play();
        AudioSource.PlayOneShot(AttackSound);
        Invoke("DamageTextAnimation", 1f);
    }

    private void DamageTextAnimation()
    {
        DamageText.enabled = true;
        DamageText.text = BattleManager.TurnOrders.First().Value.Attack.ToString();
        BattleManager.TurnOrders.Remove(BattleManager.TurnOrders.First().Key);
        BattleManager.isMoveToEnemy = true;
        Invoke("HideDamageText", 1.0f);
    }

    private void DamageTextAnimationDie()
    {
        DamageText.enabled = true;
        DamageText.text = BattleManager.TurnOrders.First().Value.Attack.ToString();
        BattleManager.TurnOrders.Remove(BattleManager.TurnOrders.First().Key);
        BattleManager.isMoveToEnemy = true;
        Invoke("HideDamageText", 1.0f);
        Invoke("HideEnemy", 1.0f);

    }

    private void HideDamageText()
    {
        DamageText.enabled = false;
    }

    private void HideEnemy()
    {
        AudioSource.PlayOneShot(DieSound);
        Debug.Log("die");
        this.gameObject.transform.Find("Slime").gameObject.SetActive(false);
    }

    private void EnemyTriggerOn()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void EnemyTriggerOff()
    {
        GetComponent<Collider>().isTrigger = false;
    }
}
