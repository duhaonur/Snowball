using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreMultiplier : MonoBehaviour
{
    private int scoreMultiplierHolder = 1;
    private int[] lastScoreMultiplier;

    private float score;

    private bool outIf = false;

    public bool endgame = false;

    public GameObject GO_5x;
    public GameObject GO_10x;
    public GameObject GO_20x;
    public GameObject GO_50x;
    public GameObject GO_100x;

    public GameObject[] aagText; // Awesome, Amazing And Good Text

    public Text scoreMultiplierText;
    public Text scoreText;

    public EndGameScript endGameScript;
    private void Start()
    {
        lastScoreMultiplier = new int[1];
        scoreMultiplierText.text = scoreMultiplierHolder + "x";
    }

    private void Update()
    {
        if (endgame)
        {
            if(transform.localScale.x <= 0.5f && !outIf)
            {
                StartCoroutine(endGameScript.SlowDownSnowBall());
                outIf = true;
                Score(lastScoreMultiplier[0]);
            }
            return;
        }

        score += Time.deltaTime * scoreMultiplierHolder * transform.localScale.x;
        scoreText.text = ((int)score).ToString();
    }

    public void SetTheAagText()
    {
        if(score >= 1 && score <= 20000)
        {
            aagText[0].SetActive(true);

        }
        else if(score >= 20000 && score <= 50000)
        {
            aagText[1].SetActive(true);
        }
        else if(score >= 50000)
        {
            aagText[2].SetActive(true);
        }
    }

    public void Score(int endScoreMultiplier)
    {
        score *= endScoreMultiplier;
        scoreText.text = ((int)score).ToString();
        SetTheAagText();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "LittleSnowBalls")
        {
            scoreMultiplierHolder += (int)other.gameObject.transform.localScale.x;
            scoreMultiplierText.text = scoreMultiplierHolder + "x";
        }
        if (other.tag == "5X")
        {
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            lastScoreMultiplier[0] = 5;
        }
        else if (other.tag == "10X")
        {
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            lastScoreMultiplier[0] = 10;
        }
        else if (other.tag == "20X")
        {
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            lastScoreMultiplier[0] = 20;
        }
        else if (other.tag == "50X")
        {
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            lastScoreMultiplier[0] = 50;
        }
        else if (other.tag == "100X")
        {
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            lastScoreMultiplier[0] = 100;
            endgame = true;
        }
    }
}
