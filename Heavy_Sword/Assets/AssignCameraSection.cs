using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignCameraSection : MonoBehaviour
{
    public int num;
    CameraMovement cameraScript;

    private void Start()
    {
        cameraScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>();
    }
    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            cameraScript.changeNode(num);
        }
    }
}
