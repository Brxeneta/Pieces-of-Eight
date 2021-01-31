using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int numOfHearts;
    public int enemyHealth;
    public int enemyNumOfHearts;

    public Image[] hearts;
    public Image[] enemyHearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private void Update()
    {

        if (health > numOfHearts)
        {
            health = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {

            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            } else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i< numOfHearts)
            {
                hearts[i].enabled = true;
            } else
            {
                hearts[i].enabled = false;
            }
        }

        // ENEMY

        if (enemyHealth > enemyNumOfHearts)
        {
            enemyHealth = enemyNumOfHearts;
        }

        for (int i = 0; i < enemyHearts.Length; i++)
        {

            if (i < enemyHealth)
            {
                enemyHearts[i].sprite = fullHeart;
            }
            else
            {
                enemyHearts[i].sprite = emptyHeart;
            }

            if (i < enemyNumOfHearts)
            {
                enemyHearts[i].enabled = true;
            }
            else
            {
                enemyHearts[i].enabled = false;
            }
        }
    }
}
