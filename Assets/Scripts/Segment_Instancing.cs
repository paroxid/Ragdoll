using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.PostProcessing;


[RequireComponent(typeof(AudioSource))]
public class Segment_Instancing : MonoBehaviour
{
    List<GameObject> SegmentList = new List<GameObject>();

    public GameObject Level01_V1;
    public GameObject Level01_V2;
    public GameObject Level01_V3;

    public GameObject Level02_V1;
    public GameObject Level02_V2;
    public GameObject Level02_V3;

    public GameObject Level03_V1;
    public GameObject Level03_V2;
    public GameObject Level03_V3;

    public GameObject Level04_V1;
    public GameObject Level04_V2;
    public GameObject Level04_V3;

    public GameObject Level05_V1;
    public GameObject Level05_V2;
    public GameObject Level05_V3;

    public GameObject Level06_V1;
    public GameObject Level06_V2;
    public GameObject Level06_V3;

    public float InstanceCounter;

    public GameObject SegmentParent;




    public AudioClip Grunt_01;
    public AudioClip Grunt_02;
    public AudioClip Grunt_03;
    public AudioClip Grunt_04;
    public AudioClip Grunt_05;
    public AudioClip Grunt_06;

    public AudioClip Impact_01;
    public AudioClip Impact_02;
    public AudioClip Impact_03;

    public List<AudioClip> Impacts = new List<AudioClip>();

    public AudioSource audioSource;

    public float Movement;
    public bool NoMovement = false;


    public Rigidbody rb;


    public PostProcessingProfile ppProfile;

    public float BloomValue;
    public float ColorValue;
    public float CAValue;
    public float TintValue;

    // Level Buidling Start ----------------------------------------------------------------------
    void Start()
    {
        Impacts.Add(Impact_01);
        Impacts.Add(Impact_02);
        Impacts.Add(Impact_03);

        SegmentList.Add(Level01_V1);
        SegmentList.Add(Level01_V2);
        SegmentList.Add(Level01_V2);

        SegmentList.Add(Level02_V1);
        SegmentList.Add(Level02_V2);
        SegmentList.Add(Level02_V3);

        SegmentList.Add(Level03_V1);
        SegmentList.Add(Level03_V2);
        SegmentList.Add(Level03_V3);

        SegmentList.Add(Level04_V1);
        SegmentList.Add(Level04_V2);
        SegmentList.Add(Level04_V3);

        SegmentList.Add(Level05_V1);
        SegmentList.Add(Level05_V2);
        SegmentList.Add(Level05_V3);

        SegmentList.Add(Level06_V1);
        SegmentList.Add(Level06_V2);
        SegmentList.Add(Level06_V3);

        Instantiate(Level01_V1, new Vector3(0, (InstanceCounter * -20) - 10, (InstanceCounter * 80) + 10), Quaternion.Euler(-90, 90, 0), SegmentParent.transform);
        InstanceCounter++;

        BloomValue = 0.06f;
        ColorValue = 60;
        CAValue = 1;
        TintValue = 0;
    }



    public void newSegment()
    {
        int prefabIndex = UnityEngine.Random.Range(0, 12);
        Instantiate((SegmentList[prefabIndex]), new Vector3(0, (InstanceCounter * -20) - 10, (InstanceCounter * 80) + 10), Quaternion.Euler(-90, 90, 0), SegmentParent.transform);
        InstanceCounter++;
    }


    // Level Buidling End ----------------------------------------------------------------------



    //    public void Update()
    //{
    //    Vector3 myCollisionVelocity = rb.velocity;

    //}


