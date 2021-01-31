using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopController : MonoBehaviour
{
    private ShopAnimationController animationController;

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
    private void Awake()
    {
        animationController = GetComponent<ShopAnimationController>();
    }

    public void showScrollsPanel()
    {
        if (playerScrolls < 8)
        {
            animationController.showInsufficentScrolls();
        }
        else if (playerScrolls == 8)
        {
            animationController.showEndGameMessage();
        }

    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Lobby");
    }
}
