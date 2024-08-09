using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance { get; private set; }

    public PopUp popUpHandler;

    PopUpWindow popUpWindow;

    LoadScene sceneLoader;

    public Slider loadSlider;

    [SerializeField] GameObject LoadingScreen;
    [SerializeField] GameObject MenuScreen;
    [SerializeField] GameObject LevelScreen;
    [SerializeField] StaminaSystem staminaCheck;

    public TextMeshProUGUI loadingText;
    public string[] texts = new string[5];

    private void Start()
    {
        sceneLoader = new LoadScene(loadSlider);
        sceneLoader.UIUpdate = OnLoadUpdate;
        if(Instance == null)
            Instance = this;
        if(!PlayerPrefs.HasKey("_LevelFlag"))
            PlayerPrefs.SetInt("_LevelFlag", 0);
    }

    void OnLoadUpdate()
    {
        int randomText = Random.Range(0,texts.Length);
        MenuScreen.SetActive(false);
        LevelScreen.SetActive(false);
        loadingText.text = texts[randomText];
        LoadingScreen.SetActive(true);

    }

    public void StartLoadScene(int sceneId)
    {
        if(staminaCheck == null)
        {
            StartCoroutine(sceneLoader.LoadSceneAsync(sceneId));
            return;
        }
        if (staminaCheck.getStamina() > 0)
            StartCoroutine(sceneLoader.LoadSceneAsync(sceneId));
    }

    public void Level1()
    {
        StartLoadScene(2);
    }

    public void Level2()
    {
        StartLoadScene(3);
    }

    public void Level3()
    {
        StartLoadScene(4);
    }

    public void WinGame()
    {
        GameManager.instance.SaveGame();
        StartLoadScene(5);
    }

    public void EndGame()
    {
        GameManager.instance.SaveGame();
        StartLoadScene(6);
    }

    public void ConfirmMenu()
    {
        popUpWindow = popUpHandler.ShowPopUp();
        popUpWindow.OnMethodCall += Menu;
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        GameManager.instance.clearStats();
        StartLoadScene(1);
        if(popUpWindow != null)
            popUpWindow.OnMethodCall -= Menu;
    }
    public void SplashToMenu()
    {
        SceneManager.LoadScene(1);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}