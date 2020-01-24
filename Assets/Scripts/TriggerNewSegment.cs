using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNewSegment : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Segment_Instancing>())
        {
            other.GetComponent<Segment_Instancing>().newSegment();
        }
    }


}
