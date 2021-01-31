using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameChoices
{
    NONE,
    BRACE,
    RELOAD,
    FIRE
}

public class GameplayController : MonoBehaviour
{
    [SerializeField]
    private Sprite brace_Sprite, reload_Sprite, fire_Sprite;

    [SerializeField]
    private Image playerChoice_Img, opponentChoice_Img;

    [Header("Text")]
    [SerializeField]
    private Text infoText;

    [SerializeField]
    private Text resultText;

    [SerializeField]
    private Text coinsEarnedText;

    [SerializeField]
    private Text bonusText;

    [SerializeField]
    private Text totalCoinsText;

    [SerializeField]
    private Text scrollsEarnedText;

    [SerializeField]
    private Text totalScrollsText;

    private GameChoices player_Choice = GameChoices.NONE, opponent_Choice = GameChoices.NONE;

    private AnimationController animationController;

    private Health hp;
    private Ammo ammo;

    [Header("Game Objects")]
    public GameObject fireButton;
    public GameObject playerShip;
    public GameObject enemyShip;

    [Header("Audio")]
    public AudioSource fireHit;
    public AudioSource fireMiss;
    public float volume = 0.5f;

    [Header("Inventory")]
    public int playerCoins;
    public int playerScrolls;
    public float scrollDropChance;

    private void OnDisable()
    {
        PlayerPrefs.SetInt("score", playerCoins);
        PlayerPrefs.SetInt("scrolls", playerScrolls);
    }

    private void OnEnable()
    {
        playerCoins = PlayerPrefs.GetInt("score");
        playerScrolls = PlayerPrefs.GetInt("scrolls");
    }

    private void Awake()
    {
        animationController = GetComponent<AnimationController>();

        hp = GetComponent<Health>();
        ammo = GetComponent<Ammo>();
    }

    public void SetChoices(GameChoices gameChoices)
    {
        SetOpponentChoice();

        switch (gameChoices)
        {
            case GameChoices.BRACE:
                playerChoice_Img.sprite = brace_Sprite;
                player_Choice = GameChoices.BRACE;
                break;
            case GameChoices.RELOAD:
                playerChoice_Img.sprite = reload_Sprite;
                player_Choice = GameChoices.RELOAD;
                ammo.ammo++;
                break;
            case GameChoices.FIRE:
                playerChoice_Img.sprite = fire_Sprite;
                ammo.ammo--;
                player_Choice = GameChoices.FIRE;
                break;
        }
        DetermineOutcome();
    }

    private void Update()
    {
        if (ammo.ammo == 0) {
            fireButton.SetActive(false);
        } 
        else
        {
            fireButton.SetActive(true);
        }

        if (playerScrolls > 8)
        {
            playerScrolls = 8;
        }
    }

    void PlayerWins()
    {
        hp.enemyHealth--;
    }

    void EnemyWins()
    {
        hp.health--;
    }

    void SetOpponentChoice()
    {
        int rnd = Random.Range(0, 3);
        int rnd2 = Random.Range(0, 2);
        float result = Random.value;
        print("AI Random Choice: " + result);

        if (ammo.ammo == 0 && ammo.enemyAmmo == 0) //if both are empty, just reload
        {
            opponent_Choice = GameChoices.RELOAD;
            ammo.enemyAmmo++;
            opponentChoice_Img.sprite = reload_Sprite;
        }
        else if (ammo.ammo >= 1 && ammo.enemyAmmo == 0) //if player has ammo and enemy does not, brace or reload
        {
            switch (rnd2)
            {
                case 0:
                    opponent_Choice = GameChoices.BRACE;
                    opponentChoice_Img.sprite = brace_Sprite;
                    break;
                case 1:
                    opponent_Choice = GameChoices.RELOAD;
                    ammo.enemyAmmo++;
                    opponentChoice_Img.sprite = reload_Sprite;
                    break;
            }
        }
        else if (ammo.ammo == 0 && ammo.enemyAmmo >= 1) //if enemy has ammo and player does not, fire or reload
        {
            if (result > 0.25)
            {
                opponent_Choice = GameChoices.FIRE;
                ammo.enemyAmmo--;
                animationController.enemyShipFireAnimation();
                opponentChoice_Img.sprite = fire_Sprite;
                StartCoroutine(RestartAnimation());
            }
            else if (result > 0.75)
            {
                opponent_Choice = GameChoices.RELOAD;
                ammo.enemyAmmo++;
                opponentChoice_Img.sprite = reload_Sprite;
            }
        }
        else if (ammo.enemyAmmo == ammo.enemyNumOfAmmo) //if enemy ammo is maxed, fire or brace
        {
            if (result > 0.2)
            {
                opponent_Choice = GameChoices.FIRE;
                ammo.enemyAmmo--;
                animationController.enemyShipFireAnimation();
                opponentChoice_Img.sprite = fire_Sprite;
                StartCoroutine(RestartAnimation());
            }
            else if (result > 0.8)
            {
                opponent_Choice = GameChoices.BRACE;
                opponentChoice_Img.sprite = brace_Sprite;
            }
        }
        else if (ammo.ammo >= 1 && ammo.enemyAmmo >= 1) //if both have ammo, brace, reload, or fire
        {
            switch (rnd)
            {
                case 0:
                    opponent_Choice = GameChoices.BRACE;
                    opponentChoice_Img.sprite = brace_Sprite;
                    break;
                case 1:
                    opponent_Choice = GameChoices.RELOAD;
                    ammo.enemyAmmo++;
                    opponentChoice_Img.sprite = reload_Sprite;
                    break;
                case 2:
                    opponent_Choice = GameChoices.FIRE;
                    ammo.enemyAmmo--;
                    animationController.enemyShipFireAnimation();
                    opponentChoice_Img.sprite = fire_Sprite;
                    StartCoroutine(RestartAnimation());
                    break;
            }
        }
    }

