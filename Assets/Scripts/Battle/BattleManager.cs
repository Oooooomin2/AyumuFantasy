using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public static Dictionary<GameObject, TurnCharactorContext> TurnOrders;
    public Canvas Canvas;

    public static bool IsDuringMotion;
    public static bool isMoveToEnemy;
    public static bool hasMotioned;
    public static bool isVictory;
    public static bool hasEndBattle;

    void Start()
    {
        IsDuringMotion = false;
        isMoveToEnemy = true;
        hasMotioned = false;
        hasEndBattle = false;
        isVictory = false;
        TurnOrders = new Dictionary<GameObject, TurnCharactorContext>();
    }

    void Update()
    {
        if (!TurnOrders.Any()) return;
        if (hasMotioned) return;

        if (hasEndBattle)
        {
            Canvas.enabled = false;
        }

        var turnCharactor = TurnOrders.First();
        IsDuringMotion = true;
        if (isMoveToEnemy)
        {
            turnCharactor.Key.GetComponent<Animator>().SetBool("isYourTurn", true);
            turnCharactor.Key.transform.position = Vector3.MoveTowards(turnCharactor.Key.transform.position, turnCharactor.Value.PlayerAttackLocation, 0.3f);
        }
        
        float distanceWithEnemy = (turnCharactor.Value.PlayerAttackLocation - turnCharactor.Key.transform.position).sqrMagnitude;
        if (distanceWithEnemy <= 2.0f && !hasMotioned && isMoveToEnemy)
        {
            turnCharactor.Key.GetComponent<Animator>().SetTrigger("Attack");
        }

        if (!isMoveToEnemy)
        {
            turnCharactor.Key.transform.position = Vector3.MoveTowards(turnCharactor.Key.transform.position, turnCharactor.Value.NowPlayerLocation, 0.6f);
            float distanceBaseLocation = (turnCharactor.Value.NowPlayerLocation - turnCharactor.Key.transform.position).sqrMagnitude;
            if(distanceBaseLocation <= 0.01f)
            {
                turnCharactor.Key.GetComponent<Animator>().SetBool("isYourTurn", false);
                IsDuringMotion = false;
            }
        }

        if (hasEndBattle)
        {
            Invoke("MoveToEndOfBattle", 3.0f);
        }
    }

    void MoveToEndOfBattle()
    {
        if (isVictory)
        {
            SceneManager.LoadScene("EndBattle");
        }
    }
}
