using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAI : MonoBehaviour
{
    State currentState;
    public GameObject[] platforms;
    void Start()
    {
        currentState = new Prestart(platforms);
    }

    // Update is called once per frame
    void Update()
    {
        currentState = currentState.process();
    }
}