    void DetermineOutcome()
    {
        if (player_Choice == GameChoices.RELOAD && opponent_Choice == GameChoices.RELOAD) //if both reload, nothing happens
        {
            infoText.text = "Nothing Happens...";
            StartCoroutine(DisplayWinnerAndRestart());
        }

        if (player_Choice == GameChoices.BRACE && opponent_Choice == GameChoices.BRACE) //if both brace, nothing happens
        {
            infoText.text = "Nothing Happens...";
            StartCoroutine(DisplayWinnerAndRestart());
        }

        if (player_Choice == GameChoices.RELOAD && opponent_Choice == GameChoices.BRACE) //if player reloads but enemy braces, nothing happens
        {
            infoText.text = "Nothing Happens...";
            StartCoroutine(DisplayWinnerAndRestart());
        }

        if (player_Choice == GameChoices.BRACE && opponent_Choice == GameChoices.RELOAD) //if player braces but enemy reloads, nothing happens
        {
            infoText.text = "Nothing Happens...";
            StartCoroutine(DisplayWinnerAndRestart());
        }

        if (player_Choice == GameChoices.FIRE && opponent_Choice == GameChoices.FIRE) //if both fire, both lose health
        {
            infoText.text = "You & The Enemy Take Damage!";
            StartCoroutine(DisplayWinnerAndRestart());
            PlayerWins();
            EnemyWins();
            fireHit.Play();
        }

        if (player_Choice == GameChoices.FIRE && opponent_Choice == GameChoices.RELOAD) //if player fires but enemy reloads, enemy takes damage
        {
            infoText.text = "The Enemy Takes Damage!";
            StartCoroutine(DisplayWinnerAndRestart());
            PlayerWins();
            fireHit.Play();
        }

        if (player_Choice == GameChoices.RELOAD && opponent_Choice == GameChoices.FIRE) //if player reloads but enemy fires, player takes damage
        {
            infoText.text = "You Take Damage!";
            StartCoroutine(DisplayWinnerAndRestart());
            EnemyWins();
            fireHit.Play();
        }

        if (player_Choice == GameChoices.BRACE && opponent_Choice == GameChoices.FIRE) //if player braces but enemy fires, player takes no damage and enemy loses 1 ammo
        {
            infoText.text = "You Brace!";
            StartCoroutine(DisplayWinnerAndRestart());
            fireMiss.Play();
        }

        if (player_Choice == GameChoices.FIRE && opponent_Choice == GameChoices.BRACE) //if enemy braces but player fires, enemy takes no damage and player loses 1 ammo
        {
            infoText.text = "The Enemy Braces!";
            StartCoroutine(DisplayWinnerAndRestart());
            fireMiss.Play();
        }

        if (hp.enemyHealth == 0)
        {
            float scrollDrop = Random.value;
            if (scrollDrop > scrollDropChance)
            {
                playerScrolls++;
            }
            int coinsEarned = 50 + hp.health * 10;
            animationController.battleReviewAnimation();
            playerCoins += coinsEarned;
            resultText.text = "You Won!";
            coinsEarnedText.text = "Coins Earned: 50";
            bonusText.text = "Remaining Hearts: " + hp.health + " * 10";
            totalCoinsText.text = "Total Coins: " + playerCoins;
            scrollsEarnedText.text = "Scrolls Earned: 1";
            totalScrollsText.text = "Total Scrolls: " + playerScrolls;
        }

        if (hp.health == 0)
        {
            animationController.battleReviewAnimation();
            playerCoins += 10;
            resultText.text = "You Lost!";
            coinsEarnedText.text = "Coins Earned: 10";
            bonusText.text = "Remaining Hearts: 0";
            totalCoinsText.text = "Total Coins: " + playerCoins;
            scrollsEarnedText.text = "Scrolls Earned: 0";
            totalScrollsText.text = "Total Scrolls: " + playerScrolls;
        }
    }

    IEnumerator DisplayWinnerAndRestart()
    {
        yield return new WaitForSeconds(1f);
        infoText.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);
        infoText.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);
        animationController.ResetAnimations();
    }

    IEnumerator RestartAnimation()
    {
        yield return new WaitForSeconds(1f);
        animationController.ResetShipAnimations();
    }
}
