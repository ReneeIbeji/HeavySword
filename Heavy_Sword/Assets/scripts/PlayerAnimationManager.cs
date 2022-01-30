using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    playerScript playerscript;
    Animator swordAnim;

    public Transform[] hairPieces;
    public float hairSpeed;

    Vector3 lastPos;

    Vector3 dist;

    // Start is called before the first frame update
    void Start()
    {
        playerscript = GetComponent<playerScript>();
        swordAnim = GameObject.FindGameObjectWithTag("Sword").GetComponent<Animator>();
        lastPos = transform.position;

        for (int i = 0; i < hairPieces.Length; i++)
        {
            if (i > 0) { hairPieces[i].position = hairPieces[i - 1].position + transform.TransformDirection(new Vector3((0.4f), 0, 0)); }
            else { hairPieces[i].position = transform.position + transform.TransformDirection(new Vector3((0.5f), 0.4f, 0)); }

            hairPieces[i].rotation = transform.rotation;

        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < hairPieces.Length; i++)
        {
            if(i > 0) { dist = hairPieces[i - 1].position + transform.TransformDirection(new Vector3((0.4f), 0, 0)) - hairPieces[i].position; }
            else { dist = transform.position + transform.TransformDirection(new Vector3((0.5f), 0.4f, 0)) - hairPieces[0].position; }

            hairPieces[i].Translate(dist * Time.deltaTime * hairSpeed, Space.World);
            hairPieces[i].rotation = transform.rotation;

        }


        swordAnim.SetBool("Swinging", playerscript.swordSwinging);

        lastPos = transform.position;
    }
}
