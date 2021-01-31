using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Text totalCoins;

    [SerializeField]
    private Text totalScrolls;

    public int playerCoins;
    public int playerScrolls;

    private void OnDisable()
    {
        PlayerPrefs.SetInt("score", playerCoins);
        PlayerPrefs.SetInt("scrolls", playerScrolls);
    }

    private void OnEnable()
    {
        playerCoins = PlayerPrefs.GetInt("score");
        playerScrolls = PlayerPrefs.GetInt("scrolls");
        totalCoins.text = "" + playerCoins;
        totalScrolls.text = "" + playerScrolls;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Battle");
    }

    public void GoToShop()
    {
        SceneManager.LoadScene("Shop");
    }
        
    public void QuitGame()
    {
        Application.Quit();
    }
}
