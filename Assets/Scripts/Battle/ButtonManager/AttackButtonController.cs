using UnityEngine;

public class AttackButtonController : MonoBehaviour
{
    public GameObject Player;
    public GameObject Panel;
    public GameObject Target;
    public static bool HasPushButton = false;

    public void AttackButton()
    {
        HasPushButton = true;
        Panel.SetActive(false);
        Invoke("SetAttackContext", 1.0f);
    }

    public void SetAttackContext()
    {
        var nowPlayerLocation = Player.transform.position;
        var playerAttackLocation = Target.transform.position - new Vector3(0.0f, 0.0f, -5.0f);

        var charactorContext = new TurnCharactorContext
        {
            PlayerAttackLocation = playerAttackLocation,
            NowPlayerLocation = nowPlayerLocation,
            Attack = Random.Range(PlayerManager.Attack - 15, PlayerManager.Attack + 15)
        };
        BattleManager.TurnOrders.Add(Player, charactorContext);
    }
}

public class TurnCharactorContext
{
    public Vector3 PlayerAttackLocation { get; set; }
    public Vector3 NowPlayerLocation { get; set; }
    public int Attack { get; set; }
}
