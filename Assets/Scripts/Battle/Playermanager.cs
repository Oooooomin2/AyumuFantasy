using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public GameObject Target;
    public AudioClip AttackSound;

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
        var damageText = Target.GetComponent<EnemyManager>().DamageText;
        var particleSystem = Target.GetComponent<ParticleSystem>();
        new GameObject("EnemyManager").AddComponent<EnemyManager>().GetHit(animator, audioSource, AttackSound, particleSystem, damageText);
    }
}
