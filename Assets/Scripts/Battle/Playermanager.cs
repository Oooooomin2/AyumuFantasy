using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermanager : MonoBehaviour
{
    public Collider Collider;
    int maxHp = 100;
    public static int hp = 100;
    int maxMp = 36;
    public static int mp = 36;
    public static int attack = 80;

    // Start is called before the first frame update
    void Start()
    {
        HideCollider();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideCollider()
    {
        Collider.enabled = false;
    }

    public void ActiveCollider()
    {
        Collider.enabled = true;
    }
}
