using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleOverview : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadScene("Battle");
        print("rfgarewg");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Lobby");
        print("rfgarewg");
    }
}