using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    playManager playManager;

    public Transform player;
    Vector3 lastPos;

    
    public int currentNode;
    
    Vector3 directionOfMov;
    public GameObject[] nodes;

    Vector3 constDistVect;
    

    public Vector3 cameraToPlayerDist;

    Vector3 axisTarget;

    private void Start()
    {
        playManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<playManager>();
        lastPos = player.transform.position;
        directionOfMov = (nodes[currentNode + 1].transform.position - nodes[currentNode].transform.position).normalized;
        constDistVect = (player.position - transform.position).normalized * playManager.constCameraDist;
    }

    private void Update()
    {





        cameraToPlayerDist = (player.position - constDistVect) - transform.position;

        transform.Translate(cameraToPlayerDist * Time.deltaTime * playManager.cameraSpeed, Space.World);

        lastPos = player.position;
    }

    public void changeNode(int num)
    {
        if (num > currentNode)
        {
            transform.position = nodes[num].transform.position;
            directionOfMov = (nodes[currentNode + 1].transform.position - nodes[currentNode].transform.position).normalized;
           
        }
        else if(num < currentNode)
        {
            transform.position = nodes[num +1].transform.position;
            directionOfMov = (nodes[currentNode + 1].transform.position - nodes[currentNode].transform.position).normalized;
            
        }

        


        currentNode = num;
        
    }


    Vector3 timesVector3(Vector3 one, Vector3 two)
    {
        return new Vector3(one.x * two.x, one.y * two.y, one.z * two.z);
    }
}
