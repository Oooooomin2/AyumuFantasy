using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInVillageController : MonoBehaviour
{
    public float MoveSpeed = 2;

    float xSpeed;
    float zSpeed;
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
        Vector3 direction = transform.position + new Vector3(xSpeed, 0, zSpeed) * MoveSpeed;
        transform.LookAt(direction);
        rigidbody.velocity = new Vector3(xSpeed, 0, zSpeed) * MoveSpeed;
        animator.SetFloat("Speed", rigidbody.velocity.magnitude);
    }
}