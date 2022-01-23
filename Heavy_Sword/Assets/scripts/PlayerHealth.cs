using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxPlayerHealth;
    public int currentHealth;
    private void Start()
    {
        currentHealth = maxPlayerHealth;
    }
    public void changeHealth(int healthChange)
    {
        
        currentHealth += healthChange;

        if(currentHealth == 0) { dead(); }
    }

    public void dead()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
