using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject PlatformSpwaner;
    public bool isgameStarted = false;

    [Header("Game Over")]
    public GameObject GameOverPanel;
    public GameObject NewHighScoreImage;
    public TMP_Text lastScoreText;
    public TMP_Text lastStarText;

    [Header("Score")]
    public TMP_Text score_text;
    public TMP_Text best_text;
    public TMP_Text diamond_text;
    public TMP_Text star_text;

    int score = 0;
    int lastScore = 0;
    int BestScore;
    int Total_diamond;
    int Total_star;
    bool Count_Score;

    [Header("Player")]
    public GameObject[] player;
    Vector3 player_start_position = new Vector3(0, 2f, 0);
    int selected_car = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        selected_car = PlayerPrefs.GetInt("SelectedCar", 0);
        Instantiate(player[selected_car], player_start_position, Quaternion.identity);
    }

    void Start()
    {
        // Restore resume data if exists
        if (PlayerPrefs.GetInt("ContinueFlag", 0) == 1)
        {
            score = PlayerPrefs.GetInt("ResumeScore", 0);
            Total_star = PlayerPrefs.GetInt("ResumeStar", 0);
            PlayerPrefs.SetInt("ContinueFlag", 0); // Reset flag
        }

        // Best score
        BestScore = PlayerPrefs.GetInt("BestScore", 0);
        Total_diamond = PlayerPrefs.GetInt("Total_diamond", 0);
        best_text.text = BestScore.ToString("D5");

        // Score UI
        score_text.text = score.ToString();
        diamond_text.text = Total_diamond.ToString("D5");
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
        Count_Score = true;
        StartCoroutine(UpdateScore());
        PlatformSpwaner.SetActive(true);
    }

    public void GameOver()
    {
        GameOverPanel.SetActive(true);

        lastScoreText.text = score.ToString();
        lastStarText.text = Total_star.ToString();
        lastScore = score;
        Count_Score = false;
        PlatformSpwaner.SetActive(false);

        if (score > BestScore)
        {
            NewHighScoreImage.SetActive(true);
            BestScore = score;
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

            score_text.text = score.ToString();

            if (score >= BestScore)
            {
                best_text.text = BestScore.ToString("D5");
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
    }

    public void replay_game()
    {
        isgameStarted = false;
        SceneManager.LoadScene("Level");
    }

    public void main_menu()
    {
        isgameStarted = false;
        SceneManager.LoadScene("ChooseCar");
    }

    public void continue_game()
    {
        PlayerPrefs.SetInt("ResumeScore", lastScore);
        PlayerPrefs.SetInt("ResumeStar", Total_star);
        PlayerPrefs.SetInt("ContinueFlag", 1); // signal resume

        isgameStarted = false;
        SceneManager.LoadScene("Level");
    }
}
