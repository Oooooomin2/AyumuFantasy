using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    public static Dictionary<GameObject, int> TurnOrders;
    public Canvas Canvas;

    [SerializeField]
    Fade fade = null;

    [SerializeField]
    Fade fadeToEnding = null;

    public static bool IsDuringMotion;
    public static bool IsVictory;
    public static bool IsLose;
    public static bool IsBoss;

    void Start()
    {
        TurnOrders = new Dictionary<GameObject, int>();

        IsDuringMotion = false;
        IsVictory = false;
        IsLose = false;
        IsBoss = false;
    }

    void Update()
    {
        if (!TurnOrders.Any()) return;
        if (IsDuringMotion) return;

        if (IsVictory)
        {
            Canvas.enabled = false;
        }

        var turnCharactor = TurnOrders.First();
        if (!IsDuringMotion)
        {
            IsDuringMotion = true;
            StartCoroutine(DelayCoroutine(0.4f, () =>
            {
                turnCharactor.Key.GetComponent<Animator>().SetTrigger("Attack");
            }));
        }
    }

    private IEnumerator DelayCoroutine(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action?.Invoke();
    }

    private void FixedUpdate()
    {
        if (IsVictory)
        {
            if(IsBoss)
            {
                IsVictory = false;
                IsBoss = false;
                VillageManager.isPowerUp = false;
                StartCoroutine(DelayCoroutine(2.0f, () =>
                {
                    MoveToEndGame();
                }));
                return;
            }
            StartCoroutine(DelayCoroutine(2.0f, () =>
            {
                MoveToEndOfBattle();
            }));
        }

        if (IsLose)
        {
            StartCoroutine(DelayCoroutine(1.0f, () =>
            {
                MoveToEndOfBattle();
            }));
        }
    }

    void MoveToEndOfBattle()
    {
        if (IsVictory)
        {
            IsVictory = false;
            SceneManager.LoadScene("EndBattle");
        }

        if (IsLose)
        {
            IsLose = false;
            fade.FadeIn(2.0f, () =>
            {
                SceneManager.LoadScene("GameOver");
            });
        }
    }

    void MoveToEndGame()
    {
        fadeToEnding.FadeIn(1.5f, () =>
        {
            SceneManager.LoadScene("EndingRole");
        });
    }
}
