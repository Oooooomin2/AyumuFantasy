using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Target;

    void Start()
    {
        transform.position = Target.transform.position + new Vector3(0.0f, 5.0f, -8.0f);
    }
}
