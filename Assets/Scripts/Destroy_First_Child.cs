using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_First_Child : MonoBehaviour
{
    public GameObject parent;

    
    void Update()
    {
        if (parent.transform.childCount > 5)
        {
            Destroy(parent.transform.GetChild(1).gameObject);
        } 
    }
}
