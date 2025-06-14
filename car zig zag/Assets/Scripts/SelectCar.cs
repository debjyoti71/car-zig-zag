using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SelectCar : MonoBehaviour
{
    public static SelectCar Instance; 

    [SerializeField] Button PreviousButton;
    [SerializeField] Button NextButton;
    [SerializeField] Button UseButton;
    [SerializeField] GameObject BuyPanel;

    [Header("Buy Panel UI")]
    public TMP_Text havediamond_txt;
    public TMP_Text carvalue_txt;
    public TMP_Text needdiamond_txt;

    int currentCar;
    string ownCarIndex;

    int havediamond;
    int carvalue;
    int needdiamond;

    // Car prices (change manually)
    int[] predefinedCarPrices = { 0, 0, 40, 150, 180};

    Color redColor = new Color(1f, 0f, 0f, 1f);
    Color greenColor = new Color(0.5f, 1f, 0.4f, 1f);

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        currentCar = 0;
        changeCar(0);
    }

    void Start()
    {
        havediamond = PlayerPrefs.GetInt("Total_diamond", 0);

        // Unlock car 1 by default
        if (!PlayerPrefs.HasKey("carNo_1"))
            PlayerPrefs.SetInt("carNo_1", 1);
    }

    public void changeCar(int _change)
    {
        currentCar = Mathf.Clamp(currentCar + _change, 0, transform.childCount - 1);
        chooseCar(currentCar);

        ownCarIndex = "carNo_" + currentCar;
        bool isOwned = PlayerPrefs.GetInt(ownCarIndex, 0) == 1;

        TMP_Text buttonText = UseButton.GetComponentInChildren<TMP_Text>();
        buttonText.text = isOwned ? "Use" : "Buy";
        UseButton.GetComponent<RawImage>().color = isOwned ? greenColor : redColor;
    }

    void chooseCar(int _index)
    {
        PreviousButton.interactable = (_index > 0);
        NextButton.interactable = (_index < transform.childCount - 1);

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == _index);
        }
    }

    public void use_btn_click()
    {
        if (PlayerPrefs.GetInt(ownCarIndex, 0) == 1)
        {
            PlayerPrefs.SetInt("SelectedCar", currentCar);
            //continueAD.Instantiate._showAdButton.interactable = true;
            SceneManager.LoadScene("Level");
        }
        else
        {
            BuyPanel.SetActive(true);

            havediamond = PlayerPrefs.GetInt("Total_diamond", 0);
            carvalue = predefinedCarPrices[currentCar];

            havediamond_txt.text = "You have " + havediamond + " diamonds";
            carvalue_txt.text = carvalue.ToString();

            needdiamond = carvalue - havediamond;
            if (needdiamond <= 0)
            {
                needdiamond_txt.gameObject.SetActive(false);
            }
            else
            {
                needdiamond_txt.gameObject.SetActive(true);
                needdiamond_txt.text = needdiamond + " more diamonds needed";
            }

            PreviousButton.interactable = false;
            NextButton.interactable = false;
            UseButton.interactable = false;
        }
    }

    public void buy_car()
    {
        carvalue = predefinedCarPrices[currentCar];

        if (havediamond >= carvalue)
        {
            havediamond -= carvalue;
            PlayerPrefs.SetInt("Total_diamond", havediamond);
            PlayerPrefs.SetInt(ownCarIndex, 1);

            BuyPanel.SetActive(false);
            changeCar(0);

            PreviousButton.interactable = true;
            NextButton.interactable = true;
            UseButton.interactable = true;
        }
        else
        {
            havediamond_txt.text = "Not enough diamonds!";
        }
    }

    public void cancel_buy()
    {
        BuyPanel.SetActive(false);
        PreviousButton.interactable = true;
        NextButton.interactable = true;
        UseButton.interactable = true;
    }

    public void add_Diamond(int amount)
    {
        havediamond += amount;
        PlayerPrefs.SetInt("Total_diamond", havediamond);
        havediamond_txt.text = "You have " + havediamond + " diamonds";
    }
}
