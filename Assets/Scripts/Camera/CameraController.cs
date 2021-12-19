using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    private Vector3 distance;

    void Start()
    {
        distance = transform.position - target.transform.position;
    }

    void FixedUpdate()
    {
        transform.position = target.transform.position + distance;
    }
}
