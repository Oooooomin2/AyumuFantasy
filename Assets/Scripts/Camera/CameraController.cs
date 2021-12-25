using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Target;
    private Vector3 distance;

    void Start()
    {
        transform.position = Target.transform.position + new Vector3(0.0f, 3.0f, -5.0f);
        distance = transform.position - Target.transform.position;
    }

    void FixedUpdate()
    {
        transform.position = Target.transform.position + distance;
    }
}
