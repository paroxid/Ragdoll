using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraController : MonoBehaviour
{

    public GameObject target;
    Vector3 Offset = new Vector3(0f, 1.5f, -2f);

    //SmoothDamp Vars
    public Vector3 Velocity = new Vector3(0, 0, 0);
    public float SmoothTime = 0.07f;


    private void FixedUpdate()
    {

        transform.position = Vector3.SmoothDamp(transform.position, target.transform.position + Offset, ref Velocity, SmoothTime);

    }

}
