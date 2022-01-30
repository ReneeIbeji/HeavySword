using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SwordSoundCaller : MonoBehaviour
{
    PlayerSoundManager playerS;

    void Start()
    {
        playerS = GameObject.FindGameObjectWithTag("Player_Model").GetComponent<PlayerSoundManager>();

    }

    // Update is called once per frame
    
    public void callSound(string name)
    {

        playerS.playRandomSound(name.Split(',')[0],Convert.ToInt32(name.Split(',')[1]));
    }
}

