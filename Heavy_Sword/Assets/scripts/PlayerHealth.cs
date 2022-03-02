using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxPlayerHealth;
    public int currentHealth;
    AudioManager Audio;
    
    private void Start()
    {
        currentHealth = maxPlayerHealth;

        Audio = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>(); 

    }
    public void changeHealth(int healthChange, string source)
    {
        
        if(source == "Enemy")
        {
            playRandomSound("Hit", 3);
        }
        currentHealth += healthChange;

        if(currentHealth == 0) { dead(); }
    }

    public void dead()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void playRandomSound(string sound, int range)
    {

            Audio.Play(sound + "_" + Random.Range(0, range));

    }

}
