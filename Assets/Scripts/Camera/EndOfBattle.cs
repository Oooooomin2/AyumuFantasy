using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfBattle : MonoBehaviour
{
    public GameObject Target;

    [SerializeField]
    Fade fade = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position + new Vector3(0.0f, 0.5f, 1.0f), 0.02f);
        if (GetComponent<Rigidbody>().IsSleeping())
        {
            Invoke("BackToField", 1.5f);
        }
    }

    private void BackToField()
    {
        fade.FadeIn(2.0f, () =>
        {
            SceneManager.LoadScene("SampleScene");
        });
    }
}
