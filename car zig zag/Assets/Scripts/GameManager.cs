using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public GameObject PlatformSpwaner;

    public bool isgameStarted;
    public bool isgameOver;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
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
        PlatformSpwaner.SetActive(true);
       
    }

    public void GameOver()
    {
        PlatformSpwaner.SetActive(false);
    }
}
