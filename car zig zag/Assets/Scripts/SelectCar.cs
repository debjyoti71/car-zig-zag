using UnityEngine;
using UnityEngine.UI; // Required for Button class

public class SelectCar : MonoBehaviour
{
    [SerializeField] Button PreviousButton;
    [SerializeField] Button NextButton;
    [SerializeField] Button UseButton;
    [SerializeField] GameObject BuyPanel;

    int currentCar;
    string ownCarIndex;
    Color redColor = new Color(1f, 0f, 0f, 1f); // RGBA for red color
    Color greenColor = new Color(0.5f, 1f, 0.4f, 1f); // RGBA for green color

    private void Awake()
    {
        changeCar(0);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeCar(int _change)
    {
       currentCar += _change;
        
       chooseCar(currentCar);
    }

    void chooseCar(int _index)
    {
        PreviousButton.interactable = (_index != 0);
        NextButton.interactable = (_index != transform.childCount - 1);
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == _index);
        }
    }
}
