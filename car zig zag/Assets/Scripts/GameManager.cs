using System.Collections;
using UnityEngine;
using TMPro;
using System.Security.Cryptography;  // <-- Add this line

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject PlatformSpwaner;

    public bool isgameStarted;

    [Header("score")]
    public TMP_Text score_text;
    public TMP_Text best_text;
    public TMP_Text diamond_text;
    public TMP_Text star_text;

    int score = 0;
    int best_score;
    int Total_diamond;
    int Total_star;
    bool Count_Score;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        //StartCoroutine(UpdateScore());

    }

    void Update()
    {
        if (!isgameStarted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameStart();
            }
        }
    }

    public void GameStart()
    {
        isgameStarted = true;
        Count_Score = true; // <-- This is the fix!
        StartCoroutine(UpdateScore()); // Optionally move this here instead of Start()
        PlatformSpwaner.SetActive(true);
    }


    public void GameOver()
    {
        Count_Score = false;
        PlatformSpwaner.SetActive(false);
    }

    IEnumerator UpdateScore()
    {
        while (Count_Score)
        {
            score++;
            score_text.text = score.ToString("D5");
            yield return new WaitForSeconds(1f);
        }
    }
}
