using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNewScene : MonoBehaviour
{

    [SerializeField] float timeToWait = 2.0f; // Time to wait before loading the new scene
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("NewScene", timeToWait); // Call LoadNewScene after 2 seconds
    }

    void NewScene()
    {
        SceneManager.LoadScene("ChooseCar"); // Load the MainMenu scene   
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