    // Sound on Impact Start ----------------------------------------------------------------------
    void OnCollisionEnter(Collision collision)
    {
        //Vector3 normal = collision.contacts[0].normal;
        //collisionAngle = 90 - (Vector3.Angle(myVelocity, -normal));
        //Debug.Log("Collision Angle:" + collisionAngle);

        //ContactPoint contact = collision.contacts[0];
        //Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);

        //Debug.Log(rotation);

        



        if (collision.relativeVelocity.magnitude > 10 && collision.relativeVelocity.magnitude < 13)
        {
            audioSource.PlayOneShot(Grunt_01, 0.6f);

            int selection = Random.Range(0, 3);
            audioSource.PlayOneShot(Impacts[selection], .2f);
        }

        if (collision.relativeVelocity.magnitude > 13 && collision.relativeVelocity.magnitude < 16)
        {
            audioSource.PlayOneShot(Grunt_02, 0.6f);

            int selection = Random.Range(0, 3);
            audioSource.PlayOneShot(Impacts[selection], .1f);
        }

        if (collision.relativeVelocity.magnitude > 16 && collision.relativeVelocity.magnitude < 20)
        {
            StartCoroutine(VisualOnImpact(.1f));
            audioSource.PlayOneShot(Grunt_03, 0.6f);

            int selection = Random.Range(0, 3);
            audioSource.PlayOneShot(Impacts[selection], .2f);
        }

        if (collision.relativeVelocity.magnitude > 20 && collision.relativeVelocity.magnitude < 30)
        {
            StartCoroutine(VisualOnImpact(.3f));
            audioSource.PlayOneShot(Grunt_04, 0.6f);

            int selection = Random.Range(0, 3);
            audioSource.PlayOneShot(Impacts[selection], .3f);
        }

        if (collision.relativeVelocity.magnitude > 30 && collision.relativeVelocity.magnitude < 40)
        {
            StartCoroutine(VisualOnImpact(.6f));
            audioSource.PlayOneShot(Grunt_05, 0.4f);

            int selection = Random.Range(0, 3);
            audioSource.PlayOneShot(Impacts[selection], .4f);
        }

        if (collision.relativeVelocity.magnitude > 40)
        {
            StartCoroutine(VisualOnImpact(1));
            audioSource.PlayOneShot(Grunt_06, 0.6f);

            int selection = Random.Range(0, 3);
            audioSource.PlayOneShot(Impacts[selection], .5f);
        }
    }

    // Sound on Impact End ----------------------------------------------------------------------



    // Sound Wind Start -------------------------------------------------------------------------


    public AudioSource Wind_Noise;

    public void FixedUpdate()
    {
        GameObject Ch11_nonPBR = GameObject.Find("Ch11_nonPBR");
        PlayerScript playerScript = Ch11_nonPBR.GetComponent<PlayerScript>();

        if (playerScript.CountDownFinished)
        {



            if (rb.velocity.magnitude <= 1)
            {
                Movement++;
            }
            else
            {
                Movement = 0;
            }

            if (Movement >= 120)
            {
                NoMovement = true;
            }

        }



        if (Time.timeScale < 1)
        {
            Wind_Noise.volume = 0.01f;
        }
        else
        {
            Wind_Noise.volume = rb.velocity.magnitude / 50 + 0.01f;
        }


        ChangeBloomAtRuntime(BloomValue);
        ChangeCAAtRuntime(CAValue);
        ChangeColorAtRuntime(ColorValue);
        ChangeTintAtRuntime(TintValue);


        if (playerScript.HighScoreCombined < 800)
        {
            ColorValue = 60 - (playerScript.HighScoreCombined * .1f);
            TintValue = 0 - (playerScript.HighScoreCombined * .03f);
        }

        if (playerScript.HighScoreCombined > 800 && playerScript.HighScoreCombined < 1600)
        {
            ColorValue = -20 + ((playerScript.HighScoreCombined - 800 )* .1f);
            TintValue = -24 + ((playerScript.HighScoreCombined - 800) * .01f);
        }
    }

    // Sound Wind End ----------------------------------------------------------------------


    void ChangeBloomAtRuntime(float val)
    {
        BloomModel.Settings bloomSettings = ppProfile.bloom.settings;
        bloomSettings.bloom.intensity = val;
        ppProfile.bloom.settings = bloomSettings;
    }

    void ChangeColorAtRuntime(float val)
    {
        ColorGradingModel.Settings colorGradingSettings = ppProfile.colorGrading.settings;
        colorGradingSettings.basic.temperature = val;
        ppProfile.colorGrading.settings = colorGradingSettings;
    }

    void ChangeTintAtRuntime(float val)
    {
        ColorGradingModel.Settings colorGradingSettings = ppProfile.colorGrading.settings;
        colorGradingSettings.basic.tint = val;
        ppProfile.colorGrading.settings = colorGradingSettings;
    }

    void ChangeCAAtRuntime(float val)
    {
        ChromaticAberrationModel.Settings chromaticAberrationSettings = ppProfile.chromaticAberration.settings;
        chromaticAberrationSettings.intensity = val;
        ppProfile.chromaticAberration.settings = chromaticAberrationSettings;
    }

    IEnumerator VisualOnImpact(float val)
    {
        CAValue = val * 2;
        //BloomValue = 0.1f;
        yield return new WaitForSeconds(0.01f);
        CAValue = val * 10;
        //BloomValue = 0.5f;
        yield return new WaitForSeconds(0.1f);
        CAValue = 1;
        //BloomValue = 0.06f;

    }
}






