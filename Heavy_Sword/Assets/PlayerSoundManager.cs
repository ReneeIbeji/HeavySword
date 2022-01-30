using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{

    AudioManager audioM;
    playerScript player;

    private void Start()
    {
        audioM = audioM = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
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
}
