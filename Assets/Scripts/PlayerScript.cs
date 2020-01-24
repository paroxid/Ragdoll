using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Phidget22;
using Phidget22.Events;
using System;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody PlayerRigid;
    public float speed = 20;
    public float resistance = 10;

    private float HorizontalMove;
    private float VerticalMove;

    public float HighScoreMeter;
    public float HighScoreCoin;
    public float HighScoreCombined;

    public Text CountDownText;
    public Text HighScoreText;
    public Text GameOverText;
    public Text GameOverHighScoreText;
    public Text TryAgainText;

    public bool ControlOn;
    public bool CountDownFinished;
    public bool GameOver;
    public bool PersonOnBoard;
    private float StandingLimit;

    public float CombinedWeight;

    private bool ThisBoolIsUseless;




    // Start Countdown Start ----------------------------------------------------------------------

    private void Awake()
    {
        HighScoreText.text = "";
        ControlOn = false;
        Physics.gravity = new Vector3(0, 0, 0);
        CountDownFinished = false;
        GameOver = false;
        StandingLimit = 0.5f;
        CountDownText.text = "step on the board to start";
        HighScoreCoin = 0;

        ThisBoolIsUseless = false;
    }


    private void Update()
    {

        GameObject Ch11_nonPBR = GameObject.Find("Ch11_nonPBR");
        PhidgetManager phidgetManager = Ch11_nonPBR.GetComponent<PhidgetManager>();

        CombinedWeight = (Mathf.Abs(phidgetManager.horizontalWeight) + Mathf.Abs(phidgetManager.verticalWeight)) / 2;


        if (Mathf.Abs(phidgetManager.horizontalWeight) > StandingLimit && Mathf.Abs(phidgetManager.verticalWeight) > StandingLimit)
        {
            PersonOnBoard = true;
        }


        if (PersonOnBoard && !CountDownFinished)
        {
            StartCoroutine(Countdown());

        }

        // Start Countdown End ----------------------------------------------------------------------


        // End of Game Screen Start ----------------------------------------------------------------------

        GameObject hips = GameObject.Find("mixamorig:Hips");
        Segment_Instancing segment_Instancing = hips.GetComponent<Segment_Instancing>();


        if (segment_Instancing.NoMovement)
        {
            GameOver = true;
            ControlOn = false;


            HighScoreText.text = "";
            GameOverText.text = "you tried.";
            GameOverHighScoreText.text = "your highscore was " + HighScoreCombined.ToString();
            TryAgainText.text = "to try again please step off the board";
        }


        if (CombinedWeight < 0.5f && segment_Instancing.NoMovement)
        {
            Debug.Log("RELOAD LEVEL");
            ThisBoolIsUseless = true;
        }

        if (ThisBoolIsUseless)
        {
            StartCoroutine(LoadGame());
            //LoadGame();
            //THIS SHOULDNT FIX IT
        }

        // End of Game Screen End ----------------------------------------------------------------------

    }

    IEnumerator Countdown()
    {
        CountDownText.text = "3";
        yield return new WaitForSeconds(1);

        CountDownText.text = "2";
        yield return new WaitForSeconds(1);

        CountDownText.text = "1";
        yield return new WaitForSeconds(1);

        CountDownText.text = "Go";
        CountDownFinished = true;
        Physics.gravity = new Vector3(0, -9.81f, 0);
        ControlOn = true;
        yield return new WaitForSeconds(2);

        CountDownText.text = "";
        HighScoreText.text = HighScoreCombined.ToString();

    }


    IEnumerator LoadGame()
    {
        CountDownText.text = "loading ...";


        yield return new WaitForSeconds(3);

        SceneManager.LoadScene("Demo01");

        CountDownText.text = "";
    }

    public void Coin()
    {
        HighScoreCoin = HighScoreCoin + 10;
    }





    private void FixedUpdate()
    {
        // Balanceboard Controls

        if (ControlOn)
        {
            GameObject Ch11_nonPBR = GameObject.Find("Ch11_nonPBR");
            PhidgetManager phidgetManager = Ch11_nonPBR.GetComponent<PhidgetManager>();


            HorizontalMove = phidgetManager.horizontalWeight / resistance;
            VerticalMove = phidgetManager.verticalWeight / resistance;
        }




        //Pfeiltasten Controls
        //float HorizontalMove = Input.GetAxis("Horizontal");
        // float VerticalMove = Input.GetAxis("Vertical");


        // Richtung für zusätzliche Kraft
        Vector3 direction = new Vector3(HorizontalMove, 0, VerticalMove);

        //Force
        PlayerRigid.AddForce(direction * speed);


        //Boost on Button B
        if (Input.GetKeyDown(KeyCode.B))
            transform.position = transform.position + new Vector3(0, 5, 0);


        // Highscore berechnen & anzeigen
        if (CountDownFinished)
        {
            foreach (Transform item in transform)
            {
                if (item.name == "mixamorig:Hips")
                {
                    HighScoreMeter = Mathf.RoundToInt(item.position.z / 3);
                }
            }
            HighScoreText.text = HighScoreCombined.ToString();
        }
        else
        {
            HighScoreText.text = "";
        }
        HighScoreCombined = HighScoreMeter + HighScoreCoin;
    }
}


