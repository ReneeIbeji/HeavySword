using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    public List<Sound> allSound;


    private void Awake()
    {

        foreach (Sound s in sounds)
        {

            if (s.attachTo != "")
            {
                s.objectList = GameObject.FindGameObjectsWithTag(s.attachTo);
                Sound[] newS= new Sound[s.objectList.Length];

                for (int i = 0; i < s.objectList.Length; i++)
                {
                    newS[i] = new Sound();
                     
                    newS[i].source = gameObject.AddComponent<AudioSource>();

                    newS[i].name = s.objectList[i].transform.name + "_" + s.name;

                    newS[i].source.clip = s.clip;
                    newS[i].source.volume = s.volume;
                    newS[i].source.pitch = s.pitch;
                    newS[i].source.loop = s.loop;

                    allSound.Add(newS[i]);

                }
            }
            else
            {
                s.source = gameObject.AddComponent<AudioSource>();

                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;

                allSound.Add(s);
            }


        }
    }

    

    public void Play(string name)
    {
        try
        {
            Sound s = null;
            foreach(Sound i in allSound)
            {
                if(i.name == name)
                {
                    s = i;
                }
            }

            s.source.Play();
            Console.WriteLine(name);
        }
        catch
        {
            Debug.Log("sound:" + name + ", doesnt exist!");
        }

    }

    public void Stop(string name)
    {

        try
        {
            Sound s = null;
            foreach (Sound i in allSound)
            {
                if (i.name == name)
                {
                    s = i;
                }
            }

            s.source.Stop();
            Console.WriteLine(name);
        }
        catch
        {
            Debug.Log("sound:" + name + ", doesnt exist!");
        }
    }
}
