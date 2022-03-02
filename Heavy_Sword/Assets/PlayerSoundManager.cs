using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{

    AudioManager audioM;
    playerScript player;

    private void Start()
    {
        audioM = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        player = GetComponent<playerScript>();
    }
    public void playRandomSound(string sound,int range)
    {
        if(sound == "Drop")
        {
            if (player.SwordOnFloor)
            {
                audioM.Play(sound + "_" + Random.Range(0, range));
            }

        }
        else
        {
            audioM.Play(sound + "_" + Random.Range(0, range));
        }

        
    }


    public void playSound(string sound)
    {
        audioM.Play(sound);
    }

    public void stopSound(string sound)
    {
        audioM.Stop(sound);
    }
}
