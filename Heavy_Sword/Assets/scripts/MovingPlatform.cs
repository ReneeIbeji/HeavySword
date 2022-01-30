using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject startPoint,endPoint;
    public float speed,buffer;
    bool goingToEndPoint;
    public bool moving;
    playerScript playerscript;
    Collider colider;

    public bool activiateWhenToched;

    Vector3 movementVector;

    // Start is called before the first frame update
    void Start()
    {
        goingToEndPoint = true;
        transform.position = startPoint.transform.position;
        playerscript = GameObject.FindGameObjectWithTag("Player_Model").GetComponent<playerScript>();
        colider = GetComponent<Collider>();

        moving = !activiateWhenToched;
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        if (activiateWhenToched && !moving && playerscript.floorHooked == transform)
        {
            StartCoroutine(newStart());
        } 

        if (moving)
        {


            if (goingToEndPoint)
            {
                movementVector = (endPoint.transform.position - startPoint.transform.position).normalized;

                if (playerscript.floorHooked != transform && playerscript.SwordOnFloor)
                {
                    colider.enabled = false;
                }

                
                if ((!playerscript.swordSwinging && playerscript.floorHooked == transform) || (playerscript.ModelOnFloor && playerscript.playerOn == transform && playerscript.floorHooked == playerscript.playerOn))
                {


                    if (playerscript.swordSwinging)
                    {
                        Vector3 referencePos = playerscript.swordPivot.transform.position - transform.position;
                        transform.Translate(movementVector * speed * Time.deltaTime);

                        playerscript.swordPivot.transform.position = transform.position + referencePos;
                        
                    }
                    else
                    {
                        Vector3 referencePos = playerscript.swordEnd.transform.position - transform.position;
                        transform.Translate(movementVector * speed * Time.deltaTime);

                        playerscript.swordEnd.transform.position = transform.position + referencePos;
                    }
                }
                else
                {
                    transform.Translate(movementVector * speed * Time.deltaTime);
                }

                
                if (Vector3.Distance(transform.position, startPoint.transform.position) >= Vector3.Distance(startPoint.transform.position, endPoint.transform.position))
                {
                    goingToEndPoint = false;
                    colider.enabled = true;
                    StartCoroutine(bufferTime());
                }
            }
            else
            {
                movementVector = (startPoint.transform.position - endPoint.transform.position).normalized;
                if (playerscript.floorHooked != transform && playerscript.SwordOnFloor)
                {
                    colider.enabled = false;
                }


                if ((!playerscript.swordSwinging && playerscript.floorHooked == transform) || (playerscript.ModelOnFloor && playerscript.playerOn == transform && playerscript.floorHooked == playerscript.playerOn))
                {


                    if (playerscript.swordSwinging)
                    {
                        Vector3 referencePos = playerscript.swordPivot.transform.position - transform.position;
                        transform.Translate(movementVector * speed * Time.deltaTime);

                        playerscript.swordPivot.transform.position = transform.position + referencePos;

                    }
                    else
                    {
                        Vector3 referencePos = playerscript.swordEnd.transform.position - transform.position;
                        transform.Translate(movementVector * speed * Time.deltaTime);

                        playerscript.swordEnd.transform.position = transform.position + referencePos;
                    }
                }
                else
                {
                    transform.Translate(movementVector * speed * Time.deltaTime);
                }

                if (Vector3.Distance(transform.position, endPoint.transform.position) > Vector3.Distance(startPoint.transform.position, endPoint.transform.position))
                {
                    goingToEndPoint = true;
                    colider.enabled = true;
                    StartCoroutine(bufferTime());
                }
            }
        }
    }

    IEnumerator bufferTime()
    {
        moving = false;
        yield return new WaitForSeconds(buffer);
            if (playerscript.floorHooked != transform)
            {
                colider.enabled = false;
            }



            moving = true;


    }

    IEnumerator newStart()
    {
        yield return new WaitForSeconds(buffer);

        moving = true;
    }
}
