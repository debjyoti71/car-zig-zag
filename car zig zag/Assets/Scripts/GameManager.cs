using System.Collections;
using UnityEngine;
using TMPro;
using System.Security.Cryptography;
using UnityEngine.SceneManagement;  // <-- Add this line


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject PlatformSpwaner;

    public bool isgameStarted;

    [Header("GmaeOver")]
    public GameObject GameOverPanel;
    public GameObject NewHighScoreImage;
    public TMP_Text lastScoreText;

    public TMP_Text lastStarText;


    [Header("score")]
    public TMP_Text score_text;
    public TMP_Text best_text;
    public TMP_Text diamond_text;
    public TMP_Text star_text;

    int score = 0;
    int BestScore;
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
        // best score
        BestScore = PlayerPrefs.GetInt("BestScore", 0);
        best_text.text = BestScore.ToString("D5");

        // total diamond
        Total_diamond = PlayerPrefs.GetInt("Total_diamond", 0);
        diamond_text.text = Total_diamond.ToString("D5");

        // total star
        Total_star = PlayerPrefs.GetInt("Total_star", 0);
        star_text.text = Total_star.ToString("D5");

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
        GameOverPanel.SetActive(true);
        lastScoreText.text = score.ToString();
        lastStarText.text = Total_star.ToString();
        Count_Score = false;
        PlatformSpwaner.SetActive(false);
        if (score > BestScore)
        {
            NewHighScoreImage.SetActive(true);
            BestScore = score; // <-- update it
            PlayerPrefs.SetInt("BestScore", BestScore);
            best_text.text = BestScore.ToString("D5");
        }
    }

    IEnumerator UpdateScore()
    {
        while (Count_Score)
        {
            yield return new WaitForSeconds(1f);
            score++;
            if (score >= BestScore)
            {
                score_text.text = score.ToString();
                best_text.text = BestScore.ToString("D5");
            }
            else {
                score_text.text = score.ToString();
            }

        }
    }

    public void AddDiamond()
    {
        Total_diamond++;
        diamond_text.text = Total_diamond.ToString("D5");
        PlayerPrefs.SetInt("Total_diamond", Total_diamond);
    }

    public void AddStar()
    {
        Total_star++;
        score++;
        star_text.text = Total_star.ToString("D5");
        //PlayerPrefs.SetInt("Total_star", 0);
    }

    public void replay_game()
    {
        isgameStarted = false;
        SceneManager.LoadScene("Level");
        
    }


}
