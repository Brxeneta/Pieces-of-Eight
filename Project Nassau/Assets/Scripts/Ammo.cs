using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ammo : MonoBehaviour
{
    public int ammo;
    public int numOfAmmo;
    public int enemyAmmo;
    public int enemyNumOfAmmo;

    public Image[] ammunition;
    public Image[] enemyAmmunition;
    public Sprite fullAmmo;
    public Sprite emptyAmmo;

    private void Update()
    {

        if (ammo > numOfAmmo)
        {
            ammo = numOfAmmo;
        }

        for (int i = 0; i < ammunition.Length; i++)
        {

            if (i < ammo)
            {
                ammunition[i].sprite = fullAmmo;
            }
            else
            {
                ammunition[i].sprite = emptyAmmo;
            }

            if (i < numOfAmmo)
            {
                ammunition[i].enabled = true;
            }
            else
            {
                ammunition[i].enabled = false;
            }
        }

        // ENEMY

        if (enemyAmmo > enemyNumOfAmmo)
        {
            enemyAmmo = enemyNumOfAmmo;
        }

        for (int i = 0; i < enemyAmmunition.Length; i++)
        {

            if (i < enemyAmmo)
            {
                enemyAmmunition[i].sprite = fullAmmo;
            }
            else
            {
                enemyAmmunition[i].sprite = emptyAmmo;
            }

            if (i < enemyNumOfAmmo)
            {
                enemyAmmunition[i].enabled = true;
            }
            else
            {
                enemyAmmunition[i].enabled = false;
            }
        }
    }
}
