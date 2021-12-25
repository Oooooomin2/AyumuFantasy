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

    void Update()
    {
        xSpeed = Input.GetAxisRaw("Horizontal");
        zSpeed = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        Vector3 direction = transform.position + new Vector3(xSpeed, 0, zSpeed) * moveSpeed;
        transform.LookAt(direction);
        rigidbody.velocity = new Vector3(xSpeed, 0, zSpeed) * moveSpeed;
        animator.SetFloat("Speed", rigidbody.velocity.magnitude);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Village")
        {
            GetComponent<PlayerController>().moveSpeed = 0;
            fade.FadeIn(1.0f, () =>
            {
                SceneManager.LoadScene("rpgpp_lt_scene_1.0 1");
            });
        }

        if(other.tag == "Kogoro")
        {
            /*MessageReceived[] receivers = FindObjectsOfType<MessageReceived>();
            if(receivers != null)
            {
                foreach(var receiver in receivers)
                {
                    receiver.OnSendFungusMessage("kogoro_message");
                }
            }*/
        }
    }

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if(collision.collider.CompareTag("Kogoro"))
        {
            
        }
    }
}
