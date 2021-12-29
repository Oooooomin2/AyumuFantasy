using UnityEngine;
using UnityEngine.UI;

public class AttackButtonController : MonoBehaviour
{
    public GameObject Player;
    public GameObject Panel;
    public Button AttackButton;
    public static bool HasPushButton = false;

    public void PushAttackButton()
    {
        HasPushButton = true;
        Panel.SetActive(false);

        var attack = Random.Range(PlayerManager.Attack - 15, PlayerManager.Attack + 15);
        BattleManager.TurnOrders.Add(Player, attack);
    }

    private void FixedUpdate()
    {
        if (BattleManager.IsDuringMotion)
        {
            AttackButton.interactable = false;
        }
        else
        {
            AttackButton.interactable = true;
        }
    }
}
