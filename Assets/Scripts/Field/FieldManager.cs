using System.Collections;
using System.Collections.Generic;
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
        var RateEncount = Random.Range(0, 1200);
        if(playerSpeed > 1.0f && RateEncount == 50)
        {
            PlayerFieldLocation = Player.transform.position;
            Player.GetComponent<PlayerController>().moveSpeed = 0;
            fade.FadeIn(2.0f, () =>
            {
                SceneManager.LoadScene("FightingToSlime");
            });
        }
    }
}
