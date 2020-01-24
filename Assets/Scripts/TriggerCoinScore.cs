using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCoinScore : MonoBehaviour
{
    public AudioClip terminal03;
    public AudioSource audioSource;

    public void OnTriggerEnter(Collider other)
    {
        transform.localScale = new Vector3(0, 0, 0);
        audioSource.PlayOneShot(terminal03, 0.7f);

        GameObject Ch11_nonPBR = GameObject.Find("Ch11_nonPBR");
        PlayerScript playerScript = Ch11_nonPBR.GetComponent<PlayerScript>();

        playerScript.Coin();
    }
}
