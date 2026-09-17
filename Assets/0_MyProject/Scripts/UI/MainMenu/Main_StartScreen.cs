using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main_StartScreen : MonoBehaviour
{
    public Button PressToStartButton;
    public Button HighScoreButton;
    public Button ShopButton;
    public Button SettingsButton;
    public Button QuitButton;

    public Text Crystals;

    private MainCanvas MainCanvas;


    private void Awake()
    {
        MainCanvas = GetComponentInParent<MainCanvas>();
    }

    void Start()
    {
        PressToStartButton.onClick.AddListener(StartGame);
        ShopButton.onClick.AddListener(Shop);
        HighScoreButton.onClick.AddListener(High_Score);
        SettingsButton.onClick.AddListener(Settings);
        QuitButton.onClick.AddListener(QuitGame);
        Crystals.text = PlayerPrefs.GetInt("Crystals", 0).ToString();
    }

    
    void StartGame()
    {
        SceneManager.LoadScene("level");
        SceneManager.UnloadSceneAsync("Main_menu");
    }

    void Shop()
    {
        MainCanvas.StartScreen.SetActive(false);
        MainCanvas.ShopScreen.SetActive(true);
    }

    void Settings()
    {
     MainCanvas.Settings_screen.SetActive(true);
     MainCanvas.StartScreen.SetActive(false);  
    }
    private void High_Score()
    {
        MainCanvas.StartScreen.SetActive(false);
        MainCanvas.HighScoreScreen.SetActive(true);
    }
    private void QuitGame()
    {
        Application.Quit();
    }

}
