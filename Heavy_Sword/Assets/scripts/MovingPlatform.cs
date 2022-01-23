using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject startPoint,endPoint;
    public float speed,buffer;
    bool goingToEndPoint;
    public bool moving;
    

    Vector2 movementVector;

    // Start is called before the first frame update
    void Start()
    {
        goingToEndPoint = true;
        transform.position = startPoint.transform.position;
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        if (moving)
        {
            if (goingToEndPoint)
            {
                movementVector = (endPoint.transform.position - startPoint.transform.position).normalized;

                transform.Translate(movementVector * speed * Time.deltaTime);

                if(Vector3.Distance(transform.position, startPoint.transform.position) >= Vector3.Distance(startPoint.transform.position, endPoint.transform.position))
                {
                    goingToEndPoint = false;
                    StartCoroutine(bufferTime());
                }
            }
            else
            {
                movementVector = (startPoint.transform.position - endPoint.transform.position).normalized;

                transform.Translate(movementVector * speed * Time.deltaTime);

                if (Vector3.Distance(transform.position, endPoint.transform.position) > Vector3.Distance(startPoint.transform.position, endPoint.transform.position))
                {
                    goingToEndPoint = true;
                    StartCoroutine(bufferTime());
                }
            }
        }
    }

    IEnumerator bufferTime()
    {
        moving = false;
        yield return new WaitForSeconds(buffer);
        moving = true;

             
    }
}
