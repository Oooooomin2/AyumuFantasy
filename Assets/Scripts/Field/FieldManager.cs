using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FieldManager : MonoBehaviour
{
    public GameObject Player;
    public static Vector3 PlayerFieldLocation;

    [SerializeField]
    Fade fade = null;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerFieldLocation.Equals(new Vector3(0, 0, 0)))
        {
            PlayerFieldLocation = new Vector3(100, 0, 100);
        }

        Player.transform.position = PlayerFieldLocation;
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody rigidbody = Player.GetComponent<Rigidbody>();
        var PlayerSpeed = rigidbody.velocity.magnitude;
        var RateEncount = Random.Range(0, 1200);
        if(PlayerSpeed > 0.5f && RateEncount == 50)
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
