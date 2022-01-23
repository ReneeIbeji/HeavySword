using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    public GameObject swordPivot;
    public GameObject swordEnd;
    public GameObject sword;

    public bool swordSwinging = false;
    public bool swingingRight = false;

    playManager playManager;
    PlayerHealth playerHealth;

    public float targetAngle;

    clickManager clickManager;

    LayerMask Ground;

    Collider playerCollider, swordEndCollider,swordPivotCollider;

    [HideInInspector]

    public bool ModelOnFloor, SwordOnFloor;

    Transform floorHooked;

    RaycastHit hit;
    Vector3 lastPoint;

    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerCollider = GetComponent<Collider>();
        playManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<playManager>();
        swordEndCollider = GameObject.FindGameObjectWithTag("Sword_End").GetComponent<Collider>();
        swordPivotCollider = GameObject.FindGameObjectWithTag("Sword_Pivot").GetComponent<Collider>();

        Physics.Raycast(swordEndCollider.bounds.center, Vector3.down, out hit, (0.2f + swordEndCollider.bounds.size.y / 2), Ground);
        floorHooked = hit.transform;
        
        lastPoint = floorHooked.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Ground = LayerMask.GetMask("Ground");
        

        StartCoroutine(checkcollision());

        if (Input.GetMouseButton(0))
        {
            swingingRight = true;
            swordPivot.transform.parent = GameObject.FindGameObjectWithTag("Player").transform;

            swordEndCollider.isTrigger = true;
            GameObject.FindGameObjectWithTag("Sword_End").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            swordPivotCollider.isTrigger = false;
            GameObject.FindGameObjectWithTag("Sword_Pivot").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            GameObject.FindGameObjectWithTag("Sword_Pivot").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;


            swordEnd.transform.parent = swordPivot.transform;
            sword.transform.parent = swordPivot.transform;
            swordPivot.transform.Rotate(new Vector3(0, playManager.roationSpeed * Time.deltaTime, 0));
            swordSwinging = true;
        } else if (Input.GetMouseButton(1))
        {

            swingingRight = false;
            swordPivot.transform.parent = GameObject.FindGameObjectWithTag("Player").transform;

            swordEndCollider.isTrigger = true;
            GameObject.FindGameObjectWithTag("Sword_End").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            swordPivotCollider.isTrigger = false;
            GameObject.FindGameObjectWithTag("Sword_Pivot").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            GameObject.FindGameObjectWithTag("Sword_Pivot").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;


            swordEnd.transform.parent = swordPivot.transform;
            sword.transform.parent = swordPivot.transform;
            swordPivot.transform.Rotate(new Vector3(0, -playManager.roationSpeed * Time.deltaTime, 0));
            swordSwinging = true;
        }
        else
        {

            swordSwinging = false;

            targetAngle= 0;
            bool moving = false;
            Vector3 movement = new Vector3();

            moving = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;

            swordPivotCollider.isTrigger = true;
            GameObject.FindGameObjectWithTag("Sword_Pivot").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            swordEndCollider.isTrigger = false;
            GameObject.FindGameObjectWithTag("Sword_End").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            GameObject.FindGameObjectWithTag("Sword_End").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

            swordEnd.transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
            swordPivot.transform.parent = swordEnd.transform;
            sword.transform.parent = swordEnd.transform;

            if (moving)
            {
                

                Vector3 finalPoint = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

                Vector3 _direction = (swordEnd.transform.position + finalPoint - swordEnd.transform.position).normalized;

                Quaternion _lookRotation = Quaternion.LookRotation(_direction);

                targetAngle = _lookRotation.eulerAngles.y - 90;

                if(targetAngle < 0) { targetAngle += 360; }
                else if(targetAngle >= 360) { targetAngle -= 360; }

                if (swordEnd.transform.eulerAngles.y > targetAngle - 2 && swordEnd.transform.eulerAngles.y < targetAngle + 2 || swordEnd.transform.eulerAngles.y == targetAngle)
                {
                    movement.x = playManager.movementSpeed;
                }
                else
                {
                    float turnLeftDist,turnRightDist,currentAngle;


                    currentAngle = swordEnd.transform.eulerAngles.y;
                    

                    if (currentAngle == 0) {currentAngle += 360; }
                    else if (currentAngle >= 360) { currentAngle -= 360; }


                    if (targetAngle < currentAngle)
                    {
                        turnLeftDist = currentAngle - targetAngle;
                    }
                    else
                    {
                        turnLeftDist = currentAngle + (360 - targetAngle);
                    }

                    if( targetAngle > currentAngle)
                    {
                        turnRightDist = targetAngle - currentAngle;
                    }
                    else
                    {
                        turnRightDist = (360-currentAngle) + targetAngle;
                    }


                    if(turnRightDist > turnLeftDist)
                    {
                        swordEnd.transform.Rotate(0, -playManager.moveAroundSpeed * Time.deltaTime, 0);
                    }
                    else
                    {
                        swordEnd.transform.Rotate(0, playManager.moveAroundSpeed * Time.deltaTime, 0);
                    }

                    

                }
            }


            swordEnd.transform.Translate(movement * Time.deltaTime,Space.Self);
           
        }
        transform.position = swordPivot.transform.position + swordPivot.transform.TransformDirection( new Vector3(0.75f,0,0));
        transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, swordPivot.transform.rotation.eulerAngles.y,transform.rotation.eulerAngles.z);

    }

    IEnumerator checkcollision()
    {
        yield return new WaitForEndOfFrame();
        ModelOnFloor = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), (0.2f + playerCollider.bounds.size.y / 2), Ground);
        SwordOnFloor = Physics.Raycast(swordEndCollider.bounds.center, transform.TransformDirection(Vector3.down), (0.2f + swordEndCollider.bounds.size.y / 2), Ground);

        if (!swordSwinging)
        {

            if (Physics.Raycast(swordEndCollider.bounds.center, Vector3.down, out hit, (0.2f + swordEndCollider.bounds.size.y / 2), Ground) && hit.transform != floorHooked)
            {
                floorHooked = hit.transform;
                Debug.Log("found floor");


            }
            else if (floorHooked != null && floorHooked.transform.position != lastPoint)
            {
                swordEnd.transform.Translate(floorHooked.transform.position - lastPoint, Space.World);


            }

        }
        if(floorHooked != null)
        {
            lastPoint = floorHooked.transform.position;
        }

        if (!SwordOnFloor && !ModelOnFloor)
        {
            GameObject.FindGameObjectWithTag("Sword_End").GetComponent<Rigidbody>().useGravity = true;
            GameObject.FindGameObjectWithTag("Sword_Pivot").GetComponent<Rigidbody>().useGravity = true;
        }
        else
        {
            GameObject.FindGameObjectWithTag("Sword_End").GetComponent<Rigidbody>().useGravity = false;
            GameObject.FindGameObjectWithTag("Sword_Pivot").GetComponent<Rigidbody>().useGravity = false;
        }
    }

     void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "DeathZone")
        {
            playerHealth.dead();
        }
    }






}
