using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    public static Dictionary<GameObject, TurnCharactorContext> TurnOrders;
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
        TurnOrders = new Dictionary<GameObject, TurnCharactorContext>();

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
            turnCharactor.Key.GetComponent<Animator>().SetTrigger("Attack");
        }
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
                Invoke("MoveToEndGame", 2.0f);
                return;
            }

            Invoke("MoveToEndOfBattle", 2.5f);
        }

        if (IsLose)
        {
            Invoke("MoveToEndOfBattle", 1.0f);
        }
    }

    void MoveToEndOfBattle()
    {
        if (IsVictory)
        {
            SceneManager.LoadScene("EndBattle");
        }

        if (IsLose)
        {
            fade.FadeIn(1.0f, () =>
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
