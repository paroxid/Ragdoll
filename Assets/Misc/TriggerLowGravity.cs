using System.Collections;
using System.Collections.Generic;
using UnityEngine.PostProcessing;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

public class TriggerLowGravity : MonoBehaviour
{
    public PostProcessingProfile pppStandart;
    public PostProcessingProfile pppPowerUp;
    public GameObject CameraGameobject;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(lowGravity());
        transform.localScale = new Vector3(0, 0, 0);
    }



    IEnumerator lowGravity()
    {
        Physics.gravity = new Vector3(0, -2f, 0);

        CameraGameobject.GetComponent<PostProcessingBehaviour>();

        // Camera.main.focalLength = 15;


        yield return new WaitForSeconds(3);

        // Camera.main.focalLength = 10;

        Physics.gravity = new Vector3(0, -9.81f, 0);

        //Destroy(gameObject);
    }
}
