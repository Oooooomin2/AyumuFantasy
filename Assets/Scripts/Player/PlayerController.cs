using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    float xSpeed;
    float zSpeed;
    public float moveSpeed = 2;

    [SerializeField]
    Fade fade = null;

    Rigidbody rigidbody;
    Animator animator;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, 0.8f, 0);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {

            transform.Rotate(0, -0.8f, 0);
        }
        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetFloat("Speed", moveSpeed);
            transform.position += transform.forward * 0.03f * moveSpeed;
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Village")
        {
            moveSpeed = 0;
            fade.FadeIn(1.0f, () =>
            {
                SceneManager.LoadScene("rpgpp_lt_scene_1.0 1");
            });
        }
    }
}
