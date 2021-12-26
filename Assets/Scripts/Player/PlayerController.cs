using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed = 2;

    [SerializeField]
    Fade fade = null;
    Animator animator;

    private void Start() => animator = GetComponent<Animator>();

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
            animator.SetFloat("Speed", MoveSpeed);
            transform.position += transform.forward * 0.03f * MoveSpeed;
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
            MoveSpeed = 0;
            fade.FadeIn(1.0f, () =>
            {
                SceneManager.LoadScene("rpgpp_lt_scene_1.0 1");
            });
        }
    }
}
