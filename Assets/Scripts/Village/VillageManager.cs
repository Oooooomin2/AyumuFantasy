using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VillageManager : MonoBehaviour
{
    public GameObject Player;
    public static bool isPowerUp = false;

    public Fungus.Flowchart flowchart = null;

    bool isTalking = false;

    [SerializeField]
    Fade fade = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isTalking)
        {
            Player.GetComponent<PlayerController>().moveSpeed = 0;
            return;
        }

        Player.GetComponent<PlayerController>().moveSpeed = 3.5f;
    }

    private void FixedUpdate()
    {
        float distanceFromCenter = (Player.transform.position - new Vector3(72.0f, 23.0f, 47.0f)).sqrMagnitude;
        if(distanceFromCenter > 1000.0f)
        {
            FieldManager.PlayerFieldLocation = new Vector3(59.0f, 0.0f, 135.0f);
            Player.GetComponent<PlayerController>().moveSpeed = 0;
            fade.FadeIn(0.5f, () =>
            {
                SceneManager.LoadScene("SampleScene");
            });
        }
    }

    public void PowerUp()
    {
        isPowerUp = true;
    }

    public void PlayerStartTalk()
    {
        isTalking = true;
        flowchart.SetBooleanVariable("isPowerUp", isPowerUp);
    }

    public void PlayerEndTalk() 
    {
        isTalking = false;
    }
}
