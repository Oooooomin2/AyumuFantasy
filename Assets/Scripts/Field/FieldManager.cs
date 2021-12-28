using UnityEngine;
using UnityEngine.SceneManagement;

public class FieldManager : MonoBehaviour
{
    public GameObject Player;
    public static Vector3 PlayerFieldLocation;

    public Fungus.Flowchart flowchart = null;

    [SerializeField]
    Fade fade = null;

    void Start()
    {
        if(PlayerFieldLocation.Equals(new Vector3(0, 0, 0)))
        {
            PlayerFieldLocation = new Vector3(100, 0, 100);
        }

        Player.transform.position = PlayerFieldLocation;
        flowchart.SetBooleanVariable("isPowerUp", VillageManager.isPowerUp);
    }

    void Update()
    {
        var playerSpeed = Player.GetComponent<Animator>().GetFloat("Speed");
        var RateEncount = Random.Range(0, 1400);
        if(playerSpeed > 1.0f && RateEncount == 50)
        {
            PlayerFieldLocation = Player.transform.position;
            Player.GetComponent<PlayerController>().MoveSpeed = 0;
            fade.FadeIn(2.0f, () =>
            {
                if (VillageManager.isPowerUp)
                {
                    SceneManager.LoadScene("FightingToSlime_isPowerUp");
                }
                else
                {
                    SceneManager.LoadScene("FightingToSlime");
                }
            });
        }
    }

    public void TalkToRedDragon()
    {
        Player.GetComponent<PlayerController>().MoveSpeed = 0;
    }

    public void EndTalkToRedDragon()
    {
        Player.GetComponent<PlayerController>().MoveSpeed = 5.5f;
    }

    public void BattleRedDragon()
    {
        fade.FadeIn(2.0f, () =>
        {
            if (VillageManager.isPowerUp)
            {
                SceneManager.LoadScene("FightingToRedDragon_isPowerUp");
            }
            else
            {
                SceneManager.LoadScene("FightingToRedDragon_isNotPowerUp");
            }
        });
    }
}
