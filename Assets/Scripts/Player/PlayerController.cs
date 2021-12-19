using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float xSpeed;
    float zSpeed;
    public float moveSpeed = 2;

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
}
