using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;


    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void changeHealth(int change)
    {
        currentHealth += change;
        if(currentHealth <= 0)
        {
            die();
        }

    }

    public void die()
    {
        Destroy(gameObject);
    }


}
