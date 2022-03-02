using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    playManager playManager;

    public Transform player;
    Vector3 lastPos;


    Vector3 constDistVect;
    

    public Vector3 cameraToPlayerDist;


    private void Start()
    {
        playManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<playManager>();
        lastPos = player.transform.position;
        constDistVect = (player.position - transform.position).normalized * playManager.constCameraDist;
    }

    private void FixedUpdate()
    {





        cameraToPlayerDist = (player.position - constDistVect) - transform.position;

        transform.Translate(cameraToPlayerDist * Time.deltaTime * playManager.cameraSpeed, Space.World);

        lastPos = player.position;
    }
}
